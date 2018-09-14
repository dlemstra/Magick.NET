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

#if WINDOWS_BUILD

using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class SvgTests
    {
        [TestMethod]
        public void Test_Dimensions()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    Width = 100,
                };

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(100, image.Width);
                Assert.AreEqual(47, image.Height);

                settings.Width = null;
                settings.Height = 200;

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(417, image.Width);
                Assert.AreEqual(200, image.Height);

                settings.Width = 300;
                settings.Height = 300;

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(143, image.Height);

                image.Ping(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(143, image.Height);
            }
        }

        [TestMethod]
        public void SvgDecoderIsThreadSafe()
        {
            var svg = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<svg width=""50"" height=""15"" xmlns=""http://www.w3.org/2000/svg"">
  <text x=""25"" y=""11"" font-size=""9px"" font-family=""Verdana"">1</text>
</svg>";
            var bytes = Encoding.UTF8.GetBytes(svg);

            string signature = LoadImage(bytes);
            Parallel.For(1, 10, (int i) =>
            {
                Assert.AreEqual(signature, LoadImage(bytes));
            });
        }

        private static string LoadImage(byte[] bytes)
        {
            using (IMagickImage image = new MagickImage(bytes))
            {
                return image.Signature;
            }
        }
    }
}

#endif