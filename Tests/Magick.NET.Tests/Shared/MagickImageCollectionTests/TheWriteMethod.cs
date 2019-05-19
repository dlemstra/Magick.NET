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
    public partial class MagickImageCollectionTests
    {
        public class TheWriteMethod
        {
            [TestClass]
            public class WithFile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            images.Write((FileInfo)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheFileExtension()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (IMagickImageCollection input = new MagickImageCollection(Files.CirclePNG, readSettings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            input.Write(tempFile);

                            using (IMagickImageCollection output = new MagickImageCollection(tempFile))
                            {
                                Assert.AreEqual(MagickFormat.Jpeg, output[0].Format);
                            }
                        }
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            images.Write((Stream)null);
                        });
                    }
                }
            }

            [TestClass]
            public class WithStreamAndFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            images.Write(null, MagickFormat.Bmp);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (IMagickImageCollection input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                input.Write(stream, MagickFormat.Tiff);

                                memoryStream.Position = 0;
                                using (IMagickImageCollection output = new MagickImageCollection(stream))
                                {
                                    Assert.AreEqual(1, output.Count);
                                    Assert.AreEqual(MagickFormat.Tiff, output[0].Format);
                                }
                            }
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            images.Write((string)null);
                        });
                    }
                }
            }
        }
    }
}
