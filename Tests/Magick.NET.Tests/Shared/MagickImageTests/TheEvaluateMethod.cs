// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        [TestClass]
        public class TheEvaluateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenArgumentsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("arguments", () =>
                    {
                        image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArgumentsIsEmpty()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentException("arguments", () =>
                    {
                        image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { });
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                    {
                        image.Evaluate(Channels.Red, null, EvaluateOperator.Set, 0.0);
                    });
                }
            }

            [TestMethod]
            public void ShouldChangeTheSpecifiedChannels()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { 5.0 });

                    ColorAssert.AreEqual(new MagickColor("#9068ffffffff"), image, 100, 295);

                    image.Evaluate(Channels.Red, new MagickGeometry(0, 0, 100, 295), EvaluateOperator.Set, 0);

                    ColorAssert.AreEqual(new MagickColor("#0ff"), image, 99, 195);
                    ColorAssert.AreEqual(new MagickColor("#9068ffffffff"), image, 100, 295);

                    image.Evaluate(Channels.Green, EvaluateOperator.Set, 0);

                    ColorAssert.AreEqual(new MagickColor("#00f"), image, 99, 195);
                    ColorAssert.AreEqual(new MagickColor("#90680000ffff"), image, 100, 295);
                }
            }
        }
    }
}
