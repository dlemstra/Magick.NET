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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Types
{
    public partial class DensityTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var density = new Density(50.0);

                Assert.IsFalse(density == null);
                Assert.IsTrue(density != null);
                Assert.IsFalse(null == density);
                Assert.IsTrue(null != density);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenUnitsAreEqual()
            {
                var first = new Density(50.0);
                var second = new Density(50);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenUnitsAreNotEqual()
            {
                var first = new Density(50.0);
                var second = new Density(50, DensityUnit.PixelsPerCentimeter);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenXAndYAreEqual()
            {
                var first = new Density(50.0, 25.0);
                var second = new Density(50, 25);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenXIsNotEqual()
            {
                var first = new Density(50.0, 25.0);
                var second = new Density(5, 25);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenYIsNotEqual()
            {
                var first = new Density(50.0, 25.0);
                var second = new Density(50, 2);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
            }
        }
    }
}
