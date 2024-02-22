

using System.Collections.ObjectModel;
using CryptoChanges.Models;


namespace CryptoChanges.Services.ExchangeManager
{
	public interface IExchangeManager
	{
        Task<List<CoinModel>> GetCoins(ObservableCollection<MarketModel> markets);

    }
}

