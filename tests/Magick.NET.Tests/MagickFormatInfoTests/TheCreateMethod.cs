// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class MagickFormatInfoTests
{
    public partial class TheCreateMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var exception = Assert.Throws<ArgumentNullException>(() => MagickFormatInfo.Create((byte[])null));

                Assert.Equal("data", exception.ParamName);
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var exception = Assert.Throws<ArgumentException>(() => MagickFormatInfo.Create(Array.Empty<byte>()));

                Assert.Equal("data", exception.ParamName);
            }

            [Fact]
            public void ShouldReturnNullWhenFormatCannotBeDetermined()
            {
                var formatInfo = MagickFormatInfo.Create(new byte[] { 42 });

                Assert.Null(formatInfo);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForTheJpgFormat()
            {
                var bytes = File.ReadAllBytes(Files.ImageMagickJPG);
                var formatInfo = MagickFormatInfo.Create(bytes);

                Assert.NotNull(formatInfo);
                Assert.Equal(MagickFormat.Jpeg, formatInfo.Format);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var exception = Assert.Throws<ArgumentNullException>(() => MagickFormatInfo.Create((FileInfo)null));

                Assert.Equal("file", exception.ParamName);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForThePngFormat()
            {
                var fileInfo = new FileInfo(Files.MagickNETIconPNG);
                var formatInfo = MagickFormatInfo.Create(fileInfo);

                Assert.NotNull(formatInfo);
                Assert.True(formatInfo.CanReadMultithreaded);
                Assert.True(formatInfo.CanWriteMultithreaded);
                Assert.Equal("Portable Network Graphics", formatInfo.Description);
                Assert.Equal(MagickFormat.Png, formatInfo.Format);
                Assert.Equal("image/png", formatInfo.MimeType);
                Assert.Equal(MagickFormat.Png, formatInfo.ModuleFormat);
                Assert.False(formatInfo.SupportsMultipleFrames);
                Assert.True(formatInfo.SupportsReading);
                Assert.True(formatInfo.SupportsWriting);
            }
        }

        public class WithFilename
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileMameIsNull()
            {
                var exception = Assert.Throws<ArgumentNullException>(() => MagickFormatInfo.Create((string)null));

                Assert.Equal("fileName", exception.ParamName);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFilenameIsEmpty()
            {
                var exception = Assert.Throws<ArgumentException>(() => MagickFormatInfo.Create(string.Empty));

                Assert.Equal("fileName", exception.ParamName);
            }

            [Fact]
            public void ShouldReturnNullForUnknownExtension()
            {
                using var tempFile = new TemporaryFile("foo.bar");

                var formatInfo = MagickFormatInfo.Create(tempFile.File.FullName);

                Assert.Null(formatInfo);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForTheJpgFormat()
            {
                var formatInfo = MagickFormatInfo.Create(Files.ImageMagickJPG);

                Assert.NotNull(formatInfo);
                Assert.True(formatInfo.CanReadMultithreaded);
                Assert.True(formatInfo.CanWriteMultithreaded);
                Assert.Equal("Joint Photographic Experts Group JFIF format", formatInfo.Description);
                Assert.Equal(MagickFormat.Jpg, formatInfo.Format);
                Assert.Equal("image/jpeg", formatInfo.MimeType);
                Assert.Equal(MagickFormat.Jpeg, formatInfo.ModuleFormat);
                Assert.False(formatInfo.SupportsMultipleFrames);
                Assert.True(formatInfo.SupportsReading);
                Assert.True(formatInfo.SupportsWriting);
            }
        }

        public class WithMagickFormat
        {
            [Fact]
            public void ShouldReturnNullForUnknownFormat()
            {
                var formatInfo = MagickFormatInfo.Create((MagickFormat)12345);

                Assert.Null(formatInfo);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForTheGradientFormat()
            {
                var formatInfo = MagickFormatInfo.Create(MagickFormat.Gradient);

                Assert.NotNull(formatInfo);
                Assert.Equal(MagickFormat.Gradient, formatInfo.Format);
                Assert.Equal("Gradual linear passing from one shade to another", formatInfo.Description);
                Assert.Null(formatInfo.MimeType);
                Assert.False(formatInfo.SupportsMultipleFrames);
                Assert.True(formatInfo.SupportsReading);
                Assert.True(formatInfo.CanReadMultithreaded);
                Assert.False(formatInfo.SupportsWriting);
                Assert.False(formatInfo.CanWriteMultithreaded);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForTheJp2Format()
            {
                var formatInfo = MagickFormatInfo.Create(MagickFormat.Jp2);

                Assert.NotNull(formatInfo);
                Assert.Equal(MagickFormat.Jp2, formatInfo.Format);
                Assert.True(formatInfo.CanReadMultithreaded);
                Assert.True(formatInfo.CanWriteMultithreaded);
                Assert.Equal("JPEG-2000 File Format Syntax", formatInfo.Description);
                Assert.Equal("image/jp2", formatInfo.MimeType);
                Assert.False(formatInfo.SupportsMultipleFrames);
                Assert.True(formatInfo.SupportsReading);
                Assert.True(formatInfo.SupportsWriting);
            }

            [Fact]
            public void ShouldReturnTheCorrectInfoForThePangoFormat()
            {
                var formatInfo = MagickFormatInfo.Create(MagickFormat.Pango);

                Assert.NotNull(formatInfo);
                Assert.False(formatInfo.CanReadMultithreaded);
                Assert.False(formatInfo.CanWriteMultithreaded);
                Assert.Equal("Pango Markup Language", formatInfo.Description);
                Assert.Equal(MagickFormat.Pango, formatInfo.Format);
                Assert.Null(formatInfo.MimeType);
                Assert.Equal(MagickFormat.Pango, formatInfo.ModuleFormat);
                Assert.False(formatInfo.SupportsMultipleFrames);
                Assert.True(formatInfo.SupportsReading);
                Assert.False(formatInfo.SupportsWriting);
            }

            [Fact]
            public void ShouldReturnFormatInfoForAllFormats()
            {
                var missingFormats = new List<string>();

                foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
                {
                    if (format == MagickFormat.Unknown)
                        continue;

                    var formatInfo = MagickFormatInfo.Create(format);
                    if (formatInfo is null)
                    {
                        if (ShouldReport(format))
                            missingFormats.Add(format.ToString());
                    }
                }

                if (missingFormats.Count > 0)
                    throw new XunitException("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
            }

            private static bool ShouldReport(MagickFormat format)
            {
                if (IsDisabledThroughPolicy(format))
                    return false;

                if ((format == MagickFormat.Clipboard || format == MagickFormat.Emf || format == MagickFormat.Wmf) && !Runtime.IsWindows)
                    return false;

                return true;
            }

            /// <summary>
            /// Disabled with <see cref="TestInitializer.ModifyPolicy(string)"/>.
            /// </summary>
            private static bool IsDisabledThroughPolicy(MagickFormat format)
                => format == MagickFormat.Sun || format == MagickFormat.Ras;
        }
    }
}
