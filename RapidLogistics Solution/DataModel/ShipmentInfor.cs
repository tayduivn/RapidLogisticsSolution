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
    
    public partial class ShipmentInfor
    {
        public int Id { get; set; }
        public string ShipmentId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string TelReceiver { get; set; }
        public Nullable<double> TotalValue { get; set; }
        public string Descrition { get; set; }
        public Nullable<int> BoxId { get; set; }
        public string Status { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> WarehouseId { get; set; }
    
        public virtual BoxInfo BoxInfo { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
