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
            public partial class WithCopyAlphaOperator
            {
                [TestMethod]
                public void ShouldAddTransparency()
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
        }
    }
}
