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
    public partial class GifOptimizerTests
    {
        public class TheCompressMethod : GifOptimizerTests
        {
            [Fact]
            public void ShouldCompress()
            {
                var result = AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF);
                Assert.InRange(result, 172864, 172865);
            }

            [Fact]
            public void ShouldTryToCompress()
            {
                AssertCompressNotSmaller(Files.RoseSparkleGIF);
            }

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertCompressTwice(Files.FujiFilmFinePixS1ProGIF);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertCompressInvalidFileFormat(Files.ImageMagickJPG);
            }

            public class WithFile : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => Optimizer.Compress((FileInfo)null));
                }
            }

            public class WithFileName : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.Compress((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => Optimizer.Compress(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        Optimizer.Compress(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                }
            }

            public class WithStreamName : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () => Optimizer.Compress((Stream)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotWriteable()
                {
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotSeekable()
                {
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }
            }
        }
    }
}
