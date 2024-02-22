

using CryptoChanges.Views;
using CryptoChanges.ViewModels;
using CryptoChanges.Services.SettingsManager;
using CryptoChanges.Services.ExchangeManager;


namespace CryptoChanges
{
	public static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .OnInitialized(OnInitialized)
                    .OnAppStart("NavigationPage/MainPage");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("MainPage")
                             .RegisterInstance(SemanticScreenReader.Default);

            //Services
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>()
                             .RegisterSingleton<IExchangeManager, ExchangeManager>();
        }

        private static void OnInitialized(IContainerProvider container)
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) => container.Resolve(type));
        }
    }
}