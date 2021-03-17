using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.SchnurschiessenRepos;
using Vereinsverwaltung.Data.Model.SchnurEntitys;
using Vereinsverwaltung.Logic.Core.Interfaces;

namespace Vereinsverwaltung.Logic.Core.SchnurschiessenCore
{
    public class SchnurAPI : IAPI<Schnur>
    {
        private readonly SchnurRepository repo;
        public SchnurAPI()
        {
            repo = new SchnurRepository();
        }

        public void Aktualisieren(Schnur entity)
        {
            repo.Speichern(entity);
        }

        public void Entfernen(int id)
        {
            repo.Entfernen(id);
        }

        public Schnur Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public ObservableCollection<Schnur> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public void Speichern(Schnur entity)
        {
            repo.Speichern(entity);
        }
    }
}
