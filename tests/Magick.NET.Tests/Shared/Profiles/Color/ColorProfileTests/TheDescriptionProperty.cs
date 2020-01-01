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
        public class TheDescriptionProperty
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.AreEqual("Adobe RGB (1998)", ColorProfile.AdobeRGB1998.Description);
                Assert.AreEqual("Apple RGB", ColorProfile.AppleRGB.Description);
                Assert.AreEqual("Coated FOGRA39 (ISO 12647-2:2004)", ColorProfile.CoatedFOGRA39.Description);
                Assert.AreEqual("ColorMatch RGB", ColorProfile.ColorMatchRGB.Description);
                Assert.AreEqual("sRGB IEC61966-2.1", ColorProfile.SRGB.Description);
                Assert.AreEqual("U.S. Web Coated (SWOP) v2", ColorProfile.USWebCoatedSWOP.Description);
            }
        }
    }
}
