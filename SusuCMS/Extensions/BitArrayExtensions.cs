using System.Collections;

namespace SusuCMS
{
    public static class BitArrayExtensions
    {
        public static byte[] ToByteArray(this BitArray value)
        {
            var length = (int)System.Math.Ceiling(value.Count / 8d);
            var list = new byte[length];
            value.CopyTo(list, 0);

            return list;
        }
    }
}
