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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        [TestClass]
        public class TheLimitMemoryMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNegative()
            {
                ExceptionAssert.Throws<ArgumentOutOfRangeException>("percentage", () => ResourceLimits.LimitMemory(new Percentage(-0.99)));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsTooHigh()
            {
                ExceptionAssert.Throws<ArgumentOutOfRangeException>("percentage", () => ResourceLimits.LimitMemory(new Percentage(100.1)));
            }

            [TestMethod]
            public void ShouldChangeAreaAndMemory()
            {
                ExecuteInsideLock(() =>
                {
                    var area = ResourceLimits.Area;
                    var memory = ResourceLimits.Memory;

                    ResourceLimits.LimitMemory((Percentage)80);

                    Assert.AreNotEqual(area, ResourceLimits.Area);
                    Assert.AreNotEqual(memory, ResourceLimits.Memory);

                    ResourceLimits.Area = area;
                    ResourceLimits.Memory = memory;
                });
            }

#if WINDOWS_BUILD
            [TestMethod]
            public void ShouldSetMemoryAndAreaToTheCorrectValues()
            {
                ExecuteInsideLock(() =>
                {
                    var area = ResourceLimits.Area;
                    var memory = ResourceLimits.Memory;

                    ResourceLimits.LimitMemory((Percentage)100);

                    Assert.AreEqual(area * 2, ResourceLimits.Area, 8192);
                    Assert.AreEqual(memory * 2, ResourceLimits.Memory, 8192);

                    ResourceLimits.Area = area;
                    ResourceLimits.Memory = memory;
                });
            }
#endif
        }
    }
}
