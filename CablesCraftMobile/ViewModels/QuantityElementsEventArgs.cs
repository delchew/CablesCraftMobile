using Cables;
using System;

namespace CablesCraftMobile
{
    public class QuantityElementsChangedEventArgs : EventArgs
    {
        public TwistInfo TwistInfo { get; }
        public QuantityElementsChangedEventArgs(TwistInfo twistInfo)
        {
            TwistInfo = twistInfo;
        }
    }
}
