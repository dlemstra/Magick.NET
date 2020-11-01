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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MatrixFactoryTests
    {
        public class TheCreateColorMatrixMethod
        {
            [Fact]
            public void ShouldCreateInstance()
            {
                var factory = new MatrixFactory();

                var matrix = factory.CreateColorMatrix(1);

                Assert.NotNull(matrix);
                Assert.IsType<MagickColorMatrix>(matrix);
            }

            [Fact]
            public void ShouldCreateInstanceWithValues()
            {
                var factory = new MatrixFactory();

                var matrix = factory.CreateColorMatrix(1, 2);

                Assert.NotNull(matrix);
                Assert.IsType<MagickColorMatrix>(matrix);
                Assert.Equal(2, matrix.GetValue(0, 0));
            }
        }
    }
}
