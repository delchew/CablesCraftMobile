using System.ComponentModel;
using System.Reflection;
using Cables;

namespace CablesCraftMobile
{
    public class TwistMode
    {
        public TwistInfo TwistInfo { get; set; }

        public double TwistStep { get; set; }

        public double TwistedCoreDiameter { get; set; }

        public double TwistedElementDiameter { get; set; }

        private TwistedElementType twistedElementType;
        public TypeOfTwist TypeOfTwist
        {
            get
            {
                var name = twistedElementType.GetType()
                                             .GetMember(twistedElementType.ToString())[0]
                                             .GetCustomAttribute<DescriptionAttribute>()
                                             .Description;
                return new TypeOfTwist
                {
                    Name = name,
                    TwistedElementType = twistedElementType
                };
            }
            set { twistedElementType = value.TwistedElementType; }
        }
    }
}
