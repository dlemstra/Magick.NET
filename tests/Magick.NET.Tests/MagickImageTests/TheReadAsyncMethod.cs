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

#if NETCOREAPP
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync(null));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream()));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (var testStream = new TestStream(false, true, true))
                    {
                        using (var image = new MagickImage())
                        {
                            await Assert.ThrowsAsync<NotSupportedException>(() => image.ReadAsync(testStream));
                        }
                    }
                }

                [Fact]
                public async Task ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.SnakewarePNG))
                        {
                            await image.ReadAsync(fileStream);
                            Assert.Equal(286, image.Width);
                            Assert.Equal(67, image.Height);
                            Assert.Equal(MagickFormat.Png, image.Format);
                        }
                    }
                }

                [Fact]
                public async Task ShouldReadImageFromSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.ImageMagickJPG))
                        {
                            await image.ReadAsync(fileStream);

                            fileStream.Position = 0;
                            using (var partialStream = new PartialStream(fileStream, true))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    await testImage.ReadAsync(partialStream);

                                    Assert.Equal(image.Width, testImage.Width);
                                    Assert.Equal(image.Height, testImage.Height);
                                    Assert.Equal(image.Format, testImage.Format);
                                    Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                                }
                            }
                        }
                    }
                }

                [Fact]
                public async Task ShouldReadImageFromNonSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.ImageMagickJPG))
                        {
                            await image.ReadAsync(fileStream);

                            fileStream.Position = 0;
                            using (var partialStream = new PartialStream(fileStream, false))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    await testImage.ReadAsync(partialStream);

                                    Assert.Equal(image.Width, testImage.Width);
                                    Assert.Equal(image.Height, testImage.Height);
                                    Assert.Equal(image.Format, testImage.Format);
                                    Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                                }
                            }
                        }
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync(null, MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream(), MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage())
                        {
                            var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => image.ReadAsync(stream, MagickFormat.Png));

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReadingStream()
                {
                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            await image.ReadAsync(stream, MagickFormat.Png);

                            Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync(null, settings));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream(), settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            await image.ReadAsync(fileStream, null);
                        }
                    }
                }

                [Fact]
                public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage())
                        {
                            var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => image.ReadAsync(stream, settings));

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReadingStream()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            await image.ReadAsync(stream, readSettings);

                            Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }
        }
    }
}
#endif