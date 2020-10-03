// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheAddRangeMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((byte[])null);
                    }
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(new byte[0]);
                    }
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenByteArrayReadSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using (var images = new MagickImageCollection())
                {
                    images.AddRange(bytes, null);

                    Assert.Single(images);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenEnumerableImagesIsNull()
            {
                Assert.Throws<ArgumentNullException>("images", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((IEnumerable<IMagickImage<QuantumType>>)null);
                    }
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenEnumerableImagesIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage<QuantumType>[0]);

                    Assert.Empty(images);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using (var images = new MagickImageCollection(Files.SnakewarePNG))
                {
                    Assert.Throws<ArgumentException>("images", () =>
                    {
                        images.AddRange(images);
                    });
                }
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage<QuantumType>[] { });

                    Assert.Empty(images);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((string)null);
                    }
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(string.Empty);
                    }
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenFileNameReadSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(Files.SnakewarePNG, null);

                    Assert.Single(images);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Add(Files.Missing);
                    }
                });

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((Stream)null);
                    }
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenStreamReadSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        images.AddRange(stream, null);

                        Assert.Single(images);
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage();
                    images.AddRange(new[] { image });

                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.AddRange(new[] { image });
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesContainsDuplicates()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        var image = new MagickImage();
                        images.AddRange(new[] { image, image });
                    });
                }
            }

            [Fact]
            public void ShouldAddAllGifFrames()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.Equal(3, images.Count);

                    images.AddRange(Files.RoseSparkleGIF);
                    Assert.Equal(6, images.Count);
                }
            }

            [Fact]
            public void ShouldNotCloneTheInputImages()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage("xc:red", 100, 100);

                    var list = new List<IMagickImage<QuantumType>> { image };

                    images.AddRange(list);

                    Assert.True(ReferenceEquals(image, list[0]));
                }
            }
        }
    }
}
