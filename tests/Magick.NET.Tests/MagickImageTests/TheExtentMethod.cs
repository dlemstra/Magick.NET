// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheExtentMethod
        {
            public class WithWidthAndHeight
            {
                [Fact]
                public void ShouldExtentTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageAtOffset()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(-1, -1, 2, 3);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3, MagickColors.Purple);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedGravity()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(2, 3, Gravity.Southeast);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(2, 3, Gravity.Northwest, MagickColors.Purple);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
                    }
                }
            }

            public class WithGeometry
            {
                [Fact]
                public void ShouldThrowExceptionWhenGeometryIsNull()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        Assert.Throws<ArgumentNullException>("geometry", () => image.Extent(null));
                    }
                }

                [Fact]
                public void ShouldExtentTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3));

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageAtOffset()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(new MagickGeometry(-1, -1, 2, 3));

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3), MagickColors.Purple);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedGravity()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.BackgroundColor = MagickColors.Purple;
                        image.Extent(new MagickGeometry(2, 3), Gravity.Southwest);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
                    }
                }

                [Fact]
                public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
                {
                    using (var image = new MagickImage(MagickColors.Black, 1, 1))
                    {
                        image.Extent(new MagickGeometry(2, 3), Gravity.Southwest, MagickColors.Purple);

                        Assert.Equal(2, image.Width);
                        Assert.Equal(3, image.Height);
                        ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
                    }
                }
            }
        }
    }
}
