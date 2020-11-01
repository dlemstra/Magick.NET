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
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PngOptimizerTests
    {
        public class TheLosslessCompressMethod : PngOptimizerTests
        {
            [Fact]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.SnakewarePNG);
                Assert.Equal(8700, result);
            }

            [Fact]
            public void ShouldTryToCompressLossLess()
            {
                AssertLosslessCompressNotSmaller(Files.MagickNETIconPNG);
            }

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertLosslessCompressTwice(Files.SnakewarePNG);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertLosslessCompressInvalidFileFormat(Files.ImageMagickJPG);
            }

            public class WithFile : TheLosslessCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null));
                }

                [Fact]
                public void ShouldNotOptimizeAnimatedPNG()
                {
                    PngOptimizer optimizer = new PngOptimizer();

                    using (TemporaryFile tempFile = new TemporaryFile(Files.Coders.AnimatedPNGexampleBouncingBeachBallPNG))
                    {
                        var result = optimizer.LosslessCompress(tempFile);
                        Assert.False(result);
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
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        Optimizer.LosslessCompress(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
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
