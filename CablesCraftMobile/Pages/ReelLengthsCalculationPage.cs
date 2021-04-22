using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class ReelLengthsCalculationPage : ContentPage
    {
        private readonly ReelsLengthsViewModel reelsLengthsViewModel;

        public ReelLengthsCalculationPage(ReelsLengthsViewModel viewModel)
        {
            reelsLengthsViewModel = viewModel;

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

            //edgeClearanceNumEntry
            var edgeClearanceNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "ДО КРАЯ БАРАБАНА, ММ",
                OnlyIntegerNumbersInput = true,
                BindingContext = reelsLengthsViewModel
            };
            edgeClearanceNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(reelsLengthsViewModel.EdgeClearance), BindingMode.TwoWay);
            edgeClearanceNumEntry.SetBinding(NumEntryControllable.MaxValueProperty, nameof(reelsLengthsViewModel.EdgeClearanceMaxValue), BindingMode.OneWay);
            edgeClearanceNumEntry.SetBinding(NumEntryControllable.MinValueProperty, nameof(reelsLengthsViewModel.EdgeClearanceMinValue), BindingMode.OneWay);
            edgeClearanceNumEntry.SetBinding(NumEntryControllable.OffsetProperty, nameof(reelsLengthsViewModel.EdgeClearanceOffset), BindingMode.OneWay);

            controlsGrid.Children.Add(edgeClearanceNumEntry, 0, 0);

            //coreDiameterNumEntry
            var coreDiameterNumEntry = new NumEntryControllable
            {
                EntryTextColor = controlsColor,
                Caption = "Ø КАБЕЛЯ, ММ",
                BindingContext = reelsLengthsViewModel
            };
            coreDiameterNumEntry.SetBinding(NumEntryControllable.ValueProperty, nameof(reelsLengthsViewModel.CoreDiameter), BindingMode.TwoWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.MaxValueProperty, nameof(reelsLengthsViewModel.CoreDiameterMaxValue), BindingMode.OneWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.MinValueProperty, nameof(reelsLengthsViewModel.CoreDiameterMinValue), BindingMode.OneWay);
            coreDiameterNumEntry.SetBinding(NumEntryControllable.OffsetProperty, nameof(reelsLengthsViewModel.CoreDiameterOffset), BindingMode.OneWay);

            controlsGrid.Children.Add(coreDiameterNumEntry, 0, 1);

            #endregion

            var thickness = new Thickness(0);

            var reelsListView = new ListView
            {
                Margin = thickness,
                HasUnevenRows = true,
                ItemsSource = reelsLengthsViewModel.ReelViewModelsList,
                ItemTemplate = new DataTemplate(GetTemplate),
                Header = GetHeaderRow(),
                SelectionMode = ListViewSelectionMode.None
            };

            var reelsListScrollView = new ScrollView
            {
                Margin = thickness,
                Padding = thickness,
                Content = reelsListView,
                VerticalScrollBarVisibility = ScrollBarVisibility.Always
            };

            var frame = new Frame
            {
                Margin = thickness,
                Padding = thickness,
                HasShadow = true,
                Content = reelsListScrollView
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

        private Grid GetHeaderRow() //Шаблон для шапки заголовков в таблице барабанов
        {
            var grid = GetCellGridTemplate();
            grid.Padding = new Thickness(5, 2, 0, 2);
            grid.BackgroundColor = Color.LightGray;

            var headerColumnsNames = new []{ "Ø, мм", "Материал", "Цвет", "Примеч.", "Длина, м" };
            var labelStyle = (Style)App.Current.Resources["labelStyle"];

            for (int i = 0; i < headerColumnsNames.Length; i++)
                grid.Children.Add(new Label { Text = headerColumnsNames[i], FontSize = 15, Style = labelStyle }, i, 0);

            return grid;
        }
        
        private ViewCell GetTemplate() //Шаблон отображения параметров барабана в ячейке таблицы барабанов
        {
            var grid = GetCellGridTemplate();
            grid.Padding = new Thickness(5, 3, 0, 0);

            int fontsize = 15;
            var labelStyle = (Style)App.Current.Resources["labelStyle"];

            var diametrLabel = new Label { FontSize = fontsize, Style = labelStyle };
            diametrLabel.SetBinding(Label.TextProperty, "Diameter");
            grid.Children.Add(diametrLabel, 0, 0);

            var materialLabel = new Label { FontSize = fontsize, Style = labelStyle };
            materialLabel.SetBinding(Label.TextProperty, "Material");
            grid.Children.Add(materialLabel, 1, 0);

            var colorLabel = new Label { FontSize = fontsize, Style = labelStyle };
            colorLabel.SetBinding(Label.TextProperty, "Color");
            grid.Children.Add(colorLabel, 2, 0);

            var noteLable = new Label { FontSize = fontsize, Style = labelStyle };
            noteLable.SetBinding(Label.TextProperty, "Note");
            grid.Children.Add(noteLable, 3, 0);

            var cableLengthLabel = new Label
            {
                FontSize = fontsize,
                FontAttributes = FontAttributes.Bold,
                Style = (Style)App.Current.Resources["changingLabelStyle"]
            };
            cableLengthLabel.SetBinding(Label.TextProperty, "Length");
            grid.Children.Add(cableLengthLabel, 4, 0);

            return new ViewCell { View = grid };
        }

        private Grid GetCellGridTemplate()
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
