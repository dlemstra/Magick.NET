// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheWriteAsyncMethod
        {
            public class WithFileInfo
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null));
                    }
                }

                [Fact]
                public async Task ShouldUseTheFileExtension()
                {
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection(Files.CirclePNG, settings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            await input.WriteAsync(tempFile.FileInfo);

                            using (var output = new MagickImageCollection(tempFile.FileInfo))
                            {
                                Assert.Equal(MagickFormat.Jpeg, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            await input.WriteAsync(tempfile.FileInfo, MagickFormat.Tiff);

                            using (var output = new MagickImageCollection(tempfile.FileInfo))
                            {
                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithFileInfoAndWriteDefines
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var defines = new TiffWriteDefines();

                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.WriteAsync((FileInfo)null, defines));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenDefinesIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var file = new FileInfo(Files.CirclePNG);

                        await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(file, null));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            var defines = new TiffWriteDefines()
                            {
                                Endian = Endian.MSB,
                            };

                            await input.WriteAsync(tempfile.FileInfo, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input[0].Format);

                            using (var output = new MagickImageCollection())
                            {
                                await output.ReadAsync(tempfile.FileInfo);

                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null));
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            await input.WriteAsync(tempfile.FullName, MagickFormat.Tiff);

                            using (var output = new MagickImageCollection(tempfile.FullName))
                            {
                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithFileNameAndWriteDefines
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        var defines = new TiffWriteDefines();

                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.WriteAsync((string)null, defines));
                    }
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenDefinesIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("defines", () => images.WriteAsync(Files.CirclePNG, null));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            var defines = new TiffWriteDefines()
                            {
                                Endian = Endian.MSB,
                            };

                            await input.WriteAsync(tempfile.FullName, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input[0].Format);

                            using (var output = new MagickImageCollection())
                            {
                                await output.ReadAsync(tempfile.FullName);

                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }
        }
    }
}

#endif
