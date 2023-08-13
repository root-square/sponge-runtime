using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Worker.Extensions
{
    internal static class StringExtensions
    {
        public static IEnumerable<string> Slice(this string text, int size)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (size <= 0)
                throw new ArgumentException("The size of a chunk must be greater than 0.", nameof(size));

            for (var i = 0; i < text.Length; i += size)
                yield return text.Substring(i, Math.Min(size, text.Length - i));
        }

        public static string[] Split(this string text, string seperator)
        {
            return text.Split(new string[1] { seperator }, StringSplitOptions.None);
        }
    }
}
