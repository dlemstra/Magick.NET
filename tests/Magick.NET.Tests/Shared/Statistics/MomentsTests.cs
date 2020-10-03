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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class MomentsTests
    {
        [Fact]
        public void Test_Moments()
        {
            using (var image = new MagickImage(Files.ImageMagickJPG))
            {
                var moments = image.Moments();
                Assert.NotNull(moments);
                var first = moments.GetChannel(PixelChannel.Red);
                Assert.NotNull(first);

                Assert.Equal(PixelChannel.Red, first.Channel);
                Assert.InRange(first.Centroid.X, 56.59, 56.60);
                Assert.InRange(first.Centroid.Y, 56.00, 56.01);
                Assert.InRange(first.EllipseAngle, 148.92, 148.93);
                Assert.InRange(first.EllipseAxis.X, 73.53, 73.54);
                Assert.InRange(first.EllipseAxis.Y, 66.82, 66.83);
                Assert.InRange(first.EllipseEccentricity, 0.41, 0.42);
                Assert.InRange(first.EllipseIntensity, 0.79, 0.80);

                var expected = new double[] { 0.2004, 0.0003, 0.0001, 0.0, 0.0, 0.0, 0.0, 0.0 };
                for (int i = 0; i < 8; i++)
                {
                    Assert.InRange(first.HuInvariants(i), expected[i] - 0.0001, expected[i] + 0.0001);
                }

                moments = image.Moments();
                var second = moments.GetChannel(PixelChannel.Red);

                Assert.True(first.Centroid == second.Centroid);
                Assert.True(first.Centroid.Equals(second.Centroid));
                Assert.True(first.Centroid.Equals((object)second.Centroid));

                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    first.HuInvariants(9);
                });
            }
        }
    }
}
