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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
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
                    ExceptionAssert.Throws<ArgumentNullException>("arguments", () =>
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
                    ExceptionAssert.Throws<ArgumentException>("arguments", () =>
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
                    ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        image.Evaluate(Channels.Red, null, EvaluateOperator.Set, 0.0);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsPercentage()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("geometry", () =>
                    {
                        var geometry = new MagickGeometry(new Percentage(100), new Percentage(100));

                        image.Evaluate(Channels.Red, geometry, EvaluateOperator.Set, 0.0);
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

            [TestMethod]
            public void ShouldUseWriteMask()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Black, 2, 1))
                {
                    using (IMagickImage mask = new MagickImage(MagickColors.White, 2, 1))
                    {
                        using (IPixelCollection pixels = mask.GetPixelsUnsafe())
                        {
                            pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0 });
                        }

                        image.SetWriteMask(mask);

                        image.Evaluate(Channels.Red, EvaluateOperator.Set, Quantum.Max);

                        ColorAssert.AreEqual(MagickColors.Red, image, 0, 0);
                        ColorAssert.AreEqual(MagickColors.Black, image, 1, 0);
                    }
                }
            }
        }
    }
}
