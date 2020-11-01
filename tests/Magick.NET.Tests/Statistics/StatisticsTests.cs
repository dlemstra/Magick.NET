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
    public class StatisticsTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var image = new MagickImage(Files.SnakewarePNG);

            var first = image.Statistics();

            Assert.False(first.Equals(null));
            Assert.True(first.Equals(first));
            Assert.True(first.Equals((object)first));

            var second = image.Statistics();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));

            image = new MagickImage(Files.MagickNETIconPNG);

            second = image.Statistics();

            Assert.True(first != second);
            Assert.False(first.Equals(second));
        }

        [Fact]
        public void Test_Statistics()
        {
            var image = new MagickImage(Files.SnakewarePNG);

            var statistics = image.Statistics();

            var red = statistics.GetChannel(PixelChannel.Red);

            Assert.NotNull(red);
            Assert.Equal(8, red.Depth);
            Assert.InRange(red.Entropy, 0.98, 0.99);
            Assert.InRange(red.Kurtosis, -1.90, -1.89);
            Assert.Equal(0, red.Minimum);
            Assert.InRange(red.Skewness, 0.32, 0.33);
#if Q8
            Assert.Equal(2, red.Maximum);
            Assert.InRange(red.Mean, 0.83, 0.84);
            Assert.InRange(red.StandardDeviation, 0.98, 0.99);
            Assert.InRange(red.Sum, 0.83, 0.84);
            Assert.InRange(red.SumCubed, 3.35, 3.36);
            Assert.InRange(red.SumFourthPower, 6.71, 6.72);
            Assert.InRange(red.SumSquared, 1.67, 1.68);
#else
            Assert.Equal(514, red.Maximum);
            Assert.InRange(red.Mean, 215.79, 215.80);
            Assert.InRange(red.StandardDeviation, 253.68, 253.69);
            Assert.InRange(red.Sum, 215.79, 215.80);
            Assert.InRange(red.SumCubed, 57013088.69, 57013088.70);
            Assert.InRange(red.SumFourthPower, 29304727586.71, 29304727586.72);
            Assert.InRange(red.SumSquared, 110920.40, 110920.41);
#endif

            Assert.NotNull(statistics.Composite());
            Assert.NotNull(statistics.GetChannel(PixelChannel.Alpha));

            Assert.Null(statistics.GetChannel(PixelChannel.Green));
            Assert.Null(statistics.GetChannel(PixelChannel.Blue));
            Assert.Null(statistics.GetChannel(PixelChannel.Black));
        }
    }
}
