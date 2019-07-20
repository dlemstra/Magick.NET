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
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class JpegOptimizerTests
    {
        [TestClass]
        public class TheCompressMethod : JpegOptimizerTests
        {
            [TestMethod]
            public void ShouldCompress()
            {
                AssertCompressSmaller(Files.ImageMagickJPG);
            }

            [TestMethod]
            public void ShouldTryToCompress()
            {
                AssertCompressNotSmaller(Files.LetterJPG);
            }

            [TestMethod]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertCompressTwice(Files.ImageMagickJPG);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertCompressInvalidFileFormat(Files.CirclePNG);
            }

            [TestClass]
            public class WithFile : TheCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () => Optimizer.Compress((FileInfo)null));
                }

                [TestMethod]
                public void ShouldResultInSmallerFileWHenQualityIsSetTo40()
                {
                    using (TemporaryFile tempFile = new TemporaryFile(Files.ImageMagickJPG))
                    {
                        JpegOptimizer optimizer = new JpegOptimizer();
                        optimizer.Compress(tempFile);

                        IMagickImageInfo info = new MagickImageInfo(tempFile);
                        Assert.AreEqual(85, info.Quality);

                        File.Copy(Files.ImageMagickJPG, tempFile.FullName, true);

                        optimizer.Compress(tempFile, 40);

                        info = new MagickImageInfo(tempFile);
                        Assert.AreEqual(40, info.Quality);
                    }
                }

                [TestMethod]
                public void ShouldPreserveTheColorProfile()
                {
                    using (MagickImage image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.IsNotNull(image.GetColorProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.Compress(tempFile);

                        Assert.IsTrue(result);

                        using (MagickImage image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.IsNotNull(image.GetColorProfile());
                        }
                    }
                }

                [TestMethod]
                public void ShouldNotPreserveTheExifProfile()
                {
                    using (MagickImage image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.IsNotNull(image.GetExifProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.Compress(tempFile);

                        Assert.IsTrue(result);

                        using (MagickImage image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.IsNull(image.GetExifProfile());
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileName : TheCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () => Optimizer.Compress((string)null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () => Optimizer.Compress(string.Empty));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        Optimizer.Compress(Files.Missing);
                    }, "Input file read error");
                }

                [TestMethod]
                public void ShouldCompressUTF8PathName()
                {
                    using (TemporaryDirectory tempDir = new TemporaryDirectory("爱"))
                    {
                        string tempFile = Path.Combine(tempDir.FullName, "ImageMagick.jpg");
                        File.Copy(Files.ImageMagickJPG, tempFile);

                        Optimizer.Compress(tempFile);
                    }
                }
            }

            [TestClass]
            public class WithStreamName : TheCompressMethod
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () => Optimizer.Compress((Stream)null));
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotWriteable()
                {
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotSeekable()
                {
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }
            }
        }
    }
}
