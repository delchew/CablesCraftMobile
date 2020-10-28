using Xamarin.Forms;
using System.Collections.Generic;

namespace CablesCraftMobile
{
    public class TwistCalculationPage : ContentPage
    {
        private readonly NumEntryControllable coreDiameterNumEntry;
        private readonly NumEntryControllable twistedElementsCountNumEntry;
        private readonly Picker twistedElementTypePicker;
        private readonly Label twistStepLabel;
        private readonly Label twistCoreDiameterLabel;
        private readonly Label twistSchemeLabel;
        private readonly TwistViewModel twistViewModel;

        public TwistCalculationPage()
        {
            twistViewModel = new TwistViewModel();

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

            var controlsColor = (Color)App.Current.Properties["controlsColor"];

            //twistedElementsCountNumEntry
            twistedElementsCountNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "КОЛИЧЕСТВО ЭЛЕМЕНТОВ СКРУТКИ",
                MaxValue = twistViewModel.MaxQuantityElements,
                MinValue = 2,
                Offset = 1,
                Value = 2
            };
            var twistedElementsCountBinding = new Binding
            {
                Source = twistViewModel,
                Path = "QuantityElements",
                Mode = BindingMode.OneWayToSource
            };
            twistedElementsCountNumEntry.SetBinding(NumEntryControllable.ValueProperty, twistedElementsCountBinding);
            gridLayout.Children.Add(twistedElementsCountNumEntry, 0, 2);

            //coreDiameterNumEntry
            coreDiameterNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЭЛЕМЕНТА СКРУТКИ, ММ",
                MaxValue = 20,
                MinValue = 0.1,
                Offset = 0.1,
                Value = 2.4
            };
            var coreDiameterBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistedElementDiameter",
                Mode = BindingMode.OneWayToSource
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, coreDiameterBinding);
            gridLayout.Children.Add(coreDiameterNumEntry, 0, 3);

            #endregion

            #region Labels

            //twistStepLabel
            twistStepLabel = new Label();
            var twistStepBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistStep",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} мм"
            };
            twistStepLabel.SetBinding(Label.TextProperty, twistStepBinding);

            //twistCoreDiameterLabel
            twistCoreDiameterLabel = new Label();
            var twistCoreDiameterBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistedCoreDiameter",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} мм"
            };
            twistCoreDiameterLabel.SetBinding(Label.TextProperty, twistCoreDiameterBinding);

            //twistSchemeLabel
            twistSchemeLabel = new Label();
            var twistSchemeLabelBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistScheme",
                Mode = BindingMode.OneWay
            };
            twistSchemeLabel.SetBinding(Label.TextProperty, twistSchemeLabelBinding);

            #endregion

            #region Pickers

            twistedElementTypePicker = new Picker();
            var twistedElementTypesSourceBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistedElementTypesCollection",
                Mode = BindingMode.OneWay,
            };
            twistedElementTypePicker.SetBinding(Picker.ItemsSourceProperty, twistedElementTypesSourceBinding);
            twistedElementTypePicker.ItemDisplayBinding = new Binding("Name");
            twistedElementTypePicker.SelectedIndex = 0; 
            var twistedElementTypeBinding = new Binding
            {
                Source = twistViewModel,
                Path = "TwistedElementType",
                Mode = BindingMode.OneWayToSource,
                Converter = new TypeOfTwistToTwistedElementTypeConverter()
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
                twistParametresGrid.Children.Add(new Label { Text = pair.Key, Style = (Style)App.Current.Properties["labelStyle"] }, 0, ++rowIndex);
                twistParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Properties["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Properties["pickerStyle"]; }
            }
            gridLayout.Children.Add(twistParametresGrid, 0, 0);

            var canvasView = twistViewModel.GetDrawingCanvasView(BackgroundColor);
            gridLayout.Children.Add(canvasView, 0, 1);
        }
    }
}