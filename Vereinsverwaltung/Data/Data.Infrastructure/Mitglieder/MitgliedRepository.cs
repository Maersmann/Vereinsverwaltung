using System;
using System.Collections.Generic;
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
            if (inMitglied.ID != 0)
                repo.Mitglieder.Update(inMitglied);
            else
                repo.Mitglieder.Add(inMitglied);

            repo.SaveChanges();
        }

        public bool VorhandenByMitgliedsNr( int inMitgliedsNr )
        {
            return repo.Mitglieder.Where(m => m.Mitgliedsnr.Equals(inMitgliedsNr)).FirstOrDefault() != null; ;

        }
    }
}
