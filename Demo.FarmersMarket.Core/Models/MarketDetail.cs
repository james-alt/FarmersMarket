using System;

namespace Demo.FarmersMarket.Core
{
	public class MarketDetail
	{
		public string Address { get; set; }
		public string GoogleLink { get; set; }
		public string Products { get; set; }
		public string Schedule { get; set; }
	}

	public class MarketDetailsResult
	{
		public MarketDetail MarketDetails;
	}
}

