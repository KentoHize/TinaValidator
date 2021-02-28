using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AnyStringPart : Part
    {
        public List<char> ExcludeChars { get; set; }
        public List<char> EscapeChars { get; set; }
        public List<string> EscapeStrings { get; set; }
        public double RandomEndChance
        {
            get => _RandomEndChance;
            set => _RandomEndChance = value >= 0 && value <= 1 ? value :
            throw new ArgumentOutOfRangeException(nameof(RandomEndChance));
        }
        private double _RandomEndChance;

        public int MinLength
        {
            get => _MinLength;
            set => _MinLength = value > MaxLength || value < 0 ?
                throw new ArgumentOutOfRangeException(nameof(MinLength)) : value;
        }
        private int _MinLength;
        public int MaxLength
        {
            get => _MaxLength;
            set => _MaxLength = MinLength > value || value < 0 ?
                throw new ArgumentOutOfRangeException(nameof(MaxLength)) : value;
        }
        private int _MaxLength;

        public AnyStringPart()
            : this(null, null, new List<char> { '\"' }, new List<char> { '\"', '\\' }, null, 0, 0)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, List<char> escapeChars = null,
            List<char> excludeChars = null, int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, id, escapeChars, excludeChars, null, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, char escapeChar = '\"', List<char> excludeChars = null,
            int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, null, new List<char> { escapeChar }, excludeChars,  null, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, char escapeChar = '\"', char excludeChar = '\"', List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0, int randomEndCharThreshold = 100, int randomEndStringThreshold = 100)
           : this(nextNode, parent, null, new List<char> { escapeChar }, new List<char> { excludeChar }, escapeStrings, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string escapeStrings = null,
           int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, null, null, new List<string> { escapeStrings }, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, string escapeStrings = null,
           int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, id, null, null, new List<string> { escapeStrings }, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, List<char> escapeChars = null, List<char> excludeChars = null, List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, null, escapeChars, excludeChars, escapeStrings, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, char escapeChar = '"', char excludeChar = '"', List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0)
            : this(nextNode, parent, id, new List<char> { escapeChar }, new List<char> { excludeChar }, escapeStrings, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, List<char> escapeChars = null, List<char> excludeChars = null, List<string> escapeStrings = null,
            int minLength = 0, int maxLength = 0, double randomEndChance = 0.1)
            : base(nextNode, parent, id)
        {
            EscapeChars = escapeChars ?? new List<char>();
            ExcludeChars = excludeChars ?? new List<char>();
            EscapeStrings = escapeStrings ?? new List<string>();
            MaxLength = maxLength;
            MinLength = minLength;
            RandomEndChance = randomEndChance;
        }
        private bool MinMaxLengthCheck(string s)
            => s.Length >= MinLength && (MaxLength == 0 || s.Length <= MaxLength);
        public override int Validate(List<object> thing, int startIndex = 0)
        {
            int i;
            if (startIndex == thing.Count)
                return -1;
            else if (thing[startIndex] is string s)
            {
                if (ExcludeChars != null && ExcludeChars.Count != 0)
                    for (i = 0; i < ExcludeChars.Count; i++)
                        if (s.Contains(ExcludeChars[i].ToString()))
                            return -1;
                return MinMaxLengthCheck(s) ? startIndex + 1 : -1;
            }

            StringBuilder sb = new StringBuilder();
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char c))
                    break;
                else if (EscapeChars != null && EscapeChars.Contains(c))
                    break;
                else if (ExcludeChars != null && ExcludeChars.Contains(c))
                    break;
                if(EscapeStrings != null && EscapeStrings.Count != 0)
                    for (int j = 0; j < EscapeStrings.Count; j++)
                        if (sb.ToString().EndsWith(EscapeStrings[j]))
                            return MinMaxLengthCheck(sb.ToString()) ? startIndex + i : -1;
                sb.Append(c);
            }
            return MinMaxLengthCheck(sb.ToString()) ? startIndex + i : -1;
        }

        public override List<object> Random()
        {
            CharUnit cu = new CharUnit();
            char c;
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random((int)DateTime.Now.Ticks);
            do
            {                
                c = (char)cu.Random();
                if(EscapeStrings != null && EscapeStrings.Count != 0)
                {
                    for (int i = 0; i < EscapeStrings.Count; i++)
                    { 
                        if ($"{sb}{c}".EndsWith(EscapeStrings[i]))
                        {
                            if (sb.Length >= MinLength)
                            {
                                sb.Remove(sb.Length - EscapeStrings[i].Length + 1, EscapeStrings[i].Length + 1);
                                break;
                            }
                            else
                                continue;
                        }
                    }
                }
                
                if (EscapeChars != null && EscapeChars.Contains(c))
                    if (sb.Length >= MinLength)
                        break;
                    else
                        continue;
                else if (ExcludeChars != null && ExcludeChars.Contains(c))
                    continue;

                sb.Append(c);

                if (sb.Length >= MinLength)
                    if (rnd.NextDouble() < RandomEndChance)
                        break;
            }
            while (MaxLength == 0 || sb.Length < MaxLength);
            return sb.ToString().ToObjectList();
        }
    }
}
