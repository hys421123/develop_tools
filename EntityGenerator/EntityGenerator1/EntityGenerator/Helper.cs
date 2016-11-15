using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

namespace EntityGenerator
{
    public class StringHelper
    {
        public static string GetCleanText(string str)
        {
            char[] ch = str.ToCharArray();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ch.Length; i++)
            {
                //如果是\"
                if ((int)ch[i] == 92)
                    continue;

                sb.Append(ch[i]);
            }

            return sb.ToString();
        }

        public static string ToTitleCase(string str)
        {
            var sb = new StringBuilder(str.Length);
            var flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(flag ? char.ToUpper(c) : c);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return sb.ToString();
        }

    }
}