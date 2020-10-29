using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class App : Application
    {

        public App()
        {
            var controlsColor = Color.FromHex("#283593");
            var greyColor = Color.FromHex("#E8E8E8");

            var labelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Label.TextColorProperty,
                        Value = Color.Black
                    },
                    new Setter
                    {
                        Property = Label.VerticalOptionsProperty,
                        Value = LayoutOptions.CenterAndExpand
                    },
                    new Setter
                    {
                        Property = Label.HorizontalOptionsProperty,
                        Value = LayoutOptions.StartAndExpand
                    }
                }
            };

            var changingLabelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Label.TextColorProperty,
                        Value = controlsColor
                    },
                    new Setter
                    {
                        Property = Label.VerticalOptionsProperty,
                        Value = LayoutOptions.CenterAndExpand
                    },
                    new Setter
                    {
                        Property = Label.HorizontalOptionsProperty,
                     Value = LayoutOptions.StartAndExpand
                    },
                    new Setter
                    {
                        Property = Label.FontSizeProperty,
                        Value = 18
                    },
                    new Setter
                    {
                        Property = Label.FontAttributesProperty,
                        Value = FontAttributes.Bold
                    }
                }
            };

            var pickerStyle = new Style(typeof(Picker))
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Picker.HorizontalOptionsProperty,
                        Value = LayoutOptions.Fill
                    },
                    new Setter
                    {
                        Property = Picker.VerticalOptionsProperty,
                        Value = LayoutOptions.CenterAndExpand
                    },
                    new Setter
                    {
                        Property = Picker.TextColorProperty,
                        Value = controlsColor
                    },
                    new Setter
                    {
                        Property = Picker.MarginProperty,
                        Value = new Thickness(0, 0, 0, 10)
                    },
                }
            };

            Resources.Add("labelStyle", labelStyle);
            Resources.Add("changingLabelStyle", changingLabelStyle);
            Resources.Add("pickerStyle", pickerStyle);
            Resources.Add("controlsColor", controlsColor);
            Resources.Add("greyColor", greyColor);

            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
