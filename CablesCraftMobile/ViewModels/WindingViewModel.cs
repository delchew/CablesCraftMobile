using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cables.Materials;
using System.Collections.ObjectModel;
using Cables.CableCalculations;

namespace CablesCraftMobile
{
    public class WindingViewModel : INotifyPropertyChanged
    {
        private readonly WindingMode windingMode;
        private List<Tape> tapesCollection;
        private Dictionary<string, ObservableCollection<double>> tapesThicknesses;

        public int[] TapesWidthsCollection { get; private set; }
        public List<string> TapesNamesCollection { get; private set; }
        public ObservableCollection<double> CurrentTapeThicknessesCollection { get; private set; }

        public double Overlap
        {
            get { return windingMode.Overlap; }
            private set
            {
                if (windingMode.Overlap != value)
                {
                    windingMode.Overlap = value;
                    OnPropertyChanged("Overlap");
                }
            }
        }

        public double WindingAngle
        {
            get { return windingMode.WindingAngle; }
            private set
            {
                if (windingMode.WindingAngle != value)
                {
                    windingMode.WindingAngle = value;
                    OnPropertyChanged("WindingAngle");
                }
            }
        }

        public double TapeExpenseKilometres
        {
            get { return windingMode.TapeExpenseKilometres; }
            private set
            {
                if (windingMode.TapeExpenseKilometres != value)
                {
                    windingMode.TapeExpenseKilometres = value;
                    OnPropertyChanged("TapeExpenseKilometres");
                }
            }
        }

        public double TapeExpenseSquareMetres
        {
            get { return windingMode.TapeExpenseSquareMetres; }
            private set
            {
                if (windingMode.TapeExpenseSquareMetres != value)
                {
                    windingMode.TapeExpenseSquareMetres = value;
                    OnPropertyChanged("TapeExpenseSquareMetres");
                }
            }
        }

        public double TapeExpenseKilogrames
        {
            get { return windingMode.TapeExpenseKilogrames; }
            private set
            {
                if (windingMode.TapeExpenseKilogrames != value)
                {
                    windingMode.TapeExpenseKilogrames = value;
                    OnPropertyChanged("TapeExpenseKilogrames");
                }
            }
        }

        public double TapeWidth
        {
            get { return windingMode.TapeWidth; }
            set
            {
                if (windingMode.TapeWidth != value)
                {
                    windingMode.TapeWidth = value;
                    RecalculateWindingParametres();
                    OnPropertyChanged("TapeWidth");
                }
            }
        }

        public double TapeThickness
        {
            get { return windingMode.Tape.Thickness; }
            set
            {
                if (windingMode.Tape.Thickness != value)
                {
                    windingMode.Tape = tapesCollection.Where(tape => tape.Thickness == value && tape.Name == TapeName)
                                                      .Single();
                    RecalculateWindingParametres();
                    OnPropertyChanged("TapeThickness");
                }
            }
        }

        public string TapeName
        {
            get { return windingMode.Tape.Name; }
            set
            {
                if (windingMode.Tape.Name != value)
                {
                    windingMode.Tape = tapesCollection.Where(tape => tape.Name == value && tape.Thickness == TapeThickness)
                                                      .Single();
                    RecalculateWindingParametres();
                    OnPropertyChanged("TapeName");
                }
            }
        }

        public double WindingStep
        {
            get { return windingMode.WindingStep; }
            set
            {
                if (windingMode.WindingStep != value)
                {
                    windingMode.WindingStep = value;
                    RecalculateWindingParametres();
                    OnPropertyChanged("WindingStep");
                }
            }
        }

        public double WindingCoreDiameter
        {
            get { return windingMode.WindingCoreDiameter; }
            set
            {
                if (windingMode.WindingCoreDiameter != value)
                {
                    windingMode.WindingCoreDiameter = value;
                    RecalculateWindingParametres();
                    OnPropertyChanged("WindingCoreDiameter");
                }
            }
        }

        public WindingViewModel()
        {
            TapesWidthsCollection = new int[] { 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70 };

            tapesCollection = new List<Tape>
            {
                new Tape
                {
                    Name = "Лавсан",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Лавсан", TapeMaterialDensity = 1400 },
                            Thickness = 15
                        }
                    }
                },
                new Tape
                {
                    Name = "Лавсан",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Лавсан", TapeMaterialDensity = 1400 },
                            Thickness = 30
                        }
                    }
                },
                new Tape
                {
                    Name = "Лавсан",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Лавсан", TapeMaterialDensity = 1400 },
                            Thickness = 50
                        }
                    }
                },
                new Tape
                {
                    Name = "Фольга алюм.",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Алюминий", TapeMaterialDensity = 2699 },
                            Thickness = 30
                        },
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Лавсан", TapeMaterialDensity = 1400 },
                            Thickness = 20
                        }
                    }
                },
                new Tape
                {
                    Name = "Фольга алюм.",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Алюминий", TapeMaterialDensity = 2699 },
                            Thickness = 50
                        },
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Лавсан", TapeMaterialDensity = 1400 },
                            Thickness = 20
                        }
                    }
                },
                new Tape
                {
                    Name = "Lantor 3E5410",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Водоблокирующее волокно", TapeMaterialDensity = 252 },
                            Thickness = 250
                        }
                    }
                },
                new Tape
                {
                    Name = "Lantor 3M1890",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Водоблокирующее волокно, морское", TapeMaterialDensity = 760 },
                            Thickness = 500
                        }
                    }
                },
                new Tape
                {
                    Name = "Лента медн.",
                    TapeLayers = new TapeLayer[]
                    {
                        new TapeLayer
                        {
                            TapeMaterial = new TapeMaterial { Name = "Медь", TapeMaterialDensity = 8890 },
                            Thickness = 500
                        }
                    }
                },
            };

            windingMode = new WindingMode
            {
                Overlap = 20,
                TapeExpenseKilogrames = 20,
                TapeExpenseKilometres = 1.2,
                TapeExpenseSquareMetres = 23,
                TapeWidth = 40,
                WindingAngle = 45,
                WindingStep = 23,
                WindingCoreDiameter = 12,
                Tape = tapesCollection.First()
            };

            TapesNamesCollection = GetTapeNamesCollection(tapesCollection);

            tapesThicknesses = new Dictionary<string, ObservableCollection<double>>(TapesNamesCollection.Count);

            foreach (var tape in tapesCollection)
            {
                if(tapesThicknesses.ContainsKey(tape.Name))
                {
                    tapesThicknesses[tape.Name].Add(tape.Thickness);
                }
                else
                {
                    var tapeThicknesses = new ObservableCollection<double>();
                    tapeThicknesses.Add(tape.Thickness);
                    tapesThicknesses.Add(tape.Name, tapeThicknesses);
                }
            }
            CurrentTapeThicknessesCollection = tapesThicknesses[TapeName];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> GetTapeNamesCollection(List<Tape> tapes)
        {
            var tapesNamesCollection = new List<string>();
            foreach (var tape in tapes)
            {
                if (!tapesNamesCollection.Contains(tape.Name))
                {
                    tapesNamesCollection.Add(tape.Name);
                }
            }
            return tapesNamesCollection;
        }

        private void RecalculateWindingParametres()
        {
            WindingAngle = Calculations.CalculateWindingAngle(WindingStep, WindingCoreDiameter, TapeThickness);
            Overlap = Calculations.CalculateWindingOverlap(WindingStep, TapeWidth, WindingCoreDiameter, TapeThickness);
            TapeExpenseKilometres = Calculations.CalculateTapeLength(WindingStep, WindingCoreDiameter, TapeThickness);
            TapeExpenseSquareMetres = TapeExpenseKilometres * TapeWidth / 1000;
            TapeExpenseKilogrames = Calculations.CalculateTapeWeight(windingMode.Tape, WindingStep, WindingCoreDiameter, TapeWidth, TapeThickness);
        }
    }
}
