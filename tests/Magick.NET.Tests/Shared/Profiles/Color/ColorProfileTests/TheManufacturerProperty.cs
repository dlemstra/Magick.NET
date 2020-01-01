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
        public class TheManufacturerProperty
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.IsNull(ColorProfile.AdobeRGB1998.Manufacturer);
                Assert.IsNull(ColorProfile.AppleRGB.Manufacturer);
                Assert.IsNull(ColorProfile.CoatedFOGRA39.Manufacturer);
                Assert.IsNull(ColorProfile.ColorMatchRGB.Manufacturer);
                Assert.AreEqual("IEC http://www.iec.ch", ColorProfile.SRGB.Manufacturer);
                Assert.IsNull(ColorProfile.USWebCoatedSWOP.Manufacturer);
            }
        }
    }
}
