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
    public partial class ColorProfileTests
    {
        [TestClass]
        public class TheModelProperty
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.IsNull(ColorProfile.AdobeRGB1998.Model);
                Assert.IsNull(ColorProfile.AppleRGB.Model);
                Assert.IsNull(ColorProfile.CoatedFOGRA39.Model);
                Assert.IsNull(ColorProfile.ColorMatchRGB.Model);
                Assert.AreEqual("IEC 61966-2.1 Default RGB colour space - sRGB", ColorProfile.SRGB.Model);
                Assert.IsNull(ColorProfile.USWebCoatedSWOP.Model);
            }
        }
    }
}
