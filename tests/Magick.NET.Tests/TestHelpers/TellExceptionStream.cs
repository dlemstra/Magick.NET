// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    internal sealed class TellExceptionStream : TestStream
    {
        public TellExceptionStream(Stream innerStream)
          : base(innerStream, true)
        {
        }

        public override long Position
        {
            get => throw new InvalidOperationException();
            set => base.Position = value;
        }
    }
}
