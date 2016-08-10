using System;
using System.IO;

namespace Magick.NET.Tests
{
  internal class FakePartialStream : Stream
  {
    private readonly Stream _InnerStream;
    private bool _FirstReadDone = false;

    public override bool CanRead => true;
    public override bool CanSeek => true; // Not really, but for our tests this suffices
    public override bool CanWrite => false;
    public override long Length => _InnerStream.Length;

    public override long Position
    {
      get
      {
        return _InnerStream.Position;
      }
      set
      {
        throw new NotSupportedException();
      }
    }

    public FakePartialStream(Stream innerStream)
    {
      if (!innerStream.CanRead || !innerStream.CanSeek)
        throw new ArgumentException("Inner stream must support reading and seeking.");

      _InnerStream = innerStream;
    }

    public override void Flush()
    {
      throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (_FirstReadDone)
        return _InnerStream.Read(buffer, offset, count);
      else
        return _InnerStream.Read(buffer, offset, count / 2);
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }
  }
}
