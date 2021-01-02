// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using NSubstitute;
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
        public class TheCompareMethod
        {
            [Fact]
            public void ShouldThrowAnExceptionWhenImageIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        image.Compare(null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowAnExceptionWhenImageIsNullAndErrorMetricIsSpecified()
            {
                using (var image = new MagickImage())
                {
                    using (var diff = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Compare(null, ErrorMetric.RootMeanSquared);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowAnExceptionWhenImageIsNullAndSettingsAreNotNull()
            {
                using (var image = new MagickImage())
                {
                    using (var diff = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("image", () =>
                        {
                            image.Compare(null, new CompareSettings(), diff);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowAnExceptionWhenSettingsIsNull()
            {
                using (var image = new MagickImage())
                {
                    using (var diff = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("settings", () =>
                        {
                            image.Compare(image, null, diff);
                        });
                    }
                }
            }

            [Fact]
            public void ShouldThrowAnExceptionWhenDifferenceIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("difference", () =>
                    {
                        image.Compare(image, new CompareSettings(), null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowAnExceptionWhenDifferenceIsNotMagickImage()
            {
                using (var image = new MagickImage())
                {
                    var diff = Substitute.For<IMagickImage<QuantumType>>();

                    Assert.Throws<NotSupportedException>(() =>
                    {
                        image.Compare(image, new CompareSettings(), diff);
                    });
                }
            }

            [Fact]
            public void ShouldReturnEmptyErrorInfoWhenTheImagesAreEqual()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var other = new MagickImage(Files.Builtin.Logo))
                    {
                        var errorInfo = image.Compare(other);

                        Assert.Equal(0, errorInfo.MeanErrorPerPixel);
                        Assert.Equal(0, errorInfo.NormalizedMaximumError);
                        Assert.Equal(0, errorInfo.NormalizedMeanError);
                    }
                }
            }

            [Fact]
            public void ShouldReturnZeroWhenTheImagesAreEqual()
            {
                var settings = new CompareSettings
                {
                    Metric = ErrorMetric.RootMeanSquared,
                };

                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var diff = new MagickImage())
                        {
                            double result = image.Compare(other, settings, diff);

                            Assert.Equal(0, result);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldReturnZeroWhenTheImagesAreEqualAndErrorMetricIsRootMeanSquared()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (var diff = new MagickImage())
                        {
                            double result = image.Compare(other, ErrorMetric.RootMeanSquared, diff);

                            Assert.Equal(0, result);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldReturnErrorInfoWhenTheImagesAreNotEqual()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var other = new MagickImage(Files.Builtin.Logo))
                    {
                        other.Rotate(180);

                        var errorInfo = image.Compare(other);

#if Q8
                        Assert.InRange(errorInfo.MeanErrorPerPixel, 44.55, 44.56);
#else
                        Assert.InRange(errorInfo.MeanErrorPerPixel, 11450.85, 11450.86);
#endif
                        Assert.Equal(1, errorInfo.NormalizedMaximumError);
                        Assert.InRange(errorInfo.NormalizedMeanError, 0.13, 0.14);
                    }
                }
            }

            [Fact]
            public void ShouldReturnNonZeroValueWhenTheImagesAreNotEqual()
            {
                var settings = new CompareSettings
                {
                    Metric = ErrorMetric.RootMeanSquared,
                    HighlightColor = MagickColors.Yellow,
                    LowlightColor = MagickColors.Red,
                    MasklightColor = MagickColors.Magenta,
                };

                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (var mask = new MagickImage("xc:white", image.Width, image.Height - 100))
                    {
                        image.SetReadMask(mask);
                    }

                    using (var other = new MagickImage(Files.Builtin.Logo))
                    {
                        other.Rotate(180);

                        using (var diff = new MagickImage())
                        {
                            double result = image.Compare(other, settings, diff);

                            Assert.InRange(result, 0.36, 0.37);
                            ColorAssert.Equal(MagickColors.Yellow, diff, 150, 50);
                            ColorAssert.Equal(MagickColors.Red, diff, 150, 250);
                            ColorAssert.Equal(MagickColors.Magenta, diff, 150, 450);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldUseTheColorFuzz()
            {
                using (var image = new MagickImage(new MagickColor("#f1d3bc"), 1, 1))
                {
                    using (var other = new MagickImage(new MagickColor("#24292e"), 1, 1))
                    {
                        using (var diff = new MagickImage())
                        {
                            image.ColorFuzz = new Percentage(81);
                            double result = image.Compare(other, ErrorMetric.Absolute, diff);

                            Assert.Equal(0, result);
                            ColorAssert.Equal(new MagickColor("#fd2ff729f28b"), diff, 0, 0);
                        }
                    }
                }
            }
        }
    }
}
