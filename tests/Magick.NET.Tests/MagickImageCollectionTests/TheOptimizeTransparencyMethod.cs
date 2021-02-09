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
    public partial class MagickImageCollectionTests
    {
        public class TheOptimizeTransparencyMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() => images.OptimizeTransparency());
                }
            }

            [Fact]
            public void ShouldCorrectlyOptimizeTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                    var image = new MagickImage(MagickColors.Red, 11, 11);
                    using (var pixels = image.GetPixels())
                    {
                        pixels.SetPixel(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                    }

                    collection.Add(image);
                    collection.OptimizeTransparency();

                    Assert.Equal(11, collection[1].Width);
                    Assert.Equal(11, collection[1].Height);
                    Assert.Equal(0, collection[1].Page.X);
                    Assert.Equal(0, collection[1].Page.Y);
                    ColorAssert.Equal(MagickColors.Lime, collection[1], 5, 5);
                    ColorAssert.Equal(new MagickColor("#f000"), collection[1], 4, 4);
                }
            }
        }
    }
}
