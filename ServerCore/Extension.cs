﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace ServerCore
{
    public static class Extension
    {
        static public string ToByteString(this Span<byte> source)
        {
            return $"[{source.Length}]: {string.Join(",", source.ToArray())}";
        }

        static public string ToByteString(this byte[] source)
        {
            return $"[{source.Length}]: {string.Join(",", source)}";
        }

        static public bool IsFileName(this string input)
        {
            if (input.StartsWith(".")) return false;
            if (input.EndsWith(".")) return false;
            return input.IndexOf(".") > 0;
        }
    }
}
