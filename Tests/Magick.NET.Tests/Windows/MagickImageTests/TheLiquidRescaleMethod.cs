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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheLiquidRescaleMethod
        {
            [TestClass]
            public class WithWidthAndHeight
            {
                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(128, 64);
                        Assert.AreEqual(64, image.Width);
                        Assert.AreEqual(64, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithGeometry
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenGeometryIsNull()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                        {
                            image.LiquidRescale(null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        var geometry = new MagickGeometry(128, 64)
                        {
                            IgnoreAspectRatio = true,
                        };

                        image.LiquidRescale(geometry);
                        Assert.AreEqual(128, image.Width);
                        Assert.AreEqual(64, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithPercentage
            {
                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(new Percentage(25));
                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(32, image.Height);
                    }

                }
                [TestMethod]
                public void ShouldIgnoreTheAspectRatio()
                {
                    using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.LiquidRescale(new Percentage(25), new Percentage(10));
                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(13, image.Height);
                    }
                }
            }
        }
    }
}

#endif