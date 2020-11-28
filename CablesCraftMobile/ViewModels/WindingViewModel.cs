using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cables.Materials;
using Cables;

namespace CablesCraftMobile
{
    public class WindingViewModel : INotifyPropertyChanged
    {
        private WindingMode windingMode;

        private const string savedModeFileName = "windingMode.json";

        public IList<double> TapesWidthsCollection { get; private set; }
        public IDictionary<string, IList<Tape>> TapesCollections { get; private set; }
        public IList<string> TapesCollectionsNames { get; private set; }

        public double Overlap
        { get => WindingBuider.CalculateWindingOverlap(WindingStep, TapeWidth, WindingCoreDiameter, CurrentTape.Thickness); }

        public double WindingAngle
        { get => WindingBuider.CalculateWindingAngle(WindingStep, WindingCoreDiameter, CurrentTape.Thickness); }

        public double TapeExpenseKilometres
        { get => WindingBuider.CalculateTapeLength(WindingStep, WindingCoreDiameter, CurrentTape.Thickness); }

        public double TapeExpenseSquareMetres
        { get => TapeExpenseKilometres * TapeWidth; }

        public double TapeExpenseKilogrames
        { get => WindingBuider.CalculateTapeWeight(CurrentTape, WindingStep, WindingCoreDiameter, TapeWidth); }

        public IList<Tape> CurrentTapesCollection
        { get => TapesCollections[CurrentTapesCollectionName]; }


        public double TapeWidth
        {
            get { return windingMode.TapeWidth; }
            set
            {
                if (windingMode.TapeWidth != value)
                {
                    windingMode.TapeWidth = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Overlap));
                    OnPropertyChanged(nameof(TapeExpenseSquareMetres));
                    OnPropertyChanged(nameof(TapeExpenseKilogrames));
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WindingAngle));
                    OnPropertyChanged(nameof(Overlap));
                    OnPropertyChanged(nameof(TapeExpenseKilometres));
                    OnPropertyChanged(nameof(TapeExpenseSquareMetres));
                    OnPropertyChanged(nameof(TapeExpenseKilogrames));
                }
            }
        }

        public double WindingStepMaxValue
        {
            get { return windingMode.WindingStepMaxValue; }
            set
            {
                if (windingMode.WindingStepMaxValue != value)
                {
                    windingMode.WindingStepMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WindingStepMinValue
        {
            get { return windingMode.WindingStepMinValue; }
            set
            {
                if (windingMode.WindingStepMinValue != value)
                {
                    windingMode.WindingStepMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WindingStepOffset
        {
            get { return windingMode.WindingStepOffset; }
            set
            {
                if (windingMode.WindingStepOffset != value)
                {
                    windingMode.WindingStepOffset = value;
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WindingAngle));
                    OnPropertyChanged(nameof(Overlap));
                    OnPropertyChanged(nameof(TapeExpenseKilometres));
                    OnPropertyChanged(nameof(TapeExpenseSquareMetres));
                    OnPropertyChanged(nameof(TapeExpenseKilogrames));
                }
            }
        }

        public double WindingCoreDiameterMaxValue
        {
            get { return windingMode.WindingCoreDiameterMaxValue; }
            set
            {
                if (windingMode.WindingCoreDiameterMaxValue != value)
                {
                    windingMode.WindingCoreDiameterMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WindingCoreDiameterMinValue
        {
            get { return windingMode.WindingCoreDiameterMinValue; }
            set
            {
                if (windingMode.WindingCoreDiameterMinValue != value)
                {
                    windingMode.WindingCoreDiameterMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double WindingCoreDiameterOffset
        {
            get { return windingMode.WindingCoreDiameterOffset; }
            set
            {
                if (windingMode.WindingCoreDiameterOffset != value)
                {
                    windingMode.WindingCoreDiameterOffset = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CurrentTapesCollectionName
        {
            get { return windingMode.CurrentTapeCollectionName; }
            set
            {
                if (windingMode.CurrentTapeCollectionName != value)
                {
                    windingMode.CurrentTapeCollectionName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentTapesCollection));
                    CurrentTape = CurrentTapesCollection.First();
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
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(WindingAngle));
                    OnPropertyChanged(nameof(Overlap));
                    OnPropertyChanged(nameof(TapeExpenseKilometres));
                    OnPropertyChanged(nameof(TapeExpenseSquareMetres));
                    OnPropertyChanged(nameof(TapeExpenseKilogrames));
                }
            }
        }

        public WindingViewModel()
        {
            LoadData();
            LoadModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveModel() => App.JsonRepository.SaveObject(windingMode, savedModeFileName);

        public void LoadModel() => windingMode = App.JsonRepository.LoadObject<WindingMode>(savedModeFileName);

        private void LoadData()
        {
            TapesWidthsCollection = App.JsonRepository.GetObjects<double>(App.dataFileName, @"$.Winding.TapesWidthsCollection");
            TapesCollections = App.JsonRepository.GetObjects<string, IList<Tape>>(App.dataFileName, @"$.Winding.TapesCollections");
            TapesCollectionsNames = TapesCollections.Keys.ToList();
        }
    }
}
