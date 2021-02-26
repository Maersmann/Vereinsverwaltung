using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;

namespace Vereinsverwaltung.Data.Infrastructure
{
    public class Repository : DbContext
    {
        public DbSet<Mitglied> Mitglieder { get; set; }

        public Repository() : base() { this.Database.Migrate(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(VersionContextConnection.GetDatabaseConnectionstring());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
