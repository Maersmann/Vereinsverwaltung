using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data.Model.ExportModels
{
    public class ExportMitgliederAenderungenModel
    {
        public int ID { get; set; }
        public int MitgliedsNr { get; set; }
        public string Name { get; set; }
        public ObservableCollection<MitgliedAenderungDatenModel> Daten { get; set; }

        public ExportMitgliederAenderungenModel()
        {
            Daten = new ObservableCollection<MitgliedAenderungDatenModel>();
        }
    }

    public class MitgliedAenderungDatenModel
    {
        public string Fieldname { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Aenderungsdatum { get; set; }
    }
}
