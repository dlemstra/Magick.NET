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
    public partial class MagickColorTests
    {
        [TestClass]
        public class TheToStringMethod
        {
            [TestMethod]
            public void ShouldReturnTheCorrectString()
            {
                var color = new MagickColor(MagickColors.Red);
#if Q8
                Assert.AreEqual("#FF0000FF", color.ToString());
#elif Q16 || Q16HDRI
                Assert.AreEqual("#FFFF00000000FFFF", color.ToString());
#else
#error Not implemented!
#endif
            }

            [TestMethod]
            public void ShouldReturnTheCorrectStringForCmykColor()
            {
#if Q8
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.Byte)(Quantum.Max / 3));
#elif Q16
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.UInt16)(Quantum.Max / 3));
#elif Q16HDRI
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.Single)(Quantum.Max / 3));
#else
#error Not implemented!
#endif
                Assert.AreEqual("cmyka(0," + Quantum.Max + ",0,0,0.3333)", color.ToString());

                color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
                Assert.AreEqual("cmyka(0," + Quantum.Max + ",0,0,1.0)", color.ToString());
            }
        }
    }
}
