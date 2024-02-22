

namespace CryptoChanges.Models
{
	public class DataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Coin { get; set; }///delete
        public float Volume { get; set; }
        public float Spread { get; set; }
        public float Price { get; set; }
        public string Range { get; set; }
        public int Order { get; set; }
        public float ComissionSpot { get; set; }
        public float ComissionNetwork { get; set; }
        public string Network { get; set; }
        public int Time { get; set; }
        public int Time_of_lives { get; set; }
        public string Futures { get; set; }//Ф'ючерси
        public bool Margin { get; set; }//Маржа
    }
}