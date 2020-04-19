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

using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheMinimumBoundingBoxMethod
        {
            [TestMethod]
            public void ShouldReturnTheMinimumBoundingBox()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var coordinates = image.MinimumBoundingBox().ToList();
                    Assert.AreEqual(4, coordinates.Count);

                    Assert.AreEqual(550, coordinates[0].X, 1);
                    Assert.AreEqual(469, coordinates[0].Y, 1);
                    Assert.AreEqual(109, coordinates[1].X, 1);
                    Assert.AreEqual(489, coordinates[1].Y, 1);
                    Assert.AreEqual(86, coordinates[2].X, 1);
                    Assert.AreEqual(7, coordinates[2].Y, 1);
                    Assert.AreEqual(527, coordinates[3].X, 1);
                    Assert.AreEqual(-13, coordinates[3].Y, 1);
                }
            }
        }
    }
}
