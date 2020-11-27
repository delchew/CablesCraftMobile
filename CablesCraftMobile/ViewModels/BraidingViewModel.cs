using System.ComponentModel;
using Cables.Materials;
using Cables.Braiding;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace CablesCraftMobile
{
    public class BraidingViewModel : INotifyPropertyChanged
    {
        private BraidingMode braidingMode;
        private readonly string savedModeFileName = "braidingMode.json";

        public IList<int> CoilsCountCollection { get; private set; }
        public IList<int> WiresCountCollection { get; private set; }
        public IList<double> WiresDiametersCollection { get; private set; }
        public IList<Metal> WiresMaterialsCollection { get; private set; }

        public double BraidingDensity
        {
            get => BraidingBuilder.CalculateBraidingDensity(CoilsCount, WiresCount, BraidingStep, BraidingCoreDiameter, WiresDiameter);
        }

        public double BraidingAngle
        {
            get => BraidingBuilder.CalculateBraidingAngle(BraidingStep, BraidingCoreDiameter, WiresDiameter);
        }

        public double WiresWeight
        {
            get => BraidingBuilder.CalculateWiresWieght(CoilsCount, WiresCount, WiresDiameter, BraidingAngle, BraidingDensity, WiresMaterial);

        }

        public int CoilsCount
        {
            get { return braidingMode.CoilsCount; }
            set
            {
                if (braidingMode.CoilsCount != value)
                {
                    braidingMode.CoilsCount = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BraidingDensity));
                    OnPropertyChanged(nameof(WiresWeight));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BraidingDensity));
                    OnPropertyChanged(nameof(WiresWeight));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BraidingDensity));
                    OnPropertyChanged(nameof(BraidingAngle));
                    OnPropertyChanged(nameof(WiresWeight));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WiresWeight));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BraidingDensity));
                    OnPropertyChanged(nameof(BraidingAngle));
                    OnPropertyChanged(nameof(WiresWeight));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BraidingDensity));
                    OnPropertyChanged(nameof(BraidingAngle));
                    OnPropertyChanged(nameof(WiresWeight));
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
