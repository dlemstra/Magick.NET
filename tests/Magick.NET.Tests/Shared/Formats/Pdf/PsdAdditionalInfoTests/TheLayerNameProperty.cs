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
using ImageMagick.Formats.Psd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Formats.Pdf.PsdAdditionalInfoTests
{
    public partial class PsdAdditionalInfoTests
    {
        [TestClass]
        public class TheLayerNameProperty
        {
            [TestMethod]
            public void ShouldReturnNullWhenImageHasNoPsdAdditionalInfo()
            {
                using (var images = new MagickImageCollection(Files.Coders.WizardPSD))
                {
                    var info = PsdAdditionalInfo.FromImage(images[1]);

                    Assert.IsNotNull(info);
                    Assert.AreEqual("Волшебник-2", info.LayerName);

                    info = PsdAdditionalInfo.FromImage(images[2]);

                    Assert.IsNotNull(info);
                    Assert.AreEqual("Wizard-1", info.LayerName);
                }
            }
        }
    }
}
