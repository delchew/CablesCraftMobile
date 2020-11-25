using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Cables;

namespace CablesCraftMobile
{
    public class TwistViewModel : INotifyPropertyChanged
    {
        private readonly TwistMode twistMode;

        private const string twistInfoFileName = "twistInfo.json";
        private const string savedModeFileName = "twistMode.json";

        public event QuantityElementsChangedEventHandler QuantityElementsChanged;

        private readonly Action RecalculateParametres;

        public int MaxQuantityElements { get { return TwistBuilder.MaxTwistedElementsCount; } }

        public IList<TypeOfTwist> TypeOfTwistCollection { get; private set; }

        public double TwistStep
        {
            get { return twistMode.TwistStep; }
            private set
            {
                if (twistMode.TwistStep != value)
                {
                    twistMode.TwistStep = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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

        public TypeOfTwist TypeOfTwist
        {
            get { return twistMode.TypeOfTwist; }
            set
            {
                if (twistMode.TypeOfTwist == null || !twistMode.TypeOfTwist.Equals(value))
                {
                    twistMode.TypeOfTwist = value;

                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
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

                    RecalculateParametres?.Invoke();
                    QuantityElementsChanged?.Invoke(this, new QuantityElementsChangedEventArgs(twistMode.TwistInfo));
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TwistScheme));
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
                    RecalculateParametres?.Invoke();
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
            var twistInfoData = App.JsonRepository.GetObjects<TwistInfo>(twistInfoFileName);
            TwistBuilder.SetTwistInfoList(twistInfoData);
            twistMode = new TwistMode();
            LoadData();
            LoadParametres();
            RecalculateParametres += RecalculateTwistParametres;
            RecalculateParametres();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateTwistParametres()
        {
            TwistedCoreDiameter = TwistBuilder.GetTwistedCoreDiameterBySingleElement(QuantityElements, TwistedElementDiameter, TypeOfTwist.TwistedElementType);
            TwistStep = TwistBuilder.GetTwistStep(TypeOfTwist.TwistedElementType, TwistedCoreDiameter);
        }

        public void SaveParametres()
        {
            App.JsonRepository.SaveObject((TwistedElementDiameter, TypeOfTwist, TwistInfo), savedModeFileName);
        }

        public void LoadParametres()
        {
            var (elementDiameter, typeOfTwist, twistInfo) = App.JsonRepository.LoadObject<(double, TypeOfTwist, TwistInfo)> (savedModeFileName);
            TwistedElementDiameter = elementDiameter;
            TypeOfTwist = typeOfTwist;
            TwistInfo = twistInfo;
        }

        private void LoadData()
        {
            TypeOfTwistCollection = App.JsonRepository.GetObjects<TypeOfTwist>(App.dataFileName, @"$.Twist.TypeOfTwistCollection");
        }
    }
}
