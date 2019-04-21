// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class BytesTests
    {
        [TestMethod]
        public void Constructor_StreamIsNull_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
            {
                new Bytes(null);
            });
        }

        [TestMethod]
        public void Constructor_StreamPositionIsNotZero_ThrowsException()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Position = 10;

                ExceptionAssert.Throws<ArgumentException>("stream", () =>
                {
                    new Bytes(memStream);
                });
            }
        }

        [TestMethod]
        public void Constructor_StreamIsEmpty_SetsProperties()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                var bytes = new Bytes(memStream);

                Assert.AreEqual(0, bytes.Length);
                Assert.IsNotNull(bytes.Data);
                Assert.AreEqual(0, bytes.Data.Length);
            }
        }

        [TestMethod]
        public void Constructor_StreamIsFileStream_SetsProperties()
        {
            using (FileStream fileStream = File.OpenRead(Files.ImageMagickJPG))
            {
                var bytes = new Bytes(fileStream);

                Assert.AreEqual(18749, bytes.Length);
                Assert.IsNotNull(bytes.Data);
                Assert.AreEqual(18749, bytes.Data.Length);
            }
        }

        [TestMethod]
        public void Constructor_StreamCannotRead_ThrowsException()
        {
            using (TestStream stream = new TestStream(false, true, true))
            {
                ExceptionAssert.Throws<ArgumentException>("stream", () =>
                {
                    new Bytes(stream);
                });
            }
        }

        [TestMethod]
        public void Constructor_StreamIsTooLong_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, true, true))
            {
                stream.SetLength(long.MaxValue);

                ExceptionAssert.Throws<ArgumentException>("length", () =>
                {
                    new Bytes(stream);
                });
            }
        }

        [TestMethod]
        public void FromStreamBuffer_StreamIsFileStream_ReturnsNull()
        {
            using (FileStream fileStream = File.OpenRead(Files.ImageMagickJPG))
            {
                Bytes bytes = Bytes.FromStreamBuffer(fileStream);

                Assert.IsNull(bytes);
            }
        }

        [TestMethod]
        public void FromStreamBuffer_StreamIsMemoryStream_ReturnsObject()
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                Bytes bytes = Bytes.FromStreamBuffer(memStream);

                Assert.IsNotNull(bytes);
            }
        }
    }
}
