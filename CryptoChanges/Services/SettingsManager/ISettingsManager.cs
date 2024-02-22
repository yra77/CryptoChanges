

namespace CryptoChanges.Services.SettingsManager
{
	public interface ISettingsManager
	{
        bool IsDark { get; set; }
        string Language { get; set; }
    }
}

