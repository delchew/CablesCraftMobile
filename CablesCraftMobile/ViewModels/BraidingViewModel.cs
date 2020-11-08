using System.ComponentModel;
using Cables.Materials;
using Cables.Braiding;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CablesCraftMobile
{
    public class BraidingViewModel : INotifyPropertyChanged
    {
        private readonly BraidingMode braidingMode;
        private readonly string savedModeFileName = "braidingMode.json";

        private readonly Action RecalculateParametres;

        public int[] CoilsCountCollection { get; private set; }
        public int[] WiresCountCollection { get; private set; }
        public double[] WiresDiametersCollection { get; private set; }
        public Metal[] WiresMaterialsCollection { get; private set; }

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
                if (!braidingMode.WiresMaterial.Equals(value))
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

        public BraidingViewModel()
        {
            braidingMode = new BraidingMode();
            LoadData();
            LoadParametres();
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

        public void SaveParametres()
        {
            App.JsonRepository.SaveObject((CoilsCount, WiresCount, WiresDiameter, WiresMaterial, BraidingStep, BraidingCoreDiameter), savedModeFileName);
        }

        public void LoadParametres()
        {
            var (coils, wires, diam, material, step, corediam) = App.JsonRepository.LoadObject<(int, int, double, Metal, double, double)>(savedModeFileName);
            CoilsCount = coils;
            WiresCount = wires;
            WiresDiameter = diam;
            WiresMaterial = material;
            BraidingStep = step;
            BraidingCoreDiameter = corediam;
        }

        public void LoadData()
        {
            CoilsCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.CoilsCountCollection").ToArray();
            WiresCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.WiresCountCollection").ToArray();
            WiresDiametersCollection = App.JsonRepository.GetObjects<double>(App.dataFileName, @"$.Braiding.WiresDiametersCollection").ToArray();
            WiresMaterialsCollection = App.JsonRepository.GetObjects<Metal>(App.dataFileName, @"$.Braiding.WiresMaterialsCollection").ToArray();
        }
    }
}
