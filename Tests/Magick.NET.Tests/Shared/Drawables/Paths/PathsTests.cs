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

using System.Collections;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class PathsTests
    {
        [TestMethod]
        public void Test_Draw_Drawables()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Green, 100, 10))
            {
                image.Draw(new Drawables()
                  .StrokeColor(MagickColors.Red)
                  .StrokeWidth(5)
                  .Paths()
                  .LineToRel(10, 2)
                  .LineToRel(80, 4));

                ColorAssert.AreEqual(MagickColors.Green, image, 9, 5);
                ColorAssert.AreEqual(MagickColors.Red, image, 55, 5);
                ColorAssert.AreEqual(MagickColors.Green, image, 90, 2);
                ColorAssert.AreEqual(MagickColors.Green, image, 90, 9);
            }
        }

        [TestMethod]
        public void Test_Draw_Paths()
        {
            using (IMagickImage image = new MagickImage(MagickColors.Fuchsia, 100, 3))
            {
                image.Draw(new Paths()
                  .LineToAbs(10, 1)
                  .LineToAbs(90, 1));

                ColorAssert.AreEqual(MagickColors.Fuchsia, image, 9, 1);

                ColorAssert.AreEqual(MagickColors.Fuchsia, image, 10, 0);
                ColorAssert.AreEqual(MagickColors.Black, image, 10, 1);
                ColorAssert.AreEqual(MagickColors.Fuchsia, image, 10, 2);

                ColorAssert.AreEqual(MagickColors.Fuchsia, image, 90, 0);
                ColorAssert.AreEqual(MagickColors.Black, image, 90, 1);
                ColorAssert.AreEqual(MagickColors.Fuchsia, image, 90, 2);
            }
        }

        [TestMethod]
        public void Test_Paths()
        {
            Paths paths = null;

            Drawables drawables = paths;
            Assert.IsNull(paths);

            paths = new Paths();
            IEnumerator enumerator = ((IEnumerable)paths).GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}