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
using Xunit;

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
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection((byte[])null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[0]);
                    });
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var input = new MagickImageCollection(bytes, readSettings))
                    {
                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection(null, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, -1);
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection(null, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        using (var images = new MagickImageCollection(new byte[] { 215 }, 0, -1, MagickFormat.Png))
                        {
                        }
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10, MagickFormat.Png))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImageCollection(null, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImageCollection(new byte[] { }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImageCollection(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        using (var images = new MagickImageCollection(new byte[] { 215 }, 0, -1, settings))
                        {
                        }
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection(bytes, 10, bytes.Length - 10, settings))
                    {
                        Assert.Single(images);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImageCollection(bytes, 0, bytes.Length, null))
                    {
                    }
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(bytes, null))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImageCollection((FileInfo)null);
                    });
                }
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImageCollection((FileInfo)null, MagickFormat.Png);
                    });
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection((FileInfo)null, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection(file, null))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImageCollection((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImageCollection(string.Empty);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImageCollection(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection(Files.CirclePNG, readSettings))
                    {
                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImageCollection((string)null, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImageCollection(string.Empty, MagickFormat.Png);
                    });
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection((string)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection(string.Empty, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
                {
                    using (var images = new MagickImageCollection(Files.SnakewarePNG, null))
                    {
                        Assert.Single(images);
                    }
                }
            }

            public class WithImages
            {
                [Fact]
                public void ShouldThrowExceptionWhenImagesIsNull()
                {
                    Assert.Throws<ArgumentNullException>("images", () =>
                    {
                        new MagickImageCollection((IEnumerable<IMagickImage<QuantumType>>)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
                {
                    using (var images = new MagickImageCollection(Files.SnakewarePNG))
                    {
                        Assert.Throws<ArgumentException>("images", () =>
                        {
                            new MagickImageCollection(images);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenImagesContainsDuplicates()
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        var image = new MagickImage();
                        new MagickImageCollection(new[] { image, image });
                    });
                }

                [Fact]
                public void ShouldNotCloneTheInputImages()
                {
                    var image = new MagickImage("xc:red", 100, 100);

                    var list = new List<IMagickImage<QuantumType>> { image };

                    using (var images = new MagickImageCollection(list))
                    {
                        Assert.True(ReferenceEquals(image, list[0]));
                    }
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImageCollection((Stream)null);
                    });
                }

                [Fact]
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
                            Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImageCollection((Stream)null, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        new MagickImageCollection(new MemoryStream(), MagickFormat.Png);
                    });
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection((Stream)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImageCollection(new MemoryStream(), settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        using (var images = new MagickImageCollection(stream, null))
                        {
                            Assert.Single(images);
                        }
                    }
                }
            }
        }
    }
}
