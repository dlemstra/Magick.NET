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
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheConstructor
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection((byte[])null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[0]);
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

                    using (var input = new MagickImageCollection(bytes, readSettings))
                    {
                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
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
                        new MagickImageCollection(null, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, -1);
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10))
                    {
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
                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection(null, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (var images = new MagickImageCollection(new byte[] { 215 }, 0, -1, MagickFormat.Png))
                        {
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10, MagickFormat.Png))
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
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection(null, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    ExceptionAssert.Throws<ArgumentException>("count", () =>
                    {
                        using (var images = new MagickImageCollection(new byte[] { 215 }, 0, -1, settings))
                        {
                        }
                    });
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10, settings))
                    {
                        EnumerableAssert.IsSingle(images);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImageCollection(bytes, 0, bytes.Length, null))
                    {
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

                    using (var images = new MagickImageCollection(bytes, null))
                    {
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
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImageCollection((FileInfo)null);
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
                        new MagickImageCollection((FileInfo)null, MagickFormat.Png);
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

                        new MagickImageCollection((FileInfo)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(file, null))
                    {
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
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImageCollection((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImageCollection(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImageCollection(Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection(Files.CirclePNG, readSettings))
                    {
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
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImageCollection((string)null, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImageCollection(string.Empty, MagickFormat.Png);
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

                        new MagickImageCollection((string)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection(string.Empty, settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
                {
                    using (var images = new MagickImageCollection(Files.SnakewarePNG, null))
                    {
                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestClass]
            public class WithImages
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImagesIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("images", () =>
                    {
                        new MagickImageCollection((IEnumerable<IMagickImage<QuantumType>>)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
                {
                    using (var images = new MagickImageCollection(Files.SnakewarePNG))
                    {
                        ExceptionAssert.Throws<ArgumentException>("images", () =>
                        {
                            new MagickImageCollection(images);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenImagesContainsDuplicates()
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        var image = new MagickImage();
                        new MagickImageCollection(new[] { image, image });
                    });
                }

                [TestMethod]
                public void ShouldNotCloneTheInputImages()
                {
                    var image = new MagickImage("xc:red", 100, 100);

                    var list = new List<IMagickImage<QuantumType>> { image };

                    using (var images = new MagickImageCollection(list))
                    {
                        Assert.IsTrue(ReferenceEquals(image, list[0]));
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
                        new MagickImageCollection((Stream)null);
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
                        using (var input = new MagickImageCollection(stream, readSettings))
                        {
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
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImageCollection((Stream)null, MagickFormat.Png);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        new MagickImageCollection(new MemoryStream(), MagickFormat.Png);
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

                        new MagickImageCollection((Stream)null, settings);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    ExceptionAssert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection(new MemoryStream(), settings);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        using (var images = new MagickImageCollection(stream, null))
                        {
                            EnumerableAssert.IsSingle(images);
                        }
                    }
                }
            }
        }
    }
}
