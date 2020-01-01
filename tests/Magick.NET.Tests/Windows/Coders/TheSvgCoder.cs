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

#if WINDOWS_BUILD

using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheSvgCoder
    {
        [TestMethod]
        public void ShouldDetectFormatFromXmlDeclaration()
        {
            var data = Encoding.ASCII.GetBytes(@"<?xml version=""1.0"" encoding=""UTF-8""?>");

            IMagickImageInfo info = new MagickImageInfo(data);

            Assert.AreEqual(MagickFormat.Svg, info.Format);
            Assert.AreEqual(0, info.Width);
            Assert.AreEqual(0, info.Height);
        }

        [TestMethod]
        public void ShouldDetectFormatFromSvgTag()
        {
            var data = Encoding.ASCII.GetBytes(@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""1000"" height=""716"">");

            IMagickImageInfo info = new MagickImageInfo(data);

            Assert.AreEqual(MagickFormat.Svg, info.Format);
            Assert.AreEqual(1000, info.Width);
            Assert.AreEqual(716, info.Height);
        }

        [TestMethod]
        public void ShouldUseWidthFromReadSettings()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    Width = 100,
                };

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(100, image.Width);
                Assert.AreEqual(48, image.Height);
            }
        }

        [TestMethod]
        public void ShouldUseHeightFromReadSettings()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    Height = 200,
                };

                image.Read(Files.Logos.MagickNETSVG, settings);
            }
        }

        [TestMethod]
        public void ShouldUseWidthAndHeightFromReadSettings()
        {
            using (IMagickImage image = new MagickImage())
            {
                MagickReadSettings settings = new MagickReadSettings
                {
                    Width = 300,
                    Height = 300,
                };

                image.Read(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(144, image.Height);

                image.Ping(Files.Logos.MagickNETSVG, settings);

                Assert.AreEqual(300, image.Width);
                Assert.AreEqual(144, image.Height);
            }
        }

        [TestMethod]
        public void ShouldReadFontsWithQuotes()
        {
            var svg = @"<?xml version=""1.0"" encoding=""utf-8""?>
<svg version=""1.1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 0 220 80"">
<style type=""text/css"">
  .st0{font-family:'Tahoma';font-size:40}
  .st1{font-family:""Courier"";font-size:40}
</style>
<g id=""changable-text"">
  <text transform=""matrix(1 0 0 1 1 35)"" class=""st0"">FONT TEST</text>
  <text transform=""matrix(1 0 0 1 1 70)"" class=""st1"">FONT TEST</text>
</g>
</svg>";
            var bytes = Encoding.UTF8.GetBytes(svg);
            using (IMagickImage image = new MagickImage(bytes))
            {
                ColorAssert.AreEqual(MagickColors.Black, image, 133, 55);
                ColorAssert.AreEqual(MagickColors.Black, image, 124, 20);
            }
        }

        [TestMethod]
        public void IsThreadSafe()
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