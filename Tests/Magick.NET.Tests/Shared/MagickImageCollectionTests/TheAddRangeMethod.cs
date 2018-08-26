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

using System.Collections.Generic;
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
            public void ShouldCloneTheImagesWhenInputIsMagickImageCollection()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.AddRange(images);

                    Assert.IsFalse(ReferenceEquals(images[0], images[3]));
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
