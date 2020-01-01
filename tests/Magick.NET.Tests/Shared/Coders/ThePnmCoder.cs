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
    public class ThePnmCoder
    {
        [TestMethod]
        public void ShouldCreateWhiteImage()
        {
            using (IMagickImage input = new MagickImage("xc:white", 1, 1))
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Pnm);

                    memoryStream.Position = 0;
                    using (IMagickImage output = new MagickImage(memoryStream))
                    {
                        ColorAssert.AreEqual(MagickColors.White, output, 0, 0);
                    }
                }
            }
        }

        [TestMethod]
        public void ShouldCreateBlackImage()
        {
            using (IMagickImage input = new MagickImage("xc:black", 1, 1))
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Pnm);

                    memoryStream.Position = 0;
                    using (IMagickImage output = new MagickImage(memoryStream))
                    {
                        ColorAssert.AreEqual(MagickColors.Black, output, 0, 0);
                    }
                }
            }
        }
    }
}
