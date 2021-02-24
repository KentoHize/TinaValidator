using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class TNode
    {
        public string ID { get; set; }
        public Area Parent { get; set; }
        public TNode NextNode { get; set; }
        protected TNode(TNode nextNode = null, Area parent = null, string id = null)
        {
            ID = id ?? IdentifyShop.GetNewID();
            Parent = parent;
            NextNode = nextNode;
        }        
    }
}
