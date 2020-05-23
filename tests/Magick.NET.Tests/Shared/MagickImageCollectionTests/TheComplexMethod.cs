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
        public class TheComplexMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() => images.Complex(new ComplexSettings()));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("complexSettings", () => images.Complex(null));
                }
            }

            [TestMethod]
            public void ShouldApplyTheOperatorToTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Read(Files.RoseSparkleGIF);

                    collection.Complex(new ComplexSettings
                    {
                        Operator = ComplexOperator.Conjugate,
                    });

                    Assert.AreEqual(2, collection.Count);

#if Q8
                    ColorAssert.AreEqual(new MagickColor("#abb4ba01"), collection[1], 10, 10);

#elif Q16
                    ColorAssert.AreEqual(new MagickColor("#aaabb3b4b9ba0001"), collection[1], 10, 10);
#elif Q16HDRI
                    collection[1].Clamp();
                    ColorAssert.AreEqual(new MagickColor("#0000000000000000"), collection[1], 10, 10);
#else
#error Not implemented!
#endif
                }
            }
        }
    }
}
