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
    public class TheDdsCoder
    {
        [TestMethod]
        public void ShouldUseDxt1AsTheDefaultCompression()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                using (var output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.DXT1, output.Compression);
                }
            }
        }

        [TestMethod]
        public void ShouldUseDxt5AsTheDefaultCompressionForImagesWithAnAlphaChannel()
        {
            using (var input = new MagickImage(Files.Builtin.Logo))
            {
                input.Alpha(AlphaOption.Set);

                using (var output = WriteDds(input))
                {
                    Assert.AreEqual(CompressionMethod.DXT5, output.Compression);
                }
            }
        }

        private static MagickImage WriteDds(MagickImage input)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                input.Format = MagickFormat.Dds;
                input.Write(memStream);
                memStream.Position = 0;

                return new MagickImage(memStream);
            }
        }
    }
}
