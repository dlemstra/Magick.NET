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
    public partial class MagickImageTests
    {
        public partial class TheConstructor
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { });
                    });
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImage(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, -1);
                    });
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage(null, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImage(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, -1, MagickFormat.Png);
                    });
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImage(bytes, 0, bytes.Length, (MagickReadSettings)null))
                    {
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 1, (PixelReadSettings)null);
                    });
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, MagickFormat.Png);
                    });
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(File.ReadAllBytes(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new byte[] { 215 }, (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldReadByteArray()
                {
                    var data = new byte[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                    };

                    var settings = new PixelReadSettings(2, 1, StorageType.Double, PixelMapping.RGBA);

                    using (var image = new MagickImage(data, settings))
                    {
                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(1, image.Height);

                        using (var pixels = image.GetPixels())
                        {
                            var pixel = pixels.GetPixel(0, 0);
                            Assert.AreEqual(4, pixel.Channels);
                            Assert.AreEqual(0, pixel.GetChannel(0));
                            Assert.AreEqual(0, pixel.GetChannel(1));
                            Assert.AreEqual(0, pixel.GetChannel(2));
                            Assert.AreEqual(Quantum.Max, pixel.GetChannel(3));

                            pixel = pixels.GetPixel(1, 0);
                            Assert.AreEqual(4, pixel.Channels);
                            Assert.AreEqual(0, pixel.GetChannel(0));
                            Assert.AreEqual(Quantum.Max, pixel.GetChannel(1));
                            Assert.AreEqual(0, pixel.GetChannel(2));
                            Assert.AreEqual(0, pixel.GetChannel(3));
                        }
                    }
                }
            }

            [TestClass]
            public class WithColor
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("color", () =>
                    {
                        new MagickImage((MagickColor)null, 1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage(MagickColors.Red, 0, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage(MagickColors.Red, 1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage(MagickColors.Red, -1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage(MagickColors.Red, 1, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var color = new MagickColor("red");

                    using (var image = new MagickImage(color, 20, 30))
                    {
                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        ColorAssert.AreEqual(color, image, 10, 10);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImage((FileInfo)null);
                    });
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImage((FileInfo)null, MagickFormat.Png);
                    });
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(new FileInfo(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new FileInfo(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldReadFileInfo()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Quantum, "R");

                    var bytes = BitConverter.GetBytes(Quantum.Max);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        FileInfo file = temporyFile;
                        using (var image = new MagickImage(file, settings))
                        {
                            Assert.AreEqual(1, image.Width);
                            Assert.AreEqual(1, image.Height);
                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public partial class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImage(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
                {
                    var exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~/test.gif");
                    }, "error/blob.c/OpenBlob");

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    Assert.IsTrue(exception.Message.Contains(baseDirectory));
                }

                [TestMethod]
                public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
                {
                    var exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~");
                    }, "error/blob.c/OpenBlob");

                    Assert.IsTrue(exception.Message.Contains("~"));
                }
            }

            [TestClass]
            public class WithFileNameAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImage(string.Empty, MagickFormat.Png);
                    });
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(Files.CirclePNG, (MagickReadSettings)null))
                    {
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(Files.CirclePNG, (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenMappingIsNull()
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, null);

                        new MagickImage(Files.CirclePNG, settings);
                    }, "mapping");
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenMappingIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);

                        new MagickImage(Files.CirclePNG, settings);
                    }, "mapping");
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNull()
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                        settings.ReadSettings.Width = null;

                        new MagickImage(Files.CirclePNG, settings);
                    }, "Width");
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNull()
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                        settings.ReadSettings.Height = null;

                        new MagickImage(Files.CirclePNG, settings);
                    }, "Height");
                }

                [TestMethod]
                public void ShouldReadFileName()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Int32, "R");

                    var bytes = BitConverter.GetBytes(4294967295U);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        var fileName = temporyFile.FullName;
                        using (var image = new MagickImage(fileName, settings))
                        {
                            Assert.AreEqual(1, image.Width);
                            Assert.AreEqual(1, image.Height);
                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndSize
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null, 1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage("xc:red", 0, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage("xc:red", 1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage("xc:red", -1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage("xc:red", 1, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage("xc:red", 20, 30))
                    {
                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        ColorAssert.AreEqual(MagickColors.Red, image, 10, 10);
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImage((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new MemoryStream());
                    });
                }
            }

            [TestClass]
            public class WithStreamAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImage((Stream)null, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        new MagickImage(new MemoryStream(), MagickFormat.Png);
                    });
                }
            }

            [TestClass]
            public class WithStreamAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage(fileStream, (MagickReadSettings)null))
                        {
                        }
                    }
                }
            }

            [TestClass]
            public class WitStreamAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new MemoryStream(new byte[] { 215 }), (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldReadStream()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Double, "R");

                    var bytes = BitConverter.GetBytes(1.0);

                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage(memoryStream, settings))
                        {
                            Assert.AreEqual(1, image.Width);
                            Assert.AreEqual(1, image.Height);
                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }
        }
    }
}
