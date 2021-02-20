using System;
using System.Collections.Generic;
using System.Text;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class TNode
    {
        public string ID { get; set; }
        public TNode NextNode { get; set; }
        protected TNode(TNode nextNode = null, string id = null)
        {
            ID = id ?? IdentifyShop.GetNewID();
            NextNode = nextNode;
        }
    }
}
