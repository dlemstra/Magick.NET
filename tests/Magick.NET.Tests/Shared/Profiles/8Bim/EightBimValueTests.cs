// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class EightBimValueTests
    {
        [TestMethod]
        public void Test_IEquatable()
        {
            var first = Get8BimValue();
            var second = Get8BimValue();

            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first.Equals((object)second));
        }

        [TestMethod]
        public void Test_ToByteArray()
        {
            var value = Get8BimValue();
            byte[] bytes = value.ToByteArray();
            Assert.AreEqual(273, bytes.Length);
        }

        private static IEightBimValue Get8BimValue()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                var profile = image.Get8BimProfile();
                return profile.Values.First();
            }
        }
    }
}
