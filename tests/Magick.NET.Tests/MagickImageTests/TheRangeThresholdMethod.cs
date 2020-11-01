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
        public class TheRangeThresholdMethod
        {
            public class WithPercentage
            {
                [Fact]
                public void ShouldThrowExceptionWhenLowBlackIsNegative()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("percentageLowBlack", () =>
                        {
                            image.RangeThreshold(new Percentage(-1), new Percentage(0), new Percentage(0), new Percentage(0));
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenLowWhiteIsNegative()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("percentageLowWhite", () =>
                        {
                            image.RangeThreshold(new Percentage(0), new Percentage(-1), new Percentage(0), new Percentage(0));
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHighWhiteIsNegative()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("percentageHighWhite", () =>
                        {
                            image.RangeThreshold(new Percentage(0), new Percentage(0), new Percentage(-1), new Percentage(0));
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHighBlackIsNegative()
                {
                    using (var image = new MagickImage(MagickColors.Red, 1, 1))
                    {
                        Assert.Throws<ArgumentException>("percentageHighBlack", () =>
                        {
                            image.RangeThreshold(new Percentage(0), new Percentage(0), new Percentage(0), new Percentage(-1));
                        });
                    }
                }

                [Fact]
                public void ShouldChangeTheImage()
                {
                    using (var image = new MagickImage("gradient:", 50, 256))
                    {
                        image.RangeThreshold(new Percentage(40), new Percentage(40), new Percentage(60), new Percentage(60));

                        ColorAssert.Equal(MagickColors.Black, image, 22, 101);
                        ColorAssert.Equal(MagickColors.White, image, 22, 102);
                        ColorAssert.Equal(MagickColors.White, image, 22, 152);
                        ColorAssert.Equal(MagickColors.Black, image, 22, 154);
                    }
                }
            }

            public class WithQuantum
            {
                [Fact]
                public void ShouldChangeTheImage()
                {
                    using (var image = new MagickImage("gradient:", 50, 256))
                    {
                        var lowBlack = (QuantumType)(Quantum.Max * 0.4);
                        var lowWhite = (QuantumType)(Quantum.Max * 0.4);
                        var highWhite = (QuantumType)(Quantum.Max * 0.6);
                        var highBlack = (QuantumType)(Quantum.Max * 0.6);
                        image.RangeThreshold(lowBlack, lowWhite, highWhite, highBlack);

                        ColorAssert.Equal(MagickColors.Black, image, 22, 101);
                        ColorAssert.Equal(MagickColors.White, image, 22, 102);
                        ColorAssert.Equal(MagickColors.White, image, 22, 152);
                        ColorAssert.Equal(MagickColors.Black, image, 22, 154);
                    }
                }
            }
        }
    }
}
