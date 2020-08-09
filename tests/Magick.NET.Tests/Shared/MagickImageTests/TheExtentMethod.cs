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
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheExtentMethod
        {
            [TestClass]
            public class WithWidthAndHeight
            {
                [TestMethod]
                public void ShouldExtentTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageAtOffset()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(-1, -1, 2, 3);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3, MagickColors.Purple);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 1, 1);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedGravity()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(2, 3, Gravity.Southeast);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3, Gravity.Northwest, MagickColors.Purple);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 1, 1);
                    }
                }
            }

            [TestClass]
            public class WithGeometry
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenGeometryIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () => image.Extent(null));
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3));

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageAtOffset()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(new MagickGeometry(-1, -1, 2, 3));

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3), MagickColors.Purple);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 1, 1);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedGravity()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(new MagickGeometry(2, 3), Gravity.Southwest);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    }
                }

                [TestMethod]
                public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3), Gravity.Southwest, MagickColors.Purple);

                        Assert.AreEqual(2, image.Width);
                        Assert.AreEqual(3, image.Height);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    }
                }
            }
        }
    }
}
