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
    public partial class MagickFactoryTests
    {
        public partial class TheCreateCollectionMethod
        {
            [TestMethod]
            public void ShouldCreateMagickImageCollection()
            {
                IMagickFactory factory = new MagickFactory();

                using (IMagickImageCollection images = factory.CreateCollection())
                {
                    Assert.IsInstanceOfType(images, typeof(MagickImage));
                }
            }

            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.CreateCollection((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.CreateCollection(new byte[] { });
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImageCollection()
                {
                    IMagickFactory factory = new MagickFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    using (IMagickImageCollection images = factory.CreateCollection(data))
                    {
                        Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.CreateCollection(null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.CreateCollection(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.CreateCollection(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.CreateCollection(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.CreateCollection(new byte[] { 215 }, 0, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    IMagickFactory factory = new MagickFactory();
                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImageCollection images = factory.CreateCollection(bytes, 10, bytes.Length - 10))
                    {
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
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.CreateCollection(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.CreateCollection(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.CreateCollection(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.CreateCollection(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (IMagickImageCollection images = factory.CreateCollection(new byte[] { 215 }, 0, -1, settings))
                        {
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImageCollection images = factory.CreateCollection(bytes, 10, bytes.Length - 10, settings))
                    {
                        Assert.AreEqual(1, images.Count);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    IMagickFactory factory = new MagickFactory();
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImageCollection image = factory.CreateCollection(bytes, 0, bytes.Length, null))
                    {
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.CreateCollection((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.CreateCollection(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (var images = factory.CreateCollection(File.ReadAllBytes(Files.CirclePNG), null))
                    {
                    }
                }

                [TestMethod]
                public void ShouldCreateMagickImageCollection()
                {
                    IMagickFactory factory = new MagickFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);
                    var readSettings = new MagickReadSettings
                    {
                        BackgroundColor = MagickColors.Goldenrod,
                    };

                    using (IMagickImageCollection image = factory.CreateCollection(data, readSettings))
                    {
                        Assert.IsInstanceOfType(image, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.CreateCollection((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    IMagickFactory factory = new MagickFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    using (IMagickImageCollection images = factory.CreateCollection(file))
                    {
                        Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.CreateCollection((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (IMagickImageCollection images = factory.CreateCollection(new FileInfo(Files.CirclePNG), null))
                    {
                        Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.CreateCollection((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.CreateCollection(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (IMagickImageCollection images = factory.CreateCollection(Files.ImageMagickJPG))
                    {
                        Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.CreateCollection((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.CreateCollection(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (IMagickImageCollection images = factory.CreateCollection(Files.CirclePNG, null))
                    {
                        Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.CreateCollection((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.CreateCollection(new MemoryStream());
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (IMagickImageCollection images = factory.CreateCollection(stream))
                        {
                            Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
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
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.CreateCollection((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    IMagickFactory factory = new MagickFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.CreateCollection(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    IMagickFactory factory = new MagickFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (IMagickImageCollection images = factory.CreateCollection(fileStream, null))
                        {
                            Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                        }
                    }
                }
            }
        }
    }
}
