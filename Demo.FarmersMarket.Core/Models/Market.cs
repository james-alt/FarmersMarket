using System;
using System.Collections.Generic;

namespace Demo.FarmersMarket.Core
{
	public class Market
	{
		public string Id { get; set; }
		public string MarketName { get; set; }
	}

	public class MarketResults
	{
		public List<Market> Results;
	}
}