// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests;

internal sealed class PartialStream : TestStream
{
    private bool _firstReadDone = false;

    public PartialStream(Stream innerStream, bool canSeek)
      : base(innerStream, canSeek)
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_firstReadDone)
            return InnerStream?.Read(buffer, offset, count) ?? -1;

        _firstReadDone = true;
        return InnerStream?.Read(buffer, offset, count / 2) ?? -1;
    }
}
