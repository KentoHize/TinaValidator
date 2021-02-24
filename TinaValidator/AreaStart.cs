using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AreaStart : TNode
    {
        public Area Area { get; set; }
        public AreaStart(Area area = null, TNode nextNode = null, Area parent = null, string id = null)
            : base(nextNode, parent, id ?? IdentifyShop.GetNewID("A"))
        {
            Area = area;
        }
    }
}
