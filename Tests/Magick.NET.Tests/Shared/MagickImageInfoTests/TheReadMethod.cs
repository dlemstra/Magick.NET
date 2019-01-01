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
        [TestClass]
        public class TheReadMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenDataIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read((byte[])null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("data", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read(new byte[0]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("file", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read((FileInfo)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read((Stream)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read((string)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("fileName", () =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read(string.Empty);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    var imageInfo = new MagickImageInfo();
                    imageInfo.Read(Files.Missing);
                }, "error/blob.c/OpenBlob");
            }

            [TestMethod]
            public void ShouldDetectIcoFormatFromByteArray()
            {
                var data = new byte[] { 0, 0, 1, 0, 0, 0 };

                IMagickImageInfo imageInfo = new MagickImageInfo(data);
                Assert.AreEqual(MagickFormat.Ico, imageInfo.Format);
            }

            [TestMethod]
            public void ShouldDetectCurFormatFromByteArray()
            {
                var data = new byte[] { 0, 0, 2, 0, 0, 0 };

                IMagickImageInfo imageInfo = new MagickImageInfo(data);
                Assert.AreEqual(MagickFormat.Cur, imageInfo.Format);
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
    }
}
