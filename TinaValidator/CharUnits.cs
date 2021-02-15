using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public static class CharUnits
    {
        public static CharUnit CharAtoZ
            => new CharUnit('A', 'Z');

        public static CharUnit Comma
            => new CharUnit(',');

        public static CharUnit Period
            => new CharUnit('.');

        public static CharUnit EndOfLine
            => new CharUnit('\n');

        public static CharUnit WhiteSpace
            => new CharUnit(' ');
        //public static CharUnit 
    }
}
