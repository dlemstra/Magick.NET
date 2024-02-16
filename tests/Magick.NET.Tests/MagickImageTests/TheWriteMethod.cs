// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheWriteMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Write((FileInfo)null));
            }

            [Fact]
            public void ShouldUseTheFileExtension()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var input = new MagickImage(Files.CirclePNG, settings);
                using var tempFile = new TemporaryFile(".jpg");
                input.Write(tempFile.File);

                using var output = new MagickImage(tempFile.File);

                Assert.Equal(MagickFormat.Jpeg, output.Format);
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Write((FileInfo)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImage(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar.jpg");

                input.Write(tempFile.File, MagickFormat.Tiff);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(tempFile.File);

                Assert.Equal(MagickFormat.Tiff, output.Format);
            }
        }

        public class WithFileInfoAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var defines = new JpegWriteDefines();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Write((FileInfo)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWriteDefinesIsNull()
            {
                var file = new FileInfo(Files.CirclePNG);
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("defines", () => image.Write(file, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImage(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");

                var defines = new JpegWriteDefines
                {
                    DctMethod = JpegDctMethod.Fast,
                };
                input.Write(tempFile.File, defines);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(tempFile.File);

                Assert.Equal(MagickFormat.Jpeg, output.Format);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Write((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Write(string.Empty));
            }

            [Fact]
            public void ShouldSyncTheExifProfile()
            {
                using var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG);

                Assert.Equal(OrientationType.TopLeft, input.Orientation);

                input.Orientation = OrientationType.RightTop;

                using var memStream = new MemoryStream();
                input.Write(memStream);
                memStream.Position = 0;

                using var output = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
                output.Read(memStream);

                Assert.Equal(OrientationType.RightTop, output.Orientation);

                var profile = output.GetExifProfile();

                Assert.NotNull(profile);

                var exifValue = profile.GetValue(ExifTag.Orientation);

                Assert.NotNull(exifValue);
                Assert.Equal((ushort)6, exifValue.Value);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Write((string)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Write(string.Empty, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImage(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                input.Write(tempFile.File.FullName, MagickFormat.Tiff);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(tempFile.File.FullName);

                Assert.Equal(MagickFormat.Tiff, output.Format);
            }
        }

        public class WithFileNameAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var defines = new JpegWriteDefines();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Write((string)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var defines = new JpegWriteDefines();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Write(string.Empty, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWriteDefinesIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("defines", () => image.Write(Files.CirclePNG, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                var defines = new JpegWriteDefines
                {
                    DctMethod = JpegDctMethod.Fast,
                };
                using var input = new MagickImage(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                input.Write(tempFile.File.FullName, defines);

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(tempFile.File.FullName);

                Assert.Equal(MagickFormat.Jpeg, output.Format);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Write((Stream)null));
            }

            [Fact]
            public void ShouldThrowStreamWithSameLengthAsFile()
            {
                using var image = new MagickImage(Files.Builtin.Logo);

                var format = MagickFormat.Bmp;
                using var memStream = new MemoryStream();
                image.Write(memStream, format);
                memStream.Position = 0;

                using var result = new MagickImage(memStream);

                Assert.Equal(image.Width, result.Width);
                Assert.Equal(image.Height, result.Height);
                Assert.Equal(format, result.Format);
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Write((Stream)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImage(Files.CirclePNG);
                using var memoryStream = new MemoryStream();
                using var stream = new NonSeekableStream(memoryStream);
                input.Write(stream, MagickFormat.Tiff);
                memoryStream.Position = 0;

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(stream);

                Assert.Equal(MagickFormat.Tiff, output.Format);
            }
        }

        public class WithStreamAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var defines = new JpegWriteDefines();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Write((Stream)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWriteDefinesIsNull()
            {
                using var stream = new MemoryStream();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("defines", () => image.Write(stream, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                var defines = new JpegWriteDefines
                {
                    DctMethod = JpegDctMethod.Fast,
                };
                using var stream = new MemoryStream();
                using var input = new MagickImage(Files.CirclePNG);
                input.Write(stream, defines);
                stream.Position = 0;

                Assert.Equal(MagickFormat.Png, input.Format);

                using var output = new MagickImage(stream);

                Assert.Equal(MagickFormat.Jpeg, output.Format);
            }
        }
    }
}
