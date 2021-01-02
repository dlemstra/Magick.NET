// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;
using Xunit;

namespace Magick.NET.Tests
{
    internal class TestStream : Stream
    {
        private readonly bool _canRead;
        private readonly bool _canSeek;
        private readonly bool _canWrite;
        private long _length;

        public TestStream(bool canRead, bool canWrite, bool canSeek)
        {
            _canRead = canRead;
            _canWrite = canWrite;
            _canSeek = canSeek;
        }

        protected TestStream(Stream innerStream, bool canSeek)
        {
            Assert.True(innerStream.CanRead);
            Assert.True(innerStream.CanSeek);

            InnerStream = innerStream;
            _canRead = true;
            _canWrite = true;
            _canSeek = canSeek;
        }

        public override bool CanRead => _canRead;

        public override bool CanSeek => _canSeek;

        public override bool CanWrite => _canWrite;

        public override long Length
        {
            get
            {
                return InnerStream?.Length ?? _length;
            }
        }

        public override long Position
        {
            get
            {
                return InnerStream?.Position ?? 0;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        protected Stream InnerStream { get; }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InnerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return InnerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _length = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InnerStream.Write(buffer, offset, count);
        }
    }
}
