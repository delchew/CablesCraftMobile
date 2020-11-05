using System.ComponentModel;
using Cables.Materials;
using Cables.Braiding;
using System.Linq;

namespace CablesCraftMobile
{
    public class BraidingViewModel : INotifyPropertyChanged
    {
        private readonly BraidingMode braidingMode;
        private readonly string savedModeFileName = "braidingMode.json";

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
                    OnPropertyChanged(nameof(BraidingDensity));
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
                    OnPropertyChanged(nameof(BraidingAngle));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(WiresWeight));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(CoilsCount));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(WiresCount));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(WiresDiameter));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(WiresMaterial));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(BraidingStep));
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
                    RecalculateBraidingParametres();
                    OnPropertyChanged(nameof(BraidingCoreDiameter));
                }
            }
        }

        public BraidingViewModel()
        {
            braidingMode = new BraidingMode();
            LoadData();
            LoadParametres();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateBraidingParametres()
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
            //CoilsCount = coils;
            //WiresCount = wires;
            //WiresDiameter = diam;
            //WiresMaterial = material;
            //BraidingStep = step;
            //BraidingCoreDiameter = corediam;
        }

        private void LoadData()
        {
            CoilsCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.CoilsCountCollection").ToArray();
            WiresCountCollection = App.JsonRepository.GetObjects<int>(App.dataFileName, @"$.Braiding.WiresCountCollection").ToArray();
            WiresDiametersCollection = App.JsonRepository.GetObjects<double>(App.dataFileName, @"$.Braiding.WiresDiametersCollection").ToArray();
            WiresMaterialsCollection = App.JsonRepository.GetObjects<Metal>(App.dataFileName, @"$.Braiding.WiresMaterialsCollection").ToArray();
        }
    }
}
