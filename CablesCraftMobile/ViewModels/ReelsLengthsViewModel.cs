using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using Cables;

namespace CablesCraftMobile
{
    public class ReelsLengthsViewModel : INotifyPropertyChanged
    {
        private readonly ReelsLengthsMode reelsLengthsMode;

        private const string reelsFileName = "reels.json";
        private readonly string savedModeFileName = "reelsLengthsMode.json";

        private readonly Action RecalculateParametres;

        public List<ReelViewModel> Reels
        {
            get { return reelsLengthsMode.ReelsLengths; }
            private set
            {
                if (reelsLengthsMode.ReelsLengths != value)
                {
                    reelsLengthsMode.ReelsLengths = value;
                    RecalculateParametres?.Invoke();
                    OnPropertyChanged();
                }
            }
        }

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

        public ReelsLengthsViewModel()
        {
            reelsLengthsMode = new ReelsLengthsMode() { EdgeClearance = -1 };
            LoadData();
            LoadParametres();
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
            foreach (var reel in Reels)
            {
                reel.Length = CableCalculations.CalculateMaxCableLengthOnReel(reel.Diameter, reel.ReelCoreDiameter, reel.Width, EdgeClearance, CoreDiameter);
            }
        }

        public void SaveParametres()
        {
            App.JsonRepository.SaveObject<(double, double)>((CoreDiameter, EdgeClearance), savedModeFileName);
        }

        public void LoadParametres()
        {
            var (diameter, clearance) = App.JsonRepository.LoadObject<(double, double)>(savedModeFileName);
            CoreDiameter = diameter;
            EdgeClearance = clearance;
        }

        public void LoadData()
        {
            var reelsList = App.JsonRepository.GetObjects<Reel>(reelsFileName);
            var reelViewModelsList = new List<ReelViewModel>(reelsList.Count);
            foreach (var reel in reelsList)
            {
                reelViewModelsList.Add(new ReelViewModel(reel));
            }
            Reels = reelViewModelsList;
        }
    }
}
