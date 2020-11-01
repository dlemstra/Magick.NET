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
    public partial class JpegOptimizerTests
    {
        public class TheLosslessCompressMethod : JpegOptimizerTests
        {
            [Fact]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.ImageMagickJPG);
                Assert.Equal(18533, result);
            }

            [Fact]
            public void ShouldTryToCompressLossLess()
            {
                AssertLosslessCompressNotSmaller(Files.LetterJPG);
            }

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertLosslessCompressTwice(Files.ImageMagickJPG);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertLosslessCompressInvalidFileFormat(Files.CirclePNG);
            }

            public class WithFile : TheLosslessCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null));
                }

                [Fact]
                public void ShouldPreserveTheColorProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.NotNull(image.GetColorProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.LosslessCompress(tempFile);

                        Assert.True(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.NotNull(image.GetColorProfile());
                        }
                    }
                }

                [Fact]
                public void ShouldPreserveTheExifProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.NotNull(image.GetExifProfile());
                    }

                    using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.LosslessCompress(tempFile);

                        Assert.True(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.NotNull(image.GetExifProfile());
                        }
                    }
                }
            }

            public class WithFileName : TheLosslessCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.LosslessCompress((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => Optimizer.LosslessCompress(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        Optimizer.LosslessCompress(Files.Missing);
                    });

                    Assert.Contains("Input file read error", exception.Message);
                }
            }

            public class WithStreamName : TheLosslessCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () => Optimizer.LosslessCompress((Stream)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotWriteable()
                {
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotSeekable()
                {
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
                    }
                }
            }
        }
    }
}
