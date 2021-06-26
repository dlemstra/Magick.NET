// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionFactoryTests
    {
        public partial class TheCreateAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();

                    await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream()));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (var images = await factory.CreateAsync(stream))
                        {
                            Assert.IsType<MagickImageCollection>(images);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageCollectionFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageCollectionFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var images = await factory.CreateAsync(fileStream, null))
                        {
                            Assert.IsType<MagickImageCollection>(images);
                        }
                    }
                }
            }
        }
    }
}

#endif
