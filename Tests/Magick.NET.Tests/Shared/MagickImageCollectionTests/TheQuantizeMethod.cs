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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public partial class TheQuantizeMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        collection.Quantize();
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.Add(Files.FujiFilmFinePixS1ProJPG);

                    ExceptionAssert.ThrowsArgumentNullException("settings", () =>
                    {
                        collection.Quantize(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenMeasureErrorsIsFalse()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.Add(Files.FujiFilmFinePixS1ProJPG);

                    QuantizeSettings settings = new QuantizeSettings
                    {
                        Colors = 1,
                        MeasureErrors = false,
                    };

                    MagickErrorInfo errorInfo = collection.Quantize(settings);
                    Assert.IsNull(errorInfo);
                }
            }
        }
    }
}
