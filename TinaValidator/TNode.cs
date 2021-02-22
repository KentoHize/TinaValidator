using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class TNode/* : IJsonObject*/
    {
        public string ID { get; set; }
        public TNode NextNode { get; set; }
        protected TNode(TNode nextNode = null, string id = null)
        {
            ID = id ?? IdentifyShop.GetNewID();
            NextNode = nextNode;
        }
        //public abstract string Serialize();
    }
}
