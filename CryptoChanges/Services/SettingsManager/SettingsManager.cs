

namespace CryptoChanges.Services.SettingsManager
{
	public class SettingsManager : ISettingsManager
	{
        public bool IsDark
        {
            get => Preferences.Get(nameof(IsDark), false);
            set => Preferences.Set(nameof(IsDark), value);
        }

        public string Language
        {
            get => Preferences.Get(nameof(Language), null);
            set => Preferences.Set(nameof(Language), value);
        }
    }
}

