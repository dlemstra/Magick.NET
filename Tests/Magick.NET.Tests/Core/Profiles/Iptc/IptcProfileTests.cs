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

using System;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class IptcProfileTests
  {
    private static void TestProfileValues(IptcProfile profile)
    {
      TestProfileValues(profile, 18);
    }

    private static void TestProfileValues(IptcProfile profile, int count)
    {
      Assert.IsNotNull(profile);

      Assert.AreEqual(count, profile.Values.Count());

      foreach (IptcValue value in profile.Values)
      {
        Assert.IsNotNull(value.Value);
      }
    }

    private static void TestValue(IptcValue value, string expected)
    {
      Assert.IsNotNull(value);
      Assert.AreEqual(expected, value.Value);
    }

    [TestMethod]
    public void Test_SetEncoding()
    {
      using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        IptcProfile profile = image.GetIptcProfile();
        TestProfileValues(profile);

        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          profile.SetEncoding(null);
        });

        profile.SetEncoding(Encoding.UTF8);
        Assert.AreEqual(Encoding.UTF8, profile.Values.First().Encoding);
      }
    }

    [TestMethod]
    public void Test_SetValue()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        string credit = null;
        for (int i = 0; i < 255; i++)
          credit += i.ToString() + ".";

        using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
        {
          IptcProfile profile = image.GetIptcProfile();
          TestProfileValues(profile);

          IptcValue value = profile.GetValue(IptcTag.Title);
          TestValue(value, "Communications");

          profile.SetValue(IptcTag.Title, "Magick.NET Title");
          TestValue(value, "Magick.NET Title");

          value = profile.GetValue(IptcTag.Title);
          TestValue(value, "Magick.NET Title");

          value = profile.Values.FirstOrDefault(val => val.Tag == IptcTag.ReferenceNumber);
          Assert.IsNull(value);

          profile.SetValue(IptcTag.ReferenceNumber, "Magick.NET ReferenceNümber");

          value = profile.GetValue(IptcTag.ReferenceNumber);
          TestValue(value, "Magick.NET ReferenceNümber");

          profile.SetValue(IptcTag.Credit, credit);

          value = profile.GetValue(IptcTag.Credit);
          TestValue(value, credit);

          // Remove the 8bim profile so we can overwrite the iptc profile.
          image.RemoveProfile("8bim");
          image.AddProfile(profile);

          image.Write(memStream);
          memStream.Position = 0;
        }

        using (IMagickImage image = new MagickImage(memStream))
        {
          IptcProfile profile = image.GetIptcProfile();
          TestProfileValues(profile, 19);

          IptcValue value = profile.GetValue(IptcTag.Title);
          TestValue(value, "Magick.NET Title");

          value = profile.GetValue(IptcTag.ReferenceNumber);
          TestValue(value, "Magick.NET ReferenceNümber");

          value = profile.GetValue(IptcTag.Credit);
          TestValue(value, credit);

          ExceptionAssert.Throws<ArgumentNullException>(delegate ()
          {
            profile.SetValue(IptcTag.Caption, null, "Test");
          });

          profile.SetValue(IptcTag.Caption, "Test");
          value = profile.Values.ElementAt(1);
          Assert.AreEqual("Test", value.Value);

          profile.SetValue(IptcTag.Caption, Encoding.UTF32, "Test");
          Assert.AreEqual(Encoding.UTF32, value.Encoding);
          Assert.AreEqual("Test", value.Value);

          Assert.IsTrue(profile.RemoveValue(IptcTag.Caption));
          Assert.IsFalse(profile.RemoveValue(IptcTag.Caption));
          Assert.IsNull(profile.GetValue(IptcTag.Caption));
        }
      }
    }

    [TestMethod]
    public void Test_Values()
    {
      using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
      {
        IptcProfile profile = image.GetIptcProfile();
        TestProfileValues(profile);

        using (IMagickImage emptyImage = new MagickImage(Files.ImageMagickJPG))
        {
          Assert.IsNull(emptyImage.GetIptcProfile());
          emptyImage.AddProfile(profile);

          profile = emptyImage.GetIptcProfile();
          TestProfileValues(profile);
        }
      }
    }
  }
}
