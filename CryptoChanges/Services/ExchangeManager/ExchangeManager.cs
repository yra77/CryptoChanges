

using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using CryptoChanges.Models;
using CryptoChanges.Services.Exchanges;
using Newtonsoft.Json;
using ThreadNetwork;


namespace CryptoChanges.Services.ExchangeManager
{
	public class ExchangeManager : IExchangeManager
	{

        readonly Dictionary<string, IExchange> _exchanges = new()
        {
            { "Binance", new BinanceExchange() },
            { "Bybit", new BybitExchange() }
        };


        public ExchangeManager()
		{
		}


        //public async Task<List<string>> GetCoins(ObservableCollection<MarketModel> markets)
        //{
        //    List<string> listCoins = new List<string>();
        //    foreach (var item in markets)
        //    {
        //        if (item.IsChecking)
        //        {
        //           var temp = await _exchanges[item.Name].GetPrices();
        //            System.Diagnostics.Debug.WriteLine("Ppppppppp " + temp.Count);
        //            listCoins = (listCoins.Count == 0)
        //                ? temp
        //                : listCoins.Concat(temp.Except(listCoins)).ToList();
        //        }
        //    }
        //    return listCoins;
        //}

        public async Task<List<CoinModel>> GetCoins(ObservableCollection<MarketModel> markets)
        {
           // List<string> listCoins;
            try
            {
                HttpClient _client = new();
                var request = new HttpRequestMessage(HttpMethod.Get, Constants.HttpPath.CoinsPath);
                _client.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.HttpPath.UserAgent);
                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var res = JsonConvert.DeserializeObject<List<CoinModel>>(await response.Content.ReadAsStringAsync());
                        // JObject.Parse(res)["data"].ToString())
                        //image replace large to thumb
                        //list[i].image.Replace("large", "thumb");
                       // listCoins = new List<string>(res.Select(a => a.Symbol).ToList());
                        //System.Diagnostics.Debug.WriteLine(listCoins.Count);
                        return res;
                    }
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error {e.Message}");
            }
            return null;
        }
    }
}