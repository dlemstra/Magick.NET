// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETSTANDARD2_0

using System;
using System.Buffers;
using System.IO;
using ImageMagick.Helpers;

namespace ImageMagick;

internal unsafe sealed class ReadOnlySequenceWrapper
{
    private readonly ReadOnlySequence<byte> _data;
    private long _currentOffset;
    private long _totalOffset;
    private ReadOnlySequence<byte>.Enumerator _enumerator;

    public ReadOnlySequenceWrapper(ReadOnlySequence<byte> data)
    {
        _data = data;
        _currentOffset = 0;
        _totalOffset = 0;
        _enumerator = data.GetEnumerator();
        _enumerator.MoveNext();
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
            long length = 0;
            ReadOnlySpan<byte> current;

            try
            {
                current = _enumerator.Current.Span;

                length = Math.Min(total, current.Length - _currentOffset);

                fixed (byte* source = current)
                {
                    NativeMemory.Copy(source + _currentOffset, destination, length);
                }
            }
            catch
            {
                return bytesRead;
            }

            _currentOffset += length;
            _totalOffset += length;
            bytesRead += length;
            destination += length;
            total -= length;

            if (_currentOffset == current.Length)
            {
                try
                {
                    if (!_enumerator.MoveNext())
                        break;
                }
                catch
                {
                    return -1;
                }

                _currentOffset = 0;
            }
        }

        return bytesRead;
    }

    public long Seek(long offset, IntPtr whence, IntPtr user_data)
    {
        var newOffset = GetNewOffset(offset, whence);
        if (newOffset < 0)
            return -1;

        var steps = newOffset - _totalOffset;
        if (steps > 0 && _currentOffset + steps < _enumerator.Current.Span.Length)
        {
            _currentOffset += steps;
            _totalOffset = newOffset;
        }
        else
        {
            _totalOffset = -1;
            _currentOffset = 0;

            var remaining = newOffset;

            try
            {
                _enumerator = _data.GetEnumerator();

                while (_enumerator.MoveNext())
                {
                    var current = _enumerator.Current.Span;

                    if (remaining < current.Length)
                    {
                        _currentOffset = remaining;
                        _totalOffset = newOffset;
                        break;
                    }

                    remaining -= current.Length;
                }
            }
            catch
            {
                return _totalOffset;
            }

            if (remaining == 0)
                _totalOffset = newOffset;
        }

        return _totalOffset;
    }

    public long Tell(IntPtr user_data)
        => _totalOffset;

    private long GetNewOffset(long offset, IntPtr whence)
        => (SeekOrigin)whence switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => _totalOffset + offset,
            SeekOrigin.End => _data.Length - offset,
            _ => -1,
        };
}

#endif
