// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class ThePingMethod
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Ping((byte[])null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(new byte[0]));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(bytes, readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Ping(null, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(new byte[] { }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("offset", () => images.Ping(new byte[] { 215 }, -1, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, -1));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        images.Ping(bytes, 10, bytes.Length - 10);
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

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Ping(null, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Ping(new byte[] { }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("offset", () => images.Ping(new byte[] { 215 }, -1, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, -1, settings));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(bytes, 10, bytes.Length - 10, settings);
                        Assert.Single(images);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImageCollection())
                    {
                        image.Ping(bytes, 0, bytes.Length, null);
                    }
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(bytes, null);

                        Assert.Single(images);
                    }
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("file", () => images.Ping((FileInfo)null));
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(file, null);

                        Assert.Single(images);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Ping((string)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => images.Ping(string.Empty));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() => images.Ping(Files.Missing));

                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection())
                    {
                        input.Ping(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Ping(Files.SnakewarePNG, null);

                        Assert.Single(images);
                    }
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => images.Ping((Stream)null));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var input = new MagickImageCollection())
                        {
                            input.Ping(stream, readSettings);

                            Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            images.Ping(stream, null);

                            Assert.Single(images);
                        }
                    }
                }
            }
        }
    }
}
