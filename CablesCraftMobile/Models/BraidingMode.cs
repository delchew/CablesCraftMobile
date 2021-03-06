﻿using Cables.Materials;

namespace CablesCraftMobile
{
    public class BraidingMode
    {
        public int CoilsCount { get; set; }

        public int WiresCount { get; set; }

        public double WiresDiameter { get; set; }

        public Metal WiresMaterial { get; set; }

        public double BraidingStep { get; set; }

        public double BraidingCoreDiameter { get; set; }

        public double BraidingStepMaxValue { get; set; }

        public double BraidingStepMinValue { get; set; }

        public double BraidingStepOffset { get; set; }

        public double BraidingCoreDiameterMaxValue { get; set; }

        public double BraidingCoreDiameterMinValue { get; set; }

        public double BraidingCoreDiameterOffset { get; set; }
    }
}
