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
    public partial class ResourceLimitsTests
    {
        [TestClass]
        public class TheMemoryProperty
        {
            [TestMethod]
            public void ShouldHaveTheCorrectValue()
            {
                if (ResourceLimits.Memory < 100000000U)
                    Assert.Fail("Invalid memory limit: " + ResourceLimits.Memory);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                ExecuteInsideLock(() =>
                {
                    var memory = ResourceLimits.Memory;

                    ResourceLimits.Memory = 10000000U;
                    Assert.AreEqual(10000000U, ResourceLimits.Memory);
                    ResourceLimits.Memory = memory;
                });
            }
        }
    }
}
