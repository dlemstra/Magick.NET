// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

internal sealed partial class Bytes
{
    public static async Task<Bytes> CreateAsync(Stream stream, CancellationToken cancellationToken)
    {
        Throw.IfNullOrEmpty(nameof(stream), stream);

        var (data, length) = await GetDataAsync(stream, cancellationToken).ConfigureAwait(false);

        return new Bytes(data, length);
    }

    private static async Task<(byte[] Bytes, int Length)> GetDataAsync(Stream stream, CancellationToken cancellationToken)
    {
        int length;
        byte[] bytes;

        if (stream is MemoryStream memStream)
        {
            bytes = GetDataFromMemoryStream(memStream, out length);
            return (bytes, length);
        }

        Throw.IfFalse(nameof(stream), stream.CanRead, "The stream is not readable.");

        if (stream.CanSeek)
            return await GetDataWithSeekableStreamAsync(stream, cancellationToken).ConfigureAwait(false);

        int count;
        var buffer = new byte[BufferSize];
        using var tempStream = new MemoryStream();
        while ((count = await stream.ReadAsync(buffer, 0, BufferSize, cancellationToken).ConfigureAwait(false)) != 0)
        {
            CheckLength(tempStream.Length + count);

            tempStream.Write(buffer, 0, count);
        }

        bytes = GetDataFromMemoryStream(tempStream, out length);
        return (bytes, length);
    }

    private static async Task<(byte[] Bytes, int Length)> GetDataWithSeekableStreamAsync(Stream stream, CancellationToken cancellationToken)
    {
        CheckLength(stream.Length);

        var length = (int)stream.Length;
        var data = new byte[length];

        var read = 0;
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(data, read, length - read, cancellationToken).ConfigureAwait(false)) != 0)
        {
            read += bytesRead;
        }

        return (data, length);
    }
}
