using System.ComponentModel;
using System.IO;
using System.Text;
using Cables;
using Xamarin.Forms;

namespace CablesCraftMobile
{
    public class TwistViewModel : INotifyPropertyChanged
    {
        private readonly TwistMode twistMode;
        private readonly TwistedCoreBuilder builder;
        private const string twistInfoFilePath = "twistInfo.json";
        private CableTwistSchemePainter painter;

        public int MaxQuantityElements { get { return builder.MaxTwistedElementsCount; } }

        public TypeOfTwist[] TwistedElementTypesCollection { get; private set; }

        public double TwistStep
        {
            get { return twistMode.TwistStep; }
            private set
            {
                if (twistMode.TwistStep != value)
                {
                    twistMode.TwistStep = value;
                    OnPropertyChanged("TwistStep");
                }
            }
        }

        public double TwistedCoreDiameter
        {
            get { return twistMode.TwistedCoreDiameter; }
            private set
            {
                if (twistMode.TwistedCoreDiameter != value)
                {
                    twistMode.TwistedCoreDiameter = value;
                    OnPropertyChanged("TwistedCoreDiameter");
                }
            }
        }

        public string TwistScheme
        {
            get
            {
                var stringBuilder = new StringBuilder(20);
                stringBuilder.Append(twistMode.TwistInfo.LayersElementsCount[0]);
                for (int i = 1; i < twistMode.TwistInfo.LayersElementsCount.Length; i++)
                {
                    stringBuilder.Append($"+{twistMode.TwistInfo.LayersElementsCount[i]}");
                }
                return stringBuilder.ToString();
            }
        }

        public TwistedElementType TwistedElementType
        {
            get { return twistMode.TwistedElementType; }
            set
            {
                if (twistMode.TwistedElementType != value)
                {
                    twistMode.TwistedElementType = value;

                    RecalculateTwistParametres();
                    OnPropertyChanged("TwistedElementType");
                }
            }
        }

        public int QuantityElements
        {
            get { return twistMode.TwistInfo.QuantityElements; }
            set
            {
                if (twistMode.TwistInfo.QuantityElements != value)
                {
                    twistMode.TwistInfo = builder.GetTwistInfo(value);

                    painter.DrawTwistScheme(twistMode.TwistInfo);
                    RecalculateTwistParametres();
                    OnPropertyChanged("QuantityElements");
                    OnPropertyChanged("TwistScheme");
                }
            }
        }

        public double TwistedElementDiameter
        {
            get { return twistMode.TwistedElementDiameter; }
            set
            {
                if (twistMode.TwistedElementDiameter != value)
                {
                    twistMode.TwistedElementDiameter = value;
                    RecalculateTwistParametres();
                    OnPropertyChanged("TwistedElementDiameter");
                }
            }
        }

        public TwistViewModel()
        {
            var filePath = FileProvider.SendResourceFileToLocalApplicationFolder(twistInfoFilePath);

            builder = new TwistedCoreBuilder(new FileInfo(filePath));
            var twistInfo = builder.GetTwistInfo(2);
            twistMode = new TwistMode()
            {
                TwistedCoreDiameter = 2,
                TwistedElementDiameter = 2,
                TwistedElementType = TwistedElementType.single,
                TwistStep = 30,
                TwistInfo = twistInfo
            };

            TwistedElementTypesCollection = new TypeOfTwist []
            {
                new TypeOfTwist { Name = "Одиночный", TwistedElementType = TwistedElementType.single },
                new TypeOfTwist { Name = "Пара", TwistedElementType = TwistedElementType.pair },
                new TypeOfTwist { Name = "Тройка", TwistedElementType = TwistedElementType.triple },
                new TypeOfTwist { Name = "Четвёрка", TwistedElementType = TwistedElementType.four }
            };
        }

        public View GetDrawingCanvasView(Color backgroundColor)
        {
            painter = new CableTwistSchemePainter(twistMode.TwistInfo)
            {
                BackgroundColor = backgroundColor
            };
            return painter.GetCanvasView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateTwistParametres()
        {
            TwistedCoreDiameter = builder.GetTwistedCoreDiameterBySingleElement(QuantityElements, TwistedElementDiameter, TwistedElementType);
            TwistStep = builder.GetTwistStep(TwistedElementType, TwistedCoreDiameter);
        }
    }
}
