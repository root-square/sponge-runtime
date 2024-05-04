﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Converter.Extensions
{
    /// <summary>
    /// String extensions utility class.
    /// </summary>
    public static class StringExtensions
    {
        public static string RemoveSuffix(this string self, char toRemove) => string.IsNullOrEmpty(self) ? self : (self.EndsWith(toRemove) ? self.Substring(0, self.Length - 1) : self);
        public static string RemoveSuffix(this string self, string toRemove) => string.IsNullOrEmpty(self) ? self : (self.EndsWith(toRemove) ? self.Substring(0, self.Length - toRemove.Length) : self);
        public static string RemoveWhiteSpace(this string self) => string.IsNullOrEmpty(self) ? self : new string(self.Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }
}
