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
    public partial class MagickNETTests
    {
        public class TheSetRandomSeedMethod
        {
            [Fact]
            public void ShouldPassOrderedTests()
            {
                ShouldMakeDifferentPlasmaImageWhenNotSet();

                ShouldMakeDuplicatePlasmaImagesWhenSet();

                ShouldMakeDifferentPlasmaImageWhenNotSet();
            }

            private void ShouldMakeDuplicatePlasmaImagesWhenSet()
            {
                using (var first = new MagickImage("plasma:red", 10, 10))
                {
                    using (var second = new MagickImage("plasma:red", 10, 10))
                    {
                        Assert.NotEqual(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }
            }

            private void ShouldMakeDifferentPlasmaImageWhenNotSet()
            {
                MagickNET.SetRandomSeed(42);

                using (var first = new MagickImage("plasma:red", 10, 10))
                {
                    using (var second = new MagickImage("plasma:red", 10, 10))
                    {
                        Assert.Equal(0.0, first.Compare(second, ErrorMetric.RootMeanSquared));
                    }
                }

                MagickNET.ResetRandomSeed();
            }
        }
    }
}
