// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    [TestClass]
    public class ResourceLimitsTests
    {
        [TestMethod]
        public void Test_Values()
        {
            Assert.AreEqual(ulong.MaxValue, ResourceLimits.Disk);
            if (ResourceLimits.Memory < 100000000U)
                Assert.Fail("Invalid memory limit: " + ResourceLimits.Memory);
            Assert.AreEqual(10000000U, ResourceLimits.Height);
            Assert.AreEqual(0U, ResourceLimits.Throttle);
            Assert.AreEqual(10000000U, ResourceLimits.Width);

            ResourceLimits.Disk = 40000U;
            Assert.AreEqual(40000U, ResourceLimits.Disk);
            ResourceLimits.Disk = ulong.MaxValue;

            ResourceLimits.Height = 100000U;
            Assert.AreEqual(100000U, ResourceLimits.Height);
            ResourceLimits.Height = 10000000U;

            ResourceLimits.ListLength = 32U;
            Assert.AreEqual(32U, ResourceLimits.ListLength);
            ResourceLimits.ListLength = ulong.MaxValue;

            ResourceLimits.Memory = 10000000U;
            Assert.AreEqual(10000000U, ResourceLimits.Memory);
            ResourceLimits.Memory = 8585838592U;

            Assert.AreEqual(1U, ResourceLimits.Thread);
            ResourceLimits.Thread = 2U;
            Assert.AreEqual(1U, ResourceLimits.Thread);

            ResourceLimits.Throttle = 1U;
            Assert.AreEqual(1U, ResourceLimits.Throttle);
            ResourceLimits.Throttle = 0U;

            ResourceLimits.Width = 200000U;
            Assert.AreEqual(200000U, ResourceLimits.Width);
            ResourceLimits.Width = 10000000U;
        }
    }
}
