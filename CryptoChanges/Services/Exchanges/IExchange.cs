

using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;


namespace CryptoChanges.Services.Exchanges
{
	public interface IExchange
	{
        Task<decimal> GetPrice(string symbol);
        Task<List<string>> GetPrices();
       // Task<IEnumerable<OpenOrder>> GetOpenOrders();
        //Task<Dictionary<string, decimal>> GetBalances();
        //Task<WebCallResult> CancelOrder(string symbol, string id);
        //Task<WebCallResult<string>> PlaceOrder(string symbol, string side, string type, decimal quantity, decimal? price);
        //Task<UpdateSubscription> SubscribePrice(string symbol, Action<decimal> handler);
    }
}