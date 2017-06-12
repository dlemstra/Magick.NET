//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class QuantumTests
    {
        [TestMethod]
        public void Test_Depth()
        {
#if Q8
            Assert.AreEqual(Quantum.Depth, 8);
#elif Q16 || Q16HDRI
            Assert.AreEqual(Quantum.Depth, 16);
#else
#error Not implemented!
#endif
        }

        [TestMethod]
        public void Test_Max()
        {
#if Q8
            Assert.AreEqual(Quantum.Max, byte.MaxValue);
#elif Q16
            Assert.AreEqual(Quantum.Max, ushort.MaxValue);
#elif Q16HDRI
            Assert.AreEqual(Quantum.Max, (float)ushort.MaxValue);
#else
#error Not implemented!
#endif
        }
    }
}
