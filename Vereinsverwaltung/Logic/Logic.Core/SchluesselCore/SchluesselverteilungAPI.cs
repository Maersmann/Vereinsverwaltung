using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;
using Vereinsverwaltung.Data.Types.SchluesselverwaltungTypes;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Exceptions;
using Vereinsverwaltung.Logic.Core.SchluesselCore.Models;

namespace Vereinsverwaltung.Logic.Core.SchluesselCore
{
    public class SchluesselverteilungAPI
    {
        public ObservableCollection<SchluesselzuteilungBesitzerModel> LadeVerteilungSchluesselbesitzer()
        {
            var returnlist = new ObservableCollection<SchluesselzuteilungBesitzerModel>();
            var BesitzerGroupList = new SchluesselzuteilungAPI().LadeAlle().GroupBy(g => g.Schluesselbesitzer).ToList();

            foreach (var group in BesitzerGroupList)
            {
                var model = new SchluesselzuteilungBesitzerModel
                {
                    Name = group.Key.Name,
                    SchluesselbesitzerID = group.Key.ID
                };
                foreach (var items in group)
                {
                    model.Anzahl += items.Anzahl;
                }
                returnlist.Add(model);
            }


            return returnlist;
        }

        public ObservableCollection<SchluesselzuteilungSchluesselModel> LadeVerteilungSchluessel()
        {
            var returnlist = new ObservableCollection<SchluesselzuteilungSchluesselModel>();
            var SchluesselGroupList = new SchluesselzuteilungAPI().LadeAlle().GroupBy(g => g.Schluessel).ToList();

            foreach (var group in SchluesselGroupList)
            {
                var model = new SchluesselzuteilungSchluesselModel
                {
                    Nummer = group.Key.Nummer,
                    Beschreibung = group.Key.Beschreibung,
                    AnzahlFrei = group.Key.FreieAnzahl,
                    AnzahlGesamt = group.Key.Bestand,
                    Bezeichnung = group.Key.Bezeichnung,
                    SchluesselID = group.Key.ID,
                    Ausgegeben = group.Key.Ausgegeben
                };
                returnlist.Add(model);
            }


            return returnlist;
        }

        public void ErhalteSchluesselZurueck( int schluesselzuteilungID, int zurueckerhalten, DateTime rueckgabeAm)
        {
            var Zuteilung = new SchluesselzuteilungAPI().Lade(schluesselzuteilungID);
            var Schluessel = new SchluesselAPI().Lade(Zuteilung.SchluesselID);

            if (Schluessel.Ausgegeben < zurueckerhalten)
                throw new ZuVieleSchlüsselZurueckgegeben();

            var History = new SchluesselzuteilungHistory
            {
                SchluesselbesitzerID = Zuteilung.SchluesselbesitzerID,
                SchluesselID = Zuteilung.SchluesselID,
                Datum = rueckgabeAm,
                State = ZuteilungState.Rueckgabe,
                Anzahl = zurueckerhalten,
                SchluesselzuteilungID = schluesselzuteilungID
            };
            new SchluesselzuteilungHistoryAPI().Speichern(History);

            Schluessel.Ausgegeben -= zurueckerhalten;

            new SchluesselAPI().Aktualisieren(Schluessel);
            new SchluesselzuteilungAPI().Entfernen(Zuteilung.ID);

        }

        public void TeileSchluesselZu(Schluesselzuteilung entity)
        {
            var schluessel = new SchluesselAPI().Lade(entity.SchluesselID);

            if (schluessel.FreieAnzahl < entity.Anzahl)
                throw new ZuWenigFreieSchluesselVorhandenException();

            schluessel.Ausgegeben += entity.Anzahl;

            new SchluesselAPI().Aktualisieren(schluessel);
            new SchluesselzuteilungAPI().Speichern(entity);

            var History = new SchluesselzuteilungHistory
            {
                SchluesselbesitzerID = entity.SchluesselbesitzerID,
                SchluesselID = entity.SchluesselID,
                Datum = entity.ErhaltenAm,
                State = ZuteilungState.Zuteilung,
                Anzahl = entity.Anzahl,
                SchluesselzuteilungID = entity.ID
            };
            new SchluesselzuteilungHistoryAPI().Speichern(History);
        }

        public void EntferneZuteilung(int schluesselzuteilungID)
        {
            var Zuteilung = new SchluesselzuteilungAPI().Lade(schluesselzuteilungID);
            var Schluessel = new SchluesselAPI().Lade(Zuteilung.SchluesselID);
            var History = new SchluesselzuteilungHistoryAPI().LadeAnhandZuteilungID(schluesselzuteilungID);

            if (History != null) new SchluesselzuteilungHistoryAPI().Entfernen(History.ID);

            Schluessel.Ausgegeben -= Zuteilung.Anzahl;

            new SchluesselAPI().Aktualisieren(Schluessel);
            new SchluesselzuteilungAPI().Entfernen(schluesselzuteilungID);
        }
    }
}
