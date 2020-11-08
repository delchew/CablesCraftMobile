using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class App : Application
    {
        public static readonly string dataFileName = "appData.json";
        private static readonly string[] dataFileNames = new string[]
        {
            dataFileName,
            "braidingMode.json",
            "reels.json",
            "twistInfo.json"
        };
        private static JsonRepository jsonRepository;
        public static JsonRepository JsonRepository
        {
            get
            {
                if (jsonRepository == null)
                    jsonRepository = new JsonRepository();
                return jsonRepository;
            }
        }

        private readonly MainPage mainPage;

        public readonly ReelsLengthsViewModel reelsLengthsViewModel;
        public readonly TwistViewModel twistViewModel;
        public readonly WindingViewModel windingViewModel;
        public readonly BraidingViewModel braidingViewModel;

        public App()
        {
            foreach (var fileName in dataFileNames)
            {
                SendResourceFileToLocalApplicationFolder(fileName);
            }

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

            mainPage = new MainPage();
            MainPage = mainPage;

        }

        protected override void OnStart()
        {
            mainPage.LoadParametres();
        }

        protected override void OnSleep()
        {
            mainPage.SaveParametres();
        }

        protected override void OnResume()
        {

        }

        public static void SendResourceFileToLocalApplicationFolder(string resourceFileName)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), resourceFileName);
            if (!File.Exists(filePath))
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                using (var stream = assembly.GetManifestResourceStream($"{typeof(App).Namespace}.JsonDataFiles.{resourceFileName}"))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
            }
        }
    }
}
