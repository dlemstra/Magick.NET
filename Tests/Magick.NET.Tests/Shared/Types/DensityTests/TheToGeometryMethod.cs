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

namespace Magick.NET.Tests.Shared.Types
{
    public partial class DensityTests
    {
        [TestClass]
        public class TheToGeometryMethod
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                var density = new Density(50.0);

                var geometry = density.ToGeometry(0.5, 2.0);
                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(25, geometry.Width);
                Assert.AreEqual(100, geometry.Height);
            }
        }
    }
}
