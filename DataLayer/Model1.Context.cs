﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SosyalYardimDB : DbContext
    {
        public SosyalYardimDB()
            : base("name=SosyalYardimDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BagisDetayResimTablo> BagisDetayResimTablo { get; set; }
        public virtual DbSet<BagisDetayTablo> BagisDetayTablo { get; set; }
        public virtual DbSet<BagisTablo> BagisTablo { get; set; }
        public virtual DbSet<DepoTablo> DepoTablo { get; set; }
        public virtual DbSet<EsyaTablo> EsyaTablo { get; set; }
        public virtual DbSet<GeriBildirimTablo> GeriBildirimTablo { get; set; }
        public virtual DbSet<IhtiyacSahibiKontrolTablo> IhtiyacSahibiKontrolTablo { get; set; }
        public virtual DbSet<IhtiyacSahibiTablo> IhtiyacSahibiTablo { get; set; }
        public virtual DbSet<IhtiyacSahibiVeKullaniciTablo> IhtiyacSahibiVeKullaniciTablo { get; set; }
        public virtual DbSet<IhtiyacSahibiVerilecekEsyaTablo> IhtiyacSahibiVerilecekEsyaTablo { get; set; }
        public virtual DbSet<IhtiyacSahibiVerilecekMaddiTablo> IhtiyacSahibiVerilecekMaddiTablo { get; set; }
        public virtual DbSet<KullaniciBilgileriTablo> KullaniciBilgileriTablo { get; set; }
        public virtual DbSet<LogTablo> LogTablo { get; set; }
        public virtual DbSet<MesajDetayTablo> MesajDetayTablo { get; set; }
        public virtual DbSet<MesajTablo> MesajTablo { get; set; }
        public virtual DbSet<RotaTablo> RotaTablo { get; set; }
        public virtual DbSet<SehirTablo> SehirTablo { get; set; }
        public virtual DbSet<SubeTablo> SubeTablo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<YetkiTablo> YetkiTablo { get; set; }
    }
}
