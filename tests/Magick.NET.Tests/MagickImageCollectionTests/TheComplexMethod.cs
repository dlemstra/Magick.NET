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

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheComplexMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() => images.Complex(new ComplexSettings()));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<ArgumentNullException>("complexSettings", () => images.Complex(null));
                }
            }

            [Fact]
            public void ShouldApplyTheOperatorToTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Read(Files.RoseSparkleGIF);

                    collection.Complex(new ComplexSettings
                    {
                        ComplexOperator = ComplexOperator.Conjugate,
                    });

                    Assert.Equal(2, collection.Count);

#if Q8
                    ColorAssert.Equal(new MagickColor("#abb4ba01"), collection[1], 10, 10);

#elif Q16
                    ColorAssert.Equal(new MagickColor("#aaabb3b4b9ba0001"), collection[1], 10, 10);
#else
                    collection[1].Clamp();
                    ColorAssert.Equal(new MagickColor("#0000000000000000"), collection[1], 10, 10);
#endif
                }
            }
        }
    }
}
