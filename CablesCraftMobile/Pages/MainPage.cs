using Xamarin.Forms;
using System.Linq;
using System;

namespace CablesCraftMobile
{
    public class MainPage : TabbedPage
    {
        public ReelLengthsCalculationPage reelLengthsPage;
        public TwistCalculationPage twistPage;
        public WindingCalculationPage windingPage;
        public BraidingCalculationPage braidingPage;

        public MainPage()
        {
            var iOS = Device.RuntimePlatform == Device.iOS;

            BarBackgroundColor = iOS ? default : (Color)App.Current.Resources["controlsColor"];
            BarTextColor = iOS ? (Color)App.Current.Resources["controlsColor"] : (Color)App.Current.Resources["greyColor"]; ;

            reelLengthsPage = new ReelLengthsCalculationPage { Title = "ДЛИНЫ" };

            twistPage = new TwistCalculationPage { Title = "СКРУТКА" };

            windingPage = new WindingCalculationPage { Title = "ОБМОТКА" };

            braidingPage = new BraidingCalculationPage { Title = "ОПЛЁТКА" };

            Children.Add(reelLengthsPage);
            Children.Add(twistPage);
            Children.Add(windingPage);
            Children.Add(braidingPage);

            LoadParametres();
        }

        public void SaveParametres()
        {
            App.Current.Properties["CurrentPageName"] = CurrentPage.GetType().FullName;
            braidingPage.SaveParametres();
        }

        private void LoadParametres()
        {
            if (App.Current.Properties.TryGetValue("CurrentPageName", out object obj))
            {
                var pageType = Type.GetType(obj.ToString());
                var fieldInfo = GetType().GetFields().Where(field => field.FieldType == pageType).First();
                var currentPage = fieldInfo.GetValue(this);
                CurrentPage = currentPage as Page;
            }
            else
                CurrentPage = reelLengthsPage;

            braidingPage.LoadParametres();
        }
    }
}
