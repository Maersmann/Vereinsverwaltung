using Data.Model.MitgliederModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.ImportModel
{
    public class MitgliedImportHistoryModel
    {
        public int ID { get; set; }
        public DateTime EingelesenAm { get; set; }
        public int GesamtEingelesen { get; set; }
        public int AnzahlNeu { get; set; }
        public int AnzahlEhemalig { get; set; }
        public int AnzahlKeineAenderung { get; set; }
        public int AnzahlAenderung { get; set; }

        public ObservableCollection<MitgliederImportModel> Importlist { get; set; }

        public MitgliedImportHistoryModel()
        {
            Importlist = new ObservableCollection<MitgliederImportModel>();
        }
    }
}
