using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TVMemory : IVariableLinker
    {
        public object GetValue(Variable v)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Variable v, IObject value)
        {
            throw new NotImplementedException();
        }

        public TVMemory()
        { }

        public TVMemory(TVMemory memory)
        { 
        
        }
    }
}
