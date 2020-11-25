using System.Collections.Generic;
using Cables.Materials;

namespace CablesCraftMobile
{
    public class WindingMode
    {
        public double Overlap { get; set; }

        public double WindingAngle { get; set; }

        public double TapeExpenseKilometres { get; set; }

        public double TapeExpenseKilogrames { get; set; }

        public double TapeExpenseSquareMetres { get; set; }

        public double TapeWidth { get; set; }

        public Tape CurrentTape { get; set; }

        public double WindingStep { get; set; }

        public double WindingCoreDiameter { get; set; }

        public IList<Tape> TapesCollection { get; set; }
    }
}