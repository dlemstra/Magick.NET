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
using NSubstitute;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheCompareMethod
        {
            [TestMethod]
            public void ShouldThrowAnExceptionWhenImageIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("image", () =>
                    {
                        image.Compare(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenImageIsNullAndErrorMetricIsSpecified()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (IMagickImage diff = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("image", () =>
                        {
                            image.Compare(null, ErrorMetric.RootMeanSquared);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenImageIsNullAndSettingsAreNotNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (IMagickImage diff = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("image", () =>
                        {
                            image.Compare(null, new CompareSettings(), diff);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenSettingsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (IMagickImage diff = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                        {
                            image.Compare(image, null, diff);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenDifferenceIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("difference", () =>
                    {
                        image.Compare(image, new CompareSettings(), null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenDifferenceIsNotMagickImage()
            {
                using (IMagickImage image = new MagickImage())
                {
                    IMagickImage diff = Substitute.For<IMagickImage>();

                    ExceptionAssert.Throws<NotSupportedException>(() =>
                    {
                        image.Compare(image, new CompareSettings(), diff);
                    });
                }
            }

            [TestMethod]
            public void ShouldSetAnArtifactWhenTheHighlightOfTheSettingsIsNotNull()
            {
                CompareSettings settings = new CompareSettings()
                {
                    HighlightColor = MagickColors.Fuchsia,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage diff = new MagickImage())
                        {
                            image.Compare(other, settings, diff);
                        }
                    }
#if Q8
                    Assert.AreEqual("#FF00FFFF", image.GetArtifact("compare:highlight-color"));
#elif Q16 || Q16HDRI
                    Assert.AreEqual("#FFFF0000FFFFFFFF", image.GetArtifact("compare:highlight-color"));
#else
#error Not implemented!
#endif
                }
            }

            [TestMethod]
            public void ShouldSetAnArtifactWhenTheLowlightOfTheSettingsIsNotNull()
            {
                CompareSettings settings = new CompareSettings()
                {
                    LowlightColor = MagickColors.Fuchsia,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage diff = new MagickImage())
                        {
                            image.Compare(other, settings, diff);
                        }
                    }
#if Q8
                    Assert.AreEqual("#FF00FFFF", image.GetArtifact("compare:lowlight-color"));
#elif Q16 || Q16HDRI
                    Assert.AreEqual("#FFFF0000FFFFFFFF", image.GetArtifact("compare:lowlight-color"));
#else
#error Not implemented!
#endif
                }
            }

            [TestMethod]
            public void ShouldSetAnArtifactWhenTheMasklightOfTheSettingsIsNotNull()
            {
                CompareSettings settings = new CompareSettings()
                {
                    MasklightColor = MagickColors.Fuchsia,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage diff = new MagickImage())
                        {
                            image.Compare(other, settings, diff);
                        }
                    }
#if Q8
                    Assert.AreEqual("#FF00FFFF", image.GetArtifact("compare:masklight-color"));
#elif Q16 || Q16HDRI
                    Assert.AreEqual("#FFFF0000FFFFFFFF", image.GetArtifact("compare:masklight-color"));
#else
#error Not implemented!
#endif
                }
            }

            [TestMethod]
            public void ShouldReturnEmptyErrorInfoWhenTheImagesAreEqual()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        MagickErrorInfo errorInfo = image.Compare(other);

                        Assert.AreEqual(0, errorInfo.MeanErrorPerPixel);
                        Assert.AreEqual(0, errorInfo.NormalizedMaximumError);
                        Assert.AreEqual(0, errorInfo.NormalizedMeanError);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnZeroWhenTheImagesAreEqual()
            {
                CompareSettings settings = new CompareSettings()
                {
                    Metric = ErrorMetric.RootMeanSquared,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage diff = new MagickImage())
                        {
                            double result = image.Compare(other, settings, diff);

                            Assert.AreEqual(0, result);
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnZeroWhenTheImagesAreEqualAndErrorMetricIsRootMeanSquared()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        using (IMagickImage diff = new MagickImage())
                        {
                            double result = image.Compare(other, ErrorMetric.RootMeanSquared, diff);

                            Assert.AreEqual(0, result);
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnErrorInfoWhenTheImagesAreNotEqual()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        other.Rotate(180);

                        MagickErrorInfo errorInfo = image.Compare(other);

#if Q8
                        Assert.AreEqual(44.55, errorInfo.MeanErrorPerPixel, 0.01);
#elif Q16 || Q16HDRI
                        Assert.AreEqual(11450.85, errorInfo.MeanErrorPerPixel, 0.01);
#else
#error Not implemented!
#endif
                        Assert.AreEqual(1, errorInfo.NormalizedMaximumError);
                        Assert.AreEqual(0.13, errorInfo.NormalizedMeanError, 0.01);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnNonZeroValueWhenTheImagesAreNotEqual()
            {
                CompareSettings settings = new CompareSettings()
                {
                    Metric = ErrorMetric.RootMeanSquared,
                    HighlightColor = MagickColors.Yellow,
                    LowlightColor = MagickColors.Red,
                    MasklightColor = MagickColors.Magenta,
                };

                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage mask = new MagickImage("xc:white", image.Width, image.Height - 100))
                    {
                        image.SetReadMask(mask);
                    }

                    using (IMagickImage other = new MagickImage(Files.Builtin.Logo))
                    {
                        other.Rotate(180);

                        using (IMagickImage diff = new MagickImage())
                        {
                            double result = image.Compare(other, settings, diff);

                            Assert.AreEqual(0.36, result, 0.01);
                            ColorAssert.AreEqual(MagickColors.Yellow, diff, 150, 50);
                            ColorAssert.AreEqual(MagickColors.Red, diff, 150, 250);
                            ColorAssert.AreEqual(MagickColors.Magenta, diff, 150, 450);
                        }
                    }
                }
            }
        }
    }
}
