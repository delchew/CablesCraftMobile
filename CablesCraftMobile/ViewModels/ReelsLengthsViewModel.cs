using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Cables;

namespace CablesCraftMobile
{
    public class ReelsLengthsViewModel : INotifyPropertyChanged
    {
        private readonly ReelsLengthsMode reelsLengthsMode;
        private const string reelsFileName = "reels.json";

        public List<ReelViewModel> Reels
        {
            get { return reelsLengthsMode.ReelsLengths; }
        }

        public double CoreDiameter
        {
            get { return reelsLengthsMode.CoreDiameter; }
            set
            {
                if (reelsLengthsMode.CoreDiameter != value)
                {
                    reelsLengthsMode.CoreDiameter = value;
                    RecalculateReelsLengths();
                    OnPropertyChanged(nameof(CoreDiameter));
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
                    RecalculateReelsLengths();
                    OnPropertyChanged(nameof(EdgeClearance));
                }
            }
        }

        public ReelsLengthsViewModel()
        {
            var filePath = FileProvider.SendResourceFileToLocalApplicationFolder(reelsFileName);
            var jsonRepository = new JsonRepository();
            var reelsList = jsonRepository.GetObjects<Reel>(reelsFileName);
            var reelViewModelsList = new List<ReelViewModel>(reelsList.Count);
            foreach(var reel in reelsList)
            {
                reelViewModelsList.Add(new ReelViewModel(reel));
            }
            reelsLengthsMode = new ReelsLengthsMode
            {
                CoreDiameter = 6,
                EdgeClearance = 5,
                ReelsLengths = reelViewModelsList
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged (string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RecalculateReelsLengths()
        {
            //foreach (var reel in Reels)
            //{
            //    reel.Length = Calculations.CalculateMaxCableLengthOnReel(reel.Diameter, reel.ReelCoreDiameter, reel.Width, EdgeClearance, CoreDiameter);
            //}
        }
    }
}
