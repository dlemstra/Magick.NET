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
    public partial class PercentageTests
    {
        [TestClass]
        public class TheEqualsMethod
        {
            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                Percentage percentage = default;

                Assert.IsFalse(percentage.Equals(null));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                Percentage percentage = default;

                Assert.IsTrue(percentage.Equals(percentage));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                Percentage percentage = default;

                Assert.IsTrue(percentage.Equals((object)percentage));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.0);

                Assert.IsTrue(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.0);

                Assert.IsTrue(first.Equals((object)second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.1);

                Assert.IsFalse(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new Percentage(50.0);
                var second = new Percentage(50.1);

                Assert.IsFalse(first.Equals((object)second));
            }
        }
    }
}
