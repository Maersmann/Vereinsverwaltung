using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Vereinsverwaltung.Data.Infrastructure
{
    public class VersionContextConnection
    {
        public static string GetDatabaseConnectionstring()
        {
            NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Port = 5432,
                Database = "db_v1",
                Username = "postgres",
                Password = "masterkey"
            };

            return npgsqlConnectionStringBuilder.ConnectionString;
        }
    }
}
