//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.ShipmentOuts = new HashSet<ShipmentOut>();
            this.ShipmentWaitToConfirms = new HashSet<ShipmentWaitToConfirm>();
            this.BoxInfoes = new HashSet<BoxInfo>();
            this.ShipmentInfors = new HashSet<ShipmentInfor>();
            this.MasterBills = new HashSet<MasterBill>();
            this.ShipmentInforTemps = new HashSet<ShipmentInforTemp>();
            this.ShipmentOutTemps = new HashSet<ShipmentOutTemp>();
            this.BoxOuts = new HashSet<BoxOut>();
        }
    
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }
        public string Role { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> WarehouseId { get; set; }
    
        public virtual ICollection<ShipmentOut> ShipmentOuts { get; set; }
        public virtual ICollection<ShipmentWaitToConfirm> ShipmentWaitToConfirms { get; set; }
        public virtual ICollection<BoxInfo> BoxInfoes { get; set; }
        public virtual ICollection<ShipmentInfor> ShipmentInfors { get; set; }
        public virtual ICollection<MasterBill> MasterBills { get; set; }
        public virtual ICollection<ShipmentInforTemp> ShipmentInforTemps { get; set; }
        public virtual ICollection<ShipmentOutTemp> ShipmentOutTemps { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<BoxOut> BoxOuts { get; set; }
    }
}
