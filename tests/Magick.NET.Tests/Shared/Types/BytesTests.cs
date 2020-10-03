// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class BytesTests
    {
        [Fact]
        public void Constructor_StreamIsNull_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>("stream", () =>
            {
                new Bytes(null);
            });
        }

        [Fact]
        public void Constructor_StreamPositionIsNotZero_ThrowsException()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Position = 10;

                Assert.Throws<ArgumentException>("stream", () =>
                {
                    new Bytes(memStream);
                });
            }
        }

        [Fact]
        public void Constructor_StreamIsEmpty_SetsProperties()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                var bytes = new Bytes(memStream);

                Assert.Equal(0, bytes.Length);
                Assert.NotNull(bytes.GetData());
                Assert.Empty(bytes.GetData());
            }
        }

        [Fact]
        public void Constructor_StreamIsFileStream_SetsProperties()
        {
            using (FileStream fileStream = File.OpenRead(Files.ImageMagickJPG))
            {
                var bytes = new Bytes(fileStream);

                Assert.Equal(18749, bytes.Length);
                Assert.NotNull(bytes.GetData());
                Assert.Equal(18749, bytes.GetData().Length);
            }
        }

        [Fact]
        public void Constructor_StreamCannotRead_ThrowsException()
        {
            using (TestStream stream = new TestStream(false, true, true))
            {
                Assert.Throws<ArgumentException>("stream", () =>
                {
                    new Bytes(stream);
                });
            }
        }

        [Fact]
        public void Constructor_StreamIsTooLong_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, true, true))
            {
                stream.SetLength(long.MaxValue);

                Assert.Throws<ArgumentException>("length", () =>
                {
                    new Bytes(stream);
                });
            }
        }

        [Fact]
        public void FromStreamBuffer_StreamIsFileStream_ReturnsNull()
        {
            using (FileStream fileStream = File.OpenRead(Files.ImageMagickJPG))
            {
                Bytes bytes = Bytes.FromStreamBuffer(fileStream);

                Assert.Null(bytes);
            }
        }

        [Fact]
        public void FromStreamBuffer_StreamIsMemoryStream_ReturnsObject()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                Bytes bytes = Bytes.FromStreamBuffer(memStream);

                Assert.NotNull(bytes);
            }
        }
    }
}
