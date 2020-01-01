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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MagickConverterTests
    {
        [TestMethod]
        public void ConvertWithObject_ValueIsNull_ReturnsDefault()
        {
            int value = MagickConverter.Convert<int>((object)null);
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void ConvertWithObject_ValueIsSameType_ReturnsValue()
        {
            int exptected = 4;
            int actual = MagickConverter.Convert<int>(exptected);
            Assert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsBooleanAndValueIsString_ReturnsValue()
        {
            bool value = MagickConverter.Convert<bool>((object)"true");
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsPercentageAndValueIsInteger_ReturnsValue()
        {
            Percentage value = MagickConverter.Convert<Percentage>(10);
            Assert.AreEqual(new Percentage(10), value);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsPercentageAndValueIsDouble_ReturnsValue()
        {
            Percentage value = MagickConverter.Convert<Percentage>(4.2);
            Assert.AreEqual(new Percentage(4.2), value);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsPercentageAndValueIsBool_ThrowsException()
        {
            ExceptionAssert.Throws<InvalidCastException>(() =>
            {
                Percentage value = MagickConverter.Convert<Percentage>(false);
            });
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsIntegerAndValueIsString_ReturnsValue()
        {
            int value = MagickConverter.Convert<int>((object)"4");
            Assert.AreEqual(4, value);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsIntegerAndValueIsInvalidString_ThrowsException()
        {
            ExceptionAssert.Throws<FormatException>(() =>
            {
                MagickConverter.Convert<int>((object)"Magick");
            });
        }

        [TestMethod]
        public void ConvertWithString_ValueIsNull_ReturnsDefault()
        {
            int value = MagickConverter.Convert<int>((string)null);
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void ConvertWithString_ValueIsEmpty_ReturnsDefault()
        {
            int value = MagickConverter.Convert<int>(string.Empty);
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsNullableEnum_ReturnsValue()
        {
            MagickFormat? value = MagickConverter.Convert<MagickFormat?>("png");
            Assert.AreEqual(MagickFormat.Png, value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsEnum_ReturnsValue()
        {
            MagickFormat value = MagickConverter.Convert<MagickFormat>("jPeg");
            Assert.AreEqual(MagickFormat.Jpeg, value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsBoolean_ReturnsValue()
        {
            bool value = MagickConverter.Convert<bool>("1");
            Assert.AreEqual(true, value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsDensity_ReturnsValue()
        {
            Density value = MagickConverter.Convert<Density>("1x2");
            Assert.AreEqual(1, value.X);
            Assert.AreEqual(2, value.Y);
            Assert.AreEqual(DensityUnit.Undefined, value.Units);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsMagickColor_ReturnsValue()
        {
            MagickColor value = MagickConverter.Convert<MagickColor>("#fff");
            ColorAssert.AreEqual(MagickColors.White, value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsMagickGeomerty_ReturnsValue()
        {
            MagickGeometry value = MagickConverter.Convert<MagickGeometry>("1x2+3-4");
            Assert.AreEqual(1, value.Width);
            Assert.AreEqual(2, value.Height);
            Assert.AreEqual(3, value.X);
            Assert.AreEqual(-4, value.Y);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsPercentage_ReturnsValue()
        {
            Percentage value = MagickConverter.Convert<Percentage>("4.2");
            Assert.AreEqual(new Percentage(4.2), value);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsPointD_ReturnsValue()
        {
            PointD value = MagickConverter.Convert<PointD>("1x2");
            Assert.AreEqual(1, value.X);
            Assert.AreEqual(2, value.Y);
        }

        [TestMethod]
        public void ConvertWithString_TypeIsInteger_ReturnsValue()
        {
            int value = MagickConverter.Convert<int>("4");
            Assert.AreEqual(4, value);
        }

        [TestMethod]
        public void ConvertWithObject_TypeIsIntegerAndValueIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<FormatException>(() =>
            {
                MagickConverter.Convert<int>("Magick");
            });
        }
    }
}
