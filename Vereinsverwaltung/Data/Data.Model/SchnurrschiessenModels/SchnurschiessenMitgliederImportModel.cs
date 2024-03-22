using Data.Model.MitgliederModels;
using Data.Types.SchnurschiessenTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.SchnurrschiessenModels
{
    public class SchnurschiessenMitgliederImportModel
    {
        public string Vorname { get; set; }
        public string Name { get; set; }
        public int? MitgliedsNr { get; set; }
        public int? MitgliedsID { get; set; }

        public SchnurschiessenImportStateType Type { get; set; }

        public ObservableCollection<SchnurschiessenMitgliedRangModel> RangList { get; set; }

        public SchnurschiessenMitgliederImportModel()
        {
            RangList = new ObservableCollection<SchnurschiessenMitgliedRangModel>();
        }
    }

    public class SchnurschiessenMitgliedRangModel
    {
        public int Jahr { get; set; }
        public bool Erhalten { get; set; }
        public int Rang { get; set; }
        public int NeueRangID { get; set; }
        public bool NeueStufe { get; set; }

    }
}
