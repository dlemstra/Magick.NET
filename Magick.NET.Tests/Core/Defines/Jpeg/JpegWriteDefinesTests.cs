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

using System.IO;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class JpegWriteDefinesTests
  {
    private const string _Category = "JpegWriteDefines";

    [TestMethod, TestCategory(_Category)]
    public void Test_DctMethod_OptimizeCoding_Quality_QuantizationTables_SamplingFactors()
    {
      JpegWriteDefines defines = new JpegWriteDefines()
      {
        DctMethod = DctMethod.Fast,
        OptimizeCoding = false,
        Quality = new MagickGeometry(80, 80),
        QuantizationTables = @"C:\path\to\file.xml",
        SamplingFactors = new MagickGeometry[]
        {
          new MagickGeometry(5, 10),
          new MagickGeometry(15, 20)
        }
      };

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Settings.SetDefines(defines);

        Assert.AreEqual("Fast", image.Settings.GetDefine(MagickFormat.Jpeg, "dct-method"));
        Assert.AreEqual("False", image.Settings.GetDefine(MagickFormat.Jpeg, "optimize-coding"));
        Assert.AreEqual("80x80", image.Settings.GetDefine(MagickFormat.Jpeg, "quality"));
        Assert.AreEqual(@"C:\path\to\file.xml", image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
        Assert.AreEqual("5x10,15x20", image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Extent()
    {
      JpegWriteDefines defines = new JpegWriteDefines()
      {
        Extent = 10
      };

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        using (MemoryStream memStream = new MemoryStream())
        {
          image.Settings.SetDefines(defines);

          image.Format = MagickFormat.Jpeg;
          image.Write(memStream);
          Assert.IsTrue(memStream.Length < 10000);
        }
      }
    }
  }
}
