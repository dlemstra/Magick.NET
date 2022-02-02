// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickFormatInfoTests
    {
        [Fact]
        public void Test_Properties()
        {
            var formatInfo = MagickNET.GetFormatInformation(MagickFormat.Gradient);
            Assert.NotNull(formatInfo);
            Assert.Equal(MagickFormat.Gradient, formatInfo.Format);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Gradual linear passing from one shade to another", formatInfo.Description);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.False(formatInfo.IsWritable);
            Assert.Null(formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jp2);
            Assert.NotNull(formatInfo);
            Assert.Equal(MagickFormat.Jp2, formatInfo.Format);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("JPEG-2000 File Format Syntax", formatInfo.Description);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/jp2", formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jpg);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Joint Photographic Experts Group JFIF format", formatInfo.Description);
            Assert.Equal(MagickFormat.Jpg, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/jpeg", formatInfo.MimeType);
            Assert.Equal(MagickFormat.Jpeg, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Png);
            Assert.NotNull(formatInfo);
            Assert.True(formatInfo.CanReadMultithreaded);
            Assert.True(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Portable Network Graphics", formatInfo.Description);
            Assert.Equal(MagickFormat.Png, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.True(formatInfo.IsWritable);
            Assert.Equal("image/png", formatInfo.MimeType);
            Assert.Equal(MagickFormat.Png, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Pango);
            Assert.NotNull(formatInfo);
            Assert.False(formatInfo.CanReadMultithreaded);
            Assert.False(formatInfo.CanWriteMultithreaded);
            Assert.Equal("Pango Markup Language", formatInfo.Description);
            Assert.Equal(MagickFormat.Pango, formatInfo.Format);
            Assert.False(formatInfo.IsMultiFrame);
            Assert.True(formatInfo.IsReadable);
            Assert.False(formatInfo.IsWritable);
            Assert.Null(formatInfo.MimeType);
            Assert.Equal(MagickFormat.Pango, formatInfo.ModuleFormat);
        }
    }
}
