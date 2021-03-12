using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.SchluesselRepos
{
    public class SchluesselzuteilungRepository : BaseRepository
    {
        public void Speichern(Schluesselzuteilung schluesselverteilung)
        {
            if (schluesselverteilung.ID == 0)
                repo.Schluesselverteilung.Add(schluesselverteilung);

            repo.SaveChanges();
        }

        public ObservableCollection<Schluesselzuteilung> LadeAlle()
        {
            return new ObservableCollection<Schluesselzuteilung>(repo.Schluesselverteilung.Include(i=>i.Schluesselbesitzer).Include(i => i.Schluessel).OrderBy(o => o.ErhaltenAm).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Schluesselverteilung.Remove(repo.Schluesselverteilung.Find(id));
            repo.SaveChanges();
        }

        public Schluesselzuteilung LadeByID(int id)
        {
            return repo.Schluesselverteilung.Where(a => a.ID == id).First();
        }

        public ObservableCollection<Schluesselzuteilung> LadeAllByBesitzerID(int besitzerid)
        {
            return new ObservableCollection<Schluesselzuteilung>(repo.Schluesselverteilung.Include(i => i.Schluesselbesitzer).Include(i => i.Schluessel).Where(s => s.SchluesselbesitzerID == besitzerid).OrderByDescending(o => o.ErhaltenAm).ToList());
        }

        public ObservableCollection<Schluesselzuteilung> LadeAllBySchluesselID(int schluesselid)
        {
            return new ObservableCollection<Schluesselzuteilung>(repo.Schluesselverteilung.Include(i => i.Schluesselbesitzer).Include(i => i.Schluessel).Where(s => s.SchluesselID == schluesselid).OrderByDescending(o => o.ErhaltenAm).ToList());
        }

        public bool IstVorhandenBySchluesselID(int schluesselID)
        {
            return repo.Schluesselverteilung.Where(a => a.SchluesselID == schluesselID).Count() != 0;
        }

        public bool IstVorhandenBySchluesselbesitzerID(int schluesselbesitzerID)
        {
            return repo.Schluesselverteilung.Where(a => a.SchluesselbesitzerID == schluesselbesitzerID).Count() != 0;
        }
    }
}
