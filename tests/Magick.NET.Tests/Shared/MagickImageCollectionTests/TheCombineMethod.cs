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

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheCombineMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    using (var collection = new MagickImageCollection())
                    {
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            collection.Combine();
                        });
                    }
                }
            }

            [Fact]
            public void ShouldCombineSeparatedImages()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    using (var collection = new MagickImageCollection())
                    {
                        collection.AddRange(rose.Separate(Channels.RGB));

                        Assert.Equal(3, collection.Count);

                        using (var image = collection.Combine())
                        {
                            Assert.Equal(rose.TotalColors, image.TotalColors);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldCombineCmykImage()
            {
                using (var cmyk = new MagickImage(Files.CMYKJPG))
                {
                    using (var collection = new MagickImageCollection())
                    {
                        collection.AddRange(cmyk.Separate(Channels.CMYK));

                        Assert.Equal(4, collection.Count);

                        using (var image = collection.Combine(ColorSpace.CMYK))
                        {
                            Assert.Equal(0.0, cmyk.Compare(image, ErrorMetric.RootMeanSquared));
                        }
                    }
                }
            }
        }
    }
}
