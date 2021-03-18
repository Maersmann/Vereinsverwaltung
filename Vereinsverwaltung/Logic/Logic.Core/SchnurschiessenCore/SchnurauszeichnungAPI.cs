using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.SchnurschiessenRepos;
using Vereinsverwaltung.Data.Model.SchnurEntitys;
using Vereinsverwaltung.Logic.Core.Interfaces;
using Vereinsverwaltung.Logic.Core.SchnurschiessenCore.Exceptions;

namespace Vereinsverwaltung.Logic.Core.SchnurschiessenCore
{
    public class SchnurauszeichnungAPI : IAPI<Schnurauszeichnung>
    {
        private readonly SchnurauszeichnungRepository repo;
        public SchnurauszeichnungAPI()
        {
            repo = new SchnurauszeichnungRepository();
        }

        public void Aktualisieren(Schnurauszeichnung entity)
        {
            if (repo.RangfolgeVorhanden(entity.Rangfolge))
                throw new RangfolgeSchonVorhandenException();

            repo.Speichern(entity);
        }

        public void Entfernen(int id)
        {
            repo.Entfernen(id);
        }

        public Schnurauszeichnung Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public ObservableCollection<Schnurauszeichnung> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public void Speichern(Schnurauszeichnung entity)
        {
            if (repo.RangfolgeVorhanden(entity.Rangfolge))
                throw new RangfolgeSchonVorhandenException();

            repo.Speichern(entity);
        }
    }
}
