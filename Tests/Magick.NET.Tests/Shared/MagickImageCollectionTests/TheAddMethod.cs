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
        public class TheAddMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenItemIsNull()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.ThrowsArgumentNullException("item", () =>
                    {
                        images.Add((IMagickImage)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Add(null);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                {
                    using (IMagickImageCollection images = new MagickImageCollection())
                    {
                        images.Add(string.Empty);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    var image = new MagickImage();
                    images.Add(image);

                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.Add(image);
                    });
                }
            }
        }
    }
}
