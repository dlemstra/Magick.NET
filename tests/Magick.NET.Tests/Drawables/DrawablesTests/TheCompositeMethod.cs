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
    public partial class DrawablesTests
    {
        public class TheCompositeMethod
        {
            public class WithOffsetAndImage
            {
                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNull()
                {
                    Assert.Throws<ArgumentNullException>("offset", () =>
                    {
                        new Drawables().Composite(null, new MagickImage());
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(new MagickGeometry(), null);
                    });
                }

                [Fact]
                public void ShouldCopyPixelsOfTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Green, 3, 1))
                    {
                        using (var inner = new MagickImage(MagickColors.Purple, 2, 2))
                        {
                            new Drawables()
                                .Composite(new MagickGeometry(1, 0, 1, 1), inner)
                                .Draw(image);
                        }

                        ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                        ColorAssert.Equal(MagickColors.Purple, image, 1, 0);
                        ColorAssert.Equal(MagickColors.Green, image, 2, 0);
                    }
                }
            }

            public class WithOffsetAndCompositeOperatorAndImage
            {
                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNull()
                {
                    Assert.Throws<ArgumentNullException>("offset", () =>
                    {
                        new Drawables().Composite(null, CompositeOperator.Over, new MagickImage());
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(new MagickGeometry(), CompositeOperator.Over, null);
                    });
                }

                [Fact]
                public void ShouldUseTheCompositeOperator()
                {
                    using (var image = new MagickImage(MagickColors.Green, 3, 1))
                    {
                        using (var inner = new MagickImage(MagickColors.Purple, 2, 2))
                        {
                            new Drawables()
                                .Composite(new MagickGeometry(1, 0, 1, 1), CompositeOperator.Plus, inner)
                                .Draw(image);
                        }

                        ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                        ColorAssert.Equal(MagickColors.Gray, image, 1, 0);
                        ColorAssert.Equal(MagickColors.Green, image, 2, 0);
                    }
                }
            }

            public class WithXYAndImage
            {
                [Fact]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(0, 0, null);
                    });
                }

                [Fact]
                public void ShouldCopyPixelsOfTheImage()
                {
                    using (var image = new MagickImage(MagickColors.Green, 3, 1))
                    {
                        using (var inner = new MagickImage(MagickColors.Purple, 2, 2))
                        {
                            new Drawables()
                                .Composite(1, 0, inner)
                                .Draw(image);
                        }

                        ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                        ColorAssert.Equal(MagickColors.Purple, image, 1, 0);
                        ColorAssert.Equal(MagickColors.Purple, image, 2, 0);
                    }
                }
            }

            public class WithXYAndCompositeOperatorAndImage
            {
                [Fact]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(0, 0, CompositeOperator.Over, null);
                    });
                }

                [Fact]
                public void ShouldUseTheCompositeOperator()
                {
                    using (var image = new MagickImage(MagickColors.Green, 3, 1))
                    {
                        using (var inner = new MagickImage(MagickColors.Purple, 2, 2))
                        {
                            new Drawables()
                                .Composite(1, 0, CompositeOperator.Plus, inner)
                                .Draw(image);
                        }

                        ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                        ColorAssert.Equal(MagickColors.Gray, image, 1, 0);
                        ColorAssert.Equal(MagickColors.Gray, image, 2, 0);
                    }
                }
            }
        }
    }
}
