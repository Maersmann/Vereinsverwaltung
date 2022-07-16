using Data.Types.KoenigschiessenTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.KoenigschiessenModels
{
    public class KoenigschiessenRundenZahlenModel
    {
        public KoenigschiessenArt Art { get; set; }
        public int SchuetzenOffen { get; set; }
        public int SchuetzenTeilgenommen { get; set; }
        public ObservableCollection<KoenigschiessenRundenZahlenDetailModel> Runden { get; set; }

        public int SchuetzenGesamt => SchuetzenOffen + SchuetzenTeilgenommen;

        public KoenigschiessenRundenZahlenModel()
        {
            Runden = new ObservableCollection<KoenigschiessenRundenZahlenDetailModel>();
        }
    }

    public class KoenigschiessenRundenZahlenDetailModel
    {
        public string RundeBezeichnug { get; set; }
        public int SchuetzenErgebnisAbgegeben { get; set; }
        public int SchuetzenErgebnisOffen { get; set; }

        public int SchuetzenGesamt => SchuetzenErgebnisAbgegeben + SchuetzenErgebnisOffen;
    }
}
