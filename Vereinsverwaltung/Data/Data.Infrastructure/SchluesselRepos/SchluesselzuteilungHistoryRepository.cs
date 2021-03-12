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
    public class SchluesselzuteilungHistoryRepository : BaseRepository
    {
        public void Speichern(SchluesselzuteilungHistory schluesselverteilunghistory)
        {
            if (schluesselverteilunghistory.ID == 0)
                repo.SchluesselzuteilungHistory.Add(schluesselverteilunghistory);

            repo.SaveChanges();
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAlle()
        {
            return new ObservableCollection<SchluesselzuteilungHistory>(repo.SchluesselzuteilungHistory.Include(i => i.Schluesselbesitzer).Include(i => i.Schluessel).OrderBy(o => o.Datum).ToList());
        }

        public void Entfernen(int id)
        {
            repo.SchluesselzuteilungHistory.Remove(repo.SchluesselzuteilungHistory.Find(id));
            repo.SaveChanges();
        }

        public SchluesselzuteilungHistory LadeByZuteilungID(int schluesselzuteilungID)
        {
            return repo.SchluesselzuteilungHistory.Where(a => a.SchluesselzuteilungID == schluesselzuteilungID).FirstOrDefault();
        }

        public SchluesselzuteilungHistory LadeByID(int id)
        {
            return repo.SchluesselzuteilungHistory.Where(a => a.ID == id).First();
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAllByBesitzerID(int besitzerid)
        {
            return new ObservableCollection<SchluesselzuteilungHistory>(repo.SchluesselzuteilungHistory.Include(i => i.Schluesselbesitzer).Include(i => i.Schluessel).Where(s => s.SchluesselbesitzerID == besitzerid).OrderByDescending(o => o.Datum).ToList());
        }

        public ObservableCollection<SchluesselzuteilungHistory> LadeAllBySchluesselID(int schluesselid)
        {
            return new ObservableCollection<SchluesselzuteilungHistory>(repo.SchluesselzuteilungHistory.Include(i => i.Schluesselbesitzer).Include(i => i.Schluessel).Where(s => s.SchluesselID == schluesselid).OrderByDescending(o => o.Datum).ToList());
        }
    }
}
