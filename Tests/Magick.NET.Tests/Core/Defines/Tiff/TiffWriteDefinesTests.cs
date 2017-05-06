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

using System.IO;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class TiffWriteDefinesTests
  {
    private static IMagickImage WriteTiff(IMagickImage image)
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        image.Format = MagickFormat.Tiff;
        image.Write(memStream);
        memStream.Position = 0;
        return new MagickImage(memStream);
      }
    }

    [TestMethod]
    public void Test_Empty()
    {
      using (IMagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(new TiffWriteDefines()
        {
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "alpha"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "endian"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "rows-per-strip"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "tile-geometry"));

        image.Settings.SetDefines(new TiffWriteDefines()
        {
          Endian = Endian.Undefined,
          FillOrder = Endian.Undefined
        });

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.Tiff, "endian"));
      }
    }

    [TestMethod]
    public void Test_Alpha_Endian()
    {
      TiffWriteDefines defines = new TiffWriteDefines()
      {
        Alpha = TiffAlpha.Associated,
        Endian = Endian.MSB,
      };

      using (IMagickImage input = new MagickImage(Files.Builtin.Logo))
      {
        input.Settings.SetDefines(defines);
        input.Alpha(AlphaOption.Set);

        using (IMagickImage output = WriteTiff(input))
        {
          Assert.AreEqual("associated", output.GetAttribute("tiff:alpha"));
          Assert.AreEqual("msb", output.GetAttribute("tiff:endian"));
        }
      }
    }

    [TestMethod]
    public void Test_FillOrder_RowsPerStrip()
    {
      TiffWriteDefines defines = new TiffWriteDefines()
      {
        FillOrder = Endian.LSB,
        RowsPerStrip = 42,
        TileGeometry = new MagickGeometry(100, 100)
      };

      using (IMagickImage image = new MagickImage(Files.Builtin.Logo))
      {
        image.Settings.SetDefines(defines);

        Assert.AreEqual("LSB", image.Settings.GetDefine(MagickFormat.Tiff, "fill-order"));
        Assert.AreEqual("42", image.Settings.GetDefine(MagickFormat.Tiff, "rows-per-strip"));
        Assert.AreEqual("100x100", image.Settings.GetDefine(MagickFormat.Tiff, "tile-geometry"));
      }
    }
  }
}
