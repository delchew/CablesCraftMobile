using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using Cables;
using Cables.Common;

namespace CablesCraftMobile
{
    public class ReelsLengthsViewModel : INotifyPropertyChanged
    {
        private ReelsLengthsMode reelsLengthsMode;

        private const string reelsFileName = "reels.json";
        private readonly string savedModeFileName = "reelsLengthsMode.json";

        private readonly Action RecalculateParametres;

        public ObservableCollection<ReelViewModel> ReelViewModelsList { get; private set; }

        public double CoreDiameter
        {
            get { return reelsLengthsMode.CoreDiameter; }
            set
            {
                if (reelsLengthsMode.CoreDiameter != value)
                {
                    reelsLengthsMode.CoreDiameter = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double CoreDiameterMaxValue
        {
            get { return reelsLengthsMode.CoreDiameterMaxValue; }
            set
            {
                if (reelsLengthsMode.CoreDiameterMaxValue != value)
                {
                    reelsLengthsMode.CoreDiameterMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double CoreDiameterMinValue
        {
            get { return reelsLengthsMode.CoreDiameterMinValue; }
            set
            {
                if (reelsLengthsMode.CoreDiameterMinValue != value)
                {
                    reelsLengthsMode.CoreDiameterMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double CoreDiameterOffset
        {
            get { return reelsLengthsMode.CoreDiameterOffset; }
            set
            {
                if (reelsLengthsMode.CoreDiameterOffset != value)
                {
                    reelsLengthsMode.CoreDiameterOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        public double EdgeClearance
        {
            get { return reelsLengthsMode.EdgeClearance; }
            set
            {
                if (reelsLengthsMode.EdgeClearance != value)
                {
                    reelsLengthsMode.EdgeClearance = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

        public double EdgeClearanceMaxValue
        {
            get { return reelsLengthsMode.EdgeClearanceMaxValue; }
            set
            {
                if (reelsLengthsMode.EdgeClearanceMaxValue != value)
                {
                    reelsLengthsMode.EdgeClearanceMaxValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double EdgeClearanceMinValue
        {
            get { return reelsLengthsMode.EdgeClearanceMinValue; }
            set
            {
                if (reelsLengthsMode.EdgeClearanceMinValue != value)
                {
                    reelsLengthsMode.EdgeClearanceMinValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double EdgeClearanceOffset
        {
            get { return reelsLengthsMode.EdgeClearanceOffset; }
            set
            {
                if (reelsLengthsMode.EdgeClearanceOffset != value)
                {
                    reelsLengthsMode.EdgeClearanceOffset = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReelsLengthsViewModel()
        {
            LoadData();
            LoadModel();
            RecalculateParametres += RecalculateReelsLengths;
            RecalculateParametres();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateReelsLengths()
        {
            foreach (var reel in ReelViewModelsList)
                reel.Length = CableCalculations.CalculateMaxCableLengthOnReel(reel.Diameter, reel.ReelCoreDiameter, reel.Width, EdgeClearance, CoreDiameter);
        }

        public void SaveModel() => App.JsonRepository.SaveObject(reelsLengthsMode, savedModeFileName);

        public void LoadModel() => reelsLengthsMode = App.JsonRepository.LoadObject<ReelsLengthsMode>(savedModeFileName);

        private void LoadData()
        {
            var reelsList = App.JsonRepository.GetObjects<Reel>(reelsFileName);
            ReelViewModelsList = new ObservableCollection<ReelViewModel>();
            foreach (var reel in reelsList)
                ReelViewModelsList.Add(new ReelViewModel(reel));
        }
    }
}
