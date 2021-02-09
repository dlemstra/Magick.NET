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
        public class ThePolynomialMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };
                        images.Polynomial(terms);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenTermsIsNull()
            {
                using (var images = new MagickImageCollection(Files.Builtin.Logo))
                {
                    Assert.Throws<ArgumentNullException>("terms", () =>
                    {
                        images.Polynomial(null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenTermsIsEmpty()
            {
                using (var images = new MagickImageCollection(Files.Builtin.Logo))
                {
                    Assert.Throws<ArgumentException>("terms", () =>
                    {
                        images.Polynomial(new double[] { });
                    });
                }
            }

            [Fact]
            public void ShouldCreateImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var channels = image.Separate();

                    using (var images = new MagickImageCollection(channels))
                    {
                        var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };

                        using (var polynomial = images.Polynomial(terms))
                        {
                            var distortion = polynomial.Compare(image, ErrorMetric.RootMeanSquared);
                            Assert.InRange(distortion, 0.086, 0.087);
                        }
                    }
                }
            }
        }
    }
}
