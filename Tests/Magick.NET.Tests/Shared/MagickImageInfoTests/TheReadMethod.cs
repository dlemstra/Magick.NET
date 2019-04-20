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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        public class TheReadMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenDataIsNull()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentNullException("data", () => imageInfo.Read((byte[])null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("data", () => imageInfo.Read(new byte[0]));
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentNullException("data", () => imageInfo.Read((byte[])null, 0, 0));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("data", () => imageInfo.Read(new byte[] { }, 0, 0));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("offset", () => imageInfo.Read(new byte[] { 215 }, -1, 0));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("count", () => imageInfo.Read(new byte[] { 215 }, 0, 0));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("count", () => imageInfo.Read(new byte[] { 215 }, 0, -1));
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentNullException("file", () => imageInfo.Read((FileInfo)null));
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentNullException("fileName", () => imageInfo.Read((string)null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentException("fileName", () => imageInfo.Read(string.Empty));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        imageInfo.Read(Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldReturnTheCorrectInformation()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();
                    imageInfo.Read(Files.ImageMagickJPG);

                    Assert.AreEqual(ColorSpace.sRGB, imageInfo.ColorSpace);
                    Assert.AreEqual(CompressionMethod.JPEG, imageInfo.Compression);
                    Assert.IsTrue(imageInfo.FileName.EndsWith("ImageMagick.jpg"));
                    Assert.AreEqual(MagickFormat.Jpeg, imageInfo.Format);
                    Assert.AreEqual(118, imageInfo.Height);
                    Assert.AreEqual(72, imageInfo.Density.X);
                    Assert.AreEqual(72, imageInfo.Density.Y);
                    Assert.AreEqual(DensityUnit.PixelsPerInch, imageInfo.Density.Units);
                    Assert.AreEqual(Interlace.NoInterlace, imageInfo.Interlace);
                    Assert.AreEqual(100, imageInfo.Quality);
                    Assert.AreEqual(123, imageInfo.Width);
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    IMagickImageInfo imageInfo = new MagickImageInfo();

                    ExceptionAssert.ThrowsArgumentNullException("stream", () => imageInfo.Read((Stream)null));
                }
            }
        }
    }
}
