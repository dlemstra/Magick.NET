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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheCropMethod
        {
            [TestMethod]
            public void ShouldSetImageToCorrectDimensions()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(40, 50);

                    Assert.AreEqual(40, image.Width);
                    Assert.AreEqual(50, image.Height);
                }
            }

            [TestMethod]
            public void ShouldUseUndefinedGravityAsTheDefault()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(150, 40);

                    Assert.AreEqual(150, image.Width);
                    Assert.AreEqual(40, image.Height);

                    ColorAssert.AreEqual(new MagickColor("#fecd08ff"), image, 146, 25);
                }
            }

            [TestMethod]
            public void ShouldUseCenterGravity()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(50, 40, Gravity.Center);

                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(40, image.Height);

                    ColorAssert.AreEqual(new MagickColor("#223e92ff"), image, 25, 20);
                }
            }

            [TestMethod]
            public void ShouldUseEastGravity()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(50, 40, Gravity.East);

                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(40, image.Height);
                    ColorAssert.AreEqual(MagickColors.White, image, 25, 20);
                }
            }

            [TestMethod]
            public void ShouldUseAspectRatioOfMagickGeometry()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("3:2"));

                    Assert.AreEqual(640, image.Width);
                    Assert.AreEqual(427, image.Height);
                    ColorAssert.AreEqual(MagickColors.White, image, 222, 0);
                }
            }

            [TestMethod]
            public void ShouldUseAspectRatioOfMagickGeometryAndGravity()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("3:2"), Gravity.South);

                    Assert.AreEqual(640, image.Width);
                    Assert.AreEqual(427, image.Height);
                    ColorAssert.AreEqual(MagickColors.Red, image, 222, 0);
                }
            }

            [TestMethod]
            public void ShouldUseOffsetFromMagickGeometryAndGravity()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry(10, 10, 100, 100), Gravity.Center);

                    Assert.AreEqual(100, image.Width);
                    Assert.AreEqual(100, image.Height);
                    ColorAssert.AreEqual(MagickColors.White, image, 99, 99);
                }
            }

            [TestMethod]
            public void ShouldUseUndefinedGravityAsTheDefaultForMagickGeometry()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Crop(new MagickGeometry("150x40"));

                    Assert.AreEqual(150, image.Width);
                    Assert.AreEqual(40, image.Height);

                    ColorAssert.AreEqual(new MagickColor("#fecd08ff"), image, 146, 25);
                }
            }
        }
    }
}
