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
using Vereinsverwaltung.Data.Model.ImportEntitys;
using Vereinsverwaltung.Data.Types.MitgliederTypes;
using Vereinsverwaltung.Logic.Core.ImportHistoryCore;
using Vereinsverwaltung.Logic.Core.MitgliederCore.Models;

namespace Vereinsverwaltung.Logic.Core.MitgliederCore
{
    public class ClassMitgliederImport
    {
        private MitgliedImportHistory history;
        public ClassMitgliederImport(MitgliedImportHistory mitgliedImportHistory)
        {
            history = mitgliedImportHistory;
        }

        public ObservableCollection<MitgliedImportModel> StartImport( string path )
        {
            history.EingelesenAm = DateTime.Now;

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
                                Mitgliedsnr = Convert.ToInt32(reader.GetString(1)),
                                Name = reader.GetString(4),
                                Vorname = reader.GetString(3),
                                Straße = reader.GetString(8),
                                Ort = reader.GetString(10),
                                Eintrittsdatum = Convert.ToDateTime(reader.GetString(13))

                            };
                            if (!reader.IsDBNull(12))
                            Mitglied.Geburtstag = Convert.ToDateTime(reader.GetString(12));
                       
                            if (!reader.IsDBNull(2) && reader.GetString(2).Equals("Frau"))
                                Mitglied.Geschlecht = Geschlecht.weiblich;

                            if (Mitglied.Straße is null) Mitglied.Straße = "";
                            if (Mitglied.Ort is null) Mitglied.Ort = "";

                            importList.Add(Mitglied);
                        }
                    } while (reader.NextResult());
                }
            }

            history.GesamtEingelesen = importList.Count();

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
                {
                    try
                    {
                        API.Speichern(new Mitglied { Eintrittsdatum = m.Eintrittsdatum, Geburtstag = m.Geburtstag, Mitgliedsnr = m.Mitgliedsnr, Name = m.Name, Ort = m.Ort, Straße = m.Straße, Vorname = m.Vorname, MitgliedStatus = MitgliedStatus.Aktiv, Geschlecht = m.Geschlecht });
                    }
                    catch (MitgliedMitMitgliedsNrVorhanden)
                    {

                    }
            }
                else if (m.State.Equals(MitgliedImportStateTypes.Update))
                {
                    var Mitglied = API.LadeByMitgliedsNr(m.Mitgliedsnr);

                    Mitglied.Name = m.Name;
                    Mitglied.Eintrittsdatum = m.Eintrittsdatum;
                    Mitglied.Geburtstag = m.Geburtstag;
                    Mitglied.Ort = m.Ort;
                    Mitglied.Straße = m.Straße;
                    Mitglied.MitgliedStatus = MitgliedStatus.Aktiv;
                    Mitglied.Vorname = m.Vorname;
                    Mitglied.Geschlecht = m.Geschlecht;
         
                    API.Aktualisieren(Mitglied);
                }
            });
            new ImportMitgliederAPI().Speichern(history);
        }

        private void CheckList(IList<MitgliedImportModel> models)
        {
            var MitgliedAPI = new MitgliedAPI();
            var MitgliedNrList = MitgliedAPI.LadeAlleAktivenMitgliedsNr();

            models.ToList().ForEach(m =>
            {
               var Mitglied = MitgliedAPI.LadeByMitgliedsNr(m.Mitgliedsnr);
                if (Mitglied == null)
                {
                    history.AnzahlNeu++;
                    m.State = MitgliedImportStateTypes.New;
                }
                else
                {
                    MitgliedNrList.Remove(m.Mitgliedsnr);

                    if (!Mitglied.Eintrittsdatum.Equals(m.Eintrittsdatum) && (!Mitglied.Geburtstag.Equals(m.Geburtstag)))
                    {
                        history.AnzahlNeu++;
                        history.AnzahlEhemalig++;
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
                            Vorname = Mitglied.Vorname,
                            Geschlecht = Mitglied.Geschlecht
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
                        history.AnzahlAenderung++;
                    }
                    else
                    {
                        history.AnzahlKeineAenderung++;
                        m.State = MitgliedImportStateTypes.NoUpdate;
                    }
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
                    Vorname = mg.Vorname,
                    Geschlecht = mg.Geschlecht
                };

                models.Add(MitgliedModel);
            });

        }
    }
}
