// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class TheWriteAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null!));
            }

            [Fact]
            public async Task ShouldUseTheFileExtension()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var input = new MagickImageCollection(Files.CirclePNG, settings);
                using var tempFile = new TemporaryFile(".jpg");
                await input.WriteAsync(tempFile.File);

                using var output = new MagickImageCollection(tempFile.File);

                Assert.Equal(MagickFormat.Jpeg, output[0].Format);
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null!, MagickFormat.Bmp));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                await input.WriteAsync(tempFile.File, MagickFormat.Tiff);

                using var output = new MagickImageCollection(tempFile.File);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileInfoAndWriteDefines
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null!, defines));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenDefinesIsNull()
            {
                var file = new FileInfo(Files.CirclePNG);
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(file, null!));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                await input.WriteAsync(tempFile.File, defines);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                await output.ReadAsync(tempFile.File);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null!));
            }

            [Fact]
            public async Task ShouldWriteToMultipleFilesForFormatThatDoesNotSupportMultipleFrames()
            {
                using var tempDir = new TemporaryDirectory();
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);
                var fileName = Path.Combine(tempDir.FullName, "image.jpg");
                await images.WriteAsync(fileName);

                var files = tempDir.GetFileNames();

                Assert.Equal(3, files.Count);
                Assert.Contains("image-0.jpg", files);
                Assert.Contains("image-1.jpg", files);
                Assert.Contains("image-2.jpg", files);
            }

            [Fact]
            public async Task ShouldAddCorrectSuffixForFormatThatDoesNotSupportMultipleFrames()
            {
                using var tempDir = new TemporaryDirectory();
                using var images = new MagickImageCollection(Files.RoseSparkleGIF);
                var fileName = Path.Combine(tempDir.FullName, "image");
                await images.WriteAsync(fileName, MagickFormat.Bmp);

                var files = tempDir.GetFileNames();

                Assert.Equal(3, files.Count);
                Assert.Contains("image-0", files);
                Assert.Contains("image-1", files);
                Assert.Contains("image-2", files);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null!, MagickFormat.Bmp));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");

                await input.WriteAsync(tempFile.File.FullName, MagickFormat.Tiff);

                using var output = new MagickImageCollection(tempFile.File.FullName);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithFileNameAndWriteDefines
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null!, defines));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenDefinesIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(Files.CirclePNG, null!));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var tempFile = new TemporaryFile("foobar");
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                await input.WriteAsync(tempFile.File.FullName, defines);

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                await output.ReadAsync(tempFile.File.FullName);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null!));
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null!, MagickFormat.Bmp));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.RoseSparkleGIF);
                using var memoryStream = new MemoryStream();
                using var stream = new NonSeekableStream(memoryStream);
                await input.WriteAsync(stream, MagickFormat.Tiff);
                memoryStream.Position = 0;

                using var output = new MagickImageCollection(stream);

                Assert.Equal(3, output.Count);
                for (var i = 0; i < 3; i++)
                    Assert.Equal(MagickFormat.Tiff, output[i].Format);
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFormatIsNotWritable()
            {
                using var memoryStream = new MemoryStream();
                using var input = new MagickImageCollection(Files.CirclePNG);

                await Assert.ThrowsAsync<MagickMissingDelegateErrorException>(() => input.WriteAsync(memoryStream, MagickFormat.Xc));
            }
        }

        public class WithStreamAndWriteDefines
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var defines = new TiffWriteDefines();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.WriteAsync((Stream)null!, defines));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenDefinesIsNull()
            {
                using var stream = new MemoryStream();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(stream, null!));
            }

            [Fact]
            public async Task ShouldUseTheSpecifiedFormat()
            {
                using var input = new MagickImageCollection(Files.CirclePNG);
                using var stream = new MemoryStream();
                var defines = new TiffWriteDefines()
                {
                    Endian = Endian.MSB,
                };
                await input.WriteAsync(stream, defines);
                stream.Position = 0;

                Assert.Equal(MagickFormat.Png, input[0].Format);

                using var output = new MagickImageCollection();
                await output.ReadAsync(stream);

                Assert.Single(output);
                Assert.Equal(MagickFormat.Tiff, output[0].Format);
            }
        }
    }
}
