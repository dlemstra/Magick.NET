// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheReadMethod
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[0]));
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
                        images.Read(bytes, readSettings);

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
                        Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[] { }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("offset", () => images.Read(new byte[] { 215 }, -1, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, -1));
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        images.Read(bytes, 10, bytes.Length - 10);
                        Assert.Single(images);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[] { }, 0, 0, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("offset", () => images.Read(new byte[] { 215 }, -1, 0, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, -1, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, 10, bytes.Length - 10, MagickFormat.Png);
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
                        Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[] { }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("offset", () => images.Read(new byte[] { 215 }, -1, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, -1, settings));
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, 10, bytes.Length - 10, settings);
                        Assert.Single(images);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImageCollection())
                    {
                        image.Read(bytes, 0, bytes.Length, null);
                    }
                }
            }

            public class WithByteArrayAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[] { }, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(bytes, MagickFormat.Png));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentException>("data", () => images.Read(new byte[] { }, settings));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(bytes, settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(bytes, null);

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
                        Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null));
                    }
                }

                public class WithFileInfoAndMagickFormat
                {
                    [Fact]
                    public void ShouldThrowExceptionWhenFileInfoIsNull()
                    {
                        using (var images = new MagickImageCollection())
                        {
                            Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null, MagickFormat.Png));
                        }
                    }

                    [Fact]
                    public void ShouldNotThrowExceptionWhenSettingsIsNull()
                    {
                        var file = new FileInfo(Files.SnakewarePNG);

                        using (var images = new MagickImageCollection())
                        {
                            images.Read(file, null);

                            Assert.Single(images);
                        }
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(file, null);

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
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() => images.Read(Files.Missing));

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
                        input.Read(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty, MagickFormat.Png));
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Read(Files.CirclePNG, null);

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
                        Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null));
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
                            input.Read(stream, readSettings);

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
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("stream", () => images.Read(new MemoryStream(), MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(stream, MagickFormat.Png));

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("stream", () => images.Read(new MemoryStream(), settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            images.Read(fileStream, null);

                            Assert.Single(images);
                        }
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(stream, settings));

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }
            }
        }
    }
}
