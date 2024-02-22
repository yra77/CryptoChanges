

using CryptoChanges.Models;
using CryptoChanges.Services.Exchanges;
using CryptoChanges.Services.ExchangeManager;
using CryptoChanges.Services.SettingsManager;

using LocalizationResourceManager.Maui;

using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;


namespace CryptoChanges.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INavigatedAware
    {


        private List<CoinModel> _inpOutList;
        private List<CoinModel> _listLang;
        private List<CoinModel> _coinsList;
        private List<CoinModel> _netList;
        string textSelect = null;
        private int _whatList = 0;//0-inpoutput, 1-language, 2 - coins0, 3 - coins1
        private ICollection<ResourceDictionary> _mergedDictionaries = Application.Current.Resources.MergedDictionaries;


        public MainPageViewModel(PageDialogService dialogService,
                                 ISettingsManager settingsManager,
                                 IExchangeManager exchangeManager,
                                 ISemanticScreenReader screenReader,
                                 INavigationService navigationService,
                                 ILocalizationResourceManager localizationResourceManager)
        {
            _screenReader = screenReader;
            _dialogService = dialogService;
            _settingsManager = settingsManager;
            _exchangeManager = exchangeManager;
            _navigationService = navigationService;
            _localizationResourceManager = localizationResourceManager;

            _settingsManager.Language ??= "uk-UA";

            Markets = new()
            {
                new MarketModel(){Id = 0, Name="Binance", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 1, Name="CoinEx", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 2, Name="KuCoin", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 0, Name="Bitfinex", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 1, Name="Bybit", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 2, Name="Huobi", Link="ppppp", IsChecking=false},
                new MarketModel(){Id = 0, Name="Bittrex", Link="ppppp", IsChecking=false},
            };

            _listLang = new()
            {
                new CoinModel{ Name = "Українська", IsChecked=false },
               new CoinModel{ Name = "English", IsChecked=false }
            };

            _netList = new()
            {
               new CoinModel{ Name = "Btc", IsChecked=false },
               new CoinModel{ Name = "ERC20", IsChecked=false }
            };

            _coinsList = new();

            IsDarkTheme = false;
            IsComboVisible = false;
            IsComboSubVisible = false;
            SelectLanguage = _listLang[0].Name;

            Words();
            ListResult = new List<DataModel>();
            Filter = new FilterModel();
        }


        #region property

        private ObservableCollection<MarketModel> _markets;
        public ObservableCollection<MarketModel> Markets
        {
            get => _markets;
            set => SetProperty(ref _markets, value);
        }


        private List<DataModel> _listResult;
        public List<DataModel> ListResult
        {
            get => _listResult;
            set => SetProperty(ref _listResult, value);
        }


        private ObservableCollection<CoinModel> _listInpOut;
        public ObservableCollection<CoinModel> ListInpOut
        {
            get => _listInpOut;
            set => SetProperty(ref _listInpOut, value);
        }


        private FilterModel _filter;
        public FilterModel Filter
        {
            get => _filter;
            set => SetProperty(ref _filter, value);
        }


        private string _selectLanguage;
        public string SelectLanguage
        {
            get => _selectLanguage;
            set => SetProperty(ref _selectLanguage, value);
        }


        private string _selectListInpOut0;
        public string SelectListInpOut0
        {
            get => _selectListInpOut0;
            set => SetProperty(ref _selectListInpOut0, value);
        }


        private string _selectListCoins0;
        public string SelectListCoins0
        {
            get => _selectListCoins0;
            set => SetProperty(ref _selectListCoins0, value);
        }


        private string _selectListCoins1;
        public string SelectListCoins1
        {
            get => _selectListCoins1;
            set => SetProperty(ref _selectListCoins1, value);
        }


        private bool _isValidInput;
        public bool IsValidInput
        {
            get => _isValidInput;
            set => SetProperty(ref _isValidInput, value);
        }


        private bool _isComboVisible;
        public bool IsComboVisible
        {
            get => _isComboVisible;
            set => SetProperty(ref _isComboVisible, value);
        }


        private bool _isComboSubVisible;
        public bool IsComboSubVisible
        {
            get => _isComboSubVisible;
            set => SetProperty(ref _isComboSubVisible, value);
        }


        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set => SetProperty(ref _isDarkTheme, value);
        }

        public DelegateCommand<string> SelectCombo => new DelegateCommand<string>(Select_Combo);
        public DelegateCommand<string> ComboBoxBtn => new DelegateCommand<string>(ComboBox_Click);
        public DelegateCommand EntryBoxApply => new DelegateCommand(EntryBox_Apply);

        #endregion


        private void EntryBox_Apply()
        {
            var listMarket = Markets.Where(a => a.IsChecking == true).ToList();
            if (listMarket.Count > 0) Filter.ExchangeList = listMarket;
            else _dialogService.DisplayAlertAsync("Info", "Select an exchange", "Ok");

        }

        private async Task GetCoins()
        {
           var res = await _exchangeManager.GetCoins(_markets);
            _coinsList = res == null ? new() : new(res);
            //System.Diagnostics.Debug.WriteLine("Ppppppppp " + _coinsList.Count);
        }

        private void Select_Combo(string obj)
        {
            if (obj != "done")
            {
                textSelect = obj;
                if (_whatList == 0)
                {
                    SelectListInpOut0 = obj;
                    Filter.InpOut = _listInpOut.First(a => a.Name == obj).Number;
                    IsComboVisible = false;
                }
                else if (_whatList == 1) ChangeLanguage(obj);
                //  else if (_whatList == 2) SelectListCoins0 = obj;
                //  else if (_whatList == 3) SelectListCoins1 = obj;
            }
            else
            {
                IsComboVisible = false;
                if (_whatList == 2)
                {
                    _coinsList = new List<CoinModel>(ListInpOut);
                    Filter.BlackCoinsList = new(_coinsList.Where(a => a.IsChecked == true));
                }
                else if (_whatList == 3)
                {
                    _netList = new List<CoinModel>(ListInpOut);
                    Filter.BlackNetsList = new(_netList.Where(a => a.IsChecked == true));
                }
            }
        }

        private void ChangeLanguage(string obj)
        {
            SelectLanguage = obj;

            if (SelectLanguage == "English")
            {
                _localizationResourceManager.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                _settingsManager.Language = "en";
            }
            if (SelectLanguage == "Українська")
            {
                _localizationResourceManager.CurrentCulture = CultureInfo.CreateSpecificCulture("uk-UA");
                _settingsManager.Language = "uk-UA";
            }

            IsComboVisible = false;
            Words();
        }

        private async void ComboBox_Click(string obj)
        {
            if (obj == "Language")
            {
                _whatList = 1;
                IsComboSubVisible = false;
                ListInpOut = new ObservableCollection<CoinModel>(_listLang);
            }
            else if (obj == "InpOutList")
            {
                _whatList = 0;
                IsComboSubVisible = false;
                ListInpOut = new ObservableCollection<CoinModel>(_inpOutList);
            }
            if (obj == "Coins0")
            {
                _whatList = 2;
                IsComboSubVisible = true;
                if(_coinsList == null || _coinsList.Count == 0) await GetCoins();
                ListInpOut = new ObservableCollection<CoinModel>(_coinsList);
            }
            else if (obj == "Coins1")
            {
                _whatList = 3;
                IsComboSubVisible = true;
                ListInpOut = new ObservableCollection<CoinModel>(_netList);
            }
            IsComboVisible = true;
        }

        private void ChangeTheme()
        {
            if (_mergedDictionaries != null)
            {
                _mergedDictionaries.Clear();
                if (IsDarkTheme)
                {
                    _mergedDictionaries.Add(new Resources.Styles.Dark());
                    _settingsManager.IsDark = true;
                }
                else
                {
                    _mergedDictionaries.Add(new Resources.Styles.Light());
                    _settingsManager.IsDark = false;
                }
            }
        }

        private void Words()
        {
            SelectListInpOut0 = _localizationResourceManager["Availability_of_transfers"];// "Доступність перевода";
            SelectListCoins0 = _localizationResourceManager["Blacklist_of_coins"];// "Чорний список монет";
            SelectListCoins1 = _localizationResourceManager["Black_list_of_networks"];// "Чорний список мереж";

            _inpOutList = new()
            {
              new CoinModel{ Name = _localizationResourceManager["Input_Output"], Number = 0 },
               new CoinModel{ Name = _localizationResourceManager["Input"], Number = 1 },
               new CoinModel{ Name = _localizationResourceManager["Output"], Number = 2 }
            };
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "IsDarkTheme":
                    ChangeTheme();
                    break;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //theme
            IsDarkTheme = _settingsManager.IsDark;
            ChangeTheme();
            //lang
            SelectLanguage = (_settingsManager.Language != null
                             && _settingsManager.Language == "en")
                             ? _listLang[1].Name : _listLang[0].Name;
            _localizationResourceManager.CurrentCulture = CultureInfo.CreateSpecificCulture(_settingsManager.Language);
        }
    }
}