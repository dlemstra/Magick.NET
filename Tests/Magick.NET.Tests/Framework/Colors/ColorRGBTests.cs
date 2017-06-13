//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Magick.NET.Tests
{
    public partial class ColorRGBTests : ColorBaseTests<ColorRGB>
    {
        [TestMethod]
        public void Test_Color()
        {
            ColorRGB fuchsia = new ColorRGB(Color.Fuchsia);

            ColorAssert.AreEqual(fuchsia, MagickColors.Fuchsia);
        }
    }
}

#endif