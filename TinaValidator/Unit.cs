using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Unit : Sequence
    {
        public string ID { get; set; }
        protected Unit(string id)
            => ID = id;
    }
}
