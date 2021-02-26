using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Types.MitgliederTypes;

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

        public bool VorhandenByMitgliedsNr( int mitgliedsnr)
        {
            return repo.Mitglieder.Where(m => m.Mitgliedsnr.Equals(mitgliedsnr)).FirstOrDefault() != null; ;

        }

        public ObservableCollection<Mitglied> LadeAlle()
        {
            return new ObservableCollection<Mitglied>(repo.Mitglieder.OrderBy(o => o.ID).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Mitglieder.Remove(repo.Mitglieder.Find(id));
            repo.SaveChanges();
        }

        public ObservableCollection<Mitglied> LadeAlleAktiven()
        {
            return new ObservableCollection<Mitglied>(repo.Mitglieder.Where( w => w.MitgliedStatus == MitgliedStatus.Aktiv).OrderBy(o => o.ID).ToList());
        }

        public ObservableCollection<Mitglied> LadeAlleAnhandMitgliedsNr(IList<int> mitgliedsnr)
        {
            return new ObservableCollection<Mitglied>(repo.Mitglieder.Where(m => mitgliedsnr.Contains(m.Mitgliedsnr.GetValueOrDefault(0))).ToList());
        }

        public Mitglied LadeByID(int id)
        {
            return repo.Mitglieder.Where(a => a.ID == id).First();
        }

        public Mitglied LadeByMitgliedsNr(int mitgliedsnr)
        {
            return repo.Mitglieder.Where(a => a.Mitgliedsnr == mitgliedsnr).FirstOrDefault();
        }
    }
}
