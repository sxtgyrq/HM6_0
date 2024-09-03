using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInput
{
    public class F
    {
        static char[] allChar = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public static string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + DateTime.Now.Millisecond;
            Random random = new Random(num2.GetHashCode());
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next(0, allChar.Length);
                str = string.Format("{0}{1}", str, allChar[num]);
            }
            return str;
        }
    }
}
