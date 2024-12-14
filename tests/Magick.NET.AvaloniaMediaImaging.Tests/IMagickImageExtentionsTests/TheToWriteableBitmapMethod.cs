// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using Avalonia;
using ImageMagick;
using Xunit;

namespace Magick.NET.AvaloniaMediaImaging.Tests;

public partial class IMagickImageExtentionsTests
{
    public class TheToWriteableBitmapMethod
    {
        [Fact]
        public void ShouldReturnWriteableBitmap()
        {
            AppBuilder
                .Configure<Application>()
                .UsePlatformDetect()
                .SetupWithoutStarting();

            using var input = new MagickImage(Files.MagickNETIconPNG);
            using var bitmap = input.ToWriteableBitmap();

            using var outputStream = new MemoryStream();
            bitmap.Save(outputStream);

            outputStream.Position = 0;
            using var output = new MagickImage(outputStream);
            var distortion = output.Compare(input, ErrorMetric.RootMeanSquared);

            Assert.Equal(0.0, distortion);
        }
    }
}
