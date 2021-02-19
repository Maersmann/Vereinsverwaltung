using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.MitgliederEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.Mitglieder
{
    public class MitgliedRepository: BaseRepository
    {
        public void Speichern(Mitglied inMitglied)
        {
            if (inMitglied.ID == 0)
                repo.Mitglieder.Add(inMitglied);

            repo.SaveChanges();
        }

        public bool VorhandenByMitgliedsNr( int inMitgliedsNr )
        {
            return repo.Mitglieder.Where(m => m.Mitgliedsnr.Equals(inMitgliedsNr)).FirstOrDefault() != null; ;

        }

        public ObservableCollection<Mitglied> LadeAlle()
        {
            return new ObservableCollection<Mitglied>(repo.Mitglieder.OrderBy(o => o.ID).ToList());
        }

        public void Entfernen(int inID)
        {
            repo.Mitglieder.Remove(repo.Mitglieder.Find(inID));
            repo.SaveChanges();
        }

        public Mitglied LadeByID(int inID)
        {
            return repo.Mitglieder.Where(a => a.ID == inID).First();
        }
    }
}
