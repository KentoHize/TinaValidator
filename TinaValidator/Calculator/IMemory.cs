using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{    
    public interface IMemory : IVariableLinker
    {
        bool DeclareVariable(string name, Type type, byte dimensions = 0, int[] counts = null, IObject initialValue = null);
        bool DeleteVariable(string name);
        void ClearVariables();
    }
}
