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
    
    public partial class ShipmentOut
    {
        public string ShipmentId { get; set; }
        public Nullable<int> BoxIdRef { get; set; }
        public Nullable<int> MasterBillId { get; set; }
        public Nullable<System.DateTime> DateOut { get; set; }
        public string BoxIdString { get; set; }
        public string MasterBillIdString { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> WarehouseId { get; set; }
        public Nullable<bool> IsSyncOms { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> DateInt { get; set; }
        public Nullable<double> Weight { get; set; }
        public int Sequence { get; set; }
        public string DeclarationNo { get; set; }
        public string DateOfCompletion { get; set; }
        public string ContactName { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> TotalValue { get; set; }
        public string Original { get; set; }
        public string Destination { get; set; }
        public string Country { get; set; }
        public string CompanyName { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual MasterBill MasterBill { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual BoxOut BoxOut { get; set; }
        public virtual BoxOut BoxOut1 { get; set; }
        public virtual BoxOut BoxOut2 { get; set; }
        public virtual BoxOut BoxOut3 { get; set; }
    }
}
