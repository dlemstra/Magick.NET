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
    public partial class MagickImageFactoryTests
    {
        public partial class TheCreateMethod
        {
            [TestMethod]
            public void ShouldCreateMagickImage()
            {
                var factory = new MagickImageFactory();

                using (var image = factory.Create())
                {
                    Assert.IsInstanceOfType(image, typeof(MagickImage));
                    Assert.AreEqual(0, image.Width);
                }
            }

            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { });
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data, 0, data.Length))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var image = factory.Create(bytes, 0, bytes.Length, (MagickReadSettings)null))
                    {
                    }
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings()
                    {
                        BackgroundColor = MagickColors.Purple,
                    };
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data, 0, data.Length, settings))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                        Assert.AreEqual(MagickColors.Purple, image.BackgroundColor);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(bytes, 0, bytes.Length, (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var image = factory.Create(data, 0, data.Length, settings))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(2, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(File.ReadAllBytes(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);
                    var readSettings = new MagickReadSettings
                    {
                        BackgroundColor = MagickColors.Goldenrod,
                    };

                    using (var image = factory.Create(data, readSettings))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                        Assert.AreEqual(MagickColors.Goldenrod, image.Settings.BackgroundColor);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(File.ReadAllBytes(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var image = factory.Create(data, settings))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(2, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithColor
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("color", () =>
                    {
                        factory.Create((MagickColor)null, 1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create(MagickColors.Red, 0, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create(MagickColors.Red, 1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create(MagickColors.Red, -1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create(MagickColors.Red, 1, -1);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var color = MagickColors.Goldenrod;

                    using (var image = factory.Create(color, 10, 5))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(10, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    using (var image = factory.Create(file))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(new FileInfo(Files.CirclePNG), (MagickReadSettings)null))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(new FileInfo(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var temporaryFile = new TemporaryFile(data))
                    {
                        using (var image = factory.Create(temporaryFile, settings))
                        {
                            Assert.IsInstanceOfType(image, typeof(MagickImage));
                            Assert.AreEqual(2, image.Width);
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
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, 1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create("xc:red", 0, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create("xc:red", 1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create("xc:red", -1, 1);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create("xc:red", 1, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create("xc:red", 20, 30))
                    {
                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        ColorAssert.AreEqual(MagickColors.Red, image, 10, 10);
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(Files.ImageMagickJPG))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                        Assert.AreEqual(123, image.Width);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(Files.CirclePNG, (MagickReadSettings)null))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImage));
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(Files.CirclePNG, (PixelReadSettings)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var temporaryFile = new TemporaryFile(data))
                    {
                        using (var image = factory.Create(temporaryFile.FullName, settings))
                        {
                            Assert.IsInstanceOfType(image, typeof(MagickImage));
                            Assert.AreEqual(2, image.Width);
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
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream());
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (var image = factory.Create(stream))
                        {
                            Assert.IsInstanceOfType(image, typeof(MagickImage));
                            Assert.AreEqual(123, image.Width);
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
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = factory.Create(fileStream, (MagickReadSettings)null))
                        {
                            Assert.IsInstanceOfType(image, typeof(MagickImage));
                        }
                    }
                }
            }

            [TestClass]
            public class WithStreamAndPixelReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                        {
                            factory.Create(fileStream, (PixelReadSettings)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var stream = new MemoryStream(data))
                    {
                        using (var image = factory.Create(stream, settings))
                        {
                            Assert.IsInstanceOfType(image, typeof(MagickImage));
                            Assert.AreEqual(2, image.Width);
                        }
                    }
                }
            }
        }
    }
}
