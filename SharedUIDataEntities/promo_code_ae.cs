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
    
    public partial class promo_code_ae
    {
        public long promo_code_ae_id { get; set; }
        public Nullable<long> person_id { get; set; }
        public string promo_code { get; set; }
        public Nullable<int> percentage { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<long> created_by { get; set; }
        public Nullable<System.DateTime> date_modified { get; set; }
        public Nullable<long> modified_by { get; set; }
        public Nullable<long> terms_and_conditions_version { get; set; }
    
        public virtual user user { get; set; }
    }
}