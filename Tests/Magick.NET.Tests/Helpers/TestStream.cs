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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal class TestStream : Stream
    {
        private readonly bool _CanRead;
        private readonly bool _CanSeek;
        private readonly bool _CanWrite;

        protected readonly Stream InnerStream;

        public TestStream(bool canRead, bool canWrite, bool canSeek)
        {
            _CanRead = canRead;
            _CanWrite = canWrite;
            _CanSeek = canSeek;
        }

        protected TestStream(Stream innerStream, bool canSeek)
        {
            Assert.IsTrue(innerStream.CanRead);
            Assert.IsTrue(innerStream.CanSeek);

            InnerStream = innerStream;
            _CanRead = true;
            _CanWrite = true;
            _CanSeek = canSeek;
        }

        public override bool CanRead => _CanRead;
        public override bool CanSeek => _CanSeek;
        public override bool CanWrite => _CanWrite;

        public override long Length
        {
            get
            {
                return InnerStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return InnerStream.Position;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

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
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InnerStream.Write(buffer, offset, count);
        }
    }
}
