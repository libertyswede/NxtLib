using System.IO;
using System.IO.Compression;

namespace NxtLib.Internal
{
    internal class GzipCompressor
    {
        internal byte[] GzipCompress(byte[] data)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(data, 0, data.Length);
                }
                return memoryStream.ToArray();
            }
        }
	
        internal byte[] GzipUncompress(byte[] data)
        {
            const int size = 4096;
            var buffer = new byte[size];

            using (var gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
            using (var memory = new MemoryStream())
            {
                int count;
                do
                {
                    if ((count = gzip.Read(buffer, 0, size)) > 0)
                    {
                        memory.Write(buffer, 0, count);
                    }
                } while (count > 0);

                return memory.ToArray();
            }
        }
    }
}