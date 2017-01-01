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
  public class Jp2ReadDefinesTests
  {
    [TestMethod]
    public void Test_Empty()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(new Jp2ReadDefines()
        {
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "quality-layers"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Jp2, "reduce-factor"));
      }
    }

    [TestMethod]
    public void Test_QualityLayers_ReduceFactor()
    {
      MagickReadSettings settings = new MagickReadSettings()
      {
        Defines = new Jp2ReadDefines()
        {
          QualityLayers = 4,
          ReduceFactor = 2
        }
      };

      using (MagickImage image = new MagickImage())
      {
        image.Read(Files.Coders.GrimJp2, settings);

        Assert.AreEqual("4", image.Settings.GetDefine(MagickFormat.Jp2, "quality-layers"));
        Assert.AreEqual("2", image.Settings.GetDefine(MagickFormat.Jp2, "reduce-factor"));
      }
    }
  }
}
