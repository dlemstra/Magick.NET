// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheReadAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync(null));
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var input = new MagickImageCollection())
                        {
                            await input.ReadAsync(stream, readSettings);

                            Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                        }
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync((Stream)null, MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("stream", () => images.ReadAsync(new MemoryStream(), MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => images.ReadAsync(stream, MagickFormat.Png));

                            Assert.Contains("ReadPNGImage", exception.Message);
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

                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync(null, settings));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("stream", () => images.ReadAsync(new MemoryStream(), settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            await images.ReadAsync(fileStream, null);

                            Assert.Single(images);
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

                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var images = new MagickImageCollection())
                        {
                            var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => images.ReadAsync(stream, settings));

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }
            }
        }
    }
}
#endif
