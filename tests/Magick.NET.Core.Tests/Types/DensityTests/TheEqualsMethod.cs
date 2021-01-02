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

namespace Magick.NET.Core.Tests
{
    public partial class DensityTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var density = new Density(50);

                Assert.False(density.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var density = new Density(50);

                Assert.True(density.Equals(density));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var density = new Density(50);

                Assert.True(density.Equals((object)density));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new Density(50);
                var second = new Density(50);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new Density(50);
                var second = new Density(50);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new Density(10, 5);
                var second = new Density(5, 10);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new Density(10, 5);
                var second = new Density(5, 10);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
