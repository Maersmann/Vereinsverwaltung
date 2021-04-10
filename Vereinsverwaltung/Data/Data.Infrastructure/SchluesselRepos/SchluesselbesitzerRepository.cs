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
    public class SchluesselbesitzerRepository : BaseRepository
    {
        public void Speichern(Schluesselbesitzer schluesselbesitzer)
        {
            if (schluesselbesitzer.ID == 0)
                repo.Schluesselbesitzer.Add(schluesselbesitzer);

            if (schluesselbesitzer.MitgliedID.GetValueOrDefault(0) == 0)
                schluesselbesitzer.Mitglied = null;    

            repo.SaveChanges();
        }

        public ObservableCollection<Schluesselbesitzer> LadeAlle()
        {
            return new ObservableCollection<Schluesselbesitzer>(repo.Schluesselbesitzer.Include( i => i.Verteilung).OrderBy(o => o.Name).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Schluesselbesitzer.Remove(repo.Schluesselbesitzer.Find(id));
            repo.SaveChanges();
        }

        public Schluesselbesitzer LadeByID(int id)
        {
            return repo.Schluesselbesitzer.Where(a => a.ID == id).First();
        }

        public bool HatMitgliedEinDatensatz(int value)
        {
            return repo.Schluesselbesitzer.Where(m => m.MitgliedID.Equals(value)).FirstOrDefault() != null; ;
        }
    }
}
