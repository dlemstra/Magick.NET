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
    public partial class TheReadPixelsMethod
    {
        public class WithReadOnlyByteSpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Span<byte>.Empty, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new Span<byte>(new byte[] { 215 }), null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = null,
                    StorageType = StorageType.Char,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = string.Empty,
                    StorageType = StorageType.Char,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsInvalid()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = "R",
                    StorageType = StorageType.Undefined,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new Span<byte>(new byte[] { 215 }), settings));
                Assert.Contains("Storage type should not be undefined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelReadSettings(2, 2, StorageType.Char, "R");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(new Span<byte>(new byte[] { 215, 215 }), settings));
                Assert.Contains("The data length is 2 but should be at least 4.", exception.Message);
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
                var settings = new PixelReadSettings(2, 1, StorageType.Double, PixelMapping.RGBA);
                using var image = new MagickImage();
                image.ReadPixels(new Span<byte>(data), settings);

                Assert.Equal(2U, image.Width);
                Assert.Equal(1U, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);

                Assert.Equal(4U, pixel.Channels);
                Assert.Equal(0, pixel.GetChannel(0));
                Assert.Equal(0, pixel.GetChannel(1));
                Assert.Equal(0, pixel.GetChannel(2));
                Assert.Equal(Quantum.Max, pixel.GetChannel(3));

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4U, pixel.Channels);
                Assert.Equal(0, pixel.GetChannel(0));
                Assert.Equal(Quantum.Max, pixel.GetChannel(1));
                Assert.Equal(0, pixel.GetChannel(2));
                Assert.Equal(0, pixel.GetChannel(3));
            }
        }

#if !Q8
        public class WithReadOnlyQuantumSpan
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var settings = new PixelReadSettings
                {
                    StorageType = StorageType.Quantum,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Span<QuantumType>.Empty, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new Span<QuantumType>(new QuantumType[] { 215 }), null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelReadSettings
                {
                    StorageType = StorageType.Quantum,
                    Mapping = string.Empty,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new Span<QuantumType>(new QuantumType[] { 215 }), settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsNotQuantum()
            {
                var settings = new PixelReadSettings
                {
                    StorageType = StorageType.Char,
                    Mapping = "R",
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new Span<QuantumType>(new QuantumType[] { 215 }), settings));
                Assert.Contains("Storage type should be Quantum.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelReadSettings(2, 2, StorageType.Quantum, "R");
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(new Span<QuantumType>(new QuantumType[] { 215, 215 }), settings));
                Assert.Contains("The data length is 2 but should be at least 4.", exception.Message);
            }

            [Fact]
            public void ShouldReadSpan()
            {
                var data = new QuantumType[]
                {
                    0, 0, 0, Quantum.Max,
                    0, Quantum.Max, 0, 0,
                };
                var settings = new PixelReadSettings(2, 1, StorageType.Quantum, PixelMapping.RGBA);
                using var image = new MagickImage();
                image.ReadPixels(new Span<QuantumType>(data), settings);

                Assert.Equal(2U, image.Width);
                Assert.Equal(1U, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);

                Assert.Equal(4U, pixel.Channels);
                Assert.Equal(0, pixel.GetChannel(0));
                Assert.Equal(0, pixel.GetChannel(1));
                Assert.Equal(0, pixel.GetChannel(2));
                Assert.Equal(Quantum.Max, pixel.GetChannel(3));

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4U, pixel.Channels);
                Assert.Equal(0, pixel.GetChannel(0));
                Assert.Equal(Quantum.Max, pixel.GetChannel(1));
                Assert.Equal(0, pixel.GetChannel(2));
                Assert.Equal(0, pixel.GetChannel(3));
            }
        }
#endif
    }
}

#endif
