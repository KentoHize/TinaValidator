using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AreaStart : TNode
    {
        public Area Area { get; set; }
        public TNode NextNode { get; set; }
        public AreaStart(Area area = null, TNode nextNode = null, string id = null)
            : base(id)
        {
            Area = area;
            NextNode = nextNode;
        }
    }
}
