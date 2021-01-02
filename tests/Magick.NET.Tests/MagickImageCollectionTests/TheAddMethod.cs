// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        public class TheAddMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenItemIsNull()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<ArgumentNullException>("item", () =>
                    {
                        images.Add((IMagickImage<QuantumType>)null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Add((string)null);
                    }
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () =>
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.Add(string.Empty);
                    }
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage();
                    images.Add(image);

                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Add(image);
                    });
                }
            }
        }
    }
}
