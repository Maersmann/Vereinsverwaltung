using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.SchluesselRepos
{
    public class SchluesselRepository : BaseRepository
    {
        public void Speichern(Schluessel schluessel)
        {
            if (schluessel.ID == 0)
                repo.Schluessels.Add(schluessel);

            repo.SaveChanges();
        }

        public bool IstSchluesselNummerVorhanden(int schluesselnummer)
        {
            return repo.Schluessels.Where(m => m.Nummer.Equals(schluesselnummer)).FirstOrDefault() != null; ;

        }

        public ObservableCollection<Schluessel> LadeAlle()
        {
            return new ObservableCollection<Schluessel>(repo.Schluessels.OrderBy(o => o.Nummer).ToList());
        }

        public void Entfernen(int id)
        {
            repo.Schluessels.Remove(repo.Schluessels.Find(id));
            repo.SaveChanges();
        }

        public Schluessel LadeByID(int id)
        {
            return repo.Schluessels.Where(a => a.ID == id).First();
        }

        public ObservableCollection<Schluessel> LadeAlleFreien()
        {
            return new ObservableCollection<Schluessel>(repo.Schluessels.Where(s => s.Bestand != s.Ausgegeben).OrderBy(o => o.Nummer).ToList());
        }
    }
}
