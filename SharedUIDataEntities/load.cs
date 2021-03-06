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
    
    public partial class load
    {
        public long load_id { get; set; }
        public long route_id { get; set; }
        public string load_status { get; set; }
        public string market_type { get; set; }
        public string commodity_cd { get; set; }
        public string container_cd { get; set; }
        public long org_id { get; set; }
        public long location_id { get; set; }
        public long person_id { get; set; }
        public string shipper_reference_number { get; set; }
        public string broker_reference_number { get; set; }
        public string inbound_outbound_flg { get; set; }
        public Nullable<System.DateTime> date_closed { get; set; }
        public string feature_code { get; set; }
        public string source_ind { get; set; }
        public Nullable<decimal> mileage { get; set; }
        public int pieces { get; set; }
        public long weight { get; set; }
        public string weight_uom_code { get; set; }
        public Nullable<decimal> target_price { get; set; }
        public Nullable<long> awarded_offer { get; set; }
        public Nullable<decimal> award_price { get; set; }
        public Nullable<System.DateTime> award_date { get; set; }
        public string special_instructions { get; set; }
        public string special_message { get; set; }
        public System.DateTime date_created { get; set; }
        public long created_by { get; set; }
        public System.DateTime date_modified { get; set; }
        public long modified_by { get; set; }
        public Nullable<long> min_weight { get; set; }
        public Nullable<decimal> min_price { get; set; }
        public string rate_type { get; set; }
        public Nullable<decimal> unit_cost { get; set; }
        public Nullable<long> tender_cycle_id { get; set; }
        public string priority { get; set; }
        public string pay_terms { get; set; }
        public Nullable<long> bill_to { get; set; }
        public Nullable<long> one_time_rate_id { get; set; }
        public Nullable<int> out_route_miles { get; set; }
        public string auto_cd_flg { get; set; }
        public string product_type { get; set; }
        public string finalization_status { get; set; }
        public string permit_load { get; set; }
        public string pro_num { get; set; }
        public Nullable<int> travel_time { get; set; }
        public string chk_brn { get; set; }
        public string premium_available { get; set; }
        public string scheduled { get; set; }
        public string dispatcher_schedule_only { get; set; }
        public Nullable<long> truck_weight { get; set; }
        public Nullable<long> gross_weight { get; set; }
        public string barge_num { get; set; }
        public string sc_flag { get; set; }
        public string op_flag { get; set; }
        public string sw_flag { get; set; }
        public string srr_flag { get; set; }
        public string srm_flag { get; set; }
        public string mm_flag { get; set; }
        public string bol_instructions { get; set; }
        public string template_set { get; set; }
        public string release_num { get; set; }
        public string lod_attribute1 { get; set; }
        public string lod_attribute2 { get; set; }
        public string lod_attribute3 { get; set; }
        public string lod_attribute4 { get; set; }
        public string lod_attribute5 { get; set; }
        public string lod_attribute6 { get; set; }
        public Nullable<System.DateTime> original_sched_pickup_date { get; set; }
        public Nullable<System.DateTime> original_sched_delivery_date { get; set; }
        public string carrier_reference_number { get; set; }
        public string shipper_premium_available { get; set; }
        public string after_hours_contact { get; set; }
        public string after_hours_phone { get; set; }
        public string is_ready { get; set; }
        public string reason_code { get; set; }
        public Nullable<System.DateTime> fin_status_date { get; set; }
        public string hfr_flag { get; set; }
        public string delivery_success { get; set; }
        public string freight_paid_by { get; set; }
        public string rate_contact { get; set; }
        public string routing_instructions { get; set; }
        public string customer_tracking_email { get; set; }
        public string notify_award_flag { get; set; }
        public string notify_schedule_flag { get; set; }
        public string notify_gate_flag { get; set; }
        public string notify_conf_pick_up_flag { get; set; }
        public string notify_conf_deliv_flag { get; set; }
        public string driver_name { get; set; }
        public string tractor_id { get; set; }
        public string driver_license { get; set; }
        public string trailer { get; set; }
        public string unit_number { get; set; }
        public string commodity_desc { get; set; }
        public string customer_truck_flag { get; set; }
        public Nullable<long> empty_weight { get; set; }
        public string cust_truck_scac { get; set; }
        public string cust_truck_carr_name { get; set; }
        public string cust_truck_person_name { get; set; }
        public string cust_truck_person_phone { get; set; }
        public string notify_gate_flag_char { get; set; }
        public Nullable<System.DateTime> gl_date { get; set; }
        public string hazmat_flag { get; set; }
        public string radio_active_flag { get; set; }
        public string customer_comments { get; set; }
        public string over_height_flag { get; set; }
        public string over_length_flag { get; set; }
        public string over_width_flag { get; set; }
        public string super_load_flag { get; set; }
        public Nullable<decimal> height { get; set; }
        public Nullable<decimal> length { get; set; }
        public Nullable<decimal> width { get; set; }
        public string over_weight_flag { get; set; }
        public string notify_initial_msg_flag { get; set; }
        public string multi_dock_sched_rqrd { get; set; }
        public string permit_num { get; set; }
        public string radio_active_secure_flag { get; set; }
        public string booked { get; set; }
        public Nullable<float> orig_sched_pickup_date_tz { get; set; }
        public Nullable<float> orig_sched_delivery_date_tz { get; set; }
        public string etd_ovr_flg { get; set; }
        public Nullable<System.DateTime> etd_date { get; set; }
        public Nullable<float> etd_date_tz { get; set; }
        public Nullable<long> awarded_by { get; set; }
        public Nullable<long> origin_region_id { get; set; }
        public Nullable<long> destination_region_id { get; set; }
        public Nullable<decimal> target_rate_min { get; set; }
        public Nullable<decimal> target_rate_max { get; set; }
        public string target_rate_ovr_flg { get; set; }
        public Nullable<long> target_rate_id_min { get; set; }
        public Nullable<long> target_rate_id_max { get; set; }
        public Nullable<long> target_tr_id_min { get; set; }
        public Nullable<long> target_tr_id_max { get; set; }
        public string mileage_type { get; set; }
        public string mileage_version { get; set; }
        public string ship_light { get; set; }
        public Nullable<System.DateTime> frt_bill_recv_date { get; set; }
        public Nullable<long> frt_bill_recv_by { get; set; }
        public string frt_bill_number { get; set; }
        public string finan_no_load_flag { get; set; }
        public string service_level_cd { get; set; }
        public Nullable<long> award_carrier_org_id { get; set; }
        public Nullable<System.DateTime> ship_date { get; set; }
        public string blind_carrier { get; set; }
        public string gl_number { get; set; }
        public string bol { get; set; }
        public string po_num { get; set; }
        public string op_bol { get; set; }
        public string part_num { get; set; }
        public int version { get; set; }
        public string gl_ref_code { get; set; }
        public Nullable<long> award_dedicated_unit_id { get; set; }
        public string frt_bill_recv_flag { get; set; }
        public string pickup_num { get; set; }
        public Nullable<decimal> frt_bill_amount { get; set; }
        public string originating_system { get; set; }
        public string inv_ship_rates_only { get; set; }
        public string inv_carr_rates_only { get; set; }
        public string customs_broker { get; set; }
        public string customs_broker_phone { get; set; }
        public string cust_req_doc_recv_flag { get; set; }
        public string so_number { get; set; }
        public Nullable<long> frt_bill_pay_to_id { get; set; }
        public string inv_approved { get; set; }
        public string volume_quote_id { get; set; }
        public Nullable<long> saved_quote_id { get; set; }
        public Nullable<long> assignee_person_id { get; set; }
        public Nullable<System.DateTime> assigned_date { get; set; }
        public string carrier_sales { get; set; }
        public Nullable<long> driver_id { get; set; }
    }
}
