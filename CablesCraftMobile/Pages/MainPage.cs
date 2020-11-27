using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using System;

namespace CablesCraftMobile
{
    public class MainPage : TabbedPage
    {
        private readonly ReelLengthsCalculationPage reelLengthsPage;
        private readonly TwistCalculationPage twistPage;
        private readonly WindingCalculationPage windingPage;
        private readonly BraidingCalculationPage braidingPage;

        private readonly ReelsLengthsViewModel reelsLengthsViewModel;
        private readonly TwistViewModel twistViewModel;
        private readonly WindingViewModel windingViewModel;
        private readonly BraidingViewModel braidingViewModel;

        public MainPage()
        {
            var iOS = Device.RuntimePlatform == Device.iOS;

            var controlsColor = (Color)App.Current.Resources["controlsColor"];
            BarBackgroundColor = iOS ? default : controlsColor;
            BarTextColor = iOS ? controlsColor : (Color)App.Current.Resources["greyColor"];

            reelsLengthsViewModel = new ReelsLengthsViewModel();
            reelLengthsPage = new ReelLengthsCalculationPage(reelsLengthsViewModel) { Title = "ДЛИНЫ" };

            twistViewModel = new TwistViewModel();
            twistPage = new TwistCalculationPage(twistViewModel) { Title = "СКРУТКА" };

            windingViewModel = new WindingViewModel();
            windingPage = new WindingCalculationPage(windingViewModel) { Title = "ОБМОТКА" };

            braidingViewModel = new BraidingViewModel();
            braidingPage = new BraidingCalculationPage(braidingViewModel) { Title = "ОПЛЁТКА" };

            Children.Add(reelLengthsPage);
            Children.Add(twistPage);
            Children.Add(windingPage);
            Children.Add(braidingPage);
        }

        public void SaveParametres()
        {
            App.Current.Properties["CurrentPageName"] = CurrentPage.GetType().FullName; //Сохраняем имя типа текущей страницы во внутренний словарь Properties

            reelsLengthsViewModel.SaveModel();
            twistViewModel.SaveModel();
            windingViewModel.SaveParametres();
            braidingViewModel.SaveModel();
        }

        public void LoadParametres()
        {
            reelsLengthsViewModel.LoadModel();
            twistViewModel.LoadModel();
            windingViewModel.LoadParametres();
            braidingViewModel.LoadModel();
        }

        public void LoadCurrentPage() => CurrentPage = GetLastUsedSavedPage();

        private Page GetLastUsedSavedPage()
        {
            if (App.Current.Properties.TryGetValue("CurrentPageName", out object obj))
            {
                var pageType = Type.GetType(obj.ToString());
                var pagefieldInfo = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                                             .Where(field => field.FieldType == pageType)
                                             .First();
                var currentPage = pagefieldInfo.GetValue(this);
                return currentPage as Page;
            }
            return reelLengthsPage; //возвращаем первую страницу по порядку, если в словаре Properties нет сохраненных значений.
        }
    }
}