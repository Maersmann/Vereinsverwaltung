using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.SchluesselRepos;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Logic.Core.Interfaces;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore
{
    public class SchluesselAPI : IAPI<Schluessel>
    {
        private readonly SchluesselRepository repo;
        public SchluesselAPI()
        {
            repo = new SchluesselRepository();
        }

        public void Aktualisieren(Schluessel entity)
        {
            repo.Speichern(entity);
        }

        public void Entfernen(int id)
        {
            if (new SchluesselzuteilungAPI().EintraegeFuerSchluesselvorhanden(id))
                throw new SchluesselIstZugeteiltException();

            repo.Entfernen(id);
        }

        public Schluessel Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public ObservableCollection<Schluessel> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public ObservableCollection<Schluessel> LadeAlleFreien()
        {
            return repo.LadeAlleFreien();
        }

        public void Speichern(Schluessel entity)
        {
            if (repo.IstSchluesselNummerVorhanden(entity.Nummer))
                throw new SchluesselNummerIstSchonVorhandenException();

            repo.Speichern(entity);
        }

        public int ErmittleFreieAnzahl(int SchluesselID)
        {
            return repo.LadeByID(SchluesselID).FreieAnzahl;
        }
    }
}
