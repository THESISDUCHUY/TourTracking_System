//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitoringTourSystem.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class warning_receiver
    {
        public int id { get; set; }
        public int warner_id { get; set; }
        public int receiver_id { get; set; }
        public string status { get; set; }
    }
}
