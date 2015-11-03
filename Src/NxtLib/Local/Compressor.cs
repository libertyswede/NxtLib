using System.IO;
using System.IO.Compression;

internal class Compressor
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
	
	// Untested
	internal byte[] GzipUncompress(byte[] data)
	{
		const int size = 4096;
		byte[] buffer = new byte[size];

		using (GZipStream gzip = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
		using (MemoryStream memory = new MemoryStream())
		{
			int count = 0;
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