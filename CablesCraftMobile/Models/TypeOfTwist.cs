using Cables;

namespace CablesCraftMobile
{
    public class TypeOfTwist
    {
        public string Name { get; set; }
        public TwistedElementType TwistedElementType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is TypeOfTwist typeOfTwist)
            {
                return Name == typeOfTwist.Name &&
                       TwistedElementType == typeOfTwist.TwistedElementType;
            }
            return false;
        }
        public override int GetHashCode()
        {
            var hash = 19;
            hash = hash * 37 + Name.GetHashCode();
            hash = hash * 37 + TwistedElementType.GetHashCode();
            return hash;

        }
    }
}
