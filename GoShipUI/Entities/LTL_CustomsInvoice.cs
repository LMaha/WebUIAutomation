namespace GoShipUI.Entities
{
	public class LTL_CustomsInvoice
	{
		public string DelCompName { get; set; }
		public string DelContactName { get; set; }
		public string DelAddress { get; set; }
		public string DelCityStateZip { get; set; }
		public string DelPhoneNo { get; set; }
		public string DelContactEmail { get; set; }

		public string CarrierName { get; set; }

		public string VendorCompanyName { get; set; }
		public string VendorContactName { get; set; }
		public string VendorAddress { get; set; }
		public string VendorCityStateZip { get; set; }
		public string VendorPhoneNo { get; set; }
		public string VendorEmail { get; set; }

		public string PurchaserCompanyName { get; set; }
		public string PurchaserContactName { get; set; }
		public string PurchaserAddress { get; set; }
		public string PurchaserCityStateZip { get; set; }
		public string PurchaserPhoneNo { get; set; }
		public string PurchaserEmail { get; set; }

		public string CommodityDescription { get; set; }
		public string CustomsBrokerCompanyName { get; set; }
		public string CustomsBrokerPhoneNo { get; set; }
		public string CustomsBrokerEmail { get; set; }
	}
}
