using System.ComponentModel;
using Cables.Materials;
using Cables.Braiding;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace CablesCraftMobile
{
    public class BraidingViewModel : INotifyPropertyChanged
    {
        private BraidingMode braidingMode;
        private readonly string savedModeFileName = "braidingMode.json";

        private readonly Action RecalculateParametres;

        public IList<int> CoilsCountCollection { get; private set; }
        public IList<int> WiresCountCollection { get; private set; }
        public IList<double> WiresDiametersCollection { get; private set; }
        public IList<Metal> WiresMaterialsCollection { get; private set; }

        public double BraidingDensity
        {
            get { return braidingMode.BraidingDensity; }
            private set
            {
                if (braidingMode.BraidingDensity != value)
                {
                    braidingMode.BraidingDensity = value;
                    OnPropertyChanged();
                }
            }
        }

        public double BraidingAngle
        {
            get { return braidingMode.BraidingAngle; }
            private set
            {
                if (braidingMode.BraidingAngle != value)
                {
                    braidingMode.BraidingAngle = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WiresWeight
        {
            get { return braidingMode.WiresWeight; }
            private set
            {
                if (braidingMode.WiresWeight != value)
                {
                    braidingMode.WiresWeight = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public int CoilsCount
        {
            get { return braidingMode.CoilsCount; }
            set
            {
                if (braidingMode.CoilsCount != value)
                {
                    braidingMode.CoilsCount = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public int WiresCount
        {
            get { return braidingMode.WiresCount; }
            set
            {
                if (braidingMode.WiresCount != value)
                {
                    braidingMode.WiresCount = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double WiresDiameter
        {
            get { return braidingMode.WiresDiameter; }
            set
            {
                if (braidingMode.WiresDiameter != value)
                {
                    braidingMode.WiresDiameter = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public Metal WiresMaterial
        {
            get { return braidingMode.WiresMaterial; }
            set
            {
                if (braidingMode.WiresMaterial == null || !braidingMode.WiresMaterial.Equals(value))
                {
                    braidingMode.WiresMaterial = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double BraidingStep
        {
            get { return braidingMode.BraidingStep; }
            set
            {
                if (braidingMode.BraidingStep != value)
                {
                    braidingMode.BraidingStep = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double BraidingStepMaxValue
        {
            get { return braidingMode.BraidingStepMaxValue; }
            private set
            {
                if (braidingMode.BraidingStepMaxValue != value)
                {
                    braidingMode.BraidingStepMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public double BraidingStepMinValue
        {
            get { return braidingMode.BraidingStepMinValue; }
            private set
            {
                if (braidingMode.BraidingStepMinValue != value)
                {
                    braidingMode.BraidingStepMinValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public double BraidingStepOffset
        {
            get { return braidingMode.BraidingStepOffset; }
            private set
            {
                if (braidingMode.BraidingStepOffset != value)
                {
                    braidingMode.BraidingStepOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        public double BraidingCoreDiameter
        {
            get { return braidingMode.BraidingCoreDiameter; }
            set
            {
                if (braidingMode.BraidingCoreDiameter != value)
                {
                    braidingMode.BraidingCoreDiameter = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double BraidingCoreDiameterMaxValue
        {
            get { return braidingMode.BraidingCoreDiameterMaxValue; }
            private set
            {
                if (braidingMode.BraidingCoreDiameterMaxValue != value)
                {
                    braidingMode.BraidingCoreDiameterMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public double BraidingCoreDiameterMinValue
        {
            get { return braidingMode.BraidingCoreDiameterMinValue; }
            private set
            {
                if (braidingMode.BraidingCoreDiameterMinValue != value)
                {
                    braidingMode.BraidingCoreDiameterMinValue = value;
                    OnPropertyChanged();
                }
            }
        }
        public double BraidingCoreDiameterOffset
        {
            get { return braidingMode.BraidingCoreDiameterOffset; }
            private set
            {
                if (braidingMode.BraidingCoreDiameterOffset != value)
                {
                    braidingMode.BraidingCoreDiameterOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        public BraidingViewModel()
        {
            LoadData();
            LoadModel();
            RecalculateParametres += RecalculateBraidingParametres;
            RecalculateParametres();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RecalculateBraidingParametres()
        {
            BraidingDensity = BraidingBuilder.CalculateBraidingDensity(CoilsCount, WiresCount, BraidingStep, BraidingCoreDiameter, WiresDiameter);
            BraidingAngle = BraidingBuilder.CalculateBraidingAngle(BraidingStep, BraidingCoreDiameter, WiresDiameter);
            WiresWeight = BraidingBuilder.CalculateWiresWieght(CoilsCount, WiresCount, WiresDiameter, BraidingAngle, BraidingDensity, WiresMaterial);
        }

        public void SaveModel() => App.JsonRepository.SaveObject(braidingMode, savedModeFileName);

        public void LoadModel() => braidingMode = App.JsonRepository.LoadObject<BraidingMode>(savedModeFileName);

        public void LoadData()
        {
            CoilsCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.CoilsCountCollection");
            WiresCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.WiresCountCollection");
            WiresDiametersCollection = App.JsonRepository.GetObjects<double>(App.dataFileName, @"$.Braiding.WiresDiametersCollection");
            WiresMaterialsCollection = App.JsonRepository.GetObjects<Metal>(App.dataFileName, @"$.Braiding.WiresMaterialsCollection");
        }
    }
}
