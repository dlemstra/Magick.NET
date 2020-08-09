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
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheCompositeMethod
        {
            [TestClass]
            public class WithImage
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldCompositeTheImage()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var other = new MagickImage("xc:purple", 1, 1))
                        {
                            image.Composite(other);

                            ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                        }
                    }
                }

                [TestMethod]
                public void ShouldPreserveGrayColorSpace()
                {
                    using (var logo = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blue = logo.Separate(Channels.Blue).First())
                        {
                            blue.Composite(logo, CompositeOperator.Modulate);

                            Assert.AreEqual(ColorSpace.Gray, blue.ColorSpace);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldOnlyCompositeTheSpecifiedChannel()
                {
                    using (var image = new MagickImage("xc:black", 1, 1))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Channels.Green);

                            ColorAssert.AreEqual(MagickColors.Lime, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public partial class WithImageAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, CompositeOperator.CopyCyan);
                        });
                    }
                }

                [TestMethod]
                public void ShouldAddTransparencyWithCopyAlpha()
                {
                    using (var image = new MagickImage(MagickColors.Red, 2, 1))
                    {
                        using (var alpha = new MagickImage(MagickColors.Black, 1, 1))
                        {
                            alpha.BackgroundColor = MagickColors.White;
                            alpha.Extent(2, 1, Gravity.East);

                            image.Composite(alpha, CompositeOperator.CopyAlpha);

                            Assert.IsTrue(image.HasAlpha);
                            ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                            ColorAssert.AreEqual(new MagickColor("#f000"), image, 1, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, CompositeOperator.CopyCyan, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldOnlyCompositeTheSpecifiedChannel()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var red = new MagickImage(MagickColors.Red, image.Width, image.Height))
                        {
                            image.Composite(red, CompositeOperator.Multiply, Channels.Blue);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, CompositeOperator.CopyCyan, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage(MagickColors.Red, image.Width, image.Height))
                        {
                            image.Composite(red, CompositeOperator.CopyCyan, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, CompositeOperator.CopyCyan, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage(MagickColors.Red, image.Width, image.Height))
                        {
                            image.Composite(red, CompositeOperator.CopyCyan, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXY
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, 100, 100);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXYAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, 100, 100, Channels.Red);

                            ColorAssert.AreEqual(MagickColors.White, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.White, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXYAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0, CompositeOperator.CopyAlpha);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, 100, 100, CompositeOperator.Copy);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXYAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0, CompositeOperator.CopyAlpha, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, 100, 100, CompositeOperator.Clear, Channels.Red);

                            ColorAssert.AreEqual(MagickColors.Aqua, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Aqua, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXYAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0, CompositeOperator.CopyAlpha, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, 0, 0, CompositeOperator.CopyAlpha, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, 0, 0, CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, 0, 0, CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndXYAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, 0, 0, CompositeOperator.CopyAlpha, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, 0, 0, CompositeOperator.CopyAlpha, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, 0, 0, CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, 0, 0, CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPoint
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0));
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, new PointD(100, 100));

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPointAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0), Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, new PointD(100, 100), Channels.Red);

                            ColorAssert.AreEqual(MagickColors.White, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.White, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPointAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0), CompositeOperator.CopyAlpha);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, new PointD(100, 100), CompositeOperator.Copy);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Yellow, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPointAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0), CompositeOperator.CopyAlpha, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheOffset()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var yellow = new MagickImage(new MagickColor("#FF0"), 100, 100))
                        {
                            image.Composite(yellow, new PointD(100, 100), CompositeOperator.Clear, Channels.Red);

                            ColorAssert.AreEqual(MagickColors.Aqua, image, 150, 150);
                            ColorAssert.AreEqual(MagickColors.Aqua, image, 199, 109);
                            ColorAssert.AreEqual(MagickColors.White, image, 200, 200);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPointAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0), CompositeOperator.CopyAlpha, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, new PointD(0, 0), CompositeOperator.CopyAlpha, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, new PointD(0, 0), CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, new PointD(0, 0), CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndPointAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, new PointD(0, 0), CompositeOperator.CopyAlpha, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, new PointD(0, 0), CompositeOperator.CopyAlpha, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, new PointD(0, 0), CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, new PointD(0, 0), CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravity
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.East);

                            ColorAssert.AreEqual(MagickColors.White, image, 2, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.West, Channels.Green);

                            ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, CompositeOperator.Blend);
                        });
                    }
                }

                [TestMethod]
                public void ShouldSetMaskWithChangeMask()
                {
                    using (var background = new MagickImage("xc:red", 100, 100))
                    {
                        background.BackgroundColor = MagickColors.White;
                        background.Extent(200, 100);

                        var drawables = new IDrawable[]
                        {
                            new DrawableFontPointSize(50),
                            new DrawableText(135, 70, "X"),
                        };

                        using (var image = background.Clone())
                        {
                            image.Draw(drawables);
                            image.Composite(background, Gravity.Center, CompositeOperator.ChangeMask);

                            using (var result = new MagickImage(MagickColors.Transparent, 200, 100))
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

                    using (var background = new MagickImage(backgroundColor, 100, 100))
                    {
                        using (var overlay = new MagickImage(overlayColor, 50, 50))
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

                    using (var background = new MagickImage(backgroundColor, 100, 100))
                    {
                        using (var overlay = new MagickImage(overlayColor, 50, 50))
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
            public class WithImageAndGravityAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, CompositeOperator.Blend, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:white", 3, 3))
                    {
                        using (var other = new MagickImage("xc:black", 1, 1))
                        {
                            image.Composite(other, Gravity.South, CompositeOperator.Clear, Channels.Green);

                            ColorAssert.AreEqual(MagickColors.Magenta, image, 1, 2);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, CompositeOperator.Blend, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, CompositeOperator.Blend, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, CompositeOperator.Blend, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, CompositeOperator.Blend, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXY
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.Northeast, 1, 1);

                            ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXYAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.West, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.Southwest, 1, 1);

                            ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXYAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, 0, 0, CompositeOperator.Blend);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.Northwest, 1, 1, CompositeOperator.Over);

                            ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXYAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, 0, 0, CompositeOperator.Blend, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:white", 3, 3))
                    {
                        using (var other = new MagickImage("xc:black", 1, 1))
                        {
                            image.Composite(other, Gravity.Southeast, 1, 1, CompositeOperator.Clear, Channels.Green);

                            ColorAssert.AreEqual(MagickColors.Magenta, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXYAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, 0, 0, CompositeOperator.Blend, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, 0, 0, CompositeOperator.Blend, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, 1, 1, CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndXYAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, 0, 0, CompositeOperator.Blend, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, 0, 0, CompositeOperator.Blend, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, 0, 0, CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPoint
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, new PointD(0, 0));
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.East, new PointD(1, 0));

                            ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPointAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.West, new PointD(0, 0));
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.West, new PointD(1, 0));

                            ColorAssert.AreEqual(MagickColors.White, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPointAndCompositeOperator
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, new PointD(0, 0), CompositeOperator.Blend);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:red", 3, 3))
                    {
                        using (var other = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(other, Gravity.West, new PointD(0, -1), CompositeOperator.Over);

                            ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPointAndCompositeOperatorAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, new PointD(0, 0), CompositeOperator.Blend, Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheGravity()
                {
                    using (var image = new MagickImage("xc:white", 3, 3))
                    {
                        using (var other = new MagickImage("xc:black", 1, 1))
                        {
                            image.Composite(other, Gravity.South, new PointD(0, 1), CompositeOperator.Clear, Channels.Green);

                            ColorAssert.AreEqual(MagickColors.Magenta, image, 1, 1);
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPointAndCompositeOperatorAndArguments
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, new PointD(0, 0), CompositeOperator.Blend, "3");
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, new PointD(0, 0), CompositeOperator.Blend, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, new PointD(1, 1), CompositeOperator.Blur, "3");
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, new PointD(0, 0), CompositeOperator.Blur, "3");

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }

            [TestClass]
            public class WithImageAndGravityAndPointAndCompositeOperatorAndArgumentsAndChannels
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenImageIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Composite(null, Gravity.East, new PointD(0, 0), CompositeOperator.Blend, "3", Channels.Red);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenArgumentIsNull()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var red = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(red, Gravity.East, new PointD(0, 0), CompositeOperator.Blend, null, Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheArguments()
                {
                    using (var image = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var blur = new MagickImage(new MagickColor("#000"), image.Width, image.Height))
                        {
                            image.Warning += (object sender, WarningEventArgs arguments) => Assert.Fail(arguments.Message);
                            image.Composite(blur, Gravity.Center, new PointD(0, 0), CompositeOperator.Blur, "3", Channels.Red);
                        }
                    }
                }

                [TestMethod]
                public void ShouldRemoveTheArtifact()
                {
                    using (var image = new MagickImage("xc:red", 1, 1))
                    {
                        using (var blur = new MagickImage("xc:white", 1, 1))
                        {
                            image.Composite(blur, Gravity.Center, new PointD(0, 0), CompositeOperator.Blur, "3", Channels.Red);

                            Assert.IsNull(image.GetArtifact("compose:args"));
                        }
                    }
                }
            }
        }
    }
}
