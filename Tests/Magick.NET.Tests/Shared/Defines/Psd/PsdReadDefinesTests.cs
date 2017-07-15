// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public class PsdReadDefinesTests
    {
        [TestMethod]
        public void Test_Empty()
        {
            using (IMagickImage image = new MagickImage())
            {
                image.Settings.SetDefines(new PsdReadDefines()
                {
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));

                image.Settings.SetDefines(new PsdReadDefines()
                {
                    AlphaUnblend = true,
                });

                Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend"));
            }
        }

        [TestMethod]
        public void Test_AlphaUnblend()
        {
            MagickReadSettings settings = new MagickReadSettings()
            {
                Defines = new PsdReadDefines()
                {
                    AlphaUnblend = false,
                },
            };

            using (IMagickImage image = new MagickImage())
            {
                image.Read(Files.Coders.PlayerPSD, settings);

                string define = image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend");
                Assert.AreEqual("False", define);
            }

            using (IMagickImage image = new MagickImage())
            {
                settings.Defines = new PsdReadDefines()
                {
                    AlphaUnblend = true,
                };

                image.Read(Files.Coders.PlayerPSD, settings);

                string define = image.Settings.GetDefine(MagickFormat.Psd, "alpha-unblend");
                Assert.IsNull(define);
            }
        }
    }
}
