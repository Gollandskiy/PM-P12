using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Helper
    {
        static char[] chars = { '!', '?', '.', ',' };
        public string Ellipsis(string input, int len)
        {
            //return (len == 5) ? "He..." : "Hel...";
            return input[..(len - 3)]+ "...";
        }
        public string Finalize(string input)
        {
            int len = input.Length;
            return (len > 0 && !chars.Contains(input[len - 1])) ? input += "." : input;
        }
    }
}
