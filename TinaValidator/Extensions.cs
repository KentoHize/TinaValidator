using Aritiafel.Artifacts.Calculator;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public static class Extensions
    {
        public static string ForEachToString(this List<ObjectConst> l)
        {
            StringBuilder result = new StringBuilder();
            foreach (ObjectConst o in l)
                result.Append(o);
            return result.ToString();
        }

        public static UnitSet ToUnitSet(this string s, string id = null)
        {
            UnitSet us = new UnitSet(id);
            for (int i = 0; i < s.Length; i++)
                us.Units.Add(new CharUnit(s[i]));
            return us;
        }

        public static List<ObjectConst> ToObjectList(this string s)
            => s.Select(m => new CharConst(m) as ObjectConst).ToList();
    }
}
