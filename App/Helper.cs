using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Helper
    {
        public String CombineUrl(params String[] parts)
        {
            if (parts is null) { throw new NullReferenceException("Parts is null"); }
            if (parts.Length == 0) { throw new ArgumentException("Parts is empty"); }

            StringBuilder result = new();
            string temp;
            bool wasNull = false;
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] is null)
                {
                    wasNull = true;
                    continue;
                }
                if (wasNull)
                {
                    throw new ArgumentException("Non-Null argument after Null one");
                }

                if (parts[i] == "..") { continue; }
                temp = "/" + parts[i].TrimStart('/').TrimEnd('/');

                if ((i != parts.Length - 1) && parts[i + 1] == "..") { continue; }
                result.Append(temp);
            }
            if (result.Length == 0)
            {
                throw new ArgumentException("All arguments are null");
            }
            return result.ToString();

        }
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
