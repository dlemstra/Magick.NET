// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick;

internal sealed partial class Bytes
{
    private const int BufferSize = 8192;
    private readonly byte[] _data;
    private readonly int _length;

    private Bytes(byte[] data, int length)
    {
        _data = data;
        _length = length;
    }

    public int Length
        => _length;

    public static Bytes Create(Stream stream, bool allowEmptyStream = false)
    {
        if (allowEmptyStream)
            Throw.IfNull(nameof(stream), stream);
        else
            Throw.IfNullOrEmpty(nameof(stream), stream);

        var data = GetData(stream, out var length);

        return new Bytes(data, length);
    }

    public static Bytes? FromStreamBuffer(Stream stream)
    {
        if (stream is not MemoryStream memStream || memStream.Position != 0)
            return null;

        var data = GetDataFromMemoryStreamBuffer(memStream, out var length);
        if (data is null)
            return null;

        return new Bytes(data, length);
    }

    public byte[] GetData()
        => _data;

    private static byte[] GetData(Stream stream, out int length)
    {
        if (stream is MemoryStream memStream)
            return GetDataFromMemoryStream(memStream, out length);

        Throw.IfFalse(nameof(stream), stream.CanRead, "The stream is not readable.");

        if (stream.CanSeek)
            return GetDataWithSeekableStream(stream, out length);

        int count;
        var buffer = new byte[BufferSize];
        using var tempStream = new MemoryStream();
        while ((count = stream.Read(buffer, 0, BufferSize)) != 0)
        {
            CheckLength(tempStream.Length + count);

            tempStream.Write(buffer, 0, count);
        }

        return GetDataFromMemoryStream(tempStream, out length);
    }

    private static byte[] GetDataWithSeekableStream(Stream stream, out int length)
    {
        CheckLength(stream.Length);

        length = (int)stream.Length;
        var data = new byte[length];

        var read = 0;
        int bytesRead;
        while ((bytesRead = stream.Read(data, read, length - read)) != 0)
        {
            read += bytesRead;
        }

        return data;
    }

    private static byte[] GetDataFromMemoryStream(MemoryStream memStream, out int length)
    {
        var data = GetDataFromMemoryStreamBuffer(memStream, out length);
        if (data is not null)
            return data;

        data = memStream.ToArray();
        length = data.Length;

        return data;
    }

    private static byte[]? GetDataFromMemoryStreamBuffer(MemoryStream memStream, out int length)
    {
        length = 0;

        if (!IsSupportedLength(memStream.Length))
            return null;

        if (!memStream.TryGetBuffer(out var buffer))
            return null;

        if (buffer.Offset == 0)
        {
            length = (int)memStream.Length;
            return buffer.Array;
        }

        return null;
    }

    private static void CheckLength(long length)
        => Throw.IfFalse(nameof(length), IsSupportedLength(length), "Streams with a length larger than {0} are not supported, read from file instead.", int.MaxValue);

    private static bool IsSupportedLength(long length)
        => length <= int.MaxValue;
}
