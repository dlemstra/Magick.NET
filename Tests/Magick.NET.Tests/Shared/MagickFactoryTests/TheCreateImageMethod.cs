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
    public partial class MagickFactoryTests
    {
        [TestClass]
        public partial class TheCreateImageMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNullAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage((byte[])null, settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmptyAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentException("data", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(new byte[] { }, settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelReadSettingsIsNullAndByteArrayIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                {
                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(new byte[] { 215 }, (PixelReadSettings)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileInfoIsNullAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("file", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage((FileInfo)null, settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelReadSettingsIsNullAndFileInfoIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                {
                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(new FileInfo(Files.CirclePNG), (PixelReadSettings)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNullAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage((Stream)null, settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsEmptyAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(new MemoryStream(), settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelReadSettingsIsNullAndStreamIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                {
                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(new MemoryStream(new byte[] { 215 }), (PixelReadSettings)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNullAndPixelReadSettingsIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    var settings = new PixelReadSettings();

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage((string)null, settings);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPixelReadSettingsIsNullAndFileNameIsSpecified()
            {
                ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                {
                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(Files.CirclePNG, (PixelReadSettings)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                ExceptionAssert.ThrowsArgumentException("settings", () =>
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, null);

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(Files.CirclePNG, settings);
                }, "mapping");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("settings", () =>
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(Files.CirclePNG, settings);
                }, "mapping");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthIsNull()
            {
                ExceptionAssert.ThrowsArgumentException("settings", () =>
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                    settings.ReadSettings.Width = null;

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(Files.CirclePNG, settings);
                }, "Width");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsNull()
            {
                ExceptionAssert.ThrowsArgumentException("settings", () =>
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                    settings.ReadSettings.Height = null;

                    MagickFactory factory = new MagickFactory();
                    factory.CreateImage(Files.CirclePNG, settings);
                }, "Height");
            }

            [TestMethod]
            public void ShouldReadByteArrayWithPixelReadSettings()
            {
                byte[] data = new byte[]
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

                MagickFactory factory = new MagickFactory();
                using (IMagickImage image = factory.CreateImage(data, settings))
                {
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

            [TestMethod]
            public void ShouldReadFileInfoWithPixelReadSettings()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Quantum, "R");

                var bytes = BitConverter.GetBytes(Quantum.Max);

                using (var temporyFile = new TemporaryFile(bytes))
                {
                    FileInfo file = temporyFile;
                    MagickFactory factory = new MagickFactory();
                    using (IMagickImage image = factory.CreateImage(file, settings))
                    {
                        Assert.AreEqual(1, image.Width);
                        Assert.AreEqual(1, image.Height);
                        ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldReadStreamWithPixelReadSettings()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Double, "R");

                var bytes = BitConverter.GetBytes(1.0);

                using (var memoryStream = new MemoryStream(bytes))
                {
                    MagickFactory factory = new MagickFactory();
                    using (IMagickImage image = factory.CreateImage(memoryStream, settings))
                    {
                        Assert.AreEqual(1, image.Width);
                        Assert.AreEqual(1, image.Height);
                        ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    }
                }
            }

            [TestMethod]
            public void ShouldReadFileNameWithPixelReadSettings()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Int32, "R");

                var bytes = BitConverter.GetBytes(uint.MaxValue);

                using (var temporyFile = new TemporaryFile(bytes))
                {
                    var fileName = temporyFile.FullName;
                    MagickFactory factory = new MagickFactory();
                    using (IMagickImage image = factory.CreateImage(fileName, settings))
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
