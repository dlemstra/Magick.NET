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

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheMinimumBoundingBoxMethod
        {
            [Fact]
            public void ShouldReturnTheMinimumBoundingBox()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var coordinates = image.MinimumBoundingBox().ToList();
                    Assert.Equal(4, coordinates.Count);

                    Assert.InRange(coordinates[0].X, 550, 551);
                    Assert.InRange(coordinates[0].Y, 469, 470);
                    Assert.InRange(coordinates[1].X, 109, 110);
                    Assert.InRange(coordinates[1].Y, 489, 490);
                    Assert.InRange(coordinates[2].X, 86, 87);
                    Assert.InRange(coordinates[2].Y, 7, 8);
                    Assert.InRange(coordinates[3].X, 527, 528);
                    Assert.InRange(coordinates[3].Y, -14, -13);
                }
            }
        }
    }
}
