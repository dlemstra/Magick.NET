//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickFormatInfoTests
  {
    private const string _Category = "MagickFormatInfo";

    [TestMethod, TestCategory(_Category)]
    public void Test_IEquatable()
    {
      MagickFormatInfo first = MagickFormatInfo.Create(MagickFormat.Png);
      MagickFormatInfo second = MagickNET.GetFormatInformation(Files.SnakewarePNG);

      Assert.IsTrue(first == second);
      Assert.IsTrue(first.Equals(second));
      Assert.IsTrue(first.Equals((object)second));
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_MimeType()
    {
      MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(MagickFormat.Jpg);
      Assert.IsNotNull(formatInfo);
      Assert.AreEqual("image/jpeg", formatInfo.MimeType);

      formatInfo = MagickNET.GetFormatInformation(MagickFormat.Png);
      Assert.IsNotNull(formatInfo);
      Assert.AreEqual("image/png", formatInfo.MimeType);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Unregister()
    {
      using (MagickImage image = new MagickImage(Files.SnakewarePNG))
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          image.Resize(256, 256);
          image.Format = MagickFormat.Ico;
          image.Write(memoryStream);
          memoryStream.Position = 0;

          MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(MagickFormat.Ico);
          Assert.IsNotNull(formatInfo);
          Assert.IsTrue(formatInfo.Unregister());

          ExceptionAssert.Throws<MagickMissingDelegateErrorException>(delegate ()
          {
            new MagickImage(memoryStream);
          });
        }
      }
    }
  }
}
