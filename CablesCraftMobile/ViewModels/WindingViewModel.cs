using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System;
using Cables.Materials;
using Cables;

namespace CablesCraftMobile
{
    public class WindingViewModel : INotifyPropertyChanged
    {
        private readonly WindingMode windingMode;

        private const string savedModeFileName = "windingMode.json";

        public IList<double> TapesWidthsCollection { get; private set; }
        public IDictionary<string, IList<Tape>> TapesCollections { get; private set; }
        public IList<string> TapesCollectionsNames { get; private set; }

        private readonly Action RecalculateParametres;

        public double Overlap
        {
            get { return windingMode.Overlap; }
            private set
            {
                if (windingMode.Overlap != value)
                {
                    windingMode.Overlap = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
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
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
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
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        private string currentTapeCollectionName;
        public string CurrentTapesCollectionName
        {
            get { return currentTapeCollectionName; }
            set
            {
                if (currentTapeCollectionName != value)
                {
                    currentTapeCollectionName = value;
                    CurrentTapesCollection = TapesCollections[currentTapeCollectionName];
                    OnPropertyChanged();
                }
            }
        }

        public IList<Tape> CurrentTapesCollection
        {
            get { return windingMode.TapesCollection; }
            set
            {
                if (windingMode.TapesCollection != value)
                {
                    windingMode.TapesCollection = value;
                    OnPropertyChanged();
                    CurrentTape = value[0];
                }
            }
        }

        public Tape CurrentTape
        {
            get { return windingMode.CurrentTape; }
            set
            {
                if (!windingMode.CurrentTape.Equals(value))
                {
                    windingMode.CurrentTape = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public WindingViewModel()
        {
            windingMode = new WindingMode();
            LoadData();
            LoadParametres();
            RecalculateParametres += RecalculateWindingParametres;
            RecalculateParametres();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateWindingParametres()
        {
            WindingAngle = WindingBuider.CalculateWindingAngle(WindingStep, WindingCoreDiameter, CurrentTape.Thickness);
            Overlap = WindingBuider.CalculateWindingOverlap(WindingStep, TapeWidth, WindingCoreDiameter, CurrentTape.Thickness);
            TapeExpenseKilometres = WindingBuider.CalculateTapeLength(WindingStep, WindingCoreDiameter, CurrentTape.Thickness);
            TapeExpenseSquareMetres = TapeExpenseKilometres * TapeWidth;
            TapeExpenseKilogrames = WindingBuider.CalculateTapeWeight(CurrentTape, WindingStep, WindingCoreDiameter, TapeWidth);
        }

        public void SaveParametres()
        {
            App.JsonRepository.SaveObject((TapeWidth, WindingStep, WindingCoreDiameter, CurrentTapesCollectionName, CurrentTape), savedModeFileName);
        }

        public void LoadParametres()
        {
            var (width, step, coreDiameter, collectionName, tape) = App.JsonRepository.LoadObject<(double, double, double, string, Tape)>(savedModeFileName);
            TapeWidth = width;
            WindingStep = step;
            WindingCoreDiameter = coreDiameter;
            CurrentTapesCollectionName = collectionName;
            CurrentTape = tape;
        }

        private void LoadData()
        {
            TapesWidthsCollection = App.JsonRepository.GetObjects<double>(App.dataFileName, @"$.Winding.TapesWidthsCollection");
            TapesCollections = App.JsonRepository.GetObjects<string, IList<Tape>>(App.dataFileName, @"$.Winding.TapesCollections");
            TapesCollectionsNames = TapesCollections.Keys.ToList();
        }
    }
}
