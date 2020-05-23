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
        public class TheNegateMethod
        {
            [TestClass]
            public class WithBoolean
            {
                [TestMethod]
                public void ShouldOnlyNegateGrayscaleWhenSetToTrue()
                {
                    using (var image = new MagickImage("xc:white", 2, 1))
                    {
                        using (var pixels = image.GetPixels())
                        {
                            var pixel = pixels.GetPixel(1, 0);
                            pixel.SetChannel(1, 0);
                            pixel.SetChannel(2, 0);
                        }

                        image.Negate(true);

                        ColorAssert.AreEqual(MagickColors.Black, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Red, image, 1, 0);
                    }
                }
            }
        }
    }
}
