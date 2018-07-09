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

using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Coders
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
    }
}
