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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Types
{
    public partial class DensityTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("value", () => { new Density(null); });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density(string.Empty); });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density("1.0x"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density("x1.0"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density("ax1.0"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density("1.0xb"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new Density("1.0x6 magick"); });
            }

            [TestMethod]
            public void ShouldUsePixelsPerInchAsTheDefaultUnits()
            {
                var density = new Density(5);
                Assert.AreEqual(5.0, density.X);
                Assert.AreEqual(5.0, density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);
            }

            [TestMethod]
            public void ShouldSetTheUnits()
            {
                var density = new Density(8.5, DensityUnit.PixelsPerCentimeter);
                Assert.AreEqual(8.5, density.X);
                Assert.AreEqual(8.5, density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerCentimeter, density.Units);
            }

            [TestMethod]
            public void ShouldSetTheXAndYDensity()
            {
                var density = new Density(2, 3);
                Assert.AreEqual(2.0, density.X);
                Assert.AreEqual(3.0, density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);
            }

            [TestMethod]
            public void ShouldSetTheXAndYDensityAndUnits()
            {
                var density = new Density(2.2, 3.3, DensityUnit.Undefined);
                Assert.AreEqual(2.2, density.X);
                Assert.AreEqual(3.3, density.Y);
                Assert.AreEqual(DensityUnit.Undefined, density.Units);
            }

            [TestMethod]
            public void ShouldSetTheXAndYDensityFromTheValue()
            {
                var density = new Density("1.0x2.5");
                Assert.AreEqual(1.0, density.X);
                Assert.AreEqual(2.5, density.Y);
                Assert.AreEqual(DensityUnit.Undefined, density.Units);
                Assert.AreEqual("1x2.5", density.ToString());
            }

            [TestMethod]
            public void ShouldSetTheCorrectUnitsForCm()
            {
                var density = new Density("2.5x1.0 cm");
                Assert.AreEqual(2.5, density.X);
                Assert.AreEqual(1.0, density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerCentimeter, density.Units);
                Assert.AreEqual("2.5x1 cm", density.ToString());
            }

            [TestMethod]
            public void ShouldSetTheCorrectUnitsForInch()
            {
                var density = new Density("2.5x1.0 inch");
                Assert.AreEqual(2.5, density.X);
                Assert.AreEqual(1.0, density.Y);
                Assert.AreEqual(DensityUnit.PixelsPerInch, density.Units);
                Assert.AreEqual("2.5x1 inch", density.ToString());
            }
        }
    }
}
