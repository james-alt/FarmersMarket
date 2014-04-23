using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Demo.FarmersMarket.Core
{
	public class MainViewModel
	{
		private bool _isBusy;

		public async Task SearchMarkets(string zipCode)
		{
			if (_isBusy)
				return;

			_isBusy = true;

			var service = new MarketService ();
			var items = await service.GetMarketByZip (zipCode);

			if (Markets == null)
				Markets = new List<Market> ();
			Markets.Clear ();
			Markets = items.Results;


			_isBusy = false;
		}

		public async Task GetMarketDetails(string id)
		{
			if (_isBusy)
				return;

			_isBusy = true;

			var service = new MarketService ();
			var item = await service.GetMarketDetailById (id);

			SelectedMarket = item.MarketDetails;

			_isBusy = false;
		}

		public string ZipCode { get; set; }
		public List<Market> Markets { get; set; }
		public MarketDetail SelectedMarket { get; set; }
	}
}
