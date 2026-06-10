// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick;

internal sealed class StreamWrapper : StreamWrapperBase
{
    private readonly Stream _stream;

    private StreamWrapper(Stream stream)
        : base(stream)
    {
        _stream = stream;
    }

    public static StreamWrapper CreateForReading(Stream stream)
    {
        Throw.IfFalse(stream.CanRead, nameof(stream), "The stream should be readable.");

        return new(stream);
    }

    public static StreamWrapper CreateForWriting(Stream stream)
    {
        Throw.IfFalse(stream.CanWrite, nameof(stream), "The stream should be writable.");

        return new(stream);
    }

    protected override int Read(int count)
        => _stream.Read(Data, 0, count);

    protected override long Seek(long offset, SeekOrigin origin)
        => _stream.Seek(offset, origin);

    protected override long Tell()
        => _stream.Position;

    protected override void Write(int count)
        => _stream.Write(Data, 0, count);
}
