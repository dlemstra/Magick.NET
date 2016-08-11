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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Coders
{
  [TestClass]
  public partial class TiffTests
  {
    private const string _Category = "TiffTests";

    private static void TestValue(IptcProfile profile, IptcTag tag, string expectedValue)
    {
      IptcValue value = profile.GetValue(tag);
      Assert.IsNotNull(value);
      Assert.AreEqual(expectedValue, value.Value);
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IgnoreTags()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefine(MagickFormat.Tiff, "ignore-tags", "32934");
        image.Read(Files.Coders.IgnoreTagTIF);
      }

      using (MagickImage image = new MagickImage())
      {
        MagickReadSettings readSettings = new MagickReadSettings(new TiffReadDefines()
        {
          IgnoreTags = new string[] { "32934" }
        });
        image.Read(Files.Coders.IgnoreTagTIF, readSettings);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_IptcProfile()
    {
      using (MagickImage input = new MagickImage(Files.MagickNETIconPNG))
      {
        IptcProfile profile = input.GetIptcProfile();
        Assert.IsNull(profile);

        profile = new IptcProfile();
        profile.SetValue(IptcTag.Headline, "Magick.NET");
        profile.SetValue(IptcTag.CopyrightNotice, "Copyright.NET");

        input.AddProfile(profile);

        using (MemoryStream memStream = new MemoryStream())
        {
          input.Format = MagickFormat.Tiff;
          input.Write(memStream);

          memStream.Position = 0;
          using (MagickImage output = new MagickImage(memStream))
          {
            profile = output.GetIptcProfile();
            Assert.IsNotNull(profile);
            TestValue(profile, IptcTag.Headline, "Magick.NET");
            TestValue(profile, IptcTag.CopyrightNotice, "Copyright.NET");
          }
        }
      }
    }
  }
}