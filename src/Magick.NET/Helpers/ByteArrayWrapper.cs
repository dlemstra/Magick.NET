// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
#if NETSTANDARD2_1_OR_GREATER
using System.Buffers;
#endif

namespace ImageMagick;

internal sealed unsafe class ByteArrayWrapper
{
    #if NETSTANDARD2_1_OR_GREATER

    private static readonly ArrayPool<byte> _pool = ArrayPool<byte>.Create(1024 * 1024 * 64, 1024);

    private byte[] _bytes = _pool.Rent(1024 * 1024);

    #else

    private byte[] _bytes = new byte[8192];

    #endif
    private int _offset;

    private int _length;

    #if NETSTANDARD2_1_OR_GREATER

    public byte[] GetBytes()
    {
        var result = new byte[_length];
        Array.Copy(_bytes, result, Math.Min(_bytes.Length, _length));
        _pool.Return(_bytes, true);
        return result;
    }
    
~ByteArrayWrapper()
    {
        if (_bytes is not null)
        {
            _pool.Return(_bytes, true);
        }
    }
    #else

    public byte[] GetBytes()
    {
        ResizeBytes(_length);

        return _bytes;
    }

    #endif
    public long Read(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        if (data == IntPtr.Zero)
        {
            return 0;
        }

        var total = (int)count;

        if (total == 0)
        {
            return 0;
        }

        var length = Math.Min(total, _length - _offset);

        if (length != 0)
        {
            var destination = (byte*)data.ToPointer();

            fixed (byte* source = _bytes)
            {
                NativeMemory.Copy(source + _offset, destination, length);
            }

            _offset += length;
        }

        return length;
    }

    public long Seek(long offset, IntPtr whence, IntPtr user_data)
    {
        var newOffset = (int)((SeekOrigin)whence switch
                                  {
                                      SeekOrigin.Begin => offset,
                                      SeekOrigin.Current => _offset + offset,
                                      SeekOrigin.End => _length - offset,
                                      _ => -1,
                                  });

        if (_offset == newOffset)
        {
            return _offset;
        }

        if (newOffset < 0)
        {
            return -1;
        }

        _offset = newOffset;

        return _offset;
    }

    public long Tell(IntPtr user_data)
    {
        return _offset;
    }

    public long Write(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        if (data == IntPtr.Zero)
        {
            return 0;
        }

        var total = (int)count;

        if (total == 0)
        {
            return 0;
        }

        var newOffset = _offset + total;

        EnsureLength(newOffset);

        var source = (byte*)data.ToPointer();

        fixed (byte* destination = _bytes)
        {
            NativeMemory.Copy(source, destination + _offset, total);
        }

        _offset = newOffset;

        return total;
    }

    private void EnsureLength(int length)
    {
        if (length < _length)
        {
            return;
        }

        _length = length;

        if (_length < _bytes.Length)
        {
            return;
        }

        var newLength = Math.Max(_bytes.Length * 2, _length);
        ResizeBytes(newLength);
    }

    #if NETSTANDARD2_1_OR_GREATER

    private void ResizeBytes(int length)
    {
        byte[] byte2 = _pool.Rent(length);
        Array.Copy(_bytes, byte2, Math.Min(_bytes.Length, length));
        _pool.Return(_bytes, true);
        _bytes = byte2;
    }

    #else

    private void ResizeBytes(int length)
    {
        Array.Resize(ref _bytes, length);
    }

    #endif
}
