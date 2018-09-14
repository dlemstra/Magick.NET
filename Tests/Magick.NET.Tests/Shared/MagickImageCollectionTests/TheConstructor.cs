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

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("data", () =>
                {
                    new MagickImageCollection((byte[])null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("data", () =>
                {
                    new MagickImageCollection(new byte[0]);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenByteArrayReadSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using (IMagickImageCollection images = new MagickImageCollection(bytes, null))
                {
                    Assert.AreEqual(1, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("file", () =>
                {
                    new MagickImageCollection((FileInfo)null);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenFileReadSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);

                using (IMagickImageCollection images = new MagickImageCollection(file, null))
                {
                    Assert.AreEqual(1, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("images", () =>
                {
                    new MagickImageCollection((IEnumerable<IMagickImage>)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using (var images = new MagickImageCollection(Files.SnakewarePNG))
                {
                    ExceptionAssert.ThrowsArgumentException("images", () =>
                    {
                        new MagickImageCollection(images);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                {
                    new MagickImageCollection((Stream)null);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
            {
                using (var stream = File.OpenRead(Files.SnakewarePNG))
                {
                    using (IMagickImageCollection images = new MagickImageCollection(stream, null))
                    {
                        Assert.AreEqual(1, images.Count);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    new MagickImageCollection((string)null);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.ThrowsArgumentException("fileName", () =>
                {
                    new MagickImageCollection(string.Empty);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.SnakewarePNG, null))
                {
                    Assert.AreEqual(1, images.Count);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    new MagickImageCollection(Files.Missing);
                }, "error/blob.c/OpenBlob");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImagesContainsDuplicates()
            {
                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    var image = new MagickImage();
                    new MagickImageCollection(new[] { image, image });
                });
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingFile()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImageCollection input = new MagickImageCollection(Files.CirclePNG, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingStream()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (var stream = File.OpenRead(Files.CirclePNG))
                {
                    using (IMagickImageCollection input = new MagickImageCollection(stream, readSettings))
                    {
                        Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                    }
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingBytes()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using (IMagickImageCollection input = new MagickImageCollection(bytes, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input[0].Settings.Format);
                }
            }

            [TestMethod]
            public void ShouldNotCloneTheInputImages()
            {
                var image = new MagickImage("xc:red", 100, 100);

                var list = new List<IMagickImage> { image };

                using (IMagickImageCollection images = new MagickImageCollection(list))
                {
                    Assert.IsTrue(ReferenceEquals(image, list[0]));
                }
            }
        }
    }
}
