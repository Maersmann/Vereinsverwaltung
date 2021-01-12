using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.Mitglieder;
using Vereinsverwaltung.Data.Model.MitgliederEntitys;

namespace Vereinsverwaltung.Logic.Core
{
    public class MitgliedAPI
    {
        public void Speichern(Mitglied inMitglied)
        {
            var repo = new MitgliedRepository();
            if ( inMitglied.Mitgliedsnr.HasValue && repo.VorhandenByMitgliedsNr( inMitglied.Mitgliedsnr.GetValueOrDefault()))
            {
                throw new MitgliedMitMitgliedsNrVorhanden();
            }
            repo.Speichern(inMitglied);
        }
    }


    public class MitgliedMitMitgliedsNrVorhanden : Exception
    {

    }
}
