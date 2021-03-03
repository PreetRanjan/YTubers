using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class ExtensionMethods
    {
        public static string ToCamelCase(this String word)
        {
            return word[0].ToString().ToUpper() + word.Substring(1, word.Length-1);
        }
    }
}
