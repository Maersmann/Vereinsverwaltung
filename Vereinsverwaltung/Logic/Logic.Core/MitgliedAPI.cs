using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.Mitglieder;
using Vereinsverwaltung.Data.Model.MitgliederEntitys;

namespace Vereinsverwaltung.Logic.Core
{
    public class MitgliedAPI
    {
        private MitgliedRepository repo;
        public MitgliedAPI()
        {
            repo = new MitgliedRepository();
        }
        public void Speichern(Mitglied inMitglied)
        {
            if ( inMitglied.Mitgliedsnr.HasValue && repo.VorhandenByMitgliedsNr( inMitglied.Mitgliedsnr.GetValueOrDefault()))
            {
                throw new MitgliedMitMitgliedsNrVorhanden();
            }
            repo.Speichern(inMitglied);
        }

        public ObservableCollection<Mitglied> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public void Entfernen(int inID)
        {
            repo.Entfernen(inID);
        }

        public Mitglied Lade(int inID)
        {
            return repo.LadeByID(inID);
        }
    }


    public class MitgliedMitMitgliedsNrVorhanden : Exception
    {

    }
}
