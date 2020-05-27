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

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class MagickColorTests
    {
        [TestClass]
        public class TheToColorMethod
        {
            [TestMethod]
            public void ShouldConvertRgbColorToRgba()
            {
                var rgbColor = new ColorRGB(Quantum.Max, 0, 0);
                var magickColor = rgbColor.ToMagickColor();

                var color = magickColor.ToColor();
                Assert.AreEqual(255, color.R);
                Assert.AreEqual(0, color.G);
                Assert.AreEqual(0, color.B);
                Assert.AreEqual(255, color.A);
            }

            [TestMethod]
            public void ShouldConvertCmykColorToRgba()
            {
                var cmkyColor = new ColorCMYK(Quantum.Max, 0, 0, 0, Quantum.Max);
                var magickColor = cmkyColor.ToMagickColor();

                var color = magickColor.ToColor();
                Assert.AreEqual(0, color.R);
                Assert.AreEqual(255, color.G);
                Assert.AreEqual(255, color.B);
                Assert.AreEqual(255, color.A);
            }
        }
    }
}
