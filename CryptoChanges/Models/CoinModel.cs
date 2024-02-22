

namespace CryptoChanges.Models
{
	public class CoinModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Image { get; set; }
        public bool IsChecked { get; set; } = false;
       // public List<DataModel> ListExchange { get; set; } = new();
    }
}