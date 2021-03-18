using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.SchnurEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.SchnurschiessenRepos
{
    public class SchnurauszeichnungRepository : BaseRepository
    {
        public void Speichern(Schnurauszeichnung schnurauszeichnung)
        {
            if (schnurauszeichnung.ID == 0)
                repo.Schnurauszeichnungen.Add(schnurauszeichnung);

            repo.SaveChanges();
        }

        public ObservableCollection<Schnurauszeichnung> LadeAlle()
        {
            return new ObservableCollection<Schnurauszeichnung>(repo.Schnurauszeichnungen.Include(i => i.Hauptteil).OrderBy(o => o.ID).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Schnurauszeichnungen.Remove(repo.Schnurauszeichnungen.Find(id));
            repo.SaveChanges();
        }

        public Schnurauszeichnung LadeByID(int id)
        {
            return repo.Schnurauszeichnungen.Include(i => i.Hauptteil).Where(a => a.ID == id).First();
        }

        public bool RangfolgeVorhanden(int rangfolge)
        {
            return (repo.Schnurauszeichnungen.Where(d => d.Rangfolge == rangfolge).Count() > 0);
        }
    }
}
