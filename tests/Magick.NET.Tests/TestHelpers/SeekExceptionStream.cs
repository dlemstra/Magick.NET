// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    internal sealed class SeekExceptionStream : TestStream
    {
        public SeekExceptionStream(Stream innerStream)
          : base(innerStream, true)
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new InvalidOperationException();
        }
    }
}
