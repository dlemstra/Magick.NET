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

namespace Magick.NET.Tests.Framework.Drawables
{
    public partial class DrawableFillColorTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void Constructor_WithColor_ColorPropertyIsCorrect()
            {
                DrawableFillColor fillColor = new DrawableFillColor(Color.DarkKhaki);
                ColorAssert.AreEqual(MagickColors.DarkKhaki, fillColor.Color);
            }
        }
    }
}

#endif