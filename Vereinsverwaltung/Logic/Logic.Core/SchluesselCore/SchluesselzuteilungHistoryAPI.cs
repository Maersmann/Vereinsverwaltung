using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.SchluesselRepos;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Logic.Core.Interfaces;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore
{
    public class SchluesselzuteilungHistoryAPI : IAPI<SchluesselzuteilungHistory>
    {
        private readonly SchluesselzuteilungHistoryRepository repo;
        public SchluesselzuteilungHistoryAPI()
        {
            repo = new SchluesselzuteilungHistoryRepository();
        }
        public void Aktualisieren(SchluesselzuteilungHistory entity)
        {
            repo.Speichern(entity);
        }
        public void Entfernen(int id)
        {
            repo.Entfernen(id);
        }
        public SchluesselzuteilungHistory Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public SchluesselzuteilungHistory LadeAnhandZuteilungID(int id)
        {
            return repo.LadeByZuteilungID(id);
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAlleFuerBesitzer(int besitzerid)
        {
            return repo.LadeAllByBesitzerID(besitzerid);
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAlleFuerSchluessel(int schluesselid)
        {
            return repo.LadeAllBySchluesselID(schluesselid);
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAlle()
        {
            return repo.LadeAlle();
        }
        public void Speichern(SchluesselzuteilungHistory entity)
        {
            repo.Speichern(entity);
        }
    }
}