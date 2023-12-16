using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Helper
    {
        // Обьединяет элементы в строку
        public string CombineUrl(params string[] parts)
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
        static char[] chars = { '!', '?', '.', ',' }; // Массив символов которые должны быть на конце строки
        public String Ellipsis(String input, int len) // Обрезает строку, и дополняет ее конец тремя точками
        {
            if (input == null)
            {
                throw new ArgumentNullException("Null detected in parameter: " + nameof(input));
            }
            if (len < 3)
            {
                throw new ArgumentException("Argument 'len' could not be less than 3");
            }
            if (input.Length < len)
            {
                throw new ArgumentOutOfRangeException("Argument 'len' could not be greater than input length");
            }
            // return "He...";
            // return (len == 5) ? "He..." : "Hel...";
            // return "Hel"[..(len-3)]+"...";
            return input[..(len - 3)] + "...";
        }
        // Добавляет точку в конце если нету
        public string Finalize(string input)
        {
            int len = input.Length;
            return (len > 0 && !chars.Contains(input[len - 1])) ? input += "." : input;
        }

    }
}
