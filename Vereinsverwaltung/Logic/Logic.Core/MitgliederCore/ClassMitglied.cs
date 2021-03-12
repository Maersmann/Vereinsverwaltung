using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Types.MitgliederTypes;

namespace Vereinsverwaltung.Logic.Core.MitgliederCore
{
    public class ClassMitglied
    {
        public void SetMitgliedStatus(Mitglied mitglied)
        {
            if (mitglied.Eintrittsdatum.HasValue)
            {
                if (mitglied.Mitgliedsnr.HasValue)
                {
                    if (mitglied.Eintrittsdatum.Value <= DateTime.Now)
                        mitglied.MitgliedStatus = MitgliedStatus.Aktiv;
                    else
                        mitglied.MitgliedStatus = MitgliedStatus.NichtAktiv;
                }
                else
                    mitglied.MitgliedStatus = MitgliedStatus.Ehemalig;

            }
            else
                mitglied.MitgliedStatus = MitgliedStatus.NichtAktiv;

            if (mitglied.AusgetretenAm.GetValueOrDefault(DateTime.MaxValue) <= DateTime.Now)
                mitglied.MitgliedStatus = MitgliedStatus.Ehemalig;

        }
    }
}
