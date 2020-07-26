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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickFactoryTests
    {
        [TestClass]
        public class TheQuantumInfoProperty
        {
            [TestMethod]
            public void ShouldHaveTheCorrectDephValue()
            {
                var factory = new MagickFactory();
#if Q8
                Assert.AreEqual(factory.QuantumInfo.Depth, 8);
#else
                Assert.AreEqual(factory.QuantumInfo.Depth, 16);
#endif
            }

            [TestMethod]
            public void ShouldHaveTheCorrectMaxValue()
            {
                var factory = new MagickFactory();
#if Q8
                Assert.AreEqual(factory.QuantumInfo.Max, byte.MaxValue);
#elif Q16
                Assert.AreEqual(factory.QuantumInfo.Max, ushort.MaxValue);
#else
                Assert.AreEqual(factory.QuantumInfo.Max, (float)ushort.MaxValue);
#endif
            }
        }
    }
}
