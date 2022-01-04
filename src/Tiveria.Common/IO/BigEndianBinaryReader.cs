using System.Text;
using System.IO;

namespace Tiveria.Common.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class BigEndianBinaryReader : EndianBinaryReader
    {
        public BigEndianBinaryReader(Stream input) : base(input, Endian.Big)
        {
        }

        public BigEndianBinaryReader(string file) : base(file, Endian.Big)
        {
        }

        public BigEndianBinaryReader(byte[] bytes) : base(bytes, Endian.Big)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding) : base(input, encoding, Endian.Big)
        {
        }

        public BigEndianBinaryReader(byte[] bytes, int offset) : base(bytes, offset, Endian.Big)
        {
        }

        public BigEndianBinaryReader(byte[] bytes, int offset, int count) : base(bytes, offset, count, Endian.Big)
        {
        }
    }
}