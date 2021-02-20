using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AnyStringPart : Part
    {   
        public List<char> EscapeChar { get; set; }
        public List<string> EscapeString { get; set; }
        public int MinLength { 
            get => _MinLength;
            set => _MinLength = MinLength > MaxLength || MinLength < 0 ?
                throw new ArgumentOutOfRangeException() : value;
             }
        private int _MinLength;
        public int MaxLength
        {
            get => _MaxLength;
            set => _MaxLength = MinLength > MaxLength || MaxLength < 0 ?
                throw new ArgumentOutOfRangeException() : value;
        }
        private int _MaxLength;
        public int RandomEndStringThreshold { get; set; } = 100;
        public int RandomEndCharThreshold { get; set; } = 100;

        private bool MinMaxLengthCheck(string s)
            => s.Length >= MinLength && (MaxLength == 0 || s.Length <= MaxLength);

        public override int Validate(List<object> thing, int startIndex = 0)
        {
            int i;
            if (startIndex == thing.Count)
                return -1;
            else if (thing[startIndex] is string s)
                if (s.Length >= MinLength && (MaxLength == 0 || s.Length <= MaxLength))
                    return MinMaxLengthCheck(s) ? startIndex + 1 : -1;

            StringBuilder sb = new StringBuilder();
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char c))
                    break;
                else if (EscapeChar.Contains(c))
                    break;
                sb.Append(c);
                for (int j = 0; j < EscapeString.Count; j++)
                    if (sb.ToString().EndsWith(EscapeString[j]))
                        return MinMaxLengthCheck(sb.ToString()) ? startIndex + 1 : -1;
            }
            return MinMaxLengthCheck(sb.ToString()) ? startIndex + 1 : -1;
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
                for (int i = 0; i < EscapeString.Count; i++)
                    if ($"{sb}{c}".EndsWith(EscapeString[i]))
                        if (sb.Length >= MinLength)
                            break;
                        else
                            continue;
                if (EscapeChar.Contains(c))
                    if (sb.Length >= MinLength)
                        break;
                    else
                        continue;

                sb.Append(c);

                //Escape String 出現
                if (sb.Length >= RandomEndStringThreshold)
                { 
                    if(rnd.Next(20) == 0 && MaxLength == 0)
                    {
                        int r = rnd.Next(EscapeString.Count);
                        if (EscapeString[r].Length + sb.Length <= MaxLength)
                            sb.Append(EscapeString[r]);
                    }
                }

                //Escape Char 出現
                if (sb.Length >= RandomEndCharThreshold)
                    if (rnd.Next(20) == 0 && MaxLength == 0 || MaxLength >= sb.Length + 1)
                        sb.Append(EscapeChar[rnd.Next(EscapeChar.Count)]);
            }
            while (MaxLength == 0 || sb.Length < MaxLength);
            return sb.ToString().ToObjectList();
        } 
    } 
}
