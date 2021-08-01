// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.IO;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheReadMethod
        {
            public class WithReadonlySpan
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty));
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReading()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(new Span<byte>(bytes), readSettings);

                        Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                    }
                }
            }

            public class WithReadonlySpanAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty, MagickFormat.Png));
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var images = new MagickImageCollection())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new Span<byte>(bytes), MagickFormat.Png));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }
            }

            public class WithReadonlySpanAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenSpanIsEmpty()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var settings = new MagickReadSettings();

                        Assert.Throws<ArgumentException>("data", () => images.Read(Span<byte>.Empty, settings));
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
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(new Span<byte>(bytes), settings));

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                    using (var images = new MagickImageCollection())
                    {
                        images.Read(new Span<byte>(bytes), null);

                        Assert.Single(images);
                    }
                }
            }
        }
    }
}

#endif
