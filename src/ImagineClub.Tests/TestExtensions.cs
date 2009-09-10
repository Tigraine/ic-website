namespace ImagineClub.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Castle.ActiveRecord;

    public static class TestExtensions
    {
        public static void SaveEach<T>(this IEnumerable<ActiveRecordBase<T>> o)
        {
            foreach (var @base in o)
            {
                @base.SaveAndFlush();
            }
        }

        public static T Second<T> (this IEnumerable<T> enumerable)
        {
            var i = 0;
            foreach (var item in enumerable)
            {
                if (i == 1) return item;
                i++;
            }
            throw new ArgumentException("Enumerable has no second item");
        }

        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var enumerable1 in enumerable)
            {
                action(enumerable1);
            }
        }

        public static string Repeat(this string s, int times)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < times; i++)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }
    }
}