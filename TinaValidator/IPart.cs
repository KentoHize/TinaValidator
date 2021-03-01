using System.Collections.Generic;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IPart
    {
        List<ObjectConst> Random(IVariableLinker vl = null);
        int Validate(List<ObjectConst> thing, int startIndex = 0, IVariableLinker vl = null);
    }
}
