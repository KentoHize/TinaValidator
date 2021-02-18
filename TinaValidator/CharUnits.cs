namespace Aritiafel.Artifacts.TinaValidator
{
    public static class CharUnits
    {
        public static CharUnit AtoZ
            => new CharUnit('A', 'Z');
        public static CharUnit atoz
            => new CharUnit('a', 'z');
        public static CharUnit Digit
            => new CharUnit('0', '9');
        public static CharUnit Comma
            => new CharUnit(',');
        public static CharUnit Semicolon
            => new CharUnit(';');
        public static CharUnit Colon
            => new CharUnit(':');
        public static CharUnit ExclamationMark
            => new CharUnit('!');
        public static CharUnit QuestionMark
            => new CharUnit('?');
        public static CharUnit Apostrophe
            => new CharUnit('\'');
        public static CharUnit QuotationMark
            => new CharUnit('\"');
        public static CharUnit Period
            => new CharUnit('.');
        public static CharUnit WhiteSpace
            => new CharUnit(' ');
        public static CharUnit LineFeed
            => new CharUnit('\n');
        public static CharUnit CarriageReturn
            => new CharUnit('\r');
        public static CharUnit HorizontalTab
            => new CharUnit('\t');
        public static CharUnit Backspace
            => new CharUnit('\b');
        public static CharUnit Null
            => new CharUnit('\0');
        public static CharUnit Slash
            => new CharUnit('/');
        public static CharUnit BackSlash
            => new CharUnit('\\');
        public static CharUnit Ellipsis
            => new CharUnit('…');
        public static CharUnit Dash
            => new CharUnit('-');
        public static CharUnit GraveAccent
            => new CharUnit('`');
        public static CharUnit VerticalBar
            => new CharUnit('|');
        public static CharUnit Tilde
            => new CharUnit('~');
        public static CharUnit Underscore
            => new CharUnit('_');
        public static CharUnit AtSign
            => new CharUnit('@');
        public static CharUnit NumberSign
            => new CharUnit('#');
        public static CharUnit DollarSign
            => new CharUnit('$');
        public static CharUnit PercentSign
            => new CharUnit('%');
        public static CharUnit Ampersand
            => new CharUnit('&');
        public static CharUnit EqualSign
            => new CharUnit('=');
        public static CharUnit PlusSign
            => new CharUnit('+');
        public static CharUnit Asterisk
            => new CharUnit('+');
        public static CharUnit GreaterThanSign
            => new CharUnit('>');
        public static CharUnit LessThanSign
            => new CharUnit('<');
        public static CharUnit Caret
            => new CharUnit('^');
        public static CharUnit LeftRoundBracket
            => new CharUnit('(');
        public static CharUnit RightRoundBracket
            => new CharUnit(')');
        public static CharUnit LeftSquareBracket
            => new CharUnit('[');
        public static CharUnit RightSquareBracket
            => new CharUnit(']');
        public static CharUnit LeftCurlyBracket
            => new CharUnit('{');
        public static CharUnit RightCurlyBracket
            => new CharUnit('}');
    }
}
