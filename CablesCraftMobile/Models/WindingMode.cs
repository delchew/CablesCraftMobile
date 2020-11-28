using Cables.Materials;

namespace CablesCraftMobile
{
    public class WindingMode
    {
        public double TapeWidth { get; set; }

        public double WindingStep { get; set; }

        public double WindingStepMaxValue { get; set; }

        public double WindingStepMinValue { get; set; }

        public double WindingStepOffset { get; set; }

        public double WindingCoreDiameter { get; set; }

        public double WindingCoreDiameterMaxValue { get; set; }

        public double WindingCoreDiameterMinValue { get; set; }

        public double WindingCoreDiameterOffset { get; set; }

        public string CurrentTapeCollectionName { get; set; }

        public Tape CurrentTape { get; set; }
    }
}