﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RapidSolutionEntities : DbContext
    {
        public RapidSolutionEntities()
            : base("name=RapidSolutionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ShipmentOut> ShipmentOuts { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<ShipmentWaitToConfirm> ShipmentWaitToConfirms { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<BoxInfo> BoxInfoes { get; set; }
        public DbSet<ShipmentInfor> ShipmentInfors { get; set; }
        public DbSet<MasterBill> MasterBills { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ShipmentInforTemp> ShipmentInforTemps { get; set; }
        public DbSet<ShipmentOutTemp> ShipmentOutTemps { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<BoxOut> BoxOuts { get; set; }
    }
}
