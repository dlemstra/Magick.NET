//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

#if !NETCOREAPP1_1

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickColorTests
    {
        [TestMethod]
        public void Test_Transparent()
        {
            MagickColor transparent = null;
            Color transparentColor = transparent;

            Assert.AreEqual(Color.Empty, transparentColor);

            transparent = MagickColors.Transparent;

            ColorAssert.IsTransparent(transparent.A);
            ColorAssert.AreEqual(Color.Transparent, transparent);

            transparent = new MagickColor("transparent");

            ColorAssert.IsTransparent(transparent.A);
            ColorAssert.AreEqual(Color.Transparent, transparent);

            transparentColor = transparent;
            Assert.AreEqual(Color.Transparent.R, transparentColor.R);
            Assert.AreEqual(Color.Transparent.G, transparentColor.G);
            Assert.AreEqual(Color.Transparent.B, transparentColor.B);
            Assert.AreEqual(Color.Transparent.A, transparentColor.A);
        }
    }
}

#endif