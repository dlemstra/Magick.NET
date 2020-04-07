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
    public partial class DensityTests
    {
        [TestClass]
        public class TheToStringMethod
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueForPixelsPerCentimeterUnits()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.AreEqual("1x2 cm", density.ToString());
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueForPixelsPerInchUnits()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerInch);

                Assert.AreEqual("1x2 inch", density.ToString());
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueForUndefinedUnits()
            {
                var density = new Density(1, 2, DensityUnit.Undefined);

                Assert.AreEqual("1x2", density.ToString());
            }

            [TestMethod]
            public void ShouldNotConvertTheValueWhenUnitsMatch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.AreEqual("1x2 cm", density.ToString(DensityUnit.PixelsPerCentimeter));
            }

            [TestMethod]
            public void ShouldNotAddUnitsWhenUndefinedIsSpecified()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.AreEqual("1x2", density.ToString(DensityUnit.Undefined));
            }

            [TestMethod]
            public void ShouldConvertPixelsPerInchToPixelsPerCentimeter()
            {
                var density = new Density(2.54, 5.08, DensityUnit.PixelsPerInch);

                Assert.AreEqual("1x2 cm", density.ToString(DensityUnit.PixelsPerCentimeter));
            }

            [TestMethod]
            public void ShouldConvertPixelsPerCentimeterToPixelsPerInch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.AreEqual("2.54x5.08 inch", density.ToString(DensityUnit.PixelsPerInch));
            }
        }
    }
}
