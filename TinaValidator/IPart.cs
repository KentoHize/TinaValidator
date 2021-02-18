using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IPart
    {
        List<object> Random();
        int Validate(List<object> thing, int startIndex = 0);
    }
}
