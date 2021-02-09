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
    public partial class MagickImageTests
    {
        public class TheGetArtifactMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("name", () =>
                    {
                        image.GetArtifact(null);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentException>("name", () =>
                    {
                        image.GetArtifact(string.Empty);
                    });
                }
            }

            [Fact]
            public void ShouldReturnNullWhenValueIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Null(image.GetArtifact("test"));
                }
            }
        }
    }
}
