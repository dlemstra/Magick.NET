// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCORE

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Colors
{
    public partial class MagickColorTests
    {
        [TestMethod]
        public void Equals_MagickColorIsNull_EqualToColorEmpty()
        {
            MagickColor transparent = null;
            Color transparentColor = transparent;

            Assert.AreEqual(Color.Empty, transparentColor);
        }

        [TestMethod]
        public void Equals_MagickColorTransparent_EqualToColorTransparent()
        {
            MagickColor transparent = MagickColors.Transparent;
            ColorAssert.AreEqual(Color.Transparent, transparent);
        }

        [TestMethod]
        public void Equals_MagickColorWithConstructedWithStringSetToTransparent_EqualToColorTransparent()
        {
            MagickColor transparent = new MagickColor("transparent");
            ColorAssert.AreEqual(Color.Transparent, transparent);
        }

        [TestMethod]
        public void TransparentColor_ChannelValuesEqualToColorTransparent()
        {
            MagickColor transparent = MagickColors.Transparent;

#if Q8
            int multiplication = 1;
#elif Q16 || Q16HDRI
            int multiplication = 257;
#else
#error Not implemented!
#endif

            Assert.AreEqual(Color.Transparent.R * multiplication, transparent.R);
            Assert.AreEqual(Color.Transparent.G * multiplication, transparent.G);
            Assert.AreEqual(Color.Transparent.B * multiplication, transparent.B);
            Assert.AreEqual(Color.Transparent.A * multiplication, transparent.A);
        }
    }
}

#endif