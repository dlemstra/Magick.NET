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
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [TestClass]
        public class TheAddRangeMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((byte[])null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("data", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(new byte[0]);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayReadSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using (var images = new MagickImageCollection())
                {
                    images.AddRange(bytes, null);

                    EnumerableAssert.IsSingle(images);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenEnumerableImagesIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("images", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((IEnumerable<IMagickImage<QuantumType>>)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenEnumerableImagesIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage<QuantumType>[0]);

                    EnumerableAssert.IsEmpty(images);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using (var images = new MagickImageCollection(Files.SnakewarePNG))
                {
                    ExceptionAssert.Throws<ArgumentException>("images", () =>
                    {
                        images.AddRange(images);
                    });
                }
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(new IMagickImage<QuantumType>[] { });

                    EnumerableAssert.IsEmpty(images);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((string)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(string.Empty);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenFileNameReadSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.AddRange(Files.SnakewarePNG, null);

                    EnumerableAssert.IsSingle(images);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Add(Files.Missing);
                    }
                }, "error/blob.c/OpenBlob");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange((Stream)null);
                    }
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenStreamReadSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    using (var stream = File.OpenRead(Files.SnakewarePNG))
                    {
                        images.AddRange(stream, null);

                        EnumerableAssert.IsSingle(images);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (var images = new MagickImageCollection())
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
                using (var images = new MagickImageCollection())
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
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.AreEqual(3, images.Count);

                    images.AddRange(Files.RoseSparkleGIF);
                    Assert.AreEqual(6, images.Count);
                }
            }

            [TestMethod]
            public void ShouldNotCloneTheInputImages()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage("xc:red", 100, 100);

                    var list = new List<IMagickImage<QuantumType>> { image };

                    images.AddRange(list);

                    Assert.IsTrue(ReferenceEquals(image, list[0]));
                }
            }
        }
    }
}
