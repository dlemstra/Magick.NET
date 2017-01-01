//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class TiffReadDefinesTests
  {
    [TestMethod]
    public void Test_Empty()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(new TiffReadDefines()
        {
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "ignore-tags"));

        image.Settings.SetDefines(new TiffReadDefines()
        {
          IgnoreExifPoperties = false,
          IgnoreTags = new string[] { }
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "ignore-tags"));
      }
    }

    [TestMethod]
    public void Test_IgnoreExifPoperties()
    {
      MagickReadSettings settings = new MagickReadSettings()
      {
        Defines = new TiffReadDefines()
        {
          IgnoreExifPoperties = true
        }
      };

      using (MagickImage image = new MagickImage())
      {
        image.Read(Files.InvitationTif);
        Assert.IsNotNull(image.GetAttribute("exif:PixelXDimension"));

        image.Read(Files.InvitationTif, settings);
        Assert.IsNull(image.GetAttribute("exif:PixelXDimension"));
      }
    }
  }
}
