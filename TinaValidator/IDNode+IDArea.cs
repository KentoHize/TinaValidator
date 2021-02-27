using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class IDNode : Status
    {
        public IDNode(string id = "")
            : base(id)
        { }
    }

    public class IDArea : Area
    {
        public IDArea(string name = "")
            : base(name)
        { }
    }
}
