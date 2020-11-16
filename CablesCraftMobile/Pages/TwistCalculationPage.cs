using Xamarin.Forms;
using System.Collections.Generic;
using Cables;

namespace CablesCraftMobile
{
    public class TwistCalculationPage : ContentPage
    {
        private readonly TwistViewModel twistViewModel;
        private readonly CableTwistSchemePainter painter;

        public TwistCalculationPage(TwistViewModel viewModel)
        {
            twistViewModel = viewModel;
            painter = new CableTwistSchemePainter()
            {
                BackgroundColor = this.BackgroundColor,
                CurrentTwistInfo = twistViewModel.TwistInfo
            };
            var canvasView = painter.CanvasView;
            twistViewModel.QuantityElementsChanged += TwistViewModel_QuantityElementsChanged;

            var gridLayout = new Grid
            {
                Padding = new Thickness(0),
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) }
                }
            };
            if (Device.RuntimePlatform == Device.iOS) gridLayout.Padding = new Thickness(0, 30, 0, 0);
            Content = gridLayout;

            #region Entries

            var controlsColor = (Color)App.Current.Resources["controlsColor"];

            //twistedElementsCountNumEntry
            var twistedElementsCountNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "КОЛИЧЕСТВО ЭЛЕМЕНТОВ СКРУТКИ",
                MaxValue = twistViewModel.MaxQuantityElements,
                MinValue = 2,
                Offset = 1,
                OnlyIntegerNumbersInput = true,
                BindingContext = twistViewModel
            };
            twistedElementsCountNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(twistViewModel.QuantityElements), BindingMode.TwoWay);
            gridLayout.Children.Add(twistedElementsCountNumEntry, 0, 2);

            //coreDiameterNumEntry
            var coreDiameterNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЭЛЕМЕНТА СКРУТКИ, ММ",
                MaxValue = 20,
                MinValue = 0.1,
                Offset = 0.1,
                BindingContext = twistViewModel
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(twistViewModel.TwistedElementDiameter), BindingMode.TwoWay);
            gridLayout.Children.Add(coreDiameterNumEntry, 0, 3);

            #endregion

            #region Labels

            //twistStepLabel
            var twistStepLabel = new Label();
            var twistStepBinding = new Binding
            {
                Source = twistViewModel,
                Path = nameof(twistViewModel.TwistStep),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} мм"
            };
            twistStepLabel.SetBinding(Label.TextProperty, twistStepBinding);

            //twistCoreDiameterLabel
            var twistCoreDiameterLabel = new Label();
            var twistCoreDiameterBinding = new Binding
            {
                Source = twistViewModel,
                Path = nameof(twistViewModel.TwistedCoreDiameter),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} мм"
            };
            twistCoreDiameterLabel.SetBinding(Label.TextProperty, twistCoreDiameterBinding);

            //twistSchemeLabel
            var twistSchemeLabel = new Label();
            var twistSchemeLabelBinding = new Binding
            {
                Source = twistViewModel,
                Path = nameof(twistViewModel.TwistScheme),
                Mode = BindingMode.OneWay
            };
            twistSchemeLabel.SetBinding(Label.TextProperty, twistSchemeLabelBinding);

            #endregion

            #region Pickers

            var twistedElementTypePicker = new Picker();
            var twistedElementTypesSourceBinding = new Binding
            {
                Source = twistViewModel,
                Path = nameof(twistViewModel.TypeOfTwistCollection),
                Mode = BindingMode.OneWay
            };
            twistedElementTypePicker.SetBinding(Picker.ItemsSourceProperty, twistedElementTypesSourceBinding);
            twistedElementTypePicker.ItemDisplayBinding = new Binding("Name");
            twistedElementTypePicker.SelectedIndex = 0; 
            var twistedElementTypeBinding = new Binding
            {
                Source = twistViewModel,
                Path = nameof(twistViewModel.TypeOfTwist),
                Mode = BindingMode.TwoWay
            };
            twistedElementTypePicker.SetBinding(Picker.SelectedItemProperty, twistedElementTypeBinding);

            #endregion

            var twistParametresGrid = new Grid
            {
                ColumnSpacing = 30,
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute) }
                }
            };

            //Controls Dictionary
            var viewsDictionary = new Dictionary<string, View>
            {
                { "ШАГ СКРУТКИ, ММ", twistStepLabel },
                { "Ø ПО СКРУТКЕ, ММ", twistCoreDiameterLabel },
                { "СХЕМА СКРУТКИ", twistSchemeLabel },
                { "ТИП ЭЛЕМЕНТА", twistedElementTypePicker },
            };

            var rowIndex = -1;
            foreach (var pair in viewsDictionary)
            {
                twistParametresGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
                twistParametresGrid.Children.Add(new Label { Text = pair.Key, Style = (Style)App.Current.Resources["labelStyle"] }, 0, ++rowIndex);
                twistParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Resources["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Resources["pickerStyle"]; }
            }
            gridLayout.Children.Add(twistParametresGrid, 0, 0);

            gridLayout.Children.Add(canvasView, 0, 1);
        }

        private void TwistViewModel_QuantityElementsChanged(object sender, QuantityElementsChangedEventArgs e)
        {
            var currentTwistInfo = e.TwistInfo;
            painter.DrawTwistScheme(currentTwistInfo);
        }
    }
}