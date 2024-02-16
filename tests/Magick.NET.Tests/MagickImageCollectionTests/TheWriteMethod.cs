// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class TheWriteMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Write((FileInfo)null));
            }

            [Fact]
            public void ShouldUseTheFileExtension()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var input = new MagickImageCollection(Files.CirclePNG, settings);
                using var tempFile = new TemporaryFile(".jpg");
                input.Write(tempFile.File);

                using var output = new MagickImageCollection(tempFile.File);

                Assert.Equal(MagickFormat.Jpeg, output[0].Format);
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Write((FileInfo)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                input.Write(tempFile.File, MagickFormat.Tif);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection(tempFile.File);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileInfoAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Write((FileInfo)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenDefinesIsNull()
            {
                var file = new FileInfo(Files.CirclePNG);
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("defines", () => images.Write(file, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                input.Write(tempFile.File, defines);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                output.Read(tempFile.File);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Write((string)null));
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Write((string)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                input.Write(tempFile.File.FullName, MagickFormat.Tiff);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection(tempFile.File.FullName);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileNameAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Write((string)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenDefinesIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("defines", () => images.Write(Files.CirclePNG, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                input.Write(tempFile.File.FullName, defines);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                output.Read(tempFile.File.FullName);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Write((Stream)null));
            }
        }

        public class WithStreamAndFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Write((Stream)null, MagickFormat.Bmp));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var memoryStream = new MemoryStream();
                using var stream = new NonSeekableStream(memoryStream);
                input.Write(stream, MagickFormat.Tiff);
                memoryStream.Position = 0;

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection(stream);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFormatIsNotWritable()
            {
                using var memoryStream = new MemoryStream();
                using var input = new MagickImageCollection(Files.CirclePNG);

                Assert.Throws<MagickMissingDelegateErrorException>(() => input.Write(memoryStream, MagickFormat.Xc));
            }
        }

        public class WithStreamAndWriteDefines
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Write((Stream)null, defines));
            }

            [Fact]
            public void ShouldThrowExceptionWhenDefinesIsNull()
            {
                using var images = new MagickImageCollection();
                using var stream = new MemoryStream();

                Assert.Throws<ArgumentNullException>("defines", () => images.Write(stream, null));
            }

            [Fact]
            public void ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var stream = new MemoryStream();
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                input.Write(stream, defines);
                stream.Position = 0;

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                output.Read(stream);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }
    }
}
