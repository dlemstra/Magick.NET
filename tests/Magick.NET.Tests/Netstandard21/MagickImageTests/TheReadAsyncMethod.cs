// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE
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
            public class WithFileInfo
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null));
                    }
                }
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null, MagickFormat.Png));
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null, settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(new FileInfo(Files.CirclePNG), null);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty));
                    }
                }

                [Fact]
                public async Task ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(Files.SnakewarePNG);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }

                [Fact]
                public async Task ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = await Assert.ThrowsAsync<FileNotFoundException>(() => image.ReadAsync("~"));

                        Assert.Contains("~", exception.Message);
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null, MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty, MagickFormat.Png));
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReadingFile()
                {
                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(Files.CirclePNG, MagickFormat.Png);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null, settings));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty, settings));
                    }
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(Files.CirclePNG, null);
                    }
                }

                [Fact]
                public async Task ShouldResetTheFormatAfterReadingFile()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }

                [Fact]
                public async Task ShouldUseTheReadSettings()
                {
                    using (var image = new MagickImage())
                    {
                        await image.ReadAsync(Files.Logos.MagickNETSVG, new MagickReadSettings
                        {
                            Density = new Density(72),
                        });

                        ColorAssert.Equal(new MagickColor("#231f20"), image, 129, 101);
                    }
                }
            }
        }
    }
}

#endif
