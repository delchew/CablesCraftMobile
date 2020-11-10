﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Cables;

namespace CablesCraftMobile
{
    public delegate void QuantityElementsEventHandler(object sender, QuantityElementsChangedEventArgs e);
    public class TwistViewModel : INotifyPropertyChanged
    {
        private readonly TwistMode twistMode;
        private const string twistInfoFileName = "twistInfo.json";
        private readonly string savedModeFileName = "windingMode.json";
        public event QuantityElementsEventHandler QuantityElementsChanged;

        private readonly Action RecalculateParametres;

        public int MaxQuantityElements { get { return TwistBuilder.MaxTwistedElementsCount; } }

        public TypeOfTwist[] TwistedElementTypesCollection { get; private set; }

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

        public TwistedElementType TwistedElementType
        {
            get { return twistMode.TwistedElementType; }
            set
            {
                if (twistMode.TwistedElementType != value)
                {
                    twistMode.TwistedElementType = value;

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
            set
            {
                if (!twistMode.Equals(value))
                {
                    twistMode.TwistInfo = value;
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
            //{
            //    TwistedCoreDiameter = 2,
            //    TwistedElementDiameter = 2,
            //    TwistedElementType = TwistedElementType.single,
            //    TwistStep = 30,
            //    TwistInfo = twistInfo
            //};

            TwistedElementTypesCollection = new TypeOfTwist []
            {
                new TypeOfTwist { Name = "Одиночный", TwistedElementType = TwistedElementType.single },
                new TypeOfTwist { Name = "Пара", TwistedElementType = TwistedElementType.pair },
                new TypeOfTwist { Name = "Тройка", TwistedElementType = TwistedElementType.triple },
                new TypeOfTwist { Name = "Четвёрка", TwistedElementType = TwistedElementType.four }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateTwistParametres()
        {
            TwistedCoreDiameter = TwistBuilder.GetTwistedCoreDiameterBySingleElement(QuantityElements, TwistedElementDiameter, TwistedElementType);
            TwistStep = TwistBuilder.GetTwistStep(TwistedElementType, TwistedCoreDiameter);
        }

        public void SaveParametres()
        {

        }

        public void LoadParametres()
        {

        }

        public void LoadData()
        {

        }
    }
}
