namespace Aritiafel.Artifacts.TinaValidator
{
    public static class StringUnitSetBuilder
    {
        public static UnitSet ToUnitSet(this string s, string id)
        {
            UnitSet us = new UnitSet(id);
            for (int i = 0; i < s.Length; i++)
                us.Units.Add(new CharUnit(s[i]));
            return us;
        }
    }
}
