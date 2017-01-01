//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
  [ExcludeFromCodeCoverage]
  internal class TestStream : Stream
  {
    private readonly bool _canRead;
    private readonly bool _canSeek;

    public TestStream(bool canRead, bool canSeek)
    {
      _canRead = canRead;
      _canSeek = CanSeek;
    }

    public override bool CanRead => _canRead;
    public override bool CanSeek => _canSeek;
    public override bool CanWrite => false;

    public override long Length
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override long Position
    {
      get
      {
        throw new NotImplementedException();
      }

      set
      {
        throw new NotImplementedException();
      }
    }

    public override void Flush()
    {
      throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      throw new NotImplementedException();
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
