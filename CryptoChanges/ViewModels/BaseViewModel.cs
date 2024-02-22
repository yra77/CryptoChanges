

using CryptoChanges.Services.ExchangeManager;
using CryptoChanges.Services.SettingsManager;
using LocalizationResourceManager.Maui;


namespace CryptoChanges.ViewModels
{
	public class BaseViewModel : BindableBase
    {


        protected bool _isPressed;

        protected ISettingsManager _settingsManager;
        protected IPageDialogService _dialogService;
        protected IExchangeManager _exchangeManager;
        protected ISemanticScreenReader _screenReader;
        protected INavigationService _navigationService;
        protected ILocalizationResourceManager _localizationResourceManager;


        public BaseViewModel()
		{
		}
	}
}

