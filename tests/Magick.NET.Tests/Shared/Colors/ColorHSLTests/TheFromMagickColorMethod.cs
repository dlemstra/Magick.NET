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
    public partial class ColorHSLTests : ColorBaseTests<ColorHSL>
    {
        [TestClass]
        public class TheFromMagickColorMethod
        {
            [TestMethod]
            public void ShouldInitializeTheProperties()
            {
                var color = new MagickColor(Quantum.Max, Quantum.Max, (QuantumType)(Quantum.Max * 0.02));
                var hslColor = ColorHSL.FromMagickColor(color);

                Assert.AreEqual(0.16, hslColor.Hue, 0.01);
                Assert.AreEqual(0.5, hslColor.Lightness, 0.01);
                Assert.AreEqual(1.0, hslColor.Saturation, 0.01);
            }
        }
    }
}
