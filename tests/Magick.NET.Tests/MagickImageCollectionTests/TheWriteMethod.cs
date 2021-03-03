// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheWriteMethod
        {
            public class WithFile
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("file", () => images.Write((FileInfo)null));
                    }
                }

                [Fact]
                public void ShouldUseTheFileExtension()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImageCollection(Files.CirclePNG, readSettings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            input.Write(tempFile);

                            using (var output = new MagickImageCollection(tempFile))
                            {
                                Assert.Equal(MagickFormat.Jpeg, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithFileAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("file", () => images.Write((FileInfo)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile, MagickFormat.Tiff);

                            using (var output = new MagickImageCollection(tempfile))
                            {
                                Assert.Single(output);
                                Assert.Equal(MagickFormat.Tiff, output[0].Format);
                            }
                        }
                    }
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => images.Write((Stream)null));
                    }
                }
            }

            public class WithStreamAndFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => images.Write((Stream)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                input.Write(stream, MagickFormat.Tiff);

                                memoryStream.Position = 0;
                                using (var output = new MagickImageCollection(stream))
                                {
                                    Assert.Single(output);
                                    Assert.Equal(MagickFormat.Tiff, output[0].Format);
                                }
                            }
                        }
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFormatIsNotWritable()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            Assert.Throws<MagickMissingDelegateErrorException>(() => input.Write(memoryStream, MagickFormat.Xc));
                        }
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Write((string)null));
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => images.Write((string)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImageCollection(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile.FullName, MagickFormat.Tiff);

                            using (var output = new MagickImageCollection(tempfile.FullName))
                            {
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
