// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheReadAsyncMethod
        {
            public class WithFileInfo
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                       await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null));
                    }
                }

                public class WithFileInfoAndMagickFormat
                {
                    [Fact]
                    public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                    {
                        using (var images = new MagickImageCollection())
                        {
                            await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null, MagickFormat.Png));
                        }
                    }

                    [Fact]
                    public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                    {
                        var file = new FileInfo(Files.SnakewarePNG);

                        using (var images = new MagickImageCollection())
                        {
                            await images.ReadAsync(file, null);

                            Assert.Single(images);
                        }
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null, settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        await images.ReadAsync(file, null);

                        Assert.Single(images);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty));
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection())
                    {
                        await input.ReadAsync(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null, MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty, MagickFormat.Png));
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null, settings));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty, settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await images.ReadAsync(Files.CirclePNG, null);

                        Assert.Single(images);
                    }
                }
            }
        }
    }
}

#endif
