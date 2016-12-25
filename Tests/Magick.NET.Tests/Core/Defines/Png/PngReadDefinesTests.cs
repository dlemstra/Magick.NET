//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class PngReadDefinesTests
  {
    [TestMethod]
    public void Test_Empty()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(new PngReadDefines()
        {
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
        Assert.AreEqual(null, image.Settings.GetDefine("profile:skip"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Png, "swap-bytes"));
      }
    }

    [TestMethod]
    public void Test_PreserveiCCP_SwapBytes()
    {
      PngReadDefines defines = new PngReadDefines()
      {
        PreserveiCCP = true,
        SwapBytes = false
      };

      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(defines);

        Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Png, "swap-bytes"));
      }

      defines = new PngReadDefines()
      {
        PreserveiCCP = false,
        SwapBytes = true
      };

      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(defines);

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
        Assert.AreEqual("True", image.Settings.GetDefine(MagickFormat.Png, "swap-bytes"));
      }
    }

    [TestMethod]
    public void Test_SkipProfiles()
    {
      MagickReadSettings settings = new MagickReadSettings()
      {
        Defines = new PngReadDefines()
        {
          SkipProfiles = ProfileTypes.Xmp | ProfileTypes.Exif
        }
      };

      using (MagickImage image = new MagickImage())
      {
        image.Read(Files.FujiFilmFinePixS1ProPNG);
        Assert.IsNotNull(image.GetExifProfile());
        Assert.IsNotNull(image.GetXmpProfile());

        image.Read(Files.FujiFilmFinePixS1ProPNG, settings);
        Assert.IsNull(image.GetExifProfile());
        Assert.IsNull(image.GetXmpProfile());
        Assert.AreEqual("Exif,Xmp", image.Settings.GetDefine("profile:skip"));
      }
    }
  }
}
