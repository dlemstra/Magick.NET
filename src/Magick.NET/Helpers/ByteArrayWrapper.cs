// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick.Helpers;

namespace ImageMagick;

internal sealed unsafe class ByteArrayWrapper
{
    private byte[] _bytes = new byte[8192];
    private int _offset = 0;
    private int _length = 0;

    public byte[] GetBytes()
    {
        ResizeBytes(_length);
        return _bytes;
    }

    public long Read(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        if (data == IntPtr.Zero)
            return 0;

        var total = (int)count;
        if (total == 0)
            return 0;

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
            return _offset;

        if (newOffset < 0)
            return -1;

        _offset = newOffset;

        return _offset;
    }

    public long Tell(IntPtr user_data)
        => _offset;

    public long Write(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        if (data == IntPtr.Zero)
            return 0;

        var total = (int)count;

        if (total == 0)
            return 0;

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
            return;

        _length = length;

        if (_length < _bytes.Length)
            return;

        var newLength = Math.Max(_bytes.Length * 2, _length);
        ResizeBytes(newLength);
    }

    private void ResizeBytes(int length)
        => Array.Resize(ref _bytes, length);
}
