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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MagickFormatInfoTests
    {
        [TestMethod]
        public void Test_IEquatable()
        {
            var first = MagickFormatInfo.Create(MagickFormat.Png);
            var second = MagickNET.GetFormatInformation(Files.SnakewarePNG);

            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first.Equals((object)second));
        }

        [TestMethod]
        public void Test_Properties()
        {
            var formatInfo = MagickNET.GetFormatInformation(MagickFormat.Gradient);
            Assert.IsNotNull(formatInfo);
            Assert.AreEqual(MagickFormat.Gradient, formatInfo.Format);
            Assert.IsTrue(formatInfo.CanReadMultithreaded);
            Assert.IsTrue(formatInfo.CanWriteMultithreaded);
            Assert.AreEqual("Gradual linear passing from one shade to another", formatInfo.Description);
            Assert.IsFalse(formatInfo.IsMultiFrame);
            Assert.IsTrue(formatInfo.IsReadable);
            Assert.IsFalse(formatInfo.IsWritable);
            Assert.AreEqual(null, formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jp2);
            Assert.IsNotNull(formatInfo);
            Assert.AreEqual(MagickFormat.Jp2, formatInfo.Format);
            Assert.IsTrue(formatInfo.CanReadMultithreaded);
            Assert.IsTrue(formatInfo.CanWriteMultithreaded);
            Assert.AreEqual("JPEG-2000 File Format Syntax", formatInfo.Description);
            Assert.IsFalse(formatInfo.IsMultiFrame);
            Assert.IsTrue(formatInfo.IsReadable);
            Assert.IsTrue(formatInfo.IsWritable);
            Assert.AreEqual("image/jp2", formatInfo.MimeType);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jpg);
            Assert.IsNotNull(formatInfo);
            Assert.IsTrue(formatInfo.CanReadMultithreaded);
            Assert.IsTrue(formatInfo.CanWriteMultithreaded);
            Assert.AreEqual("Joint Photographic Experts Group JFIF format", formatInfo.Description);
            Assert.AreEqual(MagickFormat.Jpg, formatInfo.Format);
            Assert.IsFalse(formatInfo.IsMultiFrame);
            Assert.IsTrue(formatInfo.IsReadable);
            Assert.IsTrue(formatInfo.IsWritable);
            Assert.AreEqual("image/jpeg", formatInfo.MimeType);
            Assert.AreEqual(MagickFormat.Jpeg, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Png);
            Assert.IsNotNull(formatInfo);
            Assert.IsTrue(formatInfo.CanReadMultithreaded);
            Assert.IsTrue(formatInfo.CanWriteMultithreaded);
            Assert.AreEqual("Portable Network Graphics", formatInfo.Description);
            Assert.AreEqual(MagickFormat.Png, formatInfo.Format);
            Assert.IsFalse(formatInfo.IsMultiFrame);
            Assert.IsTrue(formatInfo.IsReadable);
            Assert.IsTrue(formatInfo.IsWritable);
            Assert.AreEqual("image/png", formatInfo.MimeType);
            Assert.AreEqual(MagickFormat.Png, formatInfo.ModuleFormat);

            formatInfo = MagickNET.GetFormatInformation(MagickFormat.Xps);
            Assert.IsNotNull(formatInfo);
            Assert.IsFalse(formatInfo.CanReadMultithreaded);
            Assert.IsFalse(formatInfo.CanWriteMultithreaded);
            Assert.AreEqual("Microsoft XML Paper Specification", formatInfo.Description);
            Assert.AreEqual(MagickFormat.Xps, formatInfo.Format);
            Assert.IsFalse(formatInfo.IsMultiFrame);
            Assert.IsTrue(formatInfo.IsReadable);
            Assert.IsFalse(formatInfo.IsWritable);
            Assert.IsNull(formatInfo.MimeType);
            Assert.AreEqual(MagickFormat.Xps, formatInfo.ModuleFormat);
        }

        [TestMethod]
        public void Test_Unregister()
        {
            var formatInfo = MagickNET.GetFormatInformation(MagickFormat.X3f);
            Assert.IsNotNull(formatInfo);
            Assert.IsTrue(formatInfo.Unregister());

            var settings = new MagickReadSettings()
            {
                Format = MagickFormat.X3f,
            };

            ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
            {
                var image = new MagickImage();
                image.Read(new byte[] { 1, 2, 3, 4 }, settings);
            });
        }
    }
}
