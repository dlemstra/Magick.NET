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
        public class TheFxMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenExpressionIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("expression", () => image.Fx(null));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenExpressionIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<ArgumentException>("expression", () => image.Fx(string.Empty));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenExpressionIsInvalid()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    ExceptionAssert.Throws<MagickOptionErrorException>(() => image.Fx("foobar"));
                }
            }

            [TestMethod]
            public void ShouldEvaluateTheExpression()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Fx("b");

                    ColorAssert.AreEqual(MagickColors.Black, image, 183, 83);
                    ColorAssert.AreEqual(MagickColors.White, image, 140, 400);

                    image.Fx("1/2", Channels.Green);

                    ColorAssert.AreEqual(new MagickColor("#000080000000"), image, 183, 83);
                    ColorAssert.AreEqual(new MagickColor("#ffff8000ffff"), image, 140, 400);

                    image.Fx("1/4", Channels.Alpha);

                    ColorAssert.AreEqual(new MagickColor("#000080000000"), image, 183, 83);
                    ColorAssert.AreEqual(new MagickColor("#ffff8000ffff"), image, 140, 400);

                    image.HasAlpha = true;
                    image.Fx("1/4", Channels.Alpha);

                    ColorAssert.AreEqual(new MagickColor("#0000800000004000"), image, 183, 83);
                    ColorAssert.AreEqual(new MagickColor("#ffff8000ffff4000"), image, 140, 400);
                }
            }

            [TestMethod]
            public void ShouldEvaluateExpressionMethod()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Fx("rand()");
                }
            }
        }
    }
}
