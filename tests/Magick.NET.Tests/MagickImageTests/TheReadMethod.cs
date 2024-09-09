// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheReadMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read((byte[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var image = new MagickImage();
                image.Read(bytes);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read((byte[])null!, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("count", () => image.Read(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var image = new MagickImage();
                image.Read(bytes, 10, (uint)bytes.Length - 10);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
            }
        }

        public class WithByteArrayAndOffsetAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read(null!, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>(), 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("count", () => image.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var fileBytes = File.ReadAllBytes(Files.ImageMagickICO);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var image = new MagickImage();
                image.Read(bytes, 10, (uint)bytes.Length - 10, MagickFormat.Ico);

                Assert.Equal(64U, image.Width);
                Assert.Equal(64U, image.Height);
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read(null!, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("count", () => image.Read(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var settings = new MagickReadSettings();

                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var image = new MagickImage();
                image.Read(bytes, 10, (uint)bytes.Length - 10, settings);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(bytes, 0, (uint)bytes.Length, null);
            }
        }

        public class WithByteArrayAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read((byte[])null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>(), MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(bytes, MagickFormat.Png));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(bytes, MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("data", () => image.Read((byte[])null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("data", () => image.Read(Array.Empty<byte>(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(bytes, null);
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(bytes, settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(bytes, settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithColor
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("color", () => image.Read((MagickColor)null!, 1, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("width", () => image.Read(MagickColors.Red, 0, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("height", () => image.Read(MagickColors.Red, 1, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var red = new MagickColor("red");
                using var image = new MagickImage();
                image.Read(red, 20, 30);

                Assert.Equal(20U, image.Width);
                Assert.Equal(30U, image.Height);
                ColorAssert.Equal(red, image, 10, 10);
            }

            [Fact]
            public void ShouldReadImageFromCmkyColorName()
            {
                var red = new MagickColor("cmyk(0%,100%,100%,0)");
                using var image = new MagickImage();
                image.Read(red, 20, 30);

                Assert.Equal(20U, image.Width);
                Assert.Equal(30U, image.Height);
                Assert.Equal(ColorSpace.CMYK, image.ColorSpace);

                image.Clamp();

                ColorAssert.Equal(red, image, 10, 10);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Read((FileInfo)null!));
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Read((FileInfo)null!, MagickFormat.Png));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("file", () => image.Read((FileInfo)null!, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                image.Read(new FileInfo(Files.CirclePNG), null);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Read((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Read(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsMissing()
            {
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickBlobErrorException>(() => image.Read(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileWithFormatIsMissing()
            {
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickBlobErrorException>(() => image.Read("png:" + Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldReadImage()
            {
                using var image = new MagickImage();
                image.Read(Files.SnakewarePNG);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
            }

            [Fact]
            public void ShouldReadBuiltinImage()
            {
                using var image = new MagickImage();
                image.Read(Files.Builtin.Rose);

                Assert.Equal(70U, image.Width);
                Assert.Equal(46U, image.Height);
                Assert.Equal(MagickFormat.Pnm, image.Format);
            }

            [Fact]
            public void ShouldReadImageWithNonAsciiFileName()
            {
                using var image = new MagickImage();
                image.Read(Files.RoseSparkleGIF);

                Assert.Equal("RöseSparkle.gif", Path.GetFileName(image.FileName));
                Assert.Equal(70U, image.Width);
                Assert.Equal(46U, image.Height);
                Assert.Equal(MagickFormat.Gif, image.Format);
            }

            [Fact]
            public void ShouldReadImageWithFormat()
            {
                using var image = new MagickImage();
                image.Read("png:" + Files.SnakewarePNG);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
            }

            [Fact]
            public void ShouldReadImageFromXcColorName()
            {
                using var image = new MagickImage();
                image.Read("xc:red", 50, 50);

                Assert.Equal(50U, image.Width);
                Assert.Equal(50U, image.Height);
                ColorAssert.Equal(MagickColors.Red, image, 5, 5);
            }

            [Fact]
            public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickBlobErrorException>(() => image.Read("~/test.gif"));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                Assert.Contains(baseDirectory, exception.Message);
            }

            [Fact]
            public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
            {
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickBlobErrorException>(() => image.Read("~"));
                Assert.Contains("~", exception.Message);
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldReadAIFromNonSeekableStream()
            {
                if (!Ghostscript.IsAvailable)
                    return;

                using var stream = new NonSeekableStream(Files.Coders.CartoonNetworkStudiosLogoAI);
                using var image = new MagickImage();
                image.Read(stream);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Read((string)null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Read(string.Empty, MagickFormat.Png));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReadingFile()
            {
                using var image = new MagickImage();
                image.Read(Files.CirclePNG, MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Read((string)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("fileName", () => image.Read(string.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                image.Read(Files.CirclePNG, null);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReadingFile()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();
                image.Read(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }

            [Fact]
            public void ShouldUseTheReadSettings()
            {
                using var image = new MagickImage();
                image.Read(Files.Logos.MagickNETSVG, new MagickReadSettings
                {
                    Density = new Density(72),
                });

                ColorAssert.Equal(new MagickColor("#231f20"), image, 129, 101);
            }
        }

        public class WithFileNameAndSize
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("fileName", () => image.Read((string)null!, 1, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("width", () => image.Read("xc:red", 0, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsZero()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("height", () => image.Read("xc:red", 1, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                using var image = new MagickImage();
                image.Read("xc:red", 20, 30);

                Assert.Equal(20U, image.Width);
                Assert.Equal(30U, image.Height);
                ColorAssert.Equal(MagickColors.Red, image, 10, 10);
            }
        }

        public partial class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Read((Stream)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("stream", () => image.Read(new MemoryStream()));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var testStream = TestStream.ThatCannotRead();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("stream", () => image.Read(testStream));
            }

            [Fact]
            public void ShouldReadImage()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.SnakewarePNG);

                image.Read(fileStream);
                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
            }

            [Fact]
            public void ShouldReadImageFromSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                image.Read(fileStream);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, true);
                using var testImage = new MagickImage();
                testImage.Read(partialStream);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
            }

            [Fact]
            public void ShouldReadImageFromNonSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                image.Read(fileStream);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, false);
                using var testImage = new MagickImage();
                testImage.Read(partialStream);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
            }

            [Fact]
            public void ShouldReadImageFromMemoryStreamWhereBufferIsNotPubliclyVisible()
            {
                var data = File.ReadAllBytes(Files.CirclePNG);
                var testBuffer = new byte[data.Length + 10];
                data.CopyTo(testBuffer, index: 10);

                using var stream = new MemoryStream(testBuffer, index: 10, count: testBuffer.Length - 10);
                using var image = new MagickImage();
                image.Read(stream);
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Read((Stream)null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("stream", () => image.Read(new MemoryStream(), MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var stream = new MemoryStream(bytes);
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(stream, MagickFormat.Png));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReadingStream()
            {
                using var stream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(stream, MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("stream", () => image.Read((Stream)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("stream", () => image.Read(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(fileStream, null);
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var stream = new MemoryStream(bytes);
                using var image = new MagickImage();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Read(stream, settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReadingStream()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var stream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                image.Read(stream, settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }
    }
}
