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
    public partial class MagickImageCollectionFactoryTests
    {
        [TestClass]
        public partial class TheCreateMethod
        {
            [TestMethod]
            public void ShouldCreateMagickImageCollection()
            {
                var factory = new MagickImageCollectionFactory();

                using (var images = factory.Create())
                {
                    Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                }
            }

            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { });
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImageCollection()
                {
                    var factory = new MagickImageCollectionFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);

                    using (var images = factory.Create(data))
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
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var factory = new MagickImageCollectionFactory();
                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = factory.Create(bytes, 10, bytes.Length - 10))
                    {
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
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (var images = factory.Create(new byte[] { 215 }, 0, -1, settings))
                        {
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = factory.Create(bytes, 10, bytes.Length - 10, settings))
                    {
                        EnumerableAssert.IsSingle(images);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var image = factory.Create(bytes, 0, bytes.Length, null))
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
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var images = factory.Create(File.ReadAllBytes(Files.CirclePNG), null))
                    {
                    }
                }

                [TestMethod]
                public void ShouldCreateMagickImageCollection()
                {
                    var factory = new MagickImageCollectionFactory();
                    var data = File.ReadAllBytes(Files.ImageMagickJPG);
                    var readSettings = new MagickReadSettings
                    {
                        BackgroundColor = MagickColors.Goldenrod,
                    };

                    using (var image = factory.Create(data, readSettings))
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
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageCollectionFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    using (var images = factory.Create(file))
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
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var images = factory.Create(new FileInfo(Files.CirclePNG), null))
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
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var images = factory.Create(Files.ImageMagickJPG))
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
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var images = factory.Create(Files.CirclePNG, null))
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
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream());
                    });
                }

                [TestMethod]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (var images = factory.Create(stream))
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
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var images = factory.Create(fileStream, null))
                        {
                            Assert.IsInstanceOfType(images, typeof(MagickImageCollection));
                        }
                    }
                }
            }
        }
    }
}
