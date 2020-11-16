using System.Collections.Generic;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class WindingCalculationPage : ContentPage
    {
        private readonly WindingViewModel windingViewModel;

        public WindingCalculationPage(WindingViewModel viewModel)
        {
            windingViewModel = viewModel;

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

            var controlsColor = (Color)App.Current.Resources["controlsColor"];

            //windingStepNumEntry
            var windingStepNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "ШАГ ОБМОТКИ, ММ",
                MaxValue = 70,
                MinValue = 5,
                Offset = 1,
                BindingContext = windingViewModel
            };
            windingStepNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(windingViewModel.WindingStep), BindingMode.TwoWay);
            controlsGrid.Children.Add(windingStepNumEntry, 0, 0);

            //coreDiameterNumEntry
            var coreDiameterNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЗАГОТОВКИ",
                MaxValue = 50,
                MinValue = 0.5,
                Offset = 0.1,
                BindingContext = windingViewModel
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(windingViewModel.WindingCoreDiameter), BindingMode.TwoWay);
            controlsGrid.Children.Add(coreDiameterNumEntry, 0, 1);

            #endregion

            #region Labels

            //tapeOverlapLabel
            var tapeOverlapLabel = new Label();
            var tapeOverlapBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.Overlap),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} %"
            };
            tapeOverlapLabel.SetBinding(Label.TextProperty, tapeOverlapBinding);

            //windingAngleLabel
            var windingAngleLabel = new Label();
            var windingAngleBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.WindingAngle),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} °"
            };
            windingAngleLabel.SetBinding(Label.TextProperty, windingAngleBinding);

            //tapeExpenseKilometresLabel
            var tapeExpenseKilometresLabel = new Label();
            var tapeExpenseKilometresBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeExpenseKilometres),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} км"
            };
            tapeExpenseKilometresLabel.SetBinding(Label.TextProperty, tapeExpenseKilometresBinding);

            //tapeExpenseSquareMetresLabel
            var tapeExpenseSquareMetresLabel = new Label();
            var tapeExpenseSquareMetresBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeExpenseSquareMetres),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} м²"
            };
            tapeExpenseSquareMetresLabel.SetBinding(Label.TextProperty, tapeExpenseSquareMetresBinding);

            //tapeExpenseKilogramesLabel
            var tapeExpenseKilogramesLabel = new Label();
            var tapeExpenseKilogramesBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeExpenseKilogrames),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} кг/км"
            };
            tapeExpenseKilogramesLabel.SetBinding(Label.TextProperty, tapeExpenseKilogramesBinding);

            #endregion

            #region Pickers

            //tapeWidthPicker
            var tapeWidthPicker = new Picker();
            var tapesWidthsSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapesWidthsCollection),
                Mode = BindingMode.OneWay
            };
            tapeWidthPicker.SetBinding(Picker.ItemsSourceProperty, tapesWidthsSourceBinding);
            tapeWidthPicker.SelectedIndex = 0;
            var tapeWidthBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeWidth),
                Mode = BindingMode.OneWayToSource
            };
            tapeWidthPicker.SetBinding(Picker.SelectedItemProperty, tapeWidthBinding);

            //tapeTypePicker
            var tapeTypePicker = new Picker { FontSize = 14 };
            var tapesTypesSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapesNamesCollection),
                Mode = BindingMode.OneWay
            };
            tapeTypePicker.SetBinding(Picker.ItemsSourceProperty, tapesTypesSourceBinding);
            tapeTypePicker.SelectedIndex = 0;
            var tapeTypeBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeName),
                Mode = BindingMode.OneWayToSource
            };
            //tapeTypePicker.SetBinding(Picker.SelectedItemProperty, tapeTypeBinding); //TODO

            //tapeThicknessPicker
            var tapeThicknessPicker = new Picker();
            var tapesThicknessSourceBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.CurrentTapeThicknessesCollection),
                Mode = BindingMode.OneWay
            };
            tapeThicknessPicker.SetBinding(Picker.ItemsSourceProperty, tapesThicknessSourceBinding);
            tapeThicknessPicker.SelectedIndex = 0;
            var tapeThicknessBinding = new Binding
            {
                Source = windingViewModel,
                Path = nameof(windingViewModel.TapeThickness),
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
                windingParametresGrid.Children.Add(new Label { Text = pair.Key, Style = (Style)App.Current.Resources["labelStyle"] }, 0, ++rowIndex);
                windingParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Resources["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Resources["pickerStyle"]; }
            }

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
