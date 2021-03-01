using System.Collections.Generic;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IPart
    {
        List<ObjectConst> Random();
        int Validate(List<ObjectConst> thing, int startIndex = 0);
    }
}
