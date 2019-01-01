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

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class ThePolynomialMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };
                        images.Polynomial(terms);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTermsIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.Builtin.Logo))
                {
                    ExceptionAssert.ThrowsArgumentNullException("terms", () =>
                    {
                        images.Polynomial(null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTermsIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.Builtin.Logo))
                {
                    ExceptionAssert.ThrowsArgumentException("terms", () =>
                    {
                        images.Polynomial(new double[] { });
                    });
                }
            }

            [TestMethod]
            public void ShouldCreateImage()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
                {
                    var channels = image.Separate();

                    using (IMagickImageCollection images = new MagickImageCollection(channels))
                    {
                        var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };

                        using (IMagickImage polynomial = images.Polynomial(terms))
                        {
                            var distortion = polynomial.Compare(image, ErrorMetric.RootMeanSquared);
                            Assert.AreEqual(0.086, distortion, 0.001);
                        }
                    }
                }
            }
        }
    }
}
