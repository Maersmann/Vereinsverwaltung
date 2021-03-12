﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vereinsverwaltung.Data.Entitys.MitgliederEntitys;
using Vereinsverwaltung.Data.Model.SchluesselEntitys;

namespace Vereinsverwaltung.Data.Infrastructure
{
    public class Repository : DbContext
    {
        public DbSet<Mitglied> Mitglieder { get; set; }
        public DbSet<Schluessel> Schluessels { get; set; }
        public DbSet<Schluesselbesitzer> Schluesselbesitzer { get; set; }
        public DbSet<Schluesselzuteilung> Schluesselverteilung { get; set; }
        public DbSet<SchluesselzuteilungHistory> SchluesselzuteilungHistory { get; set; }

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
