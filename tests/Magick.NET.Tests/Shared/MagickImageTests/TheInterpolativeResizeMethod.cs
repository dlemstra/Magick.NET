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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheInterpolativeResizeMethod
        {
            [TestMethod]
            public void ShouldResizeTheImage()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    image.InterpolativeResize(32, 32, PixelInterpolateMethod.Mesh);

                    Assert.AreEqual(32, image.Width);
                    Assert.AreEqual(32, image.Height);
                }
            }

            [TestMethod]
            public void ShouldUseThePixelInterpolateMethod()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.InterpolativeResize(150, 100, PixelInterpolateMethod.Mesh);

                    Assert.AreEqual(150, image.Width);
                    Assert.AreEqual(100, image.Height);

                    ColorAssert.AreEqual(new MagickColor("#acacbcbcb2b2"), image, 20, 37);
                    ColorAssert.AreEqual(new MagickColor("#08891d1d4242"), image, 117, 39);
                }
            }
        }
    }
}
