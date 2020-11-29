using System.Runtime.CompilerServices;
using System.ComponentModel;
using Cables;

namespace CablesCraftMobile
{
    public class ReelViewModel : INotifyPropertyChanged
    {
        private readonly Reel reel;

        public string Material
        {
            get
            {
                return reel.Material;
            }
            set
            {
                if (reel.Material != value)
                {
                    reel.Material = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Color
        {
            get
            {
                return reel.Color;
            }
            set
            {
                if (reel.Color != value)
                {
                    reel.Color = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Note
        {
            get
            {
                return reel.Note;
            }
            set
            {
                if (reel.Note != value)
                {
                    reel.Note = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Diameter
        {
            get
            {
                return reel.Diameter;
            }
            set
            {
                if (reel.Diameter != value)
                {
                    reel.Diameter = value;
                    OnPropertyChanged();
                }
            }
        }
        public double ReelCoreDiameter
        {
            get
            {
                return reel.ReelCoreDiameter;
            }
            set
            {
                if (reel.ReelCoreDiameter != value)
                {
                    reel.ReelCoreDiameter = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Width
        {
            get
            {
                return reel.Width;
            }
            set
            {
                if (reel.Width != value)
                {
                    reel.Width = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Length
        {
            get
            {
                return reel.Length;
            }
            set
            {
                if (reel.Length != value)
                {
                    reel.Length = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReelViewModel(Reel reel)
        {
            this.reel = reel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
