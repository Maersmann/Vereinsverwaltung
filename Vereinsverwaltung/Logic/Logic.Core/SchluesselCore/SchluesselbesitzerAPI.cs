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
    public class SchluesselbesitzerAPI : IAPI<Schluesselbesitzer>
    {
        private readonly SchluesselbesitzerRepository repo;
        public SchluesselbesitzerAPI()
        {
            repo = new SchluesselbesitzerRepository();
        }
        public void Aktualisieren(Schluesselbesitzer entity)
        {
            repo.Speichern(entity);
        }

        public void Entfernen(int id)
        {
            if (new SchluesselzuteilungAPI().EintraegeFuerSchluesselbesitzervorhanden(id))
                throw new SchluesselbesitzerSindSchluesselZugeteiltException();

            repo.Entfernen(id);
        }

        public Schluesselbesitzer Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public ObservableCollection<Schluesselbesitzer> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public void Speichern(Schluesselbesitzer entity)
        {
            repo.Speichern(entity);
        }
    }
}
