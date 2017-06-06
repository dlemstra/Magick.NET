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

#if !NETCOREAPP1_1

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  public partial class TiffTests
  {
    [TestMethod]
    public void Test_Image_ByteArray()
    {
      using (Image img = Image.FromFile(Files.Coders.PageTIF))
      {
        byte[] bytes = null;
        using (MemoryStream memStream = new MemoryStream())
        {
          img.Save(memStream, ImageFormat.Tiff);
          bytes = memStream.GetBuffer();
        }

        using (IMagickImage image = new MagickImage(bytes))
        {
          image.CompressionMethod = CompressionMethod.Group4;

          using (MemoryStream memStream = new MemoryStream())
          {
            image.Write(memStream);
            memStream.Position = 0;

            using (IMagickImage before = new MagickImage(Files.Coders.PageTIF))
            {
              using (IMagickImage after = new MagickImage(memStream))
              {
                Assert.AreEqual(0.0, before.Compare(after, ErrorMetric.RootMeanSquared));
              }
            }
          }
        }
      }
    }
  }
}

#endif