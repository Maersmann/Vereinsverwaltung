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
    public class SchluesselzuteilungAPI : IAPI<Schluesselzuteilung>
    {
        private readonly SchluesselzuteilungRepository repo;
        public SchluesselzuteilungAPI()
        {
            repo = new SchluesselzuteilungRepository();
        }
        public void Aktualisieren(Schluesselzuteilung entity)
        {
            repo.Speichern(entity);
        }
        public void Entfernen(int id)
        {
            repo.Entfernen(id);
        }
        public Schluesselzuteilung Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public ObservableCollection<Schluesselzuteilung> LadeAlleFuerBesitzer(int besitzerid)
        {
            return repo.LadeAllByBesitzerID(besitzerid);
        }

        public ObservableCollection<Schluesselzuteilung> LadeAlleFuerSchluessel(int schluesselid)
        {
            return repo.LadeAllBySchluesselID(schluesselid);
        }

        public ObservableCollection<Schluesselzuteilung> LadeAlle()
        {
            return repo.LadeAlle();
        }
        public void Speichern(Schluesselzuteilung entity)
        {
            repo.Speichern(entity);
        }

        public bool EintraegeFuerSchluesselvorhanden(int schluesselID)
        {
            return repo.IstVorhandenBySchluesselID(schluesselID);
        }
        public bool EintraegeFuerSchluesselbesitzervorhanden(int schluesselbesitzerID)
        {
            return repo.IstVorhandenBySchluesselbesitzerID(schluesselbesitzerID);
        }
    }
}
