using System.Collections;
using System.Data.Linq;

namespace SusuCMS
{
    public static class ByteExtensions
    {
        public static BitArray ToBitArray(this byte[] bytes)
        {
            return new BitArray(bytes);
        }
    }
}
