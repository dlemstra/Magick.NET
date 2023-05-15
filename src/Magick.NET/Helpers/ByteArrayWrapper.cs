// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Runtime.InteropServices;
using ImageMagick.Helpers;

namespace ImageMagick;

internal sealed unsafe class ByteArrayWrapper : IDisposable
{
    internal const int BufferSize = 81920;

    private readonly byte[] _buffer;
    private readonly byte* _bufferStart;
    private readonly GCHandle _handle;
    private int _bufferOffset;
    private byte[] _bytes;
    private int _offset;

    public ByteArrayWrapper()
    {
        _buffer = new byte[BufferSize];
        _handle = GCHandle.Alloc(_buffer, GCHandleType.Pinned);
        _bufferStart = (byte*)_handle.AddrOfPinnedObject().ToPointer();
        _bufferOffset = 0;
        _bytes = new byte[0];
        _offset = 0;
    }

    public byte[] GetBytes()
    {
        AppendBufferToBytes();
        return _bytes;
    }

    public void Dispose()
        => _handle.Free();

    public long Read(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        var total = (int)count;
        if (total == 0)
            return 0;

        if (data == IntPtr.Zero)
            return 0;

        var length = Math.Min(total, _bytes.Length - _offset);
        if (length != 0)
        {
            var destination = (byte*)data.ToPointer();
            fixed (byte* source = _bytes)
            {
                NativeMemory.Copy(source + _offset, destination, length);
                _offset += length;
            }
        }

        return length;
    }

    public long Seek(long offset, IntPtr whence, IntPtr user_data)
    {
        AppendBufferToBytes();

        var newOffset = (int)((SeekOrigin)whence switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => _offset + offset,
            SeekOrigin.End => _bytes.Length - offset,
            _ => -1,
        });

        if (_offset == newOffset)
            return _offset;

        if (newOffset < 0)
            return -1;
        else if (newOffset > _bytes.Length)
            ResizeBytes(newOffset);

        _offset = newOffset;
        return _offset;
    }

    public long Tell(IntPtr user_data)
        => _offset + _bufferOffset;

    public long Write(IntPtr data, UIntPtr count, IntPtr user_data)
    {
        var total = (int)count;
        if (total == 0)
            return 0;

        if (data == IntPtr.Zero)
            return 0;

        var source = (byte*)data.ToPointer();

        while (total > 0)
        {
            var length = Math.Min(total, BufferSize - _bufferOffset);
            if (_offset < _bytes.Length)
            {
                AppendBufferToBytes();

                length = Math.Min(length, _buffer.Length - _offset);
                source = FillBuffer(source, length);

                CopyBufferToBytes();
            }
            else
            {
                source = FillBuffer(source, length);

                if (_bufferOffset == BufferSize)
                    AppendBufferToBytes();
            }

            total -= length;
        }

        return (long)count;
    }

    private void AppendBufferToBytes()
    {
        if (_bufferOffset == 0)
            return;

        ResizeBytes(_bytes.Length + _bufferOffset);
        CopyBufferToBytes();
    }

    private void CopyBufferToBytes()
    {
        Buffer.BlockCopy(_buffer, 0, _bytes, _offset, _bufferOffset);
        _offset += _bufferOffset;
        _bufferOffset = 0;
    }

    private byte* FillBuffer(byte* source, int length)
    {
        NativeMemory.Copy(source, _bufferStart + _bufferOffset, length);
        _bufferOffset += length;
        return source + length;
    }

    private void ResizeBytes(int length)
        => Array.Resize(ref _bytes, length);
}
