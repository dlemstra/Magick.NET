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
    public partial class IptcProfileTests
    {
        [TestClass]
        public class TheValuesProperty
        {
            [TestMethod]
            public void ShouldReturnTheValues()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    Assert.IsNotNull(profile);

                    Assert.AreEqual(18, profile.Values.Count());

                    foreach (IptcValue value in profile.Values)
                    {
                        Assert.IsNotNull(value.Value);
                    }
                }
            }
        }
    }
}
