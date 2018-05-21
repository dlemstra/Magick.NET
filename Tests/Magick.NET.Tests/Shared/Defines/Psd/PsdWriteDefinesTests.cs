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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Defines
{
    [TestClass]
    public class PsdWriteDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new PsdWriteDefines()
                {
                });

                Assert.AreEqual("None", image.Settings.GetDefine(MagickFormat.Psd, "additional-info"));
            }
        }

        [TestMethod]
        public void Test_AdditionalInfo()
        {
            using (IMagickImageCollection images = new MagickImageCollection())
            {
                images.Read(Files.Coders.LayerStylesSamplePSD);

                CheckProfile(images[1], 264);

                var defines = new PsdWriteDefines()
                {
                    AdditionalInfo = PsdAdditionalInfo.All,
                };
                WriteAndCheckProfile(images, defines, 264);

                defines.AdditionalInfo = PsdAdditionalInfo.Selective;
                WriteAndCheckProfile(images, defines, 152);

                defines.AdditionalInfo = PsdAdditionalInfo.None;
                WriteAndCheckProfile(images, defines, 0);
            }
        }

        private static void CheckProfile(IMagickImage image, int expectedLength)
        {
            var profile = image.GetProfile("psd:additional-info");
            int actualLength = profile?.ToByteArray().Length ?? 0;
            Assert.AreEqual(expectedLength, actualLength);
        }

        private static void WriteAndCheckProfile(IMagickImageCollection images, PsdWriteDefines defines, int expectedLength)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                images.Write(memStream, defines);

                memStream.Position = 0;
                images.Read(memStream);
                CheckProfile(images[1], expectedLength);
            }
        }
    }
}
