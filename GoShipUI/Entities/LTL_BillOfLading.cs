namespace GoShipUI.Entities
{
	public class LTL_BillOfLading
	{
		public string PckCompanyName { get; set; }
		public string PckContactName { get; set; }
		public string PckAddress { get; set; }
		public string PckPhoneNo { get; set; }
		public string PckContactEmail { get; set; }
		public string PckInstructions { get; set; }
		public string DelCompanyName { get; set; }
		public string DelContactName { get; set; }
		public string DelAddress { get; set; }
		public string DelFullCityStateZip { get; set; }
		public string DelPhoneNo { get; set; }
		public string DelContactEmail { get; set; }
		public string DelInstructions { get; set; }
		public string CommodityDescription { get; set; }
		public string PckWindow { get; set; }
		public string DelWindow { get; set; }
	}
}