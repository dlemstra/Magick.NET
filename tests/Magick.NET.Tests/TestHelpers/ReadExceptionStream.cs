// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    internal sealed class ReadExceptionStream : TestStream
    {
        public ReadExceptionStream(Stream innerStream)
          : base(innerStream, true)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }
    }
}
