using System;
using System.Collections.Generic;
using System.Text;
using Vereinsverwaltung.Data.Infrastructure.Base;
using Vereinsverwaltung.Data.Model.ImportEntitys;

namespace Vereinsverwaltung.Data.Infrastructure.ImportHistory
{
    public class MitgliedImportHistoryRepository : BaseRepository
    {
        public void Speichern(MitgliedImportHistory mitgliedImportHistory)
        {
            if (mitgliedImportHistory.ID == 0)
                repo.MitgliedImportHistory.Add(mitgliedImportHistory);

            repo.SaveChanges();
        }
    }
}
