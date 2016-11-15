using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonCSharpClassGenerator
{
    enum JsonTypeEnum
    {
        Anything,
        String,
        Boolean,
        Integer,
        Long,
        Float,
        Date,
        NullableInteger,
        NullableLong,
        NullableFloat,
        NullableBoolean,
        NullableDate,
        Object,
        Array,
        Dictionary,
        NullableSomething,
        NonConstrained


    }

    public class StringHelper
    {
        public static string GetCleanText(string str)
        {
            if (str.StartsWith(@"["))
            {
                str = "{\"d\":" + str + "}";
            }

            char[] ch = str.ToCharArray();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ch.Length; i++)
            {
                //如果是\"
                if ((int)ch[i] == 92 && ch[i+1] == 34)
                {
                    i++;
                    continue;
                }

                sb.Append(ch[i]);
            }

            return sb.ToString();
        }
    }
}
