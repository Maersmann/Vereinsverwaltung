using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.SchnurEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.SchnurschiessenRepos
{
    public class SchnurRepository : BaseRepository
    {
        public void Speichern(Schnur schnur)
        {
            if (schnur.ID == 0)
                repo.Schnuere.Add(schnur);

            repo.SaveChanges();
        }

        public ObservableCollection<Schnur> LadeAlle()
        {
            return new ObservableCollection<Schnur>(repo.Schnuere.OrderBy(o => o.ID).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Schnuere.Remove(repo.Schnuere.Find(id));
            repo.SaveChanges();
        }

        public Schnur LadeByID(int id)
        {
            return repo.Schnuere.Where(a => a.ID == id).First();
        }

    }
}
