// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;

namespace ImageMagick;

internal unsafe sealed class BufferWriterWrapper
{
    private const int BufferSize = 8192;

    private readonly IBufferWriter<byte> _bufferWriter;

    public BufferWriterWrapper(IBufferWriter<byte> writer)
    {
        _bufferWriter = writer;
    }

    public long Write(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        var total = (long)count;
        if (total == 0)
            return 0;

        if (data == IntPtr.Zero)
            return 0;

        var source = (byte*)data.ToPointer();

        while (total > 0)
        {
            var length = (int)Math.Min(total, BufferSize);

            try
            {
                var span = _bufferWriter.GetSpan(length);

                fixed (byte* destination = span)
                {
                    Buffer.MemoryCopy(source, destination, length, length);
                }

                _bufferWriter.Advance(length);
            }
            catch
            {
                return -1;
            }

            source += length;
            total -= length;
        }

        return (long)count;
    }
}

#endif
