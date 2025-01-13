// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;

namespace Magick.NET.Tests;

internal sealed class TestReadOnlySequence : ReadOnlySequenceSegment<byte>
{
    private TestReadOnlySequence(byte[] value, int start, int length)
    {
        Memory = new ReadOnlyMemory<byte>(value, start, length);
    }

    public static ReadOnlySequence<byte> Create(byte[] data, int sequenceSize)
    {
        var first = new TestReadOnlySequence(data, 0, sequenceSize);
        var length = sequenceSize;
        var last = first.Append(data, sequenceSize, length);

        for (var i = sequenceSize + sequenceSize; i < data.Length; i += sequenceSize)
        {
            length = Math.Min(data.Length - i, sequenceSize);
            last = last.Append(data, i, length);
        }

        return new ReadOnlySequence<byte>(first, 0, last, length);
    }

    private TestReadOnlySequence Append(byte[] value, int start, int length)
    {
        var next = new TestReadOnlySequence(value, start, length)
        {
            RunningIndex = RunningIndex + Memory.Length,
        };

        Next = next;

        return next;
    }
}

#endif
