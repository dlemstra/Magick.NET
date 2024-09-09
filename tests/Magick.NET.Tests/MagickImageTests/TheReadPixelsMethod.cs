// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
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

public partial class MagickImageTests
{
    public partial class TheReadPixelsMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ReadPixels((byte[])null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Array.Empty<byte>(), settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new byte[] { 215 }, null!));
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

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new byte[] { 215 }, settings));
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

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new byte[] { 215 }, settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsUndefined()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = "R",
                    StorageType = StorageType.Undefined,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new byte[] { 215 }, settings));
                Assert.Contains("Storage type should not be undefined.", exception.Message);
            }

            [Fact]
            public void ShouldReadByteArray()
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
                image.ReadPixels(data, settings);

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

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ReadPixels((byte[])null!, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Array.Empty<byte>(), 0, 0, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new byte[] { 215 }, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("count", () => image.ReadPixels(new byte[] { 215 }, 0, 0, settings));
                Assert.Contains("The number of bytes should be at least 1.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetExceedsArrayLength()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("offset", () => image.ReadPixels(new byte[] { 215 }, 1, 1, settings));
                Assert.Contains("The offset should not exceed the length of the array.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsTooLow()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, PixelMapping.RGB);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("count", () => image.ReadPixels(new byte[] { 215 }, settings));
                Assert.Contains("The count is 1 but should be at least 3.", exception.Message);
            }
        }

#if !Q8
        public class WithQuantumArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelReadSettings
                {
                    StorageType = StorageType.Quantum,
                };
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ReadPixels((QuantumType[])null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelReadSettings
                {
                    StorageType = StorageType.Quantum,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Array.Empty<QuantumType>(), settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new QuantumType[] { 215 }, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = null,
                    StorageType = StorageType.Quantum,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = string.Empty,
                    StorageType = StorageType.Quantum,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("Pixel storage mapping should be defined.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStorageTypeIsInvalid()
            {
                var settings = new PixelReadSettings
                {
                    Mapping = "R",
                    StorageType = StorageType.Char,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("Storage type should be Quantum.", exception.Message);
            }

            [Fact]
            public void ShouldReadQuantumArray()
            {
                var data = new QuantumType[]
                {
                    0, 0, 0, Quantum.Max,
                    0, Quantum.Max, 0, 0,
                };
                var settings = new PixelReadSettings(2, 1, StorageType.Quantum, PixelMapping.RGBA);

                using var image = new MagickImage();
                image.ReadPixels(data, settings);

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

        public class WithQuantumArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new PixelReadSettings();
                var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ReadPixels((QuantumType[])null!, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ReadPixels(Array.Empty<QuantumType>(), 0, 0, settings));
                Assert.Contains("Value cannot be empty.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new byte[] { 215 }, 0, 1, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("count", () => image.ReadPixels(new QuantumType[] { 215 }, 0, 0, settings));
                Assert.Contains("The number of items should be at least 1.", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsTooLow()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Quantum, PixelMapping.RGB);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("count", () => image.ReadPixels(new QuantumType[] { 215 }, settings));
                Assert.Contains("The count is 1 but should be at least 3.", exception.Message);
            }
        }
#endif

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.ReadPixels((FileInfo)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new FileInfo(Files.CirclePNG), null!));
            }

            [Fact]
            public void ShouldReadFileInfo()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Float, "R");
                var bytes = BitConverter.GetBytes(1.0F);

                using var tempFile = new TemporaryFile(bytes);
                using var image = new MagickImage();
                image.ReadPixels(tempFile.File, settings);

                Assert.Equal(1U, image.Width);
                Assert.Equal(1U, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.ReadPixels((string)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.ReadPixels(string.Empty, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(Files.CirclePNG, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, null!);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(Files.CirclePNG, settings));
                Assert.Contains("mapping", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("settings", () => image.ReadPixels(Files.CirclePNG, settings));
                Assert.Contains("mapping", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                settings.ReadSettings.Width = null;
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(Files.CirclePNG, settings));
                Assert.Contains("Width", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                settings.ReadSettings.Height = null;
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(Files.CirclePNG, settings));
                Assert.Contains("Height", exception.Message);
            }

            [Fact]
            public void ShouldReadFileName()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Short, "R");
                var bytes = BitConverter.GetBytes(ushort.MaxValue);
                using var tempFile = new TemporaryFile(bytes);
                using var image = new MagickImage();
                image.ReadPixels(tempFile.File.FullName, settings);

                Assert.Equal(1U, image.Width);
                Assert.Equal(1U, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new PixelReadSettings();
                var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.ReadPixels((Stream)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("stream", () => image.ReadPixels(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("settings", () => image.ReadPixels(new MemoryStream(new byte[] { 215 }), null!));
            }

            [Fact]
            public void ShouldReadStream()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Int64, "R");
                var bytes = BitConverter.GetBytes(ulong.MaxValue);
                using var memoryStream = new MemoryStream(bytes);
                using var image = new MagickImage();
                image.ReadPixels(memoryStream, settings);

                Assert.Equal(1U, image.Width);
                Assert.Equal(1U, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }

            [Fact]
            public void ShouldReadNonSeekableStream()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Int64, "R");
                var bytes = BitConverter.GetBytes(ulong.MaxValue);
                using var memoryStream = new NonSeekableStream(bytes);
                using var image = new MagickImage();
                image.ReadPixels(memoryStream, settings);

                Assert.Equal(1U, image.Width);
                Assert.Equal(1U, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }
    }
}
