// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public partial class TheWriteAsyncMethod
        {
            public class WithFile
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.WriteAsync((FileInfo)null));
                    }
                }

                [Fact]
                public async Task ShouldUseTheFileExtension()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImage(Files.CirclePNG, readSettings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            await input.WriteAsync(tempFile);

                            using (var output = new MagickImage(tempFile))
                            {
                                Assert.Equal(MagickFormat.Jpeg, output.Format);
                            }
                        }
                    }
                }
            }

            public class WithFileAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.WriteAsync((FileInfo)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            await input.WriteAsync(tempfile, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input.Format);

                            using (var output = new MagickImage(tempfile))
                            {
                                Assert.Equal(MagickFormat.Tiff, output.Format);
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
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.WriteAsync((string)null));
                    }
                }

                [Fact]
                public async Task ShouldSyncTheExifProfile()
                {
                    using (var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                    {
                        Assert.Equal(OrientationType.TopLeft, input.Orientation);

                        input.Orientation = OrientationType.RightTop;

                        using (var memStream = new MemoryStream())
                        {
                            await input.WriteAsync(memStream);

                            using (var output = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                            {
                                memStream.Position = 0;
                                await output.ReadAsync(memStream);

                                Assert.Equal(OrientationType.RightTop, output.Orientation);

                                var profile = output.GetExifProfile();

                                Assert.NotNull(profile);
                                var exifValue = profile.GetValue(ExifTag.Orientation);

                                Assert.NotNull(exifValue);
                                Assert.Equal((ushort)6, exifValue.Value);
                            }
                        }
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.WriteAsync((string)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            await input.WriteAsync(tempfile.FullName, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input.Format);

                            using (var output = new MagickImage(tempfile.FullName))
                            {
                                Assert.Equal(MagickFormat.Tiff, output.Format);
                            }
                        }
                    }
                }
            }
        }
    }
}
#endif
