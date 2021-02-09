// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheWriteMethod
        {
            public class WithFile
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("file", () => image.Write((FileInfo)null));
                    }
                }

                [Fact]
                public void ShouldUseTheFileExtension()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var input = new MagickImage(Files.CirclePNG, readSettings))
                    {
                        using (var tempFile = new TemporaryFile(".jpg"))
                        {
                            input.Write(tempFile);

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
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("file", () => image.Write((FileInfo)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile, MagickFormat.Tiff);
                            Assert.Equal(MagickFormat.Png, input.Format);

                            using (var output = new MagickImage(tempfile))
                            {
                                Assert.Equal(MagickFormat.Tiff, output.Format);
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
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => image.Write((Stream)null));
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => image.Write((Stream)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                input.Write(stream, MagickFormat.Tiff);
                                Assert.Equal(MagickFormat.Png, input.Format);

                                memoryStream.Position = 0;
                                using (var output = new MagickImage(stream))
                                {
                                    Assert.Equal(MagickFormat.Tiff, output.Format);
                                }
                            }
                        }
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => image.Write((string)null));
                    }
                }

                [Fact]
                public void ShouldSyncTheExifProfile()
                {
                    using (var input = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                    {
                        Assert.Equal(OrientationType.TopLeft, input.Orientation);

                        input.Orientation = OrientationType.RightTop;

                        using (var memStream = new MemoryStream())
                        {
                            input.Write(memStream);

                            using (var output = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                            {
                                memStream.Position = 0;
                                output.Read(memStream);

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
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () => image.Write((string)null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public void ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var tempfile = new TemporaryFile("foobar"))
                        {
                            input.Write(tempfile.FullName, MagickFormat.Tiff);
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
