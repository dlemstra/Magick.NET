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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class JpegOptimizerTests
    {
        [TestClass]
        public class TheLosslessCompressMethod : JpegOptimizerTests
        {
            [TestMethod]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.ImageMagickJPG);
                Assert.AreEqual(18533, result);
            }

            [TestMethod]
            public void ShouldTryToCompressLossLess()
            {
                AssertLosslessCompressNotSmaller(Files.LetterJPG);
            }

            [TestMethod]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertLosslessCompressTwice(Files.ImageMagickJPG);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertLosslessCompressInvalidFileFormat(Files.CirclePNG);
            }

            [TestClass]
            public class WithFile : TheLosslessCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null));
                }

                [TestMethod]
                public void ShouldPreserveTheColorProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.IsNotNull(image.GetColorProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.LosslessCompress(tempFile);

                        Assert.IsTrue(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.IsNotNull(image.GetColorProfile());
                        }
                    }
                }

                [TestMethod]
                public void ShouldPreserveTheExifProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.IsNotNull(image.GetExifProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.LosslessCompress(tempFile);

                        Assert.IsTrue(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.IsNotNull(image.GetExifProfile());
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileName : TheLosslessCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () => Optimizer.LosslessCompress((string)null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () => Optimizer.LosslessCompress(string.Empty));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        Optimizer.LosslessCompress(Files.Missing);
                    }, "Input file read error");
                }
            }

            [TestClass]
            public class WithStreamName : TheLosslessCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () => Optimizer.LosslessCompress((Stream)null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotWriteable()
                {
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotSeekable()
                {
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }
            }
        }
    }
}
