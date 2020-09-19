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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheAvifCoder
    {
        [TestMethod]
        public void ShouldEncodeAndDecodeAlphaChannel()
        {
            using (var input = new MagickImage(Files.TestPNG))
            {
                input.Resize(new Percentage(10));

                using (var stream = new MemoryStream())
                {
                    input.Write(stream, MagickFormat.Avif);

                    stream.Position = 0;

                    using (var output = new MagickImage(stream))
                    {
                        Assert.IsTrue(output.HasAlpha);
                        Assert.AreEqual(MagickFormat.Avif, output.Format);
                        Assert.AreEqual(15, output.Width);
                        Assert.AreEqual(10, output.Height);
                    }
                }
            }
        }
    }
}
