using Data.Model.MitgliederModels;
using Data.Model.SchnurrschiessenModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.ImportModel
{
    public class SchnurschiessenImportHistoryModel
    {
        public int ID { get; set; }
        public DateTime EingelesenAm { get; set; }
        public int GesamtEingelesen { get; set; }
        public int AnzahlNeu { get; set; }
        public int AnzahlEhemalig { get; set; }
        public int AnzahlKeineAenderung { get; set; }
        public int AnzahlAenderung { get; set; }

        public ObservableCollection<SchnurschiessenMitgliederImportModel> Importlist { get; set; }

        public SchnurschiessenImportHistoryModel()
        {
            Importlist = new ObservableCollection<SchnurschiessenMitgliederImportModel>();
        }
    }
}
