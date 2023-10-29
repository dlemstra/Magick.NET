// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
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

public partial class MagickImageTests
{
    public partial class TheImportPixelsMethod
    {
        public class WithReadOnlyByteSpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "r");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Span<byte>.Empty, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ImportPixels(new Span<byte>(new byte[] { 215 }), null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, null);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ImportPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, string.Empty);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsInvalid()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Undefined, "r");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Storage type should not be undefined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(new Span<byte>(new byte[] { 215, 215 }), settings));
                Assert.Contains("The data length is 2 but should be at least 3.", exception.Message);
            }

            [Fact]
            public void ShouldReadSpan()
            {
                var data = new byte[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                };
                var settings = new PixelImportSettings(1, 2, StorageType.Double, PixelMapping.RGBA);
                using var image = new MagickImage(MagickColors.Green, 2, 2);
                image.Alpha(AlphaOption.On);
                image.ImportPixels(data, settings);

                Assert.Equal(2, image.Width);
                Assert.Equal(2, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);

                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Black);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(new MagickColor("#00ff0000"));

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);
            }
        }

#if !Q8
        public class WithReadOnlyQuantumSpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "r");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Span<QuantumType>.Empty, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ImportPixels(new Span<QuantumType>(new QuantumType[] { 215 }), null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, string.Empty);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new Span<QuantumType>(new QuantumType[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsNotQuantum()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "r");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new Span<QuantumType>(new QuantumType[] { 215 }), settings));
                Assert.Contains("Storage type should be Quantum.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(new Span<QuantumType>(new QuantumType[] { 215, 215 }), settings));
                Assert.Contains("The data length is 2 but should be at least 3.", exception.Message);
            }

            [Fact]
            public void ShouldReadSpan()
            {
                var data = new QuantumType[]
                {
                    0, 0, 0, Quantum.Max,
                    0, Quantum.Max, 0, 0,
                };
                var settings = new PixelImportSettings(1, 2, StorageType.Quantum, PixelMapping.RGBA);
                using var image = new MagickImage(MagickColors.Green, 2, 2);
                image.Alpha(AlphaOption.On);
                image.ImportPixels(new Span<QuantumType>(data), settings);

                Assert.Equal(2, image.Width);
                Assert.Equal(2, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);

                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Black);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(new MagickColor("#00ff0000"));

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);
            }
        }
#endif
    }
}

#endif
