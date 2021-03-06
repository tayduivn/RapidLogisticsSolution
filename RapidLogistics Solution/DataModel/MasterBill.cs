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
    
    public partial class MasterBill
    {
        public MasterBill()
        {
            this.BoxInfoes = new HashSet<BoxInfo>();
            this.ShipmentOuts = new HashSet<ShipmentOut>();
            this.ShipmentOutTemps = new HashSet<ShipmentOutTemp>();
            this.BoxOuts = new HashSet<BoxOut>();
        }
    
        public int Id { get; set; }
        public string MasterAirWayBill { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateArrived { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> DateInt { get; set; }
    
        public virtual ICollection<BoxInfo> BoxInfoes { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ShipmentOut> ShipmentOuts { get; set; }
        public virtual ICollection<ShipmentOutTemp> ShipmentOutTemps { get; set; }
        public virtual ICollection<BoxOut> BoxOuts { get; set; }
    }
}
