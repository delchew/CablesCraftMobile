﻿using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class ReelLengthsCalculationPage : ContentPage
    {
        private readonly NumEntryControllable coreDiameterNumEntry;
        private readonly NumEntryControllable edgeClearanceNumEntry;
        private readonly ListView reelsTable;
        private Label diametrLabel;
        private Label materialLabel;
        private Label noteLable;
        private Label cableLengthLabel;
        private Label colorLabel;
        private readonly ReelsLengthsViewModel reelsLengthsViewModel;

        public ReelLengthsCalculationPage()
        {
            reelsLengthsViewModel = new ReelsLengthsViewModel();

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

            //edgeClearanceNumEntry
            edgeClearanceNumEntry = new NumEntryControllable
            {
                EntryTextColor = Color.DarkBlue,
                Caption = "ДО КРАЯ БАРАБАНА, ММ",
                MaxValue = 50,
                MinValue = 0,
                Offset = 5,
                Value = 5
            };
            var edgeClearanceBinding = new Binding
            {
                Source = reelsLengthsViewModel,
                Path = "EdgeClearance",
                Mode = BindingMode.OneWayToSource
            };
            edgeClearanceNumEntry.SetBinding(NumEntryControllable.ValueProperty, edgeClearanceBinding);
            controlsGrid.Children.Add(edgeClearanceNumEntry, 0, 0);

            //coreDiameterNumEntry
            coreDiameterNumEntry = new NumEntryControllable
            {
                EntryTextColor = Color.DarkBlue,
                Caption = "Ø КАБЕЛЯ, ММ",
                MaxValue = 50,
                MinValue = 0.5,
                Offset = 0.1,
                Value = 10
            };
            var coreDiameterBinding = new Binding
            {
                Source = reelsLengthsViewModel,
                Path = "CoreDiameter",
                Mode = BindingMode.OneWayToSource
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, coreDiameterBinding);
            controlsGrid.Children.Add(coreDiameterNumEntry, 0, 1);

            #endregion

            var thickness = new Thickness(0);

            reelsTable = new ListView
            {
                Margin = thickness,
                HasUnevenRows = true,
                ItemsSource = reelsLengthsViewModel.Reels,
                ItemTemplate = new DataTemplate(LoadTemplate),
                Header = TableRow(),
            };

            var scrollView = new ScrollView
            {
                Margin = thickness,
                Padding = thickness,
                Content = reelsTable,
                VerticalScrollBarVisibility = ScrollBarVisibility.Always
            };

            var frame = new Frame
            {
                Margin = thickness,
                Padding = thickness,
                HasShadow = true,
                Content = scrollView
            };

            var absoluteLayout = new AbsoluteLayout();
            if (Device.RuntimePlatform == Device.iOS) absoluteLayout.Padding = new Thickness(0, 30, 0, 0);

            absoluteLayout.Children.Add(frame);
            AbsoluteLayout.SetLayoutBounds(frame, new Rectangle(0.5, 0, 1, 0.6));
            AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.All);

            absoluteLayout.Children.Add(controlsGrid);
            AbsoluteLayout.SetLayoutBounds(controlsGrid, new Rectangle(0.5, 1, controlsGrid.Width, controlsGrid.Height));
            AbsoluteLayout.SetLayoutFlags(controlsGrid, AbsoluteLayoutFlags.PositionProportional);

            Content = absoluteLayout;

        }

        private Grid TableRow() //Шаблон для шапки заголовков в таблице барабанов
        {
            var grid = GetTableRowGrid();
            grid.Padding = new Thickness(5, 2, 0, 2);
            grid.BackgroundColor = Color.LightGray;

            var headerColumnsNames = new []{ "Ø, мм", "Материал", "Цвет", "Примеч.", "Длина, м" };

            for (int i = 0; i < headerColumnsNames.Length; i++)
            {
                grid.Children.Add(new Label { Text = headerColumnsNames[i], FontSize = 15 }, i, 0);
            }
            return grid;
        }
        
        private ViewCell LoadTemplate()
        {
            var grid = GetTableRowGrid();
            grid.Padding = new Thickness(5, 3, 0, 0);

            int fontsize = 15;

            diametrLabel = new Label { FontSize = fontsize, Style = (Style)App.Current.Properties["labelStyle"] };
            diametrLabel.SetBinding(Label.TextProperty, "Diameter");
            grid.Children.Add(diametrLabel, 0, 0);

            materialLabel = new Label { FontSize = fontsize, Style = (Style)App.Current.Properties["labelStyle"] };
            materialLabel.SetBinding(Label.TextProperty, "Material");
            grid.Children.Add(materialLabel, 1, 0);

            colorLabel = new Label { FontSize = fontsize, Style = (Style)App.Current.Properties["labelStyle"] };
            colorLabel.SetBinding(Label.TextProperty, "Color");
            grid.Children.Add(colorLabel, 2, 0);

            noteLable = new Label { FontSize = fontsize, Style = (Style)App.Current.Properties["labelStyle"] };
            noteLable.SetBinding(Label.TextProperty, "Note");
            grid.Children.Add(noteLable, 3, 0);

            cableLengthLabel = new Label { FontSize = fontsize, FontAttributes = FontAttributes.Bold, Style = (Style)App.Current.Properties["changingLabelStyle"] };
            cableLengthLabel.SetBinding(Label.TextProperty, "Length");
            grid.Children.Add(cableLengthLabel, 4, 0);

            return new ViewCell { View = grid };
        }

        private Grid GetTableRowGrid()
        {
            var grid = new Grid
            {
                ColumnSpacing = 5,
                RowDefinitions = { new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) }, }
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.7, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.9, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            return grid;
        }
    }
}
