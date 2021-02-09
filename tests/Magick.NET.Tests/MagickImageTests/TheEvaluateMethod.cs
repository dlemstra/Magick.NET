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
        public class TheEvaluateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenArgumentsIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("arguments", () =>
                    {
                        image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenArgumentsIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentException>("arguments", () =>
                    {
                        image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { });
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("geometry", () =>
                    {
                        image.Evaluate(Channels.Red, null, EvaluateOperator.Set, 0.0);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsPercentage()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentException>("geometry", () =>
                    {
                        var geometry = new MagickGeometry(new Percentage(100), new Percentage(100));

                        image.Evaluate(Channels.Red, geometry, EvaluateOperator.Set, 0.0);
                    });
                }
            }

            [Fact]
            public void ShouldChangeTheSpecifiedChannels()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { 5.0 });

                    ColorAssert.Equal(new MagickColor("#9068ffffffff"), image, 100, 295);

                    image.Evaluate(Channels.Red, new MagickGeometry(0, 0, 100, 295), EvaluateOperator.Set, 0);

                    ColorAssert.Equal(new MagickColor("#0ff"), image, 99, 195);
                    ColorAssert.Equal(new MagickColor("#9068ffffffff"), image, 100, 295);

                    image.Evaluate(Channels.Green, EvaluateOperator.Set, 0);

                    ColorAssert.Equal(new MagickColor("#00f"), image, 99, 195);
                    ColorAssert.Equal(new MagickColor("#90680000ffff"), image, 100, 295);
                }
            }

            [Fact]
            public void ShouldUseWriteMask()
            {
                using (var image = new MagickImage(MagickColors.Black, 2, 1))
                {
                    using (var mask = new MagickImage(MagickColors.White, 2, 1))
                    {
                        using (var pixels = mask.GetPixelsUnsafe())
                        {
                            pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0 });
                        }

                        image.SetWriteMask(mask);

                        image.Evaluate(Channels.Red, EvaluateOperator.Set, Quantum.Max);

                        ColorAssert.Equal(MagickColors.Red, image, 0, 0);
                        ColorAssert.Equal(MagickColors.Black, image, 1, 0);
                    }
                }
            }
        }
    }
}
