using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Demo.FarmersMarket.Core
{
	public class MarketService
	{
		private const string BaseUrl = "http://search.ams.usda.gov/farmersmarkets/v1/data.svc/";
		private const string MarketUrl = BaseUrl + "zipSearch?zip=";
		private const string DetailUrl = BaseUrl + "mktDetail?id=";

		public async Task<MarketResults> GetMarketByZip(string zipCode) {
			var httpClient = new HttpClient ();
			var responseString = await httpClient.GetStringAsync (MarketUrl + zipCode);

			return await ParseJson<MarketResults> (responseString);
		}

		public async Task<MarketDetailsResult> GetMarketDetailById(string id) {
			var httpClient = new HttpClient ();
			var responseString = await httpClient.GetStringAsync (DetailUrl + id);

			return await ParseJson<MarketDetailsResult> (responseString);
		}

		private Task<T> ParseJson<T>(string json) {
			return Task.Factory.StartNew (() => JsonConvert.DeserializeObject<T>(json));
		}
	}
}

