using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject
{
    internal static class TechTypeUtilities
    {
        public static TechType AsEnum(this string str)
        {
            return AsEnum<TechType>(str);
        }
        public static T AsEnum<T>(this string str) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), str, true);

        }
    }
}
