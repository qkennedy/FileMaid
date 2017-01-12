using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMaid
{
    public static class StringComparator
    {
        public static double CalcSimularity(string first, string second)
        {
            string[] arr = { alphaNumString(first), alphaNumString(second) };
            string lcs = LCS.GetLongestCommonSubstring(arr);
            return 0.0;
        }

        public static int AbsoluteMatchCounter(List<string> first, List<string> second)
        {
            int count = 0;
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; i++)
                {
                    if (first[i].Equals(second[j]))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static string alphaNumString(string title)
        {
            StringBuilder sb = new StringBuilder();
            char tmp;
            for (int i = 0; i < title.Length; i++)
            {
                tmp = title.ElementAt(i);
                if (Char.IsLetter(tmp))
                {
                    sb.Append("A");
                }
                else if (Char.IsDigit(tmp))
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }
    }
}
