//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SharedUIDataEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class international_holidays
    {
        public int holiday_id { get; set; }
        public string country_code { get; set; }
        public string state_code { get; set; }
        public string city { get; set; }
        public int holiday_year { get; set; }
        public System.DateTime holiday_date { get; set; }
        public System.DateTime observed_date { get; set; }
        public string holiday_name { get; set; }
        public bool active { get; set; }
        public bool observed { get; set; }
    }
}
