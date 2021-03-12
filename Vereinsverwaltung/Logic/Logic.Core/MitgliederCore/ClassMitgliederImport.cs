using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Types.MitgliederTypes;
using Vereinsverwaltung.Logic.Core.MitgliederCore.Models;

namespace Vereinsverwaltung.Logic.Core.MitgliederCore
{
    public class ClassMitgliederImport
    {
        public ObservableCollection<MitgliedImportModel> StartImport( string path )
        {
            var importList = new ObservableCollection<MitgliedImportModel>();
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var FirstRow = true;
                    do
                    {
                        while (reader.Read())
                        {
                            if (FirstRow) { FirstRow = false;  continue; };
                            if (reader.IsDBNull(0)) continue;

                            var Mitglied = new MitgliedImportModel
                            {
                                Mitgliedsnr = Convert.ToInt32(reader.GetString(0)),
                                Name = reader.GetString(1),
                                Straße = reader.GetString(2),
                                Ort = reader.GetString(4),
                                Eintrittsdatum = reader.GetDateTime(5)
                            };
                            if (!reader.IsDBNull(6))
                                Mitglied.Geburtstag = reader.GetDateTime(6);

                            importList.Add(Mitglied);
                        }
                    } while (reader.NextResult());
                }
            }
            CheckList(importList);

            return importList;
        }


        public void Uebernahme(IList<MitgliedImportModel> mitgliedImportModels)
        {
            var API = new MitgliedAPI();
            var MitgliederList = mitgliedImportModels.Where(mi => mi.State == MitgliedImportStateTypes.Former);
            MitgliederList.ToList().ForEach(m =>
            {
                var Mitglied = API.LadeByMitgliedsNr(m.Mitgliedsnr);
                Mitglied.MitgliedStatus = MitgliedStatus.Ehemalig;
                Mitglied.Mitgliedsnr = null;
                Mitglied.AusgetretenAm = DateTime.Now;
                API.Aktualisieren(Mitglied);
            });

            MitgliederList = mitgliedImportModels.Where(mi => (mi.State != MitgliedImportStateTypes.NoUpdate) && (mi.State != MitgliedImportStateTypes.Former));
            

            MitgliederList.ToList().ForEach(m => {
                if (m.State.Equals(MitgliedImportStateTypes.New))
                    API.Speichern(new Mitglied { Eintrittsdatum = m.Eintrittsdatum, Geburtstag = m.Geburtstag, Mitgliedsnr = m.Mitgliedsnr, Name = m.Name, Ort = m.Ort, Straße = m.Straße, Vorname = m.Vorname, MitgliedStatus = MitgliedStatus.Aktiv});
                else if (m.State.Equals(MitgliedImportStateTypes.Update))
                {
                    var Mitglied = API.LadeByMitgliedsNr(m.Mitgliedsnr);

                    Mitglied.Name = m.Name;
                    Mitglied.Eintrittsdatum = m.Eintrittsdatum;
                    Mitglied.Geburtstag = m.Geburtstag;
                    Mitglied.Ort = m.Ort;
                    Mitglied.Straße = m.Straße;
                    Mitglied.MitgliedStatus = MitgliedStatus.Aktiv;
                    Mitglied.Vorname = Mitglied.Vorname;
         
                    API.Aktualisieren(Mitglied);
                }
            });
        }

        private void CheckList(IList<MitgliedImportModel> models)
        {
            var MitgliedAPI = new MitgliedAPI();
            var MitgliedNrList = MitgliedAPI.LadeAlleAktivenMitgliedsNr();

            models.ToList().ForEach(m =>
            {
               var Mitglied = MitgliedAPI.LadeByMitgliedsNr(m.Mitgliedsnr);
                if (Mitglied == null)
                    m.State = MitgliedImportStateTypes.New;
                else
                {
                    MitgliedNrList.Remove(m.Mitgliedsnr);

                    if (!Mitglied.Eintrittsdatum.Equals(m.Eintrittsdatum) && (!Mitglied.Geburtstag.Equals(m.Geburtstag)))
                    {
                        m.State = MitgliedImportStateTypes.New;
                        var MitgliedModel = new MitgliedImportModel 
                        { 
                            Eintrittsdatum = Mitglied.Eintrittsdatum,
                            Geburtstag = Mitglied.Geburtstag,
                            Mitgliedsnr = Mitglied.Mitgliedsnr.Value,
                            Name = Mitglied.Name,
                            Ort = Mitglied.Ort,
                            State = MitgliedImportStateTypes.Former,
                            Straße = Mitglied.Straße,
                            Vorname = Mitglied.Vorname 
                        };

                        var Item = models.Where(w => w.Mitgliedsnr.Equals(MitgliedModel.Mitgliedsnr) && w.Geburtstag.Equals(MitgliedModel.Geburtstag) && w.Eintrittsdatum.Equals(MitgliedModel.Eintrittsdatum)).FirstOrDefault();
                        if (Item != null)
                            models.Remove(Item);

                        models.Add(MitgliedModel);
                    }
                    else if ( (!Mitglied.Name.Equals(m.Name)) ||
                              (!Mitglied.Ort.Equals(m.Ort)) ||
                              (!Mitglied.Straße.Equals(m.Straße)) ||
                              (!Mitglied.Eintrittsdatum.Equals(m.Eintrittsdatum)) ||
                              (!Mitglied.Geburtstag.Equals(m.Geburtstag))
                            )

                    {
                        m.State = MitgliedImportStateTypes.Update;
                    }
                    else
                        m.State = MitgliedImportStateTypes.NoUpdate;
                }
            });

            var EntfernteMitglieder = MitgliedAPI.LadeAlleAnhandMitgliedsNr(MitgliedNrList);

            EntfernteMitglieder.ToList().ForEach(mg => {
                var MitgliedModel = new MitgliedImportModel
                {
                    Eintrittsdatum = mg.Eintrittsdatum,
                    Geburtstag = mg.Geburtstag,
                    Mitgliedsnr = mg.Mitgliedsnr.Value,
                    Name = mg.Name,
                    Ort = mg.Ort,
                    State = MitgliedImportStateTypes.Former,
                    Straße = mg.Straße,
                    Vorname = mg.Vorname
                };

                models.Add(MitgliedModel);
            });

        }
    }
}
