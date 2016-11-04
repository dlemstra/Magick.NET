//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

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
