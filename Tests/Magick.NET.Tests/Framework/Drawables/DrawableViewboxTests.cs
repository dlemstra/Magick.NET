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

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class DrawableViewboxTests
    {
        [TestMethod]
        public void Constructor_WithRectangle_PropertiesAreSet()
        {
            var rectangle = new Rectangle(1, 2, 3, 4);

            var viewbox = new DrawableRectangle(rectangle);
            Assert.AreEqual(1, viewbox.UpperLeftX);
            Assert.AreEqual(2, viewbox.UpperLeftY);
            Assert.AreEqual(4, viewbox.LowerRightX);
            Assert.AreEqual(6, viewbox.LowerRightY);
        }

        [TestMethod]
        public void ExplicitCastFromRectangle_PropertiesAreSet()
        {
            var rectangle = new Rectangle(4, 3, 2, 1);

            var viewbox = (DrawableViewbox)rectangle;
            Assert.AreEqual(4, viewbox.UpperLeftX);
            Assert.AreEqual(3, viewbox.UpperLeftY);
            Assert.AreEqual(6, viewbox.LowerRightX);
            Assert.AreEqual(4, viewbox.LowerRightY);
        }
    }
}

#endif