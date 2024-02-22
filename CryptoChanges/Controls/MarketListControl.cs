

using System.Collections.ObjectModel;
using CryptoChanges.Models;


namespace CryptoChanges.Controls
{
	public class MarketListControl : StackLayout
	{


		private List<CheckBox> _checkBoxList;


		public MarketListControl()
		{
			Orientation = StackOrientation.Horizontal;
			HorizontalOptions = new LayoutOptions(LayoutAlignment.Fill, true);
			VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);

            ItemsList = new();
            MarketListControl_Loaded();
        }


        #region Property

        public static readonly BindableProperty ItemsListProperty =
                                               BindableProperty.Create("ItemsList",
                                               typeof(ObservableCollection<MarketModel>),
                                               typeof(MarketListControl),
                                               defaultBindingMode:BindingMode.TwoWay);
        public ObservableCollection<MarketModel> ItemsList
        {
            get { return GetValue(ItemsListProperty) as ObservableCollection<MarketModel>; }
            set { SetValue(ItemsListProperty, value); }
        }


        public static readonly BindableProperty Background_ColorProperty =
         BindableProperty.Create(nameof(Background_Color), typeof(Color), typeof(MarketListControl), null);
                                             //propertyChanged: (bindableObject, oldValue, newValue) =>
                                             //{
                                             //    if (newValue is Color color && bindableObject is MarketListControl view)
                                             //    {
                                             //        if (view._btn != null) view._btn.BackgroundColor = color;
                                             //    }
                                             //});
        public Color Background_Color
        {
            get => (Color)GetValue(Background_ColorProperty);
            set => SetValue(Background_ColorProperty, value);
        }


        public static readonly BindableProperty Text_ColorProperty =
            BindableProperty.Create(nameof(Text_Color), typeof(Color), typeof(MarketListControl), null);
                                              //propertyChanged: (bindableObject, oldValue, newValue) =>
                                              //{
                                              //    if (newValue is Color color && bindableObject is MarketListControl view)
                                              //    {
                                              //        if (view._btn != null) view._btn.TextColor = color;
                                              //    }
                                              //});
        public Color Text_Color
        {
            get => (Color)GetValue(Text_ColorProperty);
            set => SetValue(Text_ColorProperty, value);
        }

        #endregion


        private async void MarketListControl_Loaded()
        {
            _checkBoxList = new();
            await Task.Delay(250);
            Print();
        }

        private void Print()
        {
            var cb = CreateBox("Усi");
            cb.CheckedChanged += All_CheckedChanged;

            for (int i = 0; i < ItemsList.Count; i++)
            {
                var res = CreateBox(ItemsList[i].Name);
                res.CheckedChanged += Res_CheckedChanged;
                res.AutomationId = ItemsList[i].Name;//identity checkbox
               _checkBoxList.Add(res);
            }
		}

        private void Res_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
               // System.Console.WriteLine("ppppppppp " + checkBox.AutomationId);
                for (int i = 0; i < ItemsList.Count; i++)
                {
                    if (ItemsList[i].Name == checkBox.AutomationId)
                    {
                        ItemsList[i].IsChecking = e.Value;
                    }
                }
            }
        }

        private void All_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            for (int i = 0; i < ItemsList.Count; i++)
            {
                ItemsList[i].IsChecking = (e.Value) ? true : false;
                _checkBoxList[i].IsChecked = (e.Value) ? true : false;
            }
        }

        private CheckBox CreateBox(string text)
        {
            StackLayout sl = new()
            {
                BackgroundColor = Background_Color,
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false),
                Margin = new Thickness(0,0,10,0)
            };

            CheckBox cb = new CheckBox()
            {
                IsChecked = false,
                Color = Text_Color,
                VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false)
            };

            Label label = new()
            {
                Text = text,
                TextColor = Text_Color,
                VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false)
            };

            sl.Add(cb);
            sl.Add(label);
            Add(sl);

            return cb;
        }
    }
}