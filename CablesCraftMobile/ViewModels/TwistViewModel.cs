using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Cables;

namespace CablesCraftMobile
{
    public class TwistViewModel : INotifyPropertyChanged
    {
        private TwistMode twistMode;

        private const string twistInfoFileName = "twistInfo.json";
        private const string savedModeFileName = "twistMode.json";

        public event QuantityElementsChangedEventHandler QuantityElementsChanged;

        public IList<TypeOfTwist> TypeOfTwistCollection { get; private set; }

        public double TwistStep
        {
            get => TwistBuilder.GetTwistStep(TypeOfTwist.TwistedElementType, TwistedCoreDiameter);
        }

        public double TwistedCoreDiameter
        {
            get => TwistBuilder.GetTwistedCoreDiameterBySingleElement(QuantityElements, TwistedElementDiameter, TypeOfTwist.TwistedElementType);
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

        public TypeOfTwist TypeOfTwist
        {
            get { return twistMode.TypeOfTwist; }
            set
            {
                if (twistMode.TypeOfTwist == null || !twistMode.TypeOfTwist.Equals(value))
                {
                    twistMode.TypeOfTwist = value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TwistedCoreDiameter));
                    OnPropertyChanged(nameof(TwistStep));
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
                    twistMode.TwistInfo = TwistBuilder.GetTwistInfo(value);

                    QuantityElementsChanged?.Invoke(this, new QuantityElementsChangedEventArgs(twistMode.TwistInfo));
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TwistScheme));
                    OnPropertyChanged(nameof(TwistedCoreDiameter));
                    OnPropertyChanged(nameof(TwistStep));
                }
            }
        }

        public int QuantityElementsMaxValue
        {
            get { return TwistBuilder.MaxTwistedElementsCount; }
        }

        public int QuantityElementsMinValue
        {
            get { return twistMode.QuantityElementsMinValue; }
            private set
            {
                if (twistMode.QuantityElementsMinValue != value)
                {
                    twistMode.QuantityElementsMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public int QuantityElementsOffset
        {
            get { return twistMode.QuantityElementsOffset; }
            private set
            {
                if (twistMode.QuantityElementsOffset != value)
                {
                    twistMode.QuantityElementsOffset = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TwistedCoreDiameter));
                    OnPropertyChanged(nameof(TwistStep));
                }
            }
        }

        public double TwistedElementDiameterMaxValue
        {
            get { return twistMode.TwistedElementDiameterMaxValue; }
            private set
            {
                if (twistMode.TwistedElementDiameterMaxValue != value)
                {
                    twistMode.TwistedElementDiameterMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TwistedElementDiameterMinValue
        {
            get { return twistMode.TwistedElementDiameterMinValue; }
            private set
            {
                if (twistMode.TwistedElementDiameterMinValue != value)
                {
                    twistMode.TwistedElementDiameterMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TwistedElementDiameterOffset
        {
            get { return twistMode.TwistedElementDiameterOffset; }
            private set
            {
                if (twistMode.TwistedElementDiameterOffset != value)
                {
                    twistMode.TwistedElementDiameterOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        public TwistInfo TwistInfo
        {
            get { return twistMode.TwistInfo; }
            private set
            {
                if (!twistMode.TwistInfo.Equals(value))
                {
                    twistMode.TwistInfo = value;
                    OnPropertyChanged(nameof(QuantityElements));
                    OnPropertyChanged(nameof(TwistScheme));
                }
            }
        }

        public TwistViewModel()
        {
            LoadData();
            LoadModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveModel() => App.JsonRepository.SaveObject(twistMode, savedModeFileName);

        public void LoadModel() => twistMode = App.JsonRepository.LoadObject<TwistMode>(savedModeFileName);

        private void LoadData()
        {
            var twistInfoData = App.JsonRepository.GetObjects<TwistInfo>(twistInfoFileName);
            TwistBuilder.SetTwistInfoList(twistInfoData);
            TypeOfTwistCollection = App.JsonRepository.GetObjects<TypeOfTwist>(App.dataFileName, @"$.Twist.TypeOfTwistCollection");
        }
    }
}
