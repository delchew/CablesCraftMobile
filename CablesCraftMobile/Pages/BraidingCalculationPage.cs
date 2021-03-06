﻿using System.Collections.Generic;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class BraidingCalculationPage : ContentPage
    {
        private readonly BraidingViewModel braidingViewModel;

        public BraidingCalculationPage(BraidingViewModel viewModel)
        {
            braidingViewModel =  viewModel;

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
                BindingContext = braidingViewModel,
                OnlyIntegerNumbersInput = true
            };
            braidingStepNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(braidingViewModel.BraidingStep), BindingMode.TwoWay);
            braidingStepNumEntry.SetBinding(NumEntryControllable.MaxValueProperty, nameof(braidingViewModel.BraidingStepMaxValue), BindingMode.OneWay);
            braidingStepNumEntry.SetBinding(NumEntryControllable.MinValueProperty, nameof(braidingViewModel.BraidingStepMinValue), BindingMode.OneWay);
            braidingStepNumEntry.SetBinding(NumEntryControllable.OffsetProperty, nameof(braidingViewModel.BraidingStepOffset), BindingMode.OneWay);
            controlsGrid.Children.Add(braidingStepNumEntry, 0, 0);

            //coreDiameterNumEntry
            var coreDiameterNumEntry = new NumEntryControllable()
            {
                EntryTextColor = controlsColor,
                Caption = "Ø ЗАГОТОВКИ, ММ",
                BindingContext = braidingViewModel
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(braidingViewModel.BraidingCoreDiameter), BindingMode.TwoWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.MaxValueProperty, nameof(braidingViewModel.BraidingCoreDiameterMaxValue), BindingMode.OneWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.MinValueProperty, nameof(braidingViewModel.BraidingCoreDiameterMinValue), BindingMode.OneWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.OffsetProperty, nameof(braidingViewModel.BraidingCoreDiameterOffset), BindingMode.OneWay);
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
                Mode = BindingMode.TwoWay
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
                Mode = BindingMode.TwoWay
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
                Mode = BindingMode.TwoWay
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
                Mode = BindingMode.TwoWay
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
                    new ColumnDefinition { Width = new GridLength(110, GridUnitType.Absolute) }
                },
            };
            var rowIndex = -1;
            foreach (var pair in viewsDictionary)
            {
                braidingParametresGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });
                braidingParametresGrid.Children.Add(new Label { Text = pair.Key, Style = (Style)App.Current.Resources["labelStyle"] }, 0, ++rowIndex);
                braidingParametresGrid.Children.Add(pair.Value, 1, rowIndex);
                if (pair.Value is Label label) label.Style = (Style)App.Current.Resources["changingLabelStyle"];
                if (pair.Value is Picker picker) { picker.Style = (Style)App.Current.Resources["pickerStyle"]; }
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
    }
}
