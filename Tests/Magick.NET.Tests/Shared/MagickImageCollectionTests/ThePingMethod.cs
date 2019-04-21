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
        public class ThePingMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping((byte[])null);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[0]);
                        }
                    });
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Ping(bytes, readSettings);

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
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping((byte[])null, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { }, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, -1, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, 0, 0);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, 0, -1);
                        }
                    });
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        images.Ping(bytes, 10, bytes.Length - 10);
                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(null, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { }, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, -1, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, 0, 0, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(new byte[] { 215 }, 0, -1, settings);
                        }
                    });
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Ping(bytes, 10, bytes.Length - 10, settings);
                        Assert.AreEqual(1, images.Count);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImageCollection image = new MagickImageCollection())
                    {
                        image.Ping(bytes, 0, bytes.Length, null);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Ping(bytes, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping((FileInfo)null);
                        }
                    });
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Ping(file, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping((string)null);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(string.Empty);
                        }
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(Files.Missing);
                        }
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (IMagickImageCollection input = new MagickImageCollection())
                    {
                        input.Ping(Files.CirclePNG, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Ping(Files.SnakewarePNG, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping((Stream)null);
                        }
                    });
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
                        using (IMagickImageCollection input = new MagickImageCollection())
                        {
                            input.Ping(stream, readSettings);

                            Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            [TestClass]
            public class WithStreamAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        using (IMagickImageCollection images = new MagickImageCollection())
                        {
                            images.Ping(stream, null);

                            Assert.AreEqual(1, images.Count);
                        }
                    }
                }
            }
        }
    }
}
