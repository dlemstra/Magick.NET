// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegWriteDefinesTests
{
    public class TheArithmeticCodingProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            var defines = new JpegWriteDefines
            {
                ArithmeticCoding = false,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));
        }

        [Fact]
        public void ShouldEncodeTheImageArithmetic()
        {
            var defines = new JpegWriteDefines
            {
                ArithmeticCoding = true,
            };

            using var input = new MagickImage(Files.Builtin.Logo);
            using var memStream = new MemoryStream();
            input.Write(memStream, defines);

            Assert.Equal("true", input.Settings.GetDefine(MagickFormat.Jpeg, "arithmetic-coding"));

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            var arithmeticCoding = output.GetAttribute("jpeg:arithmetic-coding");
            Assert.Equal("true", arithmeticCoding);
        }
    }
}
