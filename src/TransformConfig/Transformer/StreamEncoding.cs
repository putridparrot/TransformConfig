using System.Text;
using TransformConfig.Common;

namespace TransformConfig.Transformer;

public class StreamEncoding
{
    public static Encoding GetEncoding(string? filename)
    {
        Validate.ThrowIfNullOrWhitespace(filename);

#pragma warning disable CS8604 // Possible null reference argument.
        using var file = new FileStream(filename, FileMode.Open, FileAccess.Read);
#pragma warning restore CS8604 // Possible null reference argument.
        return GetEncoding(file);
    }

    private static Encoding GetEncoding(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        var buffer = new byte[4];
        var read = stream.Read(buffer, 0, 4);
        if (read < 4)
        {
            return GetCurrentEncoding(stream);
        }

        if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
        {
#pragma warning disable SYSLIB0001
            return Encoding.UTF7;
#pragma warning restore SYSLIB0001
        }

        if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
        {
            return Encoding.UTF8;
        }

        if (buffer[0] == 0xff && buffer[1] == 0xfe)
        {
            return Encoding.Unicode; // UTF-16LE
        }

        if (buffer[0] == 0xfe && buffer[1] == 0xff)
        {
            return Encoding.BigEndianUnicode; // UTF-16BE
        }

        if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
        {
            return Encoding.UTF32;
        }

        return GetCurrentEncoding(stream);
    }

    private static Encoding GetCurrentEncoding(Stream stream)
    {
        using var reader = new StreamReader(stream, true);
        stream.Position = 0;
        reader.Peek();
        return reader.CurrentEncoding;
    }
}