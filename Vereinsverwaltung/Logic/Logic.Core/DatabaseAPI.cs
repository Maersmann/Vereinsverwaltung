using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vereinsverwaltung.Data.Infrastructure.Base;

namespace Vereinsverwaltung.Logic.Core
{
    public class DatabaseAPI
    {
        public void AktualisereDatenbank()
        {
            Database dbAPI = new Database();
            dbAPI.OpenConnection();
        }
    }
}
