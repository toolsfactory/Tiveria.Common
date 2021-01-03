
namespace System
{
    public static class ByteExtensions
    {
        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static bool IsBitSet(this int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}