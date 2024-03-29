﻿
using System;
using System.Text;

namespace Tiveria.Common.Extensions
{
    public static class ByteArrayExtensions
    { 
        public static string ToHex(this byte[] data, string spacer = "")
        {
            var str = BitConverter.ToString(data);
            if(spacer!="-") 
                str = str.Replace("-", spacer);
            return str;
        }

        public static void Slice(this byte[] source, byte[] destination, int srcindex, int destindex, int length)
        {
            if (srcindex + length > source.Length)
                throw new ArgumentOutOfRangeException("Source array too small");
            if (destindex + length > destination.Length)
                throw new ArgumentOutOfRangeException("Destination array too small");

            //for (var i = 0; i < length; i++)
            //    destination[destindex + i] = source[srcindex + i];
            Array.Copy(source, srcindex, destination, destindex, length);
        }

        public static byte[] Merge(this byte[] source, byte[] with, int withlen = -1)
        {
            if (withlen == -1)
                withlen = with.Length;

            var newbuf = new byte[source.Length + withlen];

            //for (var i = 0; i < length; i++)
            //    destination[destindex + i] = source[srcindex + i];
            Array.Copy(source, 0, newbuf, 0, source.Length);
            Array.Copy(with, 0, newbuf, source.Length, withlen);
            return newbuf;
        }

        public static byte[] Clone(this byte[] source, int offset, int length=-1)
        {
            if (offset > source.Length || offset < 0)
                throw new ArgumentOutOfRangeException("invalid offset");
            if (length == -1)
                length = source.Length;
            var len = Math.Min(length, source.Length - offset);
            var result = new byte[len];
            Array.Copy(source, offset, result, 0, len);
            return result;
        }

        public static string ToHexView(this byte[] bytes, int start, int len)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (len <= 0)
                return "";
            if (len > bytes.Length)
                return "invalid length";
            if (start < 0 || start + len > bytes.Length)
                return "invalid start&length";

            var sb = new StringBuilder(len * 4);
            var sb_hex = new StringBuilder(16 * 3);
            var sb_txt = new StringBuilder(16);

            var div = Math.DivRem(len, 16, out var rem);
            var lines = (rem == 0) ? div : div + 1;
            var pos = start;

            for (var line = 0; line < lines; line++)
            {
                var linepos = 0;
                while (linepos < 16)
                {
                    if (pos < len)
                    {
                        var b = bytes[pos];
                        sb_hex.AppendFormat("{0:x2} ", b);
                        if (b < 32)
                            sb_txt.Append(" ");
                        else
                            sb_txt.Append((char)b);
                    }
                    else
                    {
                        sb_hex.Append(".. ");
                        sb_txt.Append(" ");
                    }
                    pos++;
                    linepos++;
                }
                sb.Append(sb_hex).Append(" |  ").Append(sb_txt).Append(Environment.NewLine);
                sb_hex.Clear();
                sb_txt.Clear();
            }
            return sb.ToString();
        }
        public static string ToHexView(this byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            return bytes.ToHexView(0, bytes.Length);
        }
    }
}

