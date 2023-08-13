using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Worker.Extensions
{
    internal static class ByteExtensions
    {
        public static string ByteToHexadecimalString(this byte[] byteArray)
        {
            string result = string.Empty;

            foreach (byte c in byteArray)
                result += c.ToString("x2").ToUpper();

            return result;
        }

        public static byte ReadByte(this byte[] byteArray, int offset)
        {
            byte[] skipped = byteArray.Skip(offset).ToArray();
            return skipped[0];
        }

        public static byte[] ReadByte(this byte[] byteArray, int offset, int count)
        {
            byte[] skipped = byteArray.Skip(offset).ToArray();
            byte[] result = new byte[count];

            Array.Copy(skipped, 0, result, 0, count);

            return result;
        }
    }
}
