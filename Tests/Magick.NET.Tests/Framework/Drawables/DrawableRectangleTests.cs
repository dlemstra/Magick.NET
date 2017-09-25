// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCOREAPP1_1

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class DrawableRectangleTests
    {
        [TestMethod]
        public void Constructor_WithRectangle_PropertiesAreSet()
        {
            var rectangle = new Rectangle(1, 2, 3, 4);

            var drawRectangle = new DrawableRectangle(rectangle);
            Assert.AreEqual(1, drawRectangle.UpperLeftX);
            Assert.AreEqual(2, drawRectangle.UpperLeftY);
            Assert.AreEqual(4, drawRectangle.LowerRightX);
            Assert.AreEqual(6, drawRectangle.LowerRightY);
        }

        [TestMethod]
        public void ExplicitCastFromRectangle_PropertiesAreSet()
        {
            var rectangle = new Rectangle(4, 3, 2, 1);

            var drawRectangle = (DrawableRectangle)rectangle;
            Assert.AreEqual(4, drawRectangle.UpperLeftX);
            Assert.AreEqual(3, drawRectangle.UpperLeftY);
            Assert.AreEqual(6, drawRectangle.LowerRightX);
            Assert.AreEqual(4, drawRectangle.LowerRightY);
        }
    }
}

#endif