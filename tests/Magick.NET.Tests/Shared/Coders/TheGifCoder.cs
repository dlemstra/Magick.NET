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

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheGifCoder
    {
        [TestMethod]
        public void ShouldReturnTheCorrectNumberOfAnimationIterations()
        {
            using (IMagickImageCollection images = new MagickImageCollection())
            {
                images.Add(new MagickImage(MagickColors.Red, 1, 1));
                images.Add(new MagickImage(MagickColors.Green, 1, 1));

                using (var file = new TemporaryFile("output.gif"))
                {
                    images[0].AnimationIterations = 1;
                    images.Write(file.FullName);

                    images.Read(file.FullName);
                    Assert.AreEqual(1, images[0].AnimationIterations);
                }
            }
        }
    }
}
