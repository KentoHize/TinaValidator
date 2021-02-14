using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IUnit
    {
        object Random();
        bool Compare(object b);
    }
}
