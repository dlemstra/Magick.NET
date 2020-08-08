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
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheReadMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read((byte[])null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[0]);
                        });
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read((byte[])null, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[] { }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            images.Read(new byte[] { 215 }, -1, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, -1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        images.Read(bytes, 10, bytes.Length - 10);
                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read(null, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[] { }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            images.Read(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, -1, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, 10, bytes.Length - 10, MagickFormat.Png);
                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read(null, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[] { }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            images.Read(new byte[] { 215 }, -1, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            images.Read(new byte[] { 215 }, 0, -1, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, 10, bytes.Length - 10, settings);
                        EnumerableAssert.IsSingle(images);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImageCollection())
                    {
                        image.Read(bytes, 0, bytes.Length, null);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read((byte[])null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[] { }, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            images.Read(bytes, MagickFormat.Png);
                        }, "ReadPNGImage");
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            images.Read((byte[])null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            images.Read(new byte[] { }, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            images.Read(bytes, settings);
                        }, "ReadPNGImage");
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, null);

                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            images.Read((FileInfo)null);
                        });
                    }
                }

                [TestClass]
                public class WithFileInfoAndMagickFormat
                {
                    [TestMethod]
                    public void ShouldThrowExceptionWhenFileInfoIsNull()
                    {
                        using (var images = new MagickImageCollection())
                        {
                            ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                            {
                                images.Read((FileInfo)null, MagickFormat.Png);
                            });
                        }
                    }

                    [TestMethod]
                    public void ShouldNotThrowExceptionWhenSettingsIsNull()
                    {
                        var file = new FileInfo(Files.SnakewarePNG);

                        using (var images = new MagickImageCollection())
                        {
                            images.Read(file, null);

                            EnumerableAssert.IsSingle(images);
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            images.Read((FileInfo)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(file, null);

                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            images.Read((string)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            images.Read(string.Empty);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                        {
                            images.Read(Files.Missing);
                        }, "error/blob.c/OpenBlob");
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection())
                    {
                        input.Read(Files.CirclePNG, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            images.Read((string)null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            images.Read(string.Empty, MagickFormat.Png);
                        });
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            images.Read((string)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            images.Read(string.Empty, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Read(Files.CirclePNG, null);

                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            images.Read((Stream)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var input = new MagickImageCollection())
                        {
                            input.Read(stream, readSettings);

                            Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            [TestClass]
            public class WithStreamAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            images.Read((Stream)null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            images.Read(new MemoryStream(), MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                            {
                                images.Read(stream, MagickFormat.Png);
                            }, "ReadPNGImage");
                        }
                    }
                }
            }

            [TestClass]
            public class WithStreamAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            images.Read((Stream)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            images.Read(new MemoryStream(), settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            images.Read(fileStream, null);

                            EnumerableAssert.IsSingle(images);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                            {
                                images.Read(stream, settings);
                            }, "ReadPNGImage");
                        }
                    }
                }
            }
        }
    }
}
