// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportPixels((byte[])null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Array.Empty<byte>(), settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new byte[] { 215 }, null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, string.Empty);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new byte[] { 215 }, settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsUndefined()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Undefined, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new byte[] { 215 }, settings));
                Assert.Contains("Storage type should not be undefined.", exception.Message);
            }

            [Fact]
            public void ShouldImportPixelsFromByteArray()
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

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportPixels((byte[])null, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Array.Empty<byte>(), 0, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ImportPixels(new byte[] { 215 }, null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("offset", () => image.ImportPixels(new byte[] { 215 }, -1, settings));
                Assert.Contains("The offset should be positive.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetExceedsArrayLength()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("offset", () => image.ImportPixels(new byte[] { 215 }, 1, settings));
                Assert.Contains("The offset should not exceed the length of the array.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(new byte[] { 215, 215 }, 1, settings));
                Assert.Contains("The data length is 2 but should be at least 4.", exception.Message);
            }

            [Fact]
            public void ShouldImportPixelsFromByteArray()
            {
                var data = new byte[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                    0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0,
                };

                var settings = new PixelImportSettings(1, 0, 1, 2, StorageType.Double, PixelMapping.RGBA);

                using var image = new MagickImage(MagickColors.Green, 2, 2);
                image.Alpha(AlphaOption.On);
                image.ImportPixels(data, 8, settings);

                Assert.Equal(2, image.Width);
                Assert.Equal(2, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Black);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(new MagickColor("#00ff0000"));
            }
        }

#if !Q8
        public class WithQuantumArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "R");

                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportPixels((QuantumType[])null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Array.Empty<QuantumType>(), settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new QuantumType[] { 215 }, null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, string.Empty);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsNotQuantum()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Char, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ImportPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("Storage type should be Quantum.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenLengthIsTooLow()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(new QuantumType[] { 215, 215 }, 1, settings));
                Assert.Contains("The data length is 2 but should be at least 4.", exception.Message);
            }

            [Fact]
            public void ShouldImportPixelsFromByteArray()
            {
                var data = new QuantumType[]
                {
                    0, 0, 0, Quantum.Max,
                    0, Quantum.Max, 0, 0,
                };

                var settings = new PixelImportSettings(1, 2, StorageType.Quantum, PixelMapping.RGBA);

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

        public class WithQuantumArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "R");

                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportPixels((QuantumType[])null, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(Array.Empty<QuantumType>(), 0, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ImportPixels(new QuantumType[] { 215 }, null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, "R");

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("offset", () => image.ImportPixels(new QuantumType[] { 215 }, -1, settings));
                Assert.Contains("The offset should be positive.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetExceedsArrayLength()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("offset", () => image.ImportPixels(new QuantumType[] { 215 }, 1, settings));
                Assert.Contains("The offset should not exceed the length of the array.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsTooLow()
            {
                var settings = new PixelImportSettings(1, 1, StorageType.Quantum, PixelMapping.RGB);

                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportPixels(new QuantumType[] { 215, 215 }, 1, settings));
                Assert.Contains("The data length is 2 but should be at least 4.", exception.Message);
            }

            [Fact]
            public void ShouldImportPixelsFromQuantumArray()
            {
                var data = new QuantumType[]
                {
                    0, 0,
                    0, 0, 0, Quantum.Max,
                    0, Quantum.Max, 0, 0,
                };

                var settings = new PixelImportSettings(1, 0, 1, 2, StorageType.Quantum, PixelMapping.RGBA);

                using var image = new MagickImage(MagickColors.Green, 2, 2);
                image.Alpha(AlphaOption.On);
                image.ImportPixels(data, 2, settings);

                Assert.Equal(2, image.Width);
                Assert.Equal(2, image.Height);

                using var pixels = image.GetPixels();
                var pixel = pixels.GetPixel(0, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Green);

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(MagickColors.Black);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(4, pixel.Channels);
                pixel.Equals(new MagickColor("#00ff0000"));
            }
        }
#endif
    }
}
