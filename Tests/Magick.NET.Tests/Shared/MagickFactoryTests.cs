// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class MagickFactoryTests
    {
        [TestMethod]
        public void CreateCollection_ReturnsMagickImageCollection()
        {
            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection())
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(0, collection.Count);
            }
        }

        [TestMethod]
        public void CreateCollection_WithBytes_ReturnsMagickImageCollection()
        {
            var data = File.ReadAllBytes(Files.RoseSparkleGIF);

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(data))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
            }
        }

        [TestMethod]
        public void CreateCollection_WithBytesAndSettings_ReturnsMagickImageCollection()
        {
            var data = File.ReadAllBytes(Files.RoseSparkleGIF);
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Firebrick,
            };

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(data, readSettings))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
                Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
            }
        }

        [TestMethod]
        public void CreateCollection_WithFileInfo_ReturnsMagickImageCollection()
        {
            var file = new FileInfo(Files.RoseSparkleGIF);

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(file))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
            }
        }

        [TestMethod]
        public void CreateCollection_WithFileInfoAndSettings_ReturnsMagickImageCollection()
        {
            var file = new FileInfo(Files.RoseSparkleGIF);
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Firebrick,
            };

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(file, readSettings))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
                Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
            }
        }

        [TestMethod]
        public void CreateCollection_IEnumerableImages_ReturnsMagickImageCollection()
        {
            var image = new MagickImage(Files.ImageMagickJPG);
            var images = new MagickImage[] { image };

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(images))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(1, collection.Count);
                Assert.AreEqual(image, collection.First());
            }

            ExceptionAssert.Throws<ObjectDisposedException>(() =>
            {
                Assert.IsFalse(image.HasAlpha);
            });
        }

        [TestMethod]
        public void CreateCollection_WithStream_ReturnsMagickImageCollection()
        {
            using (var stream = File.OpenRead(Files.RoseSparkleGIF))
            {
                MagickFactory factory = new MagickFactory();
                using (IMagickImageCollection collection = factory.CreateCollection(stream))
                {
                    Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                    Assert.AreEqual(3, collection.Count);
                }
            }
        }

        [TestMethod]
        public void CreateCollection_WithStreamAndSettings_ReturnsMagickImageCollection()
        {
            using (var stream = File.OpenRead(Files.RoseSparkleGIF))
            {
                var readSettings = new MagickReadSettings
                {
                    BackgroundColor = MagickColors.Firebrick,
                };

                MagickFactory factory = new MagickFactory();
                using (IMagickImageCollection collection = factory.CreateCollection(stream, readSettings))
                {
                    Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                    Assert.AreEqual(3, collection.Count);
                    Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
                }
            }
        }

        [TestMethod]
        public void CreateCollection_WithFileName_ReturnsMagickImageCollection()
        {
            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(Files.RoseSparkleGIF))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
            }
        }

        [TestMethod]
        public void CreateCollection_WithFileNameAndSettings_ReturnsMagickImageCollection()
        {
            var readSettings = new MagickReadSettings
            {
                BackgroundColor = MagickColors.Firebrick,
            };

            MagickFactory factory = new MagickFactory();
            using (IMagickImageCollection collection = factory.CreateCollection(Files.RoseSparkleGIF, readSettings))
            {
                Assert.IsInstanceOfType(collection, typeof(MagickImageCollection));
                Assert.AreEqual(3, collection.Count);
                Assert.AreEqual(MagickColors.Firebrick, collection.First().Settings.BackgroundColor);
            }
        }

        [TestMethod]
        public void CreateImageInfo_ReturnsMagickImageInfo()
        {
            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo();

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(0, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithBytes_ReturnsMagickImageInfo()
        {
            var data = File.ReadAllBytes(Files.ImageMagickJPG);

            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(data);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithFileInfo_ReturnsMagickImageInfo()
        {
            var file = new FileInfo(Files.ImageMagickJPG);

            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(file);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithStream_ReturnsMagickImageInfo()
        {
            using (var stream = File.OpenRead(Files.ImageMagickJPG))
            {
                MagickFactory factory = new MagickFactory();
                IMagickImageInfo imageInfo = factory.CreateImageInfo(stream);

                Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
                Assert.AreEqual(123, imageInfo.Width);
            }
        }

        [TestMethod]
        public void CreateImageInfo_WithFileName_ReturnsMagickImageInfo()
        {
            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(Files.ImageMagickJPG);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }
    }
}
