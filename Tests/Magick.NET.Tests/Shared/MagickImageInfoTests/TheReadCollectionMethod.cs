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
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        public class TheReadCollectionMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenDataIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () => MagickImageInfo.ReadCollection((byte[])null).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(new byte[0]).ToArray());
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () => MagickImageInfo.ReadCollection(null, 0, 0).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(new byte[] { }, 0, 0).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () => MagickImageInfo.ReadCollection(new byte[] { 215 }, -1, 0).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () => MagickImageInfo.ReadCollection(new byte[] { 215 }, 0, 0).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () => MagickImageInfo.ReadCollection(new byte[] { 215 }, 0, -1).ToArray());
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("file", () => MagickImageInfo.ReadCollection((FileInfo)null).ToArray());
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () => MagickImageInfo.ReadCollection((string)null).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () => MagickImageInfo.ReadCollection(string.Empty).ToArray());
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        MagickImageInfo.ReadCollection(Files.Missing).ToArray();
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldReturnEnumerableWithCorrectCount()
                {
                    var info = MagickImageInfo.ReadCollection(Files.RoseSparkleGIF);
                    Assert.AreEqual(3, info.Count());
                }

                [TestMethod]
                public void ShouldReturnTheCorrectInformation()
                {
                    var info = MagickImageInfo.ReadCollection(Files.InvitationTIF);

                    IMagickImageInfo first = info.First();
                    Assert.AreEqual(ColorSpace.sRGB, first.ColorSpace);
                    Assert.AreEqual(CompressionMethod.Zip, first.Compression);
                    Assert.IsTrue(first.FileName.EndsWith("Invitation.tif"));
                    Assert.AreEqual(MagickFormat.Tiff, first.Format);
                    Assert.AreEqual(700, first.Height);
                    Assert.AreEqual(350, first.Density.X);
                    Assert.AreEqual(350, first.Density.Y);
                    Assert.AreEqual(DensityUnit.PixelsPerInch, first.Density.Units);
                    Assert.AreEqual(Interlace.NoInterlace, first.Interlace);
                    Assert.AreEqual(827, first.Width);
                    Assert.AreEqual(0, first.Quality);
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("stream", () => MagickImageInfo.ReadCollection((Stream)null).ToArray());
                }
            }
        }
    }
}
