﻿namespace ProgramPro.Client.Extensions
{
    public static class EnumExtensions
    {
        public static T[] GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");
            }
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
