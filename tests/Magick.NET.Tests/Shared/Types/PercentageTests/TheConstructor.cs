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
    public partial class PercentageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldDefaultToZero()
            {
                Percentage percentage = default;
                Assert.AreEqual("0%", percentage.ToString());
            }

            [TestMethod]
            public void ShouldSetValue()
            {
                var percentage = new Percentage(50);
                Assert.AreEqual("50%", percentage.ToString());
            }

            [TestMethod]
            public void ShouldHandleValueAbove100()
            {
                var percentage = new Percentage(200.0);
                Assert.AreEqual("200%", percentage.ToString());
            }

            [TestMethod]
            public void ShouldHandleNegativeValue()
            {
                var percentage = new Percentage(-25);
                Assert.AreEqual("-25%", percentage.ToString());
            }
        }
    }
}
