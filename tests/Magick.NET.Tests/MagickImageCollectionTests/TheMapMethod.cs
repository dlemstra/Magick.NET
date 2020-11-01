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
        public class TheMapMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Map(null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmptyAndImageIsNotNull()
            {
                using (var colors = new MagickImageCollection())
                {
                    colors.Add(new MagickImage(MagickColors.Red, 1, 1));
                    colors.Add(new MagickImage(MagickColors.Green, 1, 1));

                    using (var remapImage = colors.AppendHorizontally())
                    {
                        using (var collection = new MagickImageCollection())
                        {
                            Assert.Throws<InvalidOperationException>(() =>
                            {
                                collection.Map(remapImage);
                            });
                        }
                    }
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    Assert.Throws<ArgumentNullException>("image", () =>
                    {
                        images.Map(null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        images.Map(images[0], null);
                    });
                }
            }

            [Fact]
            public void ShouldDitherWhenSpecifiedInSettings()
            {
                using (var colors = new MagickImageCollection())
                {
                    colors.Add(new MagickImage(MagickColors.Red, 1, 1));
                    colors.Add(new MagickImage(MagickColors.Green, 1, 1));

                    using (var remapImage = colors.AppendHorizontally())
                    {
                        using (var collection = new MagickImageCollection())
                        {
                            collection.Read(Files.RoseSparkleGIF);

                            var settings = new QuantizeSettings
                            {
                                DitherMethod = DitherMethod.FloydSteinberg,
                            };

                            collection.Map(remapImage, settings);

                            ColorAssert.Equal(MagickColors.Red, collection[0], 60, 17);
                            ColorAssert.Equal(MagickColors.Green, collection[0], 37, 24);

                            ColorAssert.Equal(MagickColors.Red, collection[1], 27, 45);
                            ColorAssert.Equal(MagickColors.Green, collection[1], 36, 26);

                            ColorAssert.Equal(MagickColors.Red, collection[2], 55, 12);
                            ColorAssert.Equal(MagickColors.Green, collection[2], 17, 21);
                        }
                    }
                }
            }
        }
    }
}