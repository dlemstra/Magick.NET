// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Buffers;

namespace ImageMagick;

internal sealed class PooledByteArray : IDisposable
{
    private static readonly ArrayPool<byte> _pool = ArrayPool<byte>.Create(1024 * 1024 * 64, 128);
    private byte[] _bytes;

    public PooledByteArray(int length)
        => _bytes = _pool.Rent(length);

    public byte[] Data
        => _bytes;

    public int Length
        => _bytes.Length;

    public void Dispose()
    {
        if (_bytes == null)
            return;

        _pool.Return(_bytes);
        _bytes = null!;
    }

    public void Resize(int length)
    {
        if (length <= _bytes.Length)
            return;

        var newBytes = _pool.Rent(length);
        Buffer.BlockCopy(_bytes, 0, newBytes, 0, _bytes.Length);
        _pool.Return(_bytes);
        _bytes = newBytes;
    }

    public byte[] ToUnpooledArray(int length)
    {
        var result = new byte[length];
        Buffer.BlockCopy(_bytes, 0, result, 0, length);
        return result;
    }
}
