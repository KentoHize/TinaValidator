using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class IDNode : TNode
    {
        public IDNode(string id = "")
            : base(null, null, id)
        { }
    }

    public class IDArea : Area
    {
        public IDArea(string name = "")
            : base(name)
        { }
    }
}
