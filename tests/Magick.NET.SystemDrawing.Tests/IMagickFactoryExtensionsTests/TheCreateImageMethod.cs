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

using System.Drawing;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickFactoryTests
    {
        public partial class TheCreateImageMethod
        {
            [TestMethod]
            public void ShouldCreateImageFromBitmap()
            {
                using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
                {
                    MagickFactory factory = new MagickFactory();
                    using (var image = factory.CreateImage(bitmap))
                    {
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                        Assert.AreEqual(MagickFormat.Png, image.Format);
                    }
                }
            }
        }
    }
}