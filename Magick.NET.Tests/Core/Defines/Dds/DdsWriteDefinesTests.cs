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
  public class DdsWriteDefinesTests
  {
    private const string _Category = "DdsWriteDefines";

    private static MagickImage WriteDds(MagickImage input)
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        input.Format = MagickFormat.Dds;
        input.Write(memStream);
        memStream.Position = 0;

        return new MagickImage(memStream);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ClusterFit_Mipmaps_WeightByAlpha()
    {
      DdsWriteDefines defines = new DdsWriteDefines()
      {
        ClusterFit = true,
        Mipmaps = 0,
        WeightByAlpha = false,
      };

      using (MagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.SetDefines(defines);

        Assert.AreEqual("True", image.GetDefine(MagickFormat.Dds, "cluster-fit"));
        Assert.AreEqual("0", image.GetDefine(MagickFormat.Dds, "mipmaps"));
        Assert.AreEqual("False", image.GetDefine(MagickFormat.Dds, "weight-by-alpha"));
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Compression()
    {
      DdsWriteDefines defines = new DdsWriteDefines()
      {
        Compression = DdsCompression.None
      };

      using (MagickImage input = new MagickImage(Files.Builtin.Logo))
      {
        using (MagickImage output = WriteDds(input))
        {
          Assert.AreEqual(CompressionMethod.DXT1, output.CompressionMethod);
        }

        input.SetDefines(defines);

        using (MagickImage output = WriteDds(input))
        {
          Assert.AreEqual(CompressionMethod.NoCompression, output.CompressionMethod);
        }
      }
    }
  }
}
