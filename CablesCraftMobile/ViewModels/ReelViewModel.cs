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
                    OnPropertyChanged("Material");
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
                    OnPropertyChanged("Color");
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
                    OnPropertyChanged("Note");
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
                    OnPropertyChanged("Diameter");
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
                    OnPropertyChanged("ReelCoreDiameter");
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
                    OnPropertyChanged("Width");
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
                    OnPropertyChanged("Length");
                }
            }
        }

        public ReelViewModel(Reel reel)
        {
            this.reel = reel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
