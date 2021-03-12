using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.Mitglieder;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Logic.Core.Interfaces;

namespace Vereinsverwaltung.Logic.Core.MitgliederCore
{
    public class MitgliedAPI : IAPI<Mitglied>
    {
        private readonly MitgliedRepository repo;
        public MitgliedAPI()
        {
            repo = new MitgliedRepository();
        }
        public void Speichern(Mitglied mitglied)
        {
            if (mitglied.Mitgliedsnr.HasValue && repo.VorhandenByMitgliedsNr(mitglied.Mitgliedsnr.GetValueOrDefault()))
            {
                throw new MitgliedMitMitgliedsNrVorhanden();
            }
            new ClassMitglied().SetMitgliedStatus(mitglied);
            repo.Speichern(mitglied);
        }

        public void Aktualisieren(Mitglied mitglied)
        {
            new ClassMitglied().SetMitgliedStatus(mitglied);
            repo.Speichern(mitglied);
        }

        public ObservableCollection<Mitglied> LadeAlleAktiven()
        {
            return repo.LadeAlleAktiven();
        }

        public ObservableCollection<Mitglied> LadeAlle()
        {
            return repo.LadeAlle();
        }

        public ObservableCollection<Mitglied> LadeAlleAnhandMitgliedsNr( IList<int> mitgliedsnr )
        {
            return repo.LadeAlleAnhandMitgliedsNr(mitgliedsnr);
        }

        public List<int> LadeAlleAktivenMitgliedsNr()
        {
            var mitglieder = new List<int>();

            LadeAlleAktiven().ToList().ForEach(m => { mitglieder.Add(m.Mitgliedsnr.Value); });

            return mitglieder;
        }

        public void Entfernen(int id)
        {
            repo.Entfernen(id);
        }

        public Mitglied Lade(int id)
        {
            return repo.LadeByID(id);
        }

        public Mitglied LadeByMitgliedsNr(int mitgliedsnr)
        {
            return repo.LadeByMitgliedsNr(mitgliedsnr);
        }
    }


    public class MitgliedMitMitgliedsNrVorhanden : Exception
    {

    }
}
