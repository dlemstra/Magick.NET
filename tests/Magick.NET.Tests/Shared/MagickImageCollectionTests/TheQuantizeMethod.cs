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
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheQuantizeMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.Quantize();
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(Files.FujiFilmFinePixS1ProJPG);

                    ExceptionAssert.Throws<ArgumentNullException>("settings", () =>
                    {
                        images.Quantize(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenMeasureErrorsIsFalse()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(Files.FujiFilmFinePixS1ProJPG);

                    QuantizeSettings settings = new QuantizeSettings
                    {
                        Colors = 1,
                        MeasureErrors = false,
                    };

                    var errorInfo = images.Quantize(settings);
                    Assert.IsNull(errorInfo);
                }
            }

            [TestMethod]
            public void ShouldReduceTheColors()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(Files.FujiFilmFinePixS1ProJPG);

                    QuantizeSettings settings = new QuantizeSettings
                    {
                        Colors = 3,
                    };

                    collection.Quantize(settings);

#if Q8
                    ColorAssert.AreEqual(new MagickColor("#2b414f"), collection[0], 120, 140);
                    ColorAssert.AreEqual(new MagickColor("#7b929f"), collection[0], 95, 140);
                    ColorAssert.AreEqual(new MagickColor("#44739f"), collection[0], 300, 150);
#else
                    ColorAssert.AreEqual(new MagickColor("#2af841624f09"), collection[0], 120, 140);
                    ColorAssert.AreEqual(new MagickColor("#7b3c92b69f5a"), collection[0], 95, 140);
                    ColorAssert.AreEqual(new MagickColor("#44bc73059f70"), collection[0], 300, 150);
#endif
                }
            }

            [TestMethod]
            public void ShouldReturnErrorInfoWhenMeasureErrorsIsTrue()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(Files.FujiFilmFinePixS1ProJPG);

                    QuantizeSettings settings = new QuantizeSettings
                    {
                        Colors = 3,
                        MeasureErrors = true,
                    };

                    var errorInfo = collection.Quantize(settings);
                    Assert.IsNotNull(errorInfo);

#if Q8
                    Assert.AreEqual(13.62, errorInfo.MeanErrorPerPixel, 0.01);
#else
                    Assert.AreEqual(3526, errorInfo.MeanErrorPerPixel, 1);
#endif
                    Assert.AreEqual(0.47, errorInfo.NormalizedMaximumError, 0.01);
                    Assert.AreEqual(0.006, errorInfo.NormalizedMeanError, 0.001);
                }
            }
        }
    }
}
