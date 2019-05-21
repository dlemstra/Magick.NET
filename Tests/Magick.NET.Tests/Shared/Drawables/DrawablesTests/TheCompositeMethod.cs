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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class DrawablesTests
    {
        public class TheCompositeMethod
        {
            [TestClass]
            public class WithOffsetAndImage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("offset", () =>
                    {
                        new Drawables().Composite(null, new MagickImage());
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(new MagickGeometry(), null);
                    });
                }

                [TestMethod]
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

                        ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 1, 0);
                        ColorAssert.AreEqual(MagickColors.Green, image, 2, 0);
                    }
                }
            }

            [TestClass]
            public class WithOffsetAndCompositeOperatorAndImage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("offset", () =>
                    {
                        new Drawables().Composite(null, CompositeOperator.Over, new MagickImage());
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(new MagickGeometry(), CompositeOperator.Over, null);
                    });
                }

                [TestMethod]
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

                        ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Gray, image, 1, 0);
                        ColorAssert.AreEqual(MagickColors.Green, image, 2, 0);
                    }
                }
            }

            [TestClass]
            public class WithXYAndImage
            {

                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(0, 0, null);
                    });
                }

                [TestMethod]
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

                        ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 1, 0);
                        ColorAssert.AreEqual(MagickColors.Purple, image, 2, 0);
                    }
                }
            }

            [TestClass]
            public class WithXYAndCompositeOperatorAndImage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                    {
                        new Drawables().Composite(0, 0, CompositeOperator.Over, null);
                    });
                }

                [TestMethod]
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

                        ColorAssert.AreEqual(MagickColors.Green, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Gray, image, 1, 0);
                        ColorAssert.AreEqual(MagickColors.Gray, image, 2, 0);
                    }
                }
            }
        }
    }
}
