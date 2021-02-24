using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AnyStringPart : Part
    {
        public List<char> EscapeChars { get; set; }
        public List<string> EscapeStrings { get; set; }
        public int RandomEndCharThreshold { get; set; }
        public int RandomEndStringThreshold { get; set; }
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
            : this(null, null, '"', 0, 0)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, List<char> escapeChars = null,
            int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, id, escapeChars, null, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, char escapeChar = '"',
            int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, null, new List<char> { escapeChar }, null, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, char escapeChar = '"', List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0, int randomEndCharThreshold = 100, int randomEndStringThreshold = 100)
           : this(nextNode, parent, null, new List<char> { escapeChar }, escapeStrings, minLength, maxLength, randomEndCharThreshold, randomEndStringThreshold)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string escapeStrings = null,
           int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, null, new List<string> { escapeStrings }, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, string escapeStrings = null,
           int minLength = 0, int maxLength = 0)
           : this(nextNode, parent, id, null, new List<string> { escapeStrings }, minLength, maxLength)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, List<char> escapeChars = null, List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0, int randomEndCharThreshold = 100, int randomEndStringThreshold = 100)
           : this(nextNode, parent, null, escapeChars, escapeStrings, minLength, maxLength, randomEndCharThreshold, randomEndStringThreshold)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, char escapeChar = '"', List<string> escapeStrings = null,
           int minLength = 0, int maxLength = 0, int randomEndCharThreshold = 100, int randomEndStringThreshold = 100)
            : this(nextNode, parent, id, new List<char> { escapeChar }, escapeStrings, minLength, maxLength, randomEndCharThreshold, randomEndStringThreshold)
        { }

        public AnyStringPart(TNode nextNode = null, Area parent = null, string id = null, List<char> escapeChars = null, List<string> escapeStrings = null,
            int minLength = 0, int maxLength = 0, int randomEndCharThreshold = 100, int randomEndStringThreshold = 100,
            double randomEndChance = 0.1)
            : base(nextNode, parent, id)
        {
            EscapeChars = escapeChars ?? new List<char>();
            EscapeStrings = escapeStrings ?? new List<string>();
            MaxLength = maxLength;
            MinLength = minLength;
            RandomEndStringThreshold = randomEndStringThreshold;
            RandomEndCharThreshold = randomEndCharThreshold;
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
                return MinMaxLengthCheck(s) ? startIndex + 1 : -1;

            StringBuilder sb = new StringBuilder();
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char c))
                    break;
                else if (EscapeChars.Contains(c))
                    break;
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
                for (int i = 0; i < EscapeStrings.Count; i++)
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
                if (EscapeChars.Contains(c))
                {
                    if (sb.Length >= MinLength)
                        break;
                    else
                        continue;
                }
                sb.Append(c);

                ////Escape String Appear
                //if (sb.Length >= RandomEndStringThreshold && EscapeStrings.Count != 0)
                //{
                //    if (rnd.Next(20) == 0)
                //    {
                //        int r = rnd.Next(EscapeStrings.Count);
                //        if (EscapeStrings[r].Length + sb.Length <= MaxLength || MaxLength == 0)
                //        {
                //            sb.Append(EscapeStrings[r]);
                //            break;
                //        }
                //    }
                //}

                ////Escape Char Appear
                //if (sb.Length >= RandomEndCharThreshold && EscapeChars.Count != 0)
                //{
                //    if (rnd.Next(20) == 0 && (MaxLength == 0 || MaxLength >= sb.Length + 1))
                //    {
                //        sb.Append(EscapeChars[rnd.Next(EscapeChars.Count)]);
                //        break;
                //    }
                //}

                if (sb.Length >= MinLength)
                    if (rnd.NextDouble() < RandomEndChance)
                        break;
            }
            while (MaxLength == 0 || sb.Length < MaxLength);
            return sb.ToString().ToObjectList();
        }
    }
}
