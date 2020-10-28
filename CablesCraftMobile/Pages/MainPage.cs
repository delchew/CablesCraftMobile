using Xamarin.Forms;

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

            BarBackgroundColor = iOS ? default : (Color)App.Current.Properties["controlsColor"];
            BarTextColor = iOS ? (Color)App.Current.Properties["controlsColor"] : (Color)App.Current.Properties["greyColor"]; ;

            reelLengthsPage = new ReelLengthsCalculationPage { Title = "ДЛИНЫ" };

            twistPage = new TwistCalculationPage { Title = "СКРУТКА" };

            windingPage = new WindingCalculationPage { Title = "ОБМОТКА" };

            braidingPage = new BraidingCalculationPage { Title = "ОПЛЁТКА" };

            Children.Add(reelLengthsPage);
            Children.Add(twistPage);
            Children.Add(windingPage);
            Children.Add(braidingPage);
        }
    }
}
