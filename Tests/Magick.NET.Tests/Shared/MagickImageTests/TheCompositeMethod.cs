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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        public partial class TheCompositeMethod
        {
            [TestClass]
            public class WithGravityAndOperator
            {
                [TestMethod]
                public void ShouldSetMaskWithChangeMask()
                {
                    using (IMagickImage background = new MagickImage("xc:red", 100, 100))
                    {
                        background.BackgroundColor = MagickColors.White;
                        background.Extent(200, 100);

                        var drawables = new IDrawable[]
                        {
                            new DrawableFontPointSize(50),
                            new DrawableText(135, 70, "X"),
                        };

                        using (IMagickImage image = background.Clone())
                        {
                            image.Draw(drawables);
                            image.Composite(background, Gravity.Center, CompositeOperator.ChangeMask);

                            using (IMagickImage result = new MagickImage(MagickColors.Transparent, 200, 100))
                            {
                                result.Draw(drawables);
                                Assert.AreEqual(0.0603, result.Compare(image, ErrorMetric.RootMeanSquared), 0.001);
                            }
                        }
                    }
                }

                [TestMethod]
                public void ShouldDrawAtTheCorrectPositionWithWestGravity()
                {
                    var backgroundColor = MagickColors.LightBlue;
                    var overlayColor = MagickColors.YellowGreen;

                    using (IMagickImage background = new MagickImage(backgroundColor, 100, 100))
                    {
                        using (IMagickImage overlay = new MagickImage(overlayColor, 50, 50))
                        {
                            background.Composite(overlay, Gravity.West, CompositeOperator.Over);

                            ColorAssert.AreEqual(backgroundColor, background, 0, 0);
                            ColorAssert.AreEqual(overlayColor, background, 0, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 0, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 49, 0);
                            ColorAssert.AreEqual(overlayColor, background, 49, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 49, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 50, 0);
                            ColorAssert.AreEqual(backgroundColor, background, 50, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 50, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 99, 0);
                            ColorAssert.AreEqual(backgroundColor, background, 99, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 99, 75);
                        }
                    }
                }

                [TestMethod]
                public void ShouldDrawAtTheCorrectPositionWithEastGravity()
                {
                    var backgroundColor = MagickColors.LightBlue;
                    var overlayColor = MagickColors.YellowGreen;

                    using (IMagickImage background = new MagickImage(backgroundColor, 100, 100))
                    {
                        using (IMagickImage overlay = new MagickImage(overlayColor, 50, 50))
                        {
                            background.Composite(overlay, Gravity.East, CompositeOperator.Over);

                            ColorAssert.AreEqual(backgroundColor, background, 0, 0);
                            ColorAssert.AreEqual(backgroundColor, background, 0, 50);
                            ColorAssert.AreEqual(backgroundColor, background, 0, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 49, 0);
                            ColorAssert.AreEqual(backgroundColor, background, 49, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 49, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 50, 0);
                            ColorAssert.AreEqual(overlayColor, background, 50, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 50, 75);

                            ColorAssert.AreEqual(backgroundColor, background, 99, 0);
                            ColorAssert.AreEqual(overlayColor, background, 99, 25);
                            ColorAssert.AreEqual(backgroundColor, background, 99, 75);
                        }
                    }
                }
            }

            [TestClass]
            public partial class WithGravityAndOperatorAndArguments
            {
                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");
                        }
                    }
                }
            }

            [TestClass]
            public class WithOffsetAndOperator
            {
                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, new PointD(50, 50), CompositeOperator.Copy);

                            ColorAssert.AreEqual(MagickColors.White, image, 49, 49);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 50, 50);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 149, 149);
                            ColorAssert.AreEqual(MagickColors.White, image, 150, 150);
                        }
                    }
                }
            }

            [TestClass]
            public partial class WithOperator
            {
                [TestMethod]
                public void ShouldAddTransparencyWithCopyAlpha()
                {
                    using (IMagickImage image = new MagickImage(MagickColors.Red, 2, 1))
                    {
                        using (IMagickImage alpha = new MagickImage(MagickColors.Black, 1, 1))
                        {
                            alpha.BackgroundColor = MagickColors.White;
                            alpha.Extent(2, 1, Gravity.East);

                            image.Composite(alpha, CompositeOperator.CopyAlpha);

                            ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                            ColorAssert.AreEqual(new MagickColor("#f000"), image, 1, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithOperatorAndChannel
            {
                [TestMethod]
                public void ShouldOnlyModifyTheSpecifiedChannel()
                {
                    using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage red = new MagickImage(MagickColors.Red, image.Width, image.Height))
                        {
                            image.Composite(red, CompositeOperator.Multiply, Channels.Blue);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithXYAndOperator
            {
                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, 100, 100, CompositeOperator.Copy);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }
        }
    }
}
