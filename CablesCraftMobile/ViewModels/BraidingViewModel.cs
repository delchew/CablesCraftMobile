using System.ComponentModel;
using Cables.Materials;
using Cables.CableCalculations;
using Cables;

namespace CablesCraftMobile
{
    public class BraidingViewModel : INotifyPropertyChanged
    {
        private readonly BraidingMode braidingMode;

        public int[] CoilsCountCollection { get; private set; }
        public int[] WiresCountCollection { get; private set; }
        public double [] WiresDiametersCollection { get; private set; }
        public Metal[] WiresMaterialsCollection { get; private set; }

        public double BraidingDensity
        {
            get { return braidingMode.BraidingDensity; }
            private set
            {
                if (braidingMode.BraidingDensity != value)
                {
                    braidingMode.BraidingDensity = value;
                    OnPropertyChanged("BraidingDensity");
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
                    OnPropertyChanged("BraidingAngle");
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
                    OnPropertyChanged("WiresWeight");
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
                    OnPropertyChanged("CoilsCount");
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
                    OnPropertyChanged("WiresCount");
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
                    OnPropertyChanged("WiresDiameter");
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
                    OnPropertyChanged("WiresMaterial");
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
                    OnPropertyChanged("BraidingStep");
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
                    OnPropertyChanged("BraidingCoreDiameter");
                }
            }
        }

        public BraidingViewModel()
        {
            braidingMode = new BraidingMode
            {
                BraidingCoreDiameter = 10,
                BraidingStep = 50,
                CoilsCount = 16,
                WiresDiameter = 0.15,
                WiresCount = 7,
            };

            CoilsCountCollection = new int[] { 16, 24, 36 };
            WiresCountCollection = new int[] { 3, 4, 5, 6, 7, 8, 9, 10 };
            WiresDiametersCollection = new double[] { 0.12, 0.15, 0.20, 0.26, 0.30 };
            WiresMaterialsCollection = new Metal[]
            {
                new Metal { Name = "Медь", ElectricalResistance20 = 56, Density20 = 8890 },
                new Metal { Name ="Сталь", ElectricalResistance20 = 70, Density20 = 7670 }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateBraidingParametres()
        {
            BraidingDensity = Calculations.CalculateBraidingDensity(CoilsCount, WiresCount, BraidingStep, BraidingCoreDiameter, WiresDiameter);
            BraidingAngle = Calculations.CalculateBraidingAngle(BraidingStep, BraidingCoreDiameter, WiresDiameter);
            WiresWeight = Calculations.CalculateWiresWieght(CoilsCount, WiresCount, WiresDiameter, BraidingAngle, BraidingDensity, WiresMaterial);
        }
    }
}
