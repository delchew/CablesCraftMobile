using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class App : Application
    {
        public static readonly Color controlsColor = Color.FromHex("#283593");
        public static readonly Color backgroundColor = Color.FromHex("#E8E8E8");

        public readonly Style labelStyle;

        public readonly Style changingLabelStyle;

        public readonly Style pickerStyle;

        public App()
        {
            labelStyle = new Style(typeof(Label))
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

            changingLabelStyle = new Style(typeof(Label))
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

            pickerStyle = new Style(typeof(Picker))
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
            Properties.Add("labelStyle", labelStyle);
            Properties.Add("changingLabelStyle", changingLabelStyle);
            Properties.Add("pickerStyle", pickerStyle);
            Properties.Add("controlsColor", controlsColor);
            Properties.Add("blackColor", Color.Black);
            Properties.Add("greyColor", Color.FromHex("#E8E8E8"));

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
