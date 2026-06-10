// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageMagick;

internal abstract unsafe class StreamWrapperBase : IDisposable
{
    private const int BufferSize = 8192;

    private readonly PooledByteArray _buffer;
    private readonly GCHandle _handle;
    private readonly byte* _bufferStart;
    private readonly long _streamStart;

    protected StreamWrapperBase(Stream stream)
    {
        _buffer = new PooledByteArray(BufferSize);
        _handle = GCHandle.Alloc(_buffer.Data, GCHandleType.Pinned);
        _bufferStart = (byte*)_handle.AddrOfPinnedObject().ToPointer();

        try
        {
            _streamStart = stream.Position;
        }
        catch
        {
            _streamStart = 0;
        }
    }

    protected byte[] Data
        => _buffer.Data;

    public void Dispose()
    {
        _handle.Free();
        _buffer.Dispose();
    }

    public long Read(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        var total = (long)count;
        if (total == 0)
            return 0;

        if (data == IntPtr.Zero)
            return 0;

        var destination = (byte*)data.ToPointer();
        long bytesRead = 0;

        while (total > 0)
        {
            var length = (int)Math.Min(total, BufferSize);

            try
            {
                length = Read(length);
                if (length == 0)
                    break;

                NativeMemory.Copy(_bufferStart, destination, length);
            }
            catch
            {
                return -1;
            }

            if (length == 0)
                break;

            bytesRead += length;
            destination += length;
            total -= length;
        }

        return bytesRead;
    }

    public long Seek(long offset, IntPtr whence, IntPtr user_data)
    {
        try
        {
            return (SeekOrigin)whence switch
            {
                SeekOrigin.Begin => Seek(_streamStart + offset, SeekOrigin.Begin) - _streamStart,
                SeekOrigin.Current => Seek(offset, SeekOrigin.Current) - _streamStart,
                SeekOrigin.End => Seek(offset, SeekOrigin.End) - _streamStart,
                _ => -1,
            };
        }
        catch
        {
            return -1;
        }
    }

    public long Tell(IntPtr user_data)
    {
        try
        {
            return Tell() - _streamStart;
        }
        catch
        {
            return -1;
        }
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
                NativeMemory.Copy(source, _bufferStart, length);
                Write(length);
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

    protected abstract int Read(int count);

    protected abstract long Seek(long offset, SeekOrigin origin);

    protected abstract long Tell();

    protected abstract void Write(int count);
}
