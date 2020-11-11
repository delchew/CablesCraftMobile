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

            var controlsColor = (Color)App.Current.Resources["controlsColor"];
            BarBackgroundColor = iOS ? default : controlsColor;
            BarTextColor = iOS ? controlsColor : (Color)App.Current.Resources["greyColor"]; ;

            reelLengthsPage = new ReelLengthsCalculationPage { Title = "ДЛИНЫ" };

            twistPage = new TwistCalculationPage { Title = "СКРУТКА" };

            windingPage = new WindingCalculationPage { Title = "ОБМОТКА" };

            braidingPage = new BraidingCalculationPage() { Title = "ОПЛЁТКА" };

            Children.Add(reelLengthsPage);
            Children.Add(twistPage);
            Children.Add(windingPage);
            Children.Add(braidingPage);
        }

        public void SaveParametres()
        {
            App.Current.Properties["CurrentPageName"] = CurrentPage.GetType().FullName;
            braidingPage.SaveParametres();
            twistPage.SaveParametres();
        }

        public void LoadParametres()
        {
            if (App.Current.Properties.TryGetValue("CurrentPageName", out object obj))
            {
                var pageType = Type.GetType(obj.ToString());
                var pagefieldInfo = GetType().GetFields().Where(field => field.FieldType == pageType).First();
                var currentPage = pagefieldInfo.GetValue(this);
                CurrentPage = currentPage as Page;
            }
            else
                CurrentPage = reelLengthsPage;

            twistPage.LoadParametres();
            braidingPage.LoadParametres();
        }
    }
}
