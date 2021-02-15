using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class UnitSet : Sequence
    {
        public string ID { get; set; }
        public UnitSet(string id)
          => ID = id;
    }
}
