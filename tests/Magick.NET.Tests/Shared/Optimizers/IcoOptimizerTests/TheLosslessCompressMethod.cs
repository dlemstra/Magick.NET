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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class IcoOptimizerTests
    {
        [TestClass]
        public class TheLosslessCompressMethod : IcoOptimizerTests
        {
            [TestMethod]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.WandICO);
                Assert.AreEqual(41247, result);
            }

            [TestMethod]
            public void ShouldTryToCompressLossLess()
            {
                AssertLosslessCompressNotSmaller(Files.ImageMagickICO);
            }

            [TestMethod]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertLosslessCompressTwice(Files.WandICO);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertLosslessCompressInvalidFileFormat(Files.MagickNETIconPNG);
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
                public void ShouldNotOptimizeAnimatedPNG()
                {
                    PngOptimizer optimizer = new PngOptimizer();

                    using (TemporaryFile tempFile = new TemporaryFile(Files.Coders.AnimatedPNGexampleBouncingBeachBallPNG))
                    {
                        var result = optimizer.LosslessCompress(tempFile);
                        Assert.IsFalse(result);
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
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        Optimizer.LosslessCompress(Files.Missing);
                    }, "error/blob.c/OpenBlob");
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
