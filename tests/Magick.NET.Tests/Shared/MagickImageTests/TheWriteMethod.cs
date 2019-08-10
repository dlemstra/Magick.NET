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
    public partial class MagickImageTests
    {
        public class TheWriteMethod
        {
            [TestClass]
            public class WithFile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Write((FileInfo)null);
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

                    using (IMagickImage input = new MagickImage(Files.CirclePNG, readSettings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            input.Write(tempFile);

                            using (IMagickImage output = new MagickImage(tempFile))
                            {
                                Assert.AreEqual(MagickFormat.Jpeg, output.Format);
                            }
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileAndFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Write((FileInfo)null, MagickFormat.Bmp);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (IMagickImage input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile, MagickFormat.Tiff);
                            Assert.AreEqual(MagickFormat.Png, input.Format);

                            using (IMagickImage output = new MagickImage(tempfile))
                            {
                                Assert.AreEqual(MagickFormat.Tiff, output.Format);
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
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Write((Stream)null);
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
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Write((Stream)null, MagickFormat.Bmp);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (IMagickImage input = new MagickImage(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                input.Write(stream, MagickFormat.Tiff);
                                Assert.AreEqual(MagickFormat.Png, input.Format);

                                memoryStream.Position = 0;
                                using (IMagickImage output = new MagickImage(stream))
                                {
                                    Assert.AreEqual(MagickFormat.Tiff, output.Format);
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
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Write((string)null);
                        });
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Write((string)null, MagickFormat.Bmp);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (IMagickImage input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile.FullName, MagickFormat.Tiff);
                            Assert.AreEqual(MagickFormat.Png, input.Format);

                            using (IMagickImage output = new MagickImage(tempfile.FullName))
                            {
                                Assert.AreEqual(MagickFormat.Tiff, output.Format);
                            }
                        }
                    }
                }
            }
        }
    }
}
