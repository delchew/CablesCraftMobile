using System.Collections.Generic;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class BraidingCalculationPage : ContentPage
    {
        private readonly BraidingViewModel braidingViewModel;

        public BraidingCalculationPage()
        {
            braidingViewModel =  new BraidingViewModel();

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
            //braidingStepNumEntry
            var braidingStepNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "ШАГ ОПЛЕТКИ, ММ",
                MaxValue = 280,
                MinValue = 1,
                Offset = 1,
                Value = 10
            };
            var braidingStepBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.BraidingStep),
                Mode = BindingMode.OneWayToSource
            };
            braidingStepNumEntry.SetBinding(NumEntryControllable.ValueProperty, braidingStepBinding);
            controlsGrid.Children.Add(braidingStepNumEntry, 0, 0);

            //coreDiameterNumEntry
            var coreDiameterNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЗАГОТОВКИ, ММ",
                MaxValue = 30,
                MinValue = 0.5,
                Offset = 0.1,
                Value = 5
            };
            var coreDiameterBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.BraidingCoreDiameter),
                Mode = BindingMode.OneWayToSource
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, coreDiameterBinding);
            controlsGrid.Children.Add(coreDiameterNumEntry, 0, 1);

            #endregion

            #region Labels

            //braidingDensityLable
            var braidingDensityLable = new Label();
            var braidingDensityBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.BraidingDensity),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} %"
            };
            braidingDensityLable.SetBinding(Label.TextProperty, braidingDensityBinding);

            //braidingAngleLable
            var braidingAngleLable = new Label();
            var braidingAngleBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.BraidingAngle),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} °"
            };
            braidingAngleLable.SetBinding(Label.TextProperty, braidingAngleBinding);

            //wiresWeightKilogramsLable
            var wiresWeightKilogramsLable = new Label();
            var wiresWeightKilogramsBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresWeight),
                Mode = BindingMode.OneWay,
                StringFormat = "{0:F2} кг/км"
            };
            wiresWeightKilogramsLable.SetBinding(Label.TextProperty, wiresWeightKilogramsBinding);

            #endregion

            #region Pickers

            //CoilsCountPicker
            var coilsCountPicker = new Picker();
            var CoilsCountSourceBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.CoilsCountCollection),
                Mode = BindingMode.OneWay
            };
            coilsCountPicker.SetBinding(Picker.ItemsSourceProperty, CoilsCountSourceBinding);
            var coilsCountBinding = new Binding()
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.CoilsCount),
                Mode = BindingMode.OneWayToSource
            };
            coilsCountPicker.SetBinding(Picker.SelectedItemProperty, coilsCountBinding);

            //WiresCountPicker
            var wiresCountPicker = new Picker();
            var wiresCountSourceBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresCountCollection),
                Mode = BindingMode.OneWay
            };
            wiresCountPicker.SetBinding(Picker.ItemsSourceProperty, wiresCountSourceBinding);
            var wiresCountBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresCount),
                Mode = BindingMode.OneWayToSource
            };
            wiresCountPicker.SetBinding(Picker.SelectedItemProperty, wiresCountBinding);

            //WiresDiameterPicker
            var wiresDiameterPicker = new Picker();
            var wiresDiametersSourceBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresDiametersCollection),
                Mode = BindingMode.OneWay
            };
            wiresDiameterPicker.SetBinding(Picker.ItemsSourceProperty, wiresDiametersSourceBinding);
            var wiresDiameterBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresDiameter),
                Mode = BindingMode.OneWayToSource
            };
            wiresDiameterPicker.SetBinding(Picker.SelectedItemProperty, wiresDiameterBinding);

            //WiresMaterialPicker
            var wiresMaterialPicker = new Picker();
            var wiresMaterialSourceBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresMaterialsCollection),
                Mode = BindingMode.OneWay
            };
            wiresMaterialPicker.SetBinding(Picker.ItemsSourceProperty, wiresMaterialSourceBinding);
            wiresMaterialPicker.ItemDisplayBinding = new Binding("Name");
            var wiresMaterialBinding = new Binding
            {
                Source = braidingViewModel,
                Path = nameof(braidingViewModel.WiresMaterial),
                Mode = BindingMode.OneWayToSource
            };
            wiresMaterialPicker.SetBinding(Picker.SelectedItemProperty, wiresMaterialBinding);

            #endregion

            //Controls Dictionary
            var viewsDictionary = new Dictionary<string,View>
            {
                { "ПЛОТНОСТЬ ОПЛЁТКИ", braidingDensityLable },
                { "УГОЛ ОПЛЁТКИ", braidingAngleLable },
                { "МАССА ПРОВОЛОКИ", wiresWeightKilogramsLable },
                { "ЧИСЛО КАТУШЕК", coilsCountPicker },
                { "ЧИСЛО ПРОВОЛОК", wiresCountPicker },
                { "Ø ПРОВОЛОК, ММ", wiresDiameterPicker },
                { "МАТЕРИАЛ ПРОВОЛОК", wiresMaterialPicker }
            };

            var braidingParametresGrid = new Grid
            {
                ColumnSpacing = 30,
                RowSpacing = 0,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) }
                },
            };
            var rowIndex = -1;
            foreach (var pair in viewsDictionary)
            {
                braidingParametresGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
                braidingParametresGrid.Children.Add(new Label { Text = pair.Key, Style = (Style)App.Current.Resources["labelStyle"] }, 0, ++rowIndex);
                braidingParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Resources["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Resources["pickerStyle"]; picker.SelectedIndex = 0; }
            }

            var absoluteLayout = new AbsoluteLayout();
            if (Device.RuntimePlatform == Device.iOS) absoluteLayout.Padding = new Thickness(0, 30, 0, 0);

            absoluteLayout.Children.Add(braidingParametresGrid);
            AbsoluteLayout.SetLayoutBounds(braidingParametresGrid, new Rectangle(0.5, 0, braidingParametresGrid.Width, braidingParametresGrid.Height));
            AbsoluteLayout.SetLayoutFlags(braidingParametresGrid, AbsoluteLayoutFlags.PositionProportional);

            absoluteLayout.Children.Add(controlsGrid);
            AbsoluteLayout.SetLayoutBounds(controlsGrid, new Rectangle(0.5, 1, controlsGrid.Width, controlsGrid.Height));
            AbsoluteLayout.SetLayoutFlags(controlsGrid, AbsoluteLayoutFlags.PositionProportional);

            Content = absoluteLayout;
        }

        public void SaveParametres() => braidingViewModel.SaveParametres();

        public void LoadParametres() => braidingViewModel.LoadParametres();
    }
}
