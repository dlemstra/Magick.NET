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

namespace Magick.NET.Core.Tests
{
    public partial class ThresholdTests
    {
        [TestClass]
        public class TheToStringMethod
        {
            [TestMethod]
            public void ShouldReturnSingleValueWhenOnlyMimimumIsSet()
            {
                var point = new Threshold(1.2);
                Assert.AreEqual("1.2", point.ToString());
            }

            [TestMethod]
            public void ShouldReturnValueWithMinimumAndMaximum()
            {
                var point = new Threshold(1.2, 3.4);
                Assert.AreEqual("1.2-3.4", point.ToString());
            }
        }
    }
}
