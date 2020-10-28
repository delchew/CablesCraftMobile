using System;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class NumEntryControllable : ContentView
    {
        private double offset;
        public double Offset
        {
            get { return offset; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Шаг кнопок не может быть меньше или равен 0!");
                offset = value;
            }
        }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public bool OnlyIntegerNumbersInput { get; set; }

        public string Caption
        {
            get { return labelCaption.Text; }
            set { labelCaption.Text = value; }
        }

        public Color EntryTextColor
        {
            get { return numEntry.TextColor; }
            set { numEntry.TextColor = value; }
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(NumEntryControllable), 0d);

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                numEntry.Text = value.ToString();
            }
        }

        private readonly Entry numEntry;
        private readonly Label labelCaption;
        private readonly Button buttonPlus;
        private readonly Button buttonMinus;
        private readonly Grid grid;
        private string savedEntryText;

        public NumEntryControllable()
        {
            var BottomMargin = new Thickness(0, 0, 0, 10);
            Margin = BottomMargin;

            labelCaption = new Label
            {
                FontSize = 14,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                Margin = BottomMargin
            };

            buttonPlus = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.LightGray,
                BorderWidth = 1.3

            };
            buttonPlus.Clicked += ButtonPlus_Clicked;

            buttonMinus = new Button
            {
                Text = "-",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.LightGray,
                BorderWidth = 1.3
            };
            buttonMinus.Clicked += ButtonMinus_Clicked;

            if (Device.RuntimePlatform == Device.iOS)
            {
                buttonPlus.WidthRequest = buttonMinus.WidthRequest = 80;
                buttonPlus.HeightRequest = buttonMinus.HeightRequest = 30;
                buttonPlus.BorderColor = buttonMinus.BorderColor = Color.LightGray;
            }
            if (Device.RuntimePlatform == Device.Android)
            {
                buttonPlus.CornerRadius = buttonMinus.CornerRadius = 8;
                buttonPlus.TextColor = buttonMinus.TextColor = default;
            }

            numEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20,
                Keyboard = Keyboard.Numeric,
                PlaceholderColor = Color.LightGray,
            };

            numEntry.Focused += NumEntry_Focused;
            numEntry.Unfocused += NumEntry_Unfocused;

            grid = new Grid
            {
                ColumnSpacing = 8,
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(80, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) }
                }
            };

            grid.Children.Add(numEntry, 0, 1);
            grid.Children.Add(buttonMinus, 1, 1);
            grid.Children.Add(buttonPlus, 2, 1);

            var stackLayout = new StackLayout();
            stackLayout.Children.Add(labelCaption);
            stackLayout.Children.Add(grid);
            Content = stackLayout;
        }

        private void NumEntry_Focused(object sender, FocusEventArgs e)
        {
            savedEntryText = numEntry.Text;
            numEntry.Text = string.Empty;
        }

        private void NumEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (double.TryParse(numEntry.Text, out var result))
            {
                if (result < MinValue)
                {
                    result = MinValue;
                }
                else if (result > MaxValue)
                {
                    result = MaxValue;
                }
                Value = result;
                return;
            }
            numEntry.Text = savedEntryText;
        }

        private void ButtonPlus_Clicked(object sender, EventArgs e)
        {
            if (Value == MaxValue) return;
            var increasedValue = Value + Offset;
            if (increasedValue > MaxValue) increasedValue = MaxValue;
            Value = increasedValue;
        }

        private void ButtonMinus_Clicked(object sender, EventArgs e)
        {
            if (Value == MinValue) return;
            var decreasedValue = Value - Offset;
            if (decreasedValue < MinValue) decreasedValue = MinValue;
            Value = decreasedValue;
        }
    }
}
