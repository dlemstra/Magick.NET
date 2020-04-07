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

namespace Magick.NET.Tests
{
    public partial class EightBimProfileTests
    {
        [TestClass]
        public class TheValuesProperty
        {
            [TestMethod]
            public void ShouldReturnTheValues()
            {
                using (IMagickImage image = new MagickImage(Files.EightBimTIF))
                {
                    var profile = image.Get8BimProfile();

                    Assert.IsNotNull(profile);

                    Assert.AreEqual(25, profile.Values.Count());

                    var firstValue = profile.Values.First();
                    Assert.AreEqual(1061, firstValue.ID);

                    byte[] bytes = new byte[16] { 154, 137, 173, 93, 40, 109, 186, 33, 2, 200, 203, 169, 103, 5, 63, 219 };
                    CollectionAssert.AreEqual(bytes, firstValue.ToByteArray());

                    foreach (var value in profile.Values)
                    {
                        Assert.IsNotNull(value.ToByteArray());
                    }
                }
            }
        }
    }
}
