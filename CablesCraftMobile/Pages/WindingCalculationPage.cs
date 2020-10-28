using System.Collections.Generic;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class WindingCalculationPage : ContentPage
    {
        private readonly NumEntryControllable coreDiameterNumEntry;
        private readonly NumEntryControllable windingStepNumEntry;
        private readonly Picker tapeWidthPicker;
        private readonly Picker tapeThicknessPicker;
        private readonly Picker tapeTypePicker;
        private readonly Label tapeOverlapLabel;
        private readonly Label windingAngleLabel;
        private readonly Label tapeExpenseKilometresLabel;
        private readonly Label tapeExpenseSquareMetresLabel;
        private readonly Label tapeExpenseKilogramesLabel;
        private readonly WindingViewModel windingViewModel;

        public WindingCalculationPage()
        {
            windingViewModel = new WindingViewModel();

            var controlsGrid = new Grid
            {
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) }
                },
            };

            #region Entries

            var controlsColor = (Color)App.Current.Properties["controlsColor"];

            //windingStepNumEntry
            windingStepNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "ШАГ ОБМОТКИ, ММ",
                MaxValue = 70,
                MinValue = 5,
                Offset = 1,
                Value = 25
            };
            var windingStepBinding = new Binding
            {
                Source = windingViewModel,
                Path = "WindingStep",
                Mode = BindingMode.OneWayToSource
            };
            windingStepNumEntry.SetBinding(NumEntryControllable.ValueProperty, windingStepBinding);
            controlsGrid.Children.Add(windingStepNumEntry, 0, 0);

            //coreDiameterNumEntry
            coreDiameterNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЗАГОТОВКИ",
                MaxValue = 50,
                MinValue = 0.5,
                Offset = 0.1,
                Value = 10
            };
            var windingCoreDiameterBinding = new Binding
            {
                Source = windingViewModel,
                Path = "WindingCoreDiameter",
                Mode = BindingMode.OneWayToSource
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, windingCoreDiameterBinding);
            controlsGrid.Children.Add(coreDiameterNumEntry, 0, 1);

            #endregion

            #region Labels

            //tapeOverlapLabel
            tapeOverlapLabel = new Label();
            var tapeOverlapBinding = new Binding
            {
                Source = windingViewModel,
                Path = "Overlap",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} %"
            };
            tapeOverlapLabel.SetBinding(Label.TextProperty, tapeOverlapBinding);

            //windingAngleLabel
            windingAngleLabel = new Label();
            var windingAngleBinding = new Binding
            {
                Source = windingViewModel,
                Path = "WindingAngle",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} °"
            };
            windingAngleLabel.SetBinding(Label.TextProperty, windingAngleBinding);

            //tapeExpenseKilometresLabel
            tapeExpenseKilometresLabel = new Label();
            var tapeExpenseKilometresBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeExpenseKilometres",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} км"
            };
            tapeExpenseKilometresLabel.SetBinding(Label.TextProperty, tapeExpenseKilometresBinding);

            //tapeExpenseSquareMetresLabel
            tapeExpenseSquareMetresLabel = new Label();
            var tapeExpenseSquareMetresBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeExpenseSquareMetres",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} м²"
            };
            tapeExpenseSquareMetresLabel.SetBinding(Label.TextProperty, tapeExpenseSquareMetresBinding);

            //tapeExpenseKilogramesLabel
            tapeExpenseKilogramesLabel = new Label();
            var tapeExpenseKilogramesBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeExpenseKilogrames",
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} кг/км"
            };
            tapeExpenseKilogramesLabel.SetBinding(Label.TextProperty, tapeExpenseKilogramesBinding);

            #endregion

            #region Pickers

            //tapeWidthPicker
            tapeWidthPicker = new Picker();
            var tapesWidthsSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapesWidthsCollection",
                Mode = BindingMode.OneWay
            };
            tapeWidthPicker.SetBinding(Picker.ItemsSourceProperty, tapesWidthsSourceBinding);
            tapeWidthPicker.SelectedIndex = 0;
            var tapeWidthBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeWidth",
                Mode = BindingMode.OneWayToSource
            };
            tapeWidthPicker.SetBinding(Picker.SelectedItemProperty, tapeWidthBinding);

            //tapeTypePicker
            tapeTypePicker = new Picker { FontSize = 14 };
            var tapesTypesSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapesNamesCollection",
                Mode = BindingMode.OneWay
            };
            tapeTypePicker.SetBinding(Picker.ItemsSourceProperty, tapesTypesSourceBinding);
            tapeTypePicker.SelectedIndex = 0;
            var tapeTypeBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeName",
                Mode = BindingMode.OneWayToSource
            };
            //tapeTypePicker.SetBinding(Picker.SelectedItemProperty, tapeTypeBinding); //TODO

            //tapeThicknessPicker
            tapeThicknessPicker = new Picker();
            var tapesThicknessSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = "CurrentTapeThicknessesCollection",
                Mode = BindingMode.OneWay
            };
            tapeThicknessPicker.SetBinding(Picker.ItemsSourceProperty, tapesThicknessSourceBinding);
            tapeThicknessPicker.SelectedIndex = 0;
            var tapeThicknessBinding = new Binding
            {
                Source = windingViewModel,
                Path = "TapeThickness",
                Mode = BindingMode.OneWayToSource
            };
            tapeThicknessPicker.SetBinding(Picker.SelectedItemProperty, tapeThicknessBinding);

            #endregion

            var viewsDictionary = new Dictionary<string, View>
            {
                { "ПЕРЕКРЫТИЕ", tapeOverlapLabel },
                { "УГОЛ ОБМОТКИ", windingAngleLabel },
                { "РАСХОД ЛЕНТЫ, КМ", tapeExpenseKilometresLabel },
                { "РАСХОД ЛЕНТЫ, М²", tapeExpenseSquareMetresLabel },
                { "РАСХОД ЛЕНТЫ, КГ", tapeExpenseKilogramesLabel },
                { "ШИРИНА ЛЕНТЫ, ММ", tapeWidthPicker },
                { "ТОЛЩИНА ЛЕНТЫ, МКМ", tapeThicknessPicker },
                { "ТИП ЛЕНТЫ", tapeTypePicker }
            };

            var windingParametresGrid = new Grid
            {
                ColumnSpacing = 30,
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(110, GridUnitType.Absolute) }
                }
            };

            var rowIndex = -1;
            foreach (var pair in viewsDictionary)
            {
                windingParametresGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
                windingParametresGrid.Children.Add(new Label { Text = pair.Key }, 0, ++rowIndex);
                windingParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Properties["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Properties["pickerStyle"]; }
            }

            //tapeTypePicker.SelectedIndexChanged += (sender, args) => tapeThicknessPicker.SelectedIndex = 0;

            var absoluteLayout = new AbsoluteLayout();
            if (Device.RuntimePlatform == Device.iOS) absoluteLayout.Padding = new Thickness(0, 30, 0, 0);

            absoluteLayout.Children.Add(windingParametresGrid);
            AbsoluteLayout.SetLayoutBounds(windingParametresGrid, new Rectangle(0.5, 0, windingParametresGrid.Width, windingParametresGrid.Height));
            AbsoluteLayout.SetLayoutFlags(windingParametresGrid, AbsoluteLayoutFlags.PositionProportional);

            absoluteLayout.Children.Add(controlsGrid);
            AbsoluteLayout.SetLayoutBounds(controlsGrid, new Rectangle(0.5, 1, controlsGrid.Width, controlsGrid.Height));
            AbsoluteLayout.SetLayoutFlags(controlsGrid, AbsoluteLayoutFlags.PositionProportional);

            Content = absoluteLayout;
        }
    }
}
