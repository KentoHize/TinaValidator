using Aritiafel.Locations;
using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public enum VariableSource
    {   
        UserVariable = 0,
        EnvoirmentVariable
    }

    public abstract class Variable : IObject
    {
        public VariableSource Source { get; set; }
        public object Parent { get; set; }
        public string Name { get; set; }
        public List<object> Keys { get; set; }
        public object Tag { get; set; }
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
        protected Variable(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
        {
            Name = name ?? IdentifyShop.GetNewID("Var");
            Parent = parent;
            Keys = keys;
            Source = source;
        }
    }
}
