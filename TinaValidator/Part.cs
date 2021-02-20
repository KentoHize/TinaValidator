using Aritiafel.Locations;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{

    public abstract class Part : TNode, IPart
    {
        public abstract int Validate(List<object> thing, int startIndex = 0);
        public abstract List<object> Random();
        protected Part(TNode nextNode = null, string id = null)
            : base(nextNode, id ?? IdentifyShop.GetNewID("P"))
        { }
    }
}