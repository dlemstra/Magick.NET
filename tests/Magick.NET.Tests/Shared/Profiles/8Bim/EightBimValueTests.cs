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
    public class EightBimValueTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var first = Get8BimValue();
            var second = Get8BimValue();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_ToByteArray()
        {
            var value = Get8BimValue();
            byte[] bytes = value.ToByteArray();
            Assert.Equal(273, bytes.Length);
        }

        private static IEightBimValue Get8BimValue()
        {
            using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.Get8BimProfile();
                return profile.Values.First();
            }
        }
    }
}
