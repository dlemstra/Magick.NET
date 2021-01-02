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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DensityExtensionsTests
    {
        public class TheToGeometryMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                var density = new Density(50.0);

                var geometry = density.ToGeometry(0.5, 2.0);
                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(25, geometry.Width);
                Assert.Equal(100, geometry.Height);
            }
        }
    }
}
