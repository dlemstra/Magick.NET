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
        public class TheResizeMethod
        {
            [TestClass]
            public class WithMagickGeometry
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenGeometryIsNull()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () => image.Resize(null));
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.RedPNG))
                    {
                        image.Resize(new MagickGeometry(64, 64));

                        Assert.AreEqual(64, image.Width);
                        Assert.AreEqual(21, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldIgnoreTheAspectRatioWhenSpecified()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize(new MagickGeometry("5x10!"));

                        Assert.AreEqual(5, image.Width);
                        Assert.AreEqual(10, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldNotResizeTheImageWhenLarger()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize(new MagickGeometry("32x32<"));

                        Assert.AreEqual(128, image.Width);
                        Assert.AreEqual(128, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImageWhenSmaller()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize(new MagickGeometry("256x256<"));

                        Assert.AreEqual(256, image.Width);
                        Assert.AreEqual(256, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldNotResizeTheImageWhenSmaller()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize(new MagickGeometry("256x256>"));

                        Assert.AreEqual(128, image.Width);
                        Assert.AreEqual(128, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImageWhenLarger()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize(new MagickGeometry("32x32>"));

                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(32, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldResizeToSmallerArea()
                {
                    using (var image = new MagickImage(Files.SnakewarePNG))
                    {
                        image.Resize(new MagickGeometry("4096@"));

                        Assert.IsTrue((image.Width * image.Height) < 4096);
                    }
                }
            }

            [TestClass]
            public class WithPercentage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenPercentageIsNegative()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        var percentage = new Percentage(-0.5);

                        ExceptionAssert.Throws<ArgumentException>("percentage", () => image.Resize(percentage));
                    }
                }

                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.MagickNETIconPNG))
                    {
                        image.Resize((Percentage)200);

                        Assert.AreEqual(256, image.Width);
                        Assert.AreEqual(256, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithWidthAndHeight
            {
                [TestMethod]
                public void ShouldResizeTheImage()
                {
                    using (var image = new MagickImage(Files.RedPNG))
                    {
                        image.Resize(32, 32);

                        Assert.AreEqual(32, image.Width);
                        Assert.AreEqual(11, image.Height);
                    }
                }
            }
        }
    }
}
