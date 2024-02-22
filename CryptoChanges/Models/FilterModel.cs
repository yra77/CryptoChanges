

namespace CryptoChanges.Models
{
	public class FilterModel
    {
        public float MinVolume { get; set; }
        public float MaxVolume { get; set; }
        public float Spread { get; set; }//%
        public float Sum { get; set; }
        public string Hedging { get; set; }
        public int PeriodUpdate { get; set; }
        public float MaxComission { get; set; }
        /// <summary>
        /// 0 - input,
        /// 1 - output,
        /// 2 - input_output
        /// </summary>
        public int InpOut { get; set; }
        public bool BigRisk { get; set; } = false;
        public bool IsNotify { get; set; } = false;
        public List<CoinModel> BlackCoinsList { get; set; } = new List<CoinModel>();
        public List<CoinModel> BlackNetsList { get; set; } = new List<CoinModel>();
        public List<MarketModel> ExchangeList { get; set; } = new List<MarketModel>();
    }
}