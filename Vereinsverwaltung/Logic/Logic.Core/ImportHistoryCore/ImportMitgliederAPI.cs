using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.ImportHistory;
using Vereinsverwaltung.Data.Model.ImportEntitys;

namespace Vereinsverwaltung.Logic.Core.ImportHistoryCore
{
    public class ImportMitgliederAPI
    {
        public void Speichern(MitgliedImportHistory mitgliedImportHistory)
        {
            new MitgliedImportHistoryRepository().Speichern(mitgliedImportHistory);
        }
    }
}
