﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class NumEntryControllable : ContentView
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(NumEntryControllable), 0d);

        public static readonly BindableProperty OffsetPropery =
            BindableProperty.Create(nameof(Offset), typeof(double), typeof(NumEntryControllable), 0.1);

        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(double), typeof(NumEntryControllable), 0d);

        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(NumEntryControllable), 0d);

        public static readonly BindableProperty CaptionProperty =
            BindableProperty.Create(nameof(Caption), typeof(string), typeof(NumEntryControllable), "NumEntryControllable");

        public static readonly BindableProperty EntryTextColorProperty =
            BindableProperty.Create(nameof(EntryTextColor), typeof(Color), typeof(NumEntryControllable), Color.Default);

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double Offset
        {
            get { return (double)GetValue(OffsetPropery); }
            set { SetValue(OffsetPropery, value); }
        }

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public Color EntryTextColor
        {
            get { return (Color)GetValue(EntryTextColorProperty); }
            set { SetValue(EntryTextColorProperty, value); }
        }

        public bool OnlyIntegerNumbersInput { get; set; } = false;

        private readonly Entry numEntry;
        private string savedEntryText;
        //private static readonly string separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        private bool textEditing = false;

        public NumEntryControllable()
        {
            var BottomMargin = new Thickness(0, 0, 0, 10);
            Margin = BottomMargin;

            var labelCaption = new Label
            {
                FontSize = 14,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                Margin = BottomMargin,
                BindingContext = this
            };
            labelCaption.SetBinding(Label.TextProperty, nameof(Caption), BindingMode.OneWay);

            var buttonPlus = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.LightGray,
                BorderWidth = 1.3,
            };
            buttonPlus.Clicked += ButtonPlus_Clicked;

            var buttonMinus = new Button
            {
                Text = "-",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                TextColor = Color.Black,
                BackgroundColor = Color.LightGray,
                BorderWidth = 1.3,
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
                ReturnType = ReturnType.Send,
                PlaceholderColor = Color.LightGray,
                BindingContext = this
            };
            numEntry.SetBinding(Entry.TextColorProperty, nameof(EntryTextColor), BindingMode.OneWay);

            numEntry.Focused += NumEntry_Focused;
            numEntry.Unfocused += NumEntry_Unfocused;
            numEntry.TextChanged += NumEntry_TextChanged;

            var grid = new Grid
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

            PropertyChanged += NumEntryControllable_PropertyChanged;
        }

        private void NumEntryControllable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Value))
                numEntry.Text = Value.ToString();
            ;
        }

        private void NumEntry_Focused(object sender, FocusEventArgs e)
        {
            textEditing = true;
            savedEntryText = numEntry.Text;
            numEntry.Text = string.Empty;
        }

        private void NumEntry_Unfocused(object sender, FocusEventArgs e) // TODO доделать безглючную логику поведения
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
            }
            else
            {
                numEntry.Text = savedEntryText;
            }
            textEditing = false;
        }

        private const string emptyStringRegex = @"^$";
        private const string zeroAndNaturalNumbersRegex = @"^0$|^[1-9]\d*$";
        private const string zeroAndPositiveRealNumbersRegex = @"^(0|[1-9]+[0-9]*)([.,]\d*)?$";

        private void NumEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!textEditing) return;
            var numbersTypeRegex = OnlyIntegerNumbersInput ? zeroAndNaturalNumbersRegex : zeroAndPositiveRealNumbersRegex;
            var currentRegex = $"{emptyStringRegex}|{numbersTypeRegex}";
            if (Regex.IsMatch(e.NewTextValue, currentRegex))
                return;
            (sender as Entry).Text = e.OldTextValue;
        }

        private void ButtonPlus_Clicked(object sender, EventArgs e)
        {
            if (Value == MaxValue) return;
            var increasedValue = Value + Offset;
            if (increasedValue <= MaxValue) Value = increasedValue;
        }

        private void ButtonMinus_Clicked(object sender, EventArgs e)
        {
            if (Value == MinValue) return;
            var decreasedValue = Value - Offset;
            if (decreasedValue >= MinValue) Value = decreasedValue;
        }
    }
}
