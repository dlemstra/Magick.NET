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

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public partial class MagickFactoryTests
    {
        [TestMethod]
        public void CreateImageInfo_ReturnsMagickImageInfo()
        {
            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo();

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(0, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithBytes_ReturnsMagickImageInfo()
        {
            var data = File.ReadAllBytes(Files.ImageMagickJPG);

            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(data);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithFileInfo_ReturnsMagickImageInfo()
        {
            var file = new FileInfo(Files.ImageMagickJPG);

            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(file);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }

        [TestMethod]
        public void CreateImageInfo_WithStream_ReturnsMagickImageInfo()
        {
            using (var stream = File.OpenRead(Files.ImageMagickJPG))
            {
                MagickFactory factory = new MagickFactory();
                IMagickImageInfo imageInfo = factory.CreateImageInfo(stream);

                Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
                Assert.AreEqual(123, imageInfo.Width);
            }
        }

        [TestMethod]
        public void CreateImageInfo_WithFileName_ReturnsMagickImageInfo()
        {
            MagickFactory factory = new MagickFactory();
            IMagickImageInfo imageInfo = factory.CreateImageInfo(Files.ImageMagickJPG);

            Assert.IsInstanceOfType(imageInfo, typeof(MagickImageInfo));
            Assert.AreEqual(123, imageInfo.Width);
        }
    }
}
