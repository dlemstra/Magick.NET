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
        public class TheSelectiveBlurMethod
        {
            [TestMethod]
            public void ShouldBlurTheImage()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Rose))
                {
                    image.SelectiveBlur(0, 5, new Percentage(20));

#if Q8
                    ColorAssert.AreEqual(new MagickColor("#df3a39ff"), image, 37, 20);
#elif Q16 || Q16HDRI
                    ColorAssert.AreEqual(new MagickColor("#df003a7738aeffff"), image, 37, 20);
#else
#error Not implemented!
#endif
                }
            }
        }
    }
}
