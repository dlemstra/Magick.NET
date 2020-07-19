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

using System;
using ImageMagick;
using ImageMagick.Formats.Tiff;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheToBase64Method
        {
            [TestMethod]
            public void ShouldReturnBase64EncodedString()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64();
                    Assert.IsNotNull(base64);
                    Assert.AreEqual(11704, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(8778, bytes.Length);
                }
            }

            [TestMethod]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedFormat()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var base64 = image.ToBase64(MagickFormat.Jpeg);
                    Assert.IsNotNull(base64);
                    Assert.AreEqual(1140, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(853, bytes.Length);
                }
            }

            [TestMethod]
            public void ShouldReturnBase64EncodedStringUsingTheSpecifiedDefines()
            {
                using (var image = new MagickImage(Files.SnakewarePNG))
                {
                    var defines = new TiffWriteDefines
                    {
                        PreserveCompression = true,
                    };

                    var base64 = image.ToBase64(defines);
                    Assert.IsNotNull(base64);
                    Assert.AreEqual(10800, base64.Length);

                    var bytes = Convert.FromBase64String(base64);
                    Assert.IsNotNull(bytes);
                    Assert.AreEqual(8100, bytes.Length);
                }
            }
        }
    }
}
