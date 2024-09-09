// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheAddRangeMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.AddRange((byte[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.AddRange(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenReadSettingsIsNull()
            {
                using var images = new MagickImageCollection();

                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                images.AddRange(bytes, null);

                Assert.Single(images);
            }
        }

        public class WithEnumerableImages
        {
            [Fact]
            public void ShouldThrowExceptionWhenNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("images", () => images.AddRange((IEnumerable<IMagickImage<QuantumType>>)null!));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenEmpty()
            {
                using var images = new MagickImageCollection();

                images.AddRange(Array.Empty<IMagickImage<QuantumType>>());

                Assert.Empty(images);
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using var images = new MagickImageCollection(Files.SnakewarePNG);

                Assert.Throws<ArgumentException>("images", () => images.AddRange(images));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenCollectionIsEmpty()
            {
                using var images = new MagickImageCollection();

                images.AddRange(Array.Empty<IMagickImage<QuantumType>>());

                Assert.Empty(images);
            }

            [Fact]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using var images = new MagickImageCollection();

                var image = new MagickImage();
                images.AddRange(new[] { image });

                Assert.Throws<InvalidOperationException>(() => images.AddRange(new[] { image }));
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesContainsDuplicates()
            {
                using var images = new MagickImageCollection();
                using var image = new MagickImage();

                Assert.Throws<InvalidOperationException>(() => images.AddRange(new[] { image, image }));
            }

            [Fact]
            public void ShouldNotCloneTheInputImages()
            {
                using var images = new MagickImageCollection();
                var image = new MagickImage("xc:red", 100, 100);

                var list = new List<IMagickImage<QuantumType>> { image };

                images.AddRange(list);

                Assert.True(ReferenceEquals(image, list[0]));
            }
        }

        public class WithFilename
        {
            [Fact]
            public void ShouldThrowExceptionWhenNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.AddRange((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("fileName", () => images.AddRange(string.Empty));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenReadSettingsIsNull()
            {
                using var images = new MagickImageCollection();

                images.AddRange(Files.SnakewarePNG, null);

                Assert.Single(images);
            }

            [Fact]
            public void ShouldThrowExceptionWhenInvalid()
            {
                using var images = new MagickImageCollection();
                var exception = Assert.Throws<MagickBlobErrorException>(() => images.Add(Files.Missing));

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldAddAllGifFrames()
            {
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);

                Assert.Equal(3, images.Count);

                images.AddRange(Files.RoseSparkleGIF);
                Assert.Equal(6, images.Count);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.AddRange((Stream)null!));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenStreamReadSettingsIsNull()
            {
                using var images = new MagickImageCollection();
                using var stream = File.OpenRead(Files.SnakewarePNG);

                images.AddRange(stream, null);

                Assert.Single(images);
            }
        }
    }
}
