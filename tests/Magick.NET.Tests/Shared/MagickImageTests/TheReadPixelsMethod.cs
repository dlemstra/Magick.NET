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
        public class TheReadPixelsMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.ReadPixels((byte[])null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.ReadPixels(new byte[] { }, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(new byte[] { 215 }, null);
                        });
                    }
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

                    using (IMagickImage image = new MagickImage())
                    {
                        image.ReadPixels(data, settings);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(1, image.Height);

                        using (IPixelCollection pixels = image.GetPixels())
                        {
                            Pixel pixel = pixels.GetPixel(0, 0);
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
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.ReadPixels(null, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.ReadPixels(new byte[] { }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            image.ReadPixels(new byte[] { 215 }, -1, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.ReadPixels(new byte[] { 215 }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.ReadPixels(new byte[] { 215 }, 0, -1, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(new byte[] { 215 }, 0, 1, (PixelReadSettings)null);
                        });
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.ReadPixels((FileInfo)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(new FileInfo(Files.CirclePNG), null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadFileInfo()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Float, "R");

                    var bytes = BitConverter.GetBytes(1.0F);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        FileInfo file = temporyFile;
                        using (IMagickImage image = new MagickImage())
                        {
                            image.ReadPixels(file, settings);

                            Assert.AreEqual(1, image.Width);
                            Assert.AreEqual(1, image.Height);
                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.ReadPixels((string)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.ReadPixels(string.Empty, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(Files.CirclePNG, null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenMappingIsNull()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, null);

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("settings", () =>
                        {
                            image.ReadPixels(Files.CirclePNG, settings);
                        }, "mapping");
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenMappingIsEmpty()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("settings", () =>
                        {
                            image.ReadPixels(Files.CirclePNG, settings);
                        }, "mapping");
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNull()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                    settings.ReadSettings.Width = null;

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("settings", () =>
                        {
                            image.ReadPixels(Files.CirclePNG, settings);
                        }, "Width");
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNull()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                    settings.ReadSettings.Height = null;

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("settings", () =>
                        {
                            image.ReadPixels(Files.CirclePNG, settings);
                        }, "Height");
                    }
                }

                [TestMethod]
                public void ShouldReadFileName()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Short, "R");

                    var bytes = BitConverter.GetBytes(ushort.MaxValue);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        var fileName = temporyFile.FullName;
                        using (IMagickImage image = new MagickImage())
                        {
                            image.ReadPixels(fileName, settings);

                            Assert.AreEqual(1, image.Width);
                            Assert.AreEqual(1, image.Height);
                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.ReadPixels((Stream)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new PixelReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            image.ReadPixels(new MemoryStream(), settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.ReadPixels(new MemoryStream(new byte[] { 215 }), null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadStream()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Int64, "R");

                    var bytes = BitConverter.GetBytes(ulong.MaxValue);

                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        using (IMagickImage image = new MagickImage())
                        {
                            image.ReadPixels(memoryStream, settings);

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
