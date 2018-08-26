// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheAddRangeMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange((byte[])null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("data", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange(new byte[0]);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayReadSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.AddRange(bytes, null);

                    Assert.AreEqual(1, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenEnumerableImagesIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("images", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange((IEnumerable<IMagickImage>)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenEnumerableImagesIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage[0]);

                    Assert.AreEqual(0, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using (var images = new MagickImageCollection(Files.SnakewarePNG))
                {
                    ExceptionAssert.ThrowsArgumentException("images", () =>
                    {
                        images.AddRange(images);
                    });
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage[] { });

                    Assert.AreEqual(0, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange((string)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("fileName", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange(string.Empty);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenFileNameReadSettingsIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    images.AddRange(Files.SnakewarePNG, null);

                    Assert.AreEqual(1, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Add(Files.Missing);
                    }
                }, "error/blob.c/OpenBlob");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.AddRange((Stream)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenStreamReadSettingsIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        images.AddRange(stream, null);

                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    var image = new MagickImage();
                    images.AddRange(new[] { image });

                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.AddRange(new[] { image });
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesContainsDuplicates()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        var image = new MagickImage();
                        images.AddRange(new[] { image, image });
                    });
                }
            }

            [TestMethod]
            public void ShouldAddAllGifFrames()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.AreEqual(3, images.Count);

                    images.AddRange(Files.RoseSparkleGIF);
                    Assert.AreEqual(6, images.Count);
                }
            }

            [TestMethod]
            public void ShouldNotCloneTheInputImages()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    var image = new MagickImage("xc:red", 100, 100);

                    var list = new List<IMagickImage> { image };

                    images.AddRange(list);

                    Assert.IsTrue(ReferenceEquals(image, list[0]));
                }
            }
        }
    }
}
