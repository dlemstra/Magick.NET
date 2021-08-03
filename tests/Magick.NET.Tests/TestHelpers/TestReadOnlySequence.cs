// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.Buffers;

namespace Magick.NET.Tests
{
    internal sealed class TestReadOnlySequence : ReadOnlySequenceSegment<byte>
    {
        private TestReadOnlySequence(byte value)
        {
            Memory = new ReadOnlyMemory<byte>(new[] { value }, 0, 1);
        }

        public static ReadOnlySequence<byte> Create(byte[] data)
        {
            var first = new TestReadOnlySequence(data[0]);
            var last = first.Append(data[1]);

            for (var i = 2; i < data.Length; i++)
            {
                last = last.Append(data[i]);
            }

            return new ReadOnlySequence<byte>(first, 0, last, 1);
        }

        private TestReadOnlySequence Append(byte value)
        {
            var next = new TestReadOnlySequence(value)
            {
                RunningIndex = RunningIndex + Memory.Length,
            };

            Next = next;

            return next;
        }
    }
}

#endif
