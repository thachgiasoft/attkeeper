//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ObjectManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Machine
    {
        public int MachineId { get; set; }
        public Nullable<int> EnrolleeId { get; set; }
        public Nullable<int> MachineInsId { get; set; }
        public Nullable<int> MachineNo { get; set; }
        public Nullable<int> EnrolleeNo { get; set; }
        public Nullable<int> IYear { get; set; }
        public Nullable<int> IMonth { get; set; }
        public Nullable<int> IDay { get; set; }
        public Nullable<int> IHour { get; set; }
        public Nullable<int> IMin { get; set; }
        public Nullable<int> ISecond { get; set; }
        public Nullable<int> InOutCode { get; set; }
        public Nullable<int> VerifyCode { get; set; }
    
        public virtual Enrollee Enrollee { get; set; }
        public virtual MachineInstance MachineInstance { get; set; }
    }
}
