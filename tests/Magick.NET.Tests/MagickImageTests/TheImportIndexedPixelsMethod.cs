// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheImportIndexedPixelsMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorsIsNull()
            {
                var data = new byte[] { 0 };
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("colors", () => image.ImportIndexedPixels(1, 1, null!, data));
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorsIEmpty()
            {
                var colors = Array.Empty<MagickColor>();
                var data = new byte[] { 0 };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("colors", () => image.ImportIndexedPixels(1, 1, colors, data));
                ExceptionAssert.Contains("Value cannot be empty.", exception);
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportIndexedPixels(1, 1, colors, (byte[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                var data = Array.Empty<byte>();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportIndexedPixels(1, 1, colors, data));
                ExceptionAssert.Contains("Value cannot be empty.", exception);
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsToSmall()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                var data = new byte[] { 0 };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportIndexedPixels(1, 2, colors, data));
                ExceptionAssert.Contains("The data length is 1 but should be at least 2.", exception);
            }

            [Fact]
            public void ShouldImportPixelsFromByteArray()
            {
                var data = new byte[]
                {
                    2, 0,
                    3, 1,
                };

                var colors = new MagickColor[]
                {
                    MagickColors.PaleGreen,
                    MagickColors.RebeccaPurple,
                    MagickColors.PaleVioletRed,
                    MagickColors.Orchid,
                };

                using var image = new MagickImage();
                image.ImportIndexedPixels(2, 2, colors, data);

                Assert.Equal(ColorType.Palette, image.ColorType);
                Assert.Equal(ColorSpace.Gray, image.ColorSpace);

                Assert.Equal(2U, image.Width);
                Assert.Equal(2U, image.Height);

                using var pixels = image.GetPixelsUnsafe();
                var pixel = pixels.GetPixel(0, 0);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.PaleVioletRed);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.PaleGreen);

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.Orchid);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.RebeccaPurple);
            }
        }

        public class WithUshortArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorsIsNull()
            {
                var data = new ushort[] { 0 };
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("colors", () => image.ImportIndexedPixels(1, 1, null!, data));
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorsIEmpty()
            {
                var colors = Array.Empty<MagickColor>();
                var data = new ushort[] { 0 };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("colors", () => image.ImportIndexedPixels(1, 1, colors, data));
                ExceptionAssert.Contains("Value cannot be empty.", exception);
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.ImportIndexedPixels(1, 1, colors, (ushort[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                var data = Array.Empty<ushort>();
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportIndexedPixels(1, 1, colors, data));
                ExceptionAssert.Contains("Value cannot be empty.", exception);
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsToSmall()
            {
                var colors = new MagickColor[] { MagickColors.Red };
                var data = new ushort[] { 0 };
                using var image = new MagickImage();

                var exception = Assert.Throws<ArgumentException>("data", () => image.ImportIndexedPixels(1, 2, colors, data));
                ExceptionAssert.Contains("The data length is 1 but should be at least 2.", exception);
            }

            [Fact]
            public void ShouldImportPixelsFromUshortArray()
            {
#if Q8
                var data = new ushort[]
                {
                    157, 7,
                    255, 57,
                };

                var colors = new MagickColor[256];
                colors[7] = MagickColors.PaleGreen;
                colors[57] = MagickColors.RebeccaPurple;
                colors[157] = MagickColors.PaleVioletRed;
                colors[255] = MagickColors.Orchid;
#else
                var data = new ushort[]
                {
                    11257, 157,
                    65535, 1257,
                };

                var colors = new MagickColor[65536];
                colors[157] = MagickColors.PaleGreen;
                colors[1257] = MagickColors.RebeccaPurple;
                colors[11257] = MagickColors.PaleVioletRed;
                colors[65535] = MagickColors.Orchid;
#endif

                using var image = new MagickImage();
                image.ImportIndexedPixels(2, 2, colors, data);

                Assert.Equal(ColorType.Palette, image.ColorType);
                Assert.Equal(ColorSpace.Gray, image.ColorSpace);

                Assert.Equal(2U, image.Width);
                Assert.Equal(2U, image.Height);

                using var pixels = image.GetPixelsUnsafe();
                var pixel = pixels.GetPixel(0, 0);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.PaleVioletRed);

                pixel = pixels.GetPixel(0, 1);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.PaleGreen);

                pixel = pixels.GetPixel(1, 0);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.Orchid);

                pixel = pixels.GetPixel(1, 1);
                Assert.Equal(2U, pixel.Channels);
                pixel.Equals(MagickColors.RebeccaPurple);
            }
        }
    }
}
