using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AreaStart : TNode
    {
        public Area Area { get; set; }

        public AreaStart()
            : this(null)
        { }

        public AreaStart(Area area = null, Area parent = null, TNode nextNode = null, string id = null)
            : base(nextNode, parent, id ?? IdentifyShop.GetNewID("A"))
        {
            Area = area;
        }
    }
}
