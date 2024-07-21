// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.Buffers;
using System.IO;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionFactoryTests
{
    public partial class TheCreateMethod
    {
        public class WithReadOnlySequence
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(ReadOnlySequence<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageCollectionFactory();
                using var images = factory.Create(new ReadOnlySequence<byte>(data));

                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithReadOnlySequenceAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(ReadOnlySequence<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);

                var factory = new MagickImageCollectionFactory();
                using var images = factory.Create(new ReadOnlySequence<byte>(bytes), null);
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var factory = new MagickImageCollectionFactory();
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Goldenrod,
                };

                using var image = factory.Create(new ReadOnlySequence<byte>(data), settings);

                Assert.IsType<MagickImageCollection>(image);
            }
        }

        public class WithReadOnlySpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty));
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var factory = new MagickImageCollectionFactory();
                using var images = factory.Create(new Span<byte>(data));

                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithReadOnlySpanAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new MagickReadSettings();
                var factory = new MagickImageCollectionFactory();

                Assert.Throws<ArgumentException>("data", () => factory.Create(Span<byte>.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                var factory = new MagickImageCollectionFactory();

                using var images = factory.Create(new Span<byte>(bytes), null);
            }

            [Fact]
            public void ShouldCreateMagickImageCollection()
            {
                var data = File.ReadAllBytes(Files.ImageMagickJPG);
                var settings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Goldenrod,
                };
                var factory = new MagickImageCollectionFactory();

                using var image = factory.Create(new Span<byte>(data), settings);

                Assert.IsType<MagickImageCollection>(image);
            }
        }
    }
}

#endif
