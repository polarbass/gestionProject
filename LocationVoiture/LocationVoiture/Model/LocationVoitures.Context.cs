﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocationVoiture.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class locationvoitureEntities : DbContext
    {
        public locationvoitureEntities()
            : base("name=locationvoitureEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<administrateur> administrateurs { get; set; }
        public DbSet<client> clients { get; set; }
        public DbSet<constats_dommages> constats_dommages { get; set; }
        public DbSet<employe> employes { get; set; }
        public DbSet<fabriquant> fabriquants { get; set; }
        public DbSet<location> locations { get; set; }
        public DbSet<modele> modeles { get; set; }
        public DbSet<reservation> reservations { get; set; }
        public DbSet<succursale> succursales { get; set; }
        public DbSet<type> types { get; set; }
        public DbSet<vehicule> vehicules { get; set; }
    }
}