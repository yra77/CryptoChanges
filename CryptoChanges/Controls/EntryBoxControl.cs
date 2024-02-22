

using Microsoft.Maui.Controls.Shapes;
using System.Text.RegularExpressions;


namespace CryptoChanges.Controls
{
	public class EntryBoxControl : Grid
    {


        private Entry _entry;
        private Label _label;
        private Border _border;
        private int _entrySize = 16;
        private int _labelSize = 12;
        private int _errorText = 10;
        private bool _isValid;


        public EntryBoxControl() : base()
        {
            VerticalOptions = LayoutOptions.Center;
            MyEntry_Loaded();
        }


        #region Property

        public static readonly BindableProperty SelectedTypeProperty =
     BindableProperty.Create("SelectedType",
                             typeof(string),
                             typeof(EntryBoxControl));
        public string SelectedType
        {
            get { return GetValue(SelectedTypeProperty) as string; }
            set { SetValue(SelectedTypeProperty, value); }
        }


        public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create("IsValid",
                                typeof(bool),
                                typeof(EntryBoxControl),
                               defaultBindingMode: BindingMode.TwoWay);
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }


        public static readonly BindableProperty LabelNameProperty =
            BindableProperty.Create(nameof(LabelName), typeof(string), typeof(EntryBoxControl), null,
                 propertyChanged: (bindableObject, oldValue, newValue) =>
                 {
                     if (newValue is string text && bindableObject is EntryBoxControl view)
                     {
                         if (view._label != null) view._label.Text = text;
                     }
                 });
        public string LabelName
        {
            get => (string)GetValue(LabelNameProperty);
            set => SetValue(LabelNameProperty, value);
        }


        public static readonly BindableProperty LabelColorProperty =
            BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(EntryBoxControl), null);
        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }


        public static readonly BindableProperty InputTextProperty =
                               BindableProperty.Create(nameof(InputText), typeof(string),
                                   typeof(EntryBoxControl), null,
                               defaultBindingMode: BindingMode.TwoWay);
        public string InputText
        {
            get => (string)GetValue(InputTextProperty);
            set => SetValue(InputTextProperty, value);
        }


        public static readonly BindableProperty ErrorTextProperty =
                               BindableProperty.Create(nameof(ErrorText), typeof(string),
                                   typeof(EntryBoxControl), null);
        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }


        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryBoxControl), null);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }


        public static readonly BindableProperty Background_ColorProperty =
            BindableProperty.Create(nameof(Background_Color), typeof(Color), typeof(EntryBoxControl), null);
        public Color Background_Color
        {
            get => (Color)GetValue(Background_ColorProperty);
            set => SetValue(Background_ColorProperty, value);
        }


        public static readonly BindableProperty Text_ColorProperty =
            BindableProperty.Create(nameof(Text_Color), typeof(Color), typeof(EntryBoxControl), null);
        public Color Text_Color
        {
            get => (Color)GetValue(Text_ColorProperty);
            set => SetValue(Text_ColorProperty, value);
        }


        public static readonly BindableProperty BorderThinckProperty =
            BindableProperty.Create(nameof(BorderThinck), typeof(int), typeof(EntryBoxControl), 1);
        public int BorderThinck
        {
            get => (int)GetValue(BorderThinckProperty);
            set => SetValue(BorderThinckProperty, value);
        }


        public static readonly BindableProperty RoundProperty =
            BindableProperty.Create(nameof(Round), typeof(double), typeof(EntryBoxControl), null);
        public double Round
        {
            get => (double)GetValue(RoundProperty);
            set => SetValue(RoundProperty, value);
        }

        #endregion


        private async void MyEntry_Loaded()
        {
            await Task.Delay(250);
            _label = new Label()
            {
                TranslationX = 15,
                TranslationY = 13,
                FontSize = _labelSize,
            };

            _entry = new Entry()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = _entrySize
            };

            _border = new Border()
            {
                BackgroundColor = Colors.Transparent,
                HeightRequest = 45,
                Padding = new Thickness(15, 0, 0, 0),
            };

            _border.Content = _entry;
             _entry.Focused += Entry_Focused;
             _entry.Unfocused += Entry_UnFocused;
             _entry.TextChanged += Entry_TextChanged;

            Add(_label);
            Add(_border);
            _border.Stroke = BorderColor;
            _border.StrokeThickness = BorderThinck;
            _border.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(Round)
            };

            _label.Text = LabelName;
            _label.TextColor = LabelColor;

            _entry.Text = InputText;
            _entry.BackgroundColor = Background_Color;
            _entry.TextColor = Text_Color;

            switch (SelectedType)
            {
                case "textEN":
                    _entry.Keyboard = Keyboard.Text;
                    _entry.MaxLength = 20;
                    break;

                case "textUA":
                    _entry.Keyboard = Keyboard.Text;
                    _entry.MaxLength = 20;
                    break;

                case "email":
                    _entry.Keyboard = Keyboard.Email;
                    _entry.MaxLength = 40;
                    break;

                case "password":
                    _entry.Keyboard = Keyboard.Text;
                    _entry.MaxLength = 10;
                    break;

                case "digit":
                    _entry.Keyboard = Keyboard.Numeric;
                    _entry.MaxLength = 10;
                    break;

                case "mixed":
                    _entry.Keyboard = Keyboard.Text;
                    _entry.MaxLength = 40;
                    break;

                default:
                    break;
            }
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            _label.TranslationX = 0;
            _label.TranslationY = -23;
        }

        private void Entry_UnFocused(object sender, FocusEventArgs e)
        {
            if (_entry.Text == null || _entry.Text.Length == 0)
            {
                _label.TranslationX = 15;
                _label.TranslationY = 13;
                _label.Text = LabelName;
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_entry.Text == null || _entry.Text == "" || _entry.Text.Length == 0)
            {
                _label.TranslationY = 13;
                _label.TranslationX = 15;
                _label.ScaleX = 1f;
                _label.ScaleY = 1f;
                _label.Text = LabelName;
                _label.FontSize = _labelSize;
            }
            else
            {
                _label.TranslationY = -23;
                _label.TranslationX = 0;
                _label.ScaleX = 1f;
                _label.ScaleY = 1f;
            }

            if (_entry.Text != null && _entry.Text.Length > 0)
            {
                TextChanged();
                CheckData();
                IsValid = _isValid;
                InputText = _entry.Text;
            }
            else
            {
                // _label.FontSize = _errorText;
                _border.Stroke = BorderColor;
                _label.FontSize = _labelSize;
            }
        }

        private bool CheckData()
        {
            var res = false;
            _isValid = false;
            IsValid = _isValid;

            switch (SelectedType)
            {
                case "textEN":
                    res = Text_EN_Verify();
                    if (_entry.Text.Length > 2)
                    {
                        _isValid = IsValidTextEN();
                    }
                    if (!_isValid)
                    {
                        _label.FontSize = _errorText;
                        _border.Stroke = Colors.Red;
                        _label.Text = ErrorText;// "Only english syblols";
                    }
                    else
                    {
                        _label.FontSize = _labelSize;
                        _border.Stroke = Colors.DarkGreen;
                        _label.Text = LabelName;
                    }
                    break;

                case "textUA":
                    res = Text_UA_Verify();
                    if (_entry.Text.Length > 2)
                    {
                        _isValid = IsValidTextUA();
                    }
                    if (!_isValid)
                    {
                        _label.FontSize = _errorText;
                        _border.Stroke = Colors.Red;
                        _label.Text = ErrorText;//"Тільки українські літери";
                    }
                    else
                    {
                        _label.FontSize = _labelSize;
                        _border.Stroke = Colors.DarkGreen;
                        _label.Text = LabelName;
                    }
                    break;

                case "email":
                    res = EmailVerify();
                    _isValid = IsValidEmail(_entry.Text);
                    break;

                case "digit":
                    res = DigitVerify();
                    if (_entry.Text.Length > 9)
                    {
                        _isValid = IsValidDigit();
                    }
                    if (!_isValid)
                    {
                        _label.FontSize = _errorText;
                        _border.Stroke = Colors.Red;
                        _label.Text = ErrorText;//"10 цифр 0681112233";
                    }
                    else
                    {
                        _label.FontSize = _labelSize;
                        _border.Stroke = Colors.DarkGreen;
                        _label.Text = LabelName;
                    }
                    break;

                case "password":
                    res = PasswordCheckin();
                    if (_entry.Text.Length > 7) _isValid = IsValidPassword(_entry.Text);
                    if (!_isValid)
                    {
                        _label.FontSize = _errorText;
                        _border.Stroke = Colors.Red;
                        _label.Text = ErrorText;//"8-10 символив, перша велика літера, у кінці одну цифру";
                    }
                    else
                    {
                        _label.FontSize = _labelSize;
                        _border.Stroke = Colors.DarkGreen;
                        _label.Text = LabelName;
                    }
                    break;

                case "mixed":
                    res = Mixed();
                    if (_entry.Text.Length > 0)
                    {
                        _isValid = IsValidMixed();
                    }
                    break;
            }
            IsValid = _isValid;
            return res;
        }

        private void TextChanged()
        {

            var res = true;

            if (_entry.Text != null && _entry.Text.Length > 0)
            {
                res = CheckData();

                // _entry.SelectionLength = _entry.Text.Length;

                if (res || _isValid)
                {
                    _border.Stroke = Colors.DarkGreen;
                }
                if (!res || !_isValid)
                {
                    _border.Stroke = Colors.Red;
                }
            }
        }

        private bool Text_EN_Verify()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if ((temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z'))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidTextEN()
        {
            return Regex.IsMatch(_entry.Text, @"^[a-zA-Z]+$");
        }

        private bool Text_UA_Verify()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if ((temp[i] >= 'А' && temp[i] <= 'Щ')
                    || (temp[i] >= 'Ю' && temp[i] <= 'Я') || (temp[i] >= 'а' && temp[i] <= 'п') || (temp[i] >= 'р' && temp[i] <= 'щ')
                    || (temp[i] >= 'ю' && temp[i] <= 'я') || temp[i] == 'Ь' || temp[i] == 'ь' || temp[i] == 'Ї' || temp[i] == 'ї'
                    || temp[i] == 'І' || temp[i] == 'і' || temp[i] == 'Є' || temp[i] == 'є' || temp[i] == '`' || temp[i] == 'ь')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidTextUA()
        {
            return Regex.IsMatch(_entry.Text, @"^[А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`]+$");
        }

        private bool EmailVerify()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z')
                    || temp[i] == '.' || temp[i] == '@' || temp[i] == '_' || temp[i] == '-')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        public bool IsValidEmail(string email)
        {
            Regex regex =
         new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
         RegexOptions.CultureInvariant | RegexOptions.Singleline);

            return regex.IsMatch(email);
        }

        private bool DigitVerify()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidDigit()
        {
            return Regex.IsMatch(_entry.Text, @"^[0-9]+$");
        }

        private bool PasswordCheckin()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z'))
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidPassword(string str)
        {

            Regex regex = new Regex(@"^([A-Z]{1}[A-Za-z0-9]{0,}[0-9]{1,})",
             RegexOptions.CultureInvariant | RegexOptions.Singleline);

            return regex.IsMatch(str);
        }

        private bool Mixed()
        {
            bool flag = true;
            string temp = _entry.Text;

            for (int i = 0; i < temp.Length; i++)
            {

                if (char.IsDigit(temp[i]) || (temp[i] >= 'A' && temp[i] <= 'Z') || (temp[i] >= 'a' && temp[i] <= 'z')
                    || (temp[i] >= 'А' && temp[i] <= 'Щ')
                    || (temp[i] >= 'Ю' && temp[i] <= 'Я') || (temp[i] >= 'а' && temp[i] <= 'п') || (temp[i] >= 'р' && temp[i] <= 'щ')
                    || (temp[i] >= 'ю' && temp[i] <= 'я') || temp[i] == 'Ь' || temp[i] == 'ь' || temp[i] == 'Ї' || temp[i] == 'ї'
                    || temp[i] == 'І' || temp[i] == 'і' || temp[i] == 'Є' || temp[i] == 'є' || temp[i] == '`' || temp[i] == ' ')
                {
                    continue;
                }
                else
                {
                    temp = temp.Remove(i, 1);
                    flag = false;
                    _entry.Text = temp;
                }
            }

            return flag;
        }

        private bool IsValidMixed()
        {
            return Regex.IsMatch(_entry.Text, @"^[0-9А-ЩЮ-Яа-пр-щю-яьЬЇїІіЄє`a-zA-Z ]+$");
        }

    }
}