// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests;

internal sealed class NonSeekableStream : TestStream
{
    public NonSeekableStream(string fileName)
      : base(File.OpenRead(fileName), false)
    {
    }

    public NonSeekableStream(byte[] data)
      : base(new MemoryStream(data), false)
    {
    }

    public NonSeekableStream(Stream innerStream)
      : base(innerStream, false)
    {
    }
}
