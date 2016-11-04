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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class ExifDescriptionAttributeTests
  {
    [TestMethod]
    public void Test_ExifTag()
    {
      var exifProfile = new ExifProfile();

      exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)1);
      ExifValue value = exifProfile.GetValue(ExifTag.ResolutionUnit);
      Assert.AreEqual("None", value.ToString());

      exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)2);
      value = exifProfile.GetValue(ExifTag.ResolutionUnit);
      Assert.AreEqual("Inches", value.ToString());

      exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)3);
      value = exifProfile.GetValue(ExifTag.ResolutionUnit);
      Assert.AreEqual("Centimeter", value.ToString());

      exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)4);
      value = exifProfile.GetValue(ExifTag.ResolutionUnit);
      Assert.AreEqual("4", value.ToString());

      exifProfile.SetValue(ExifTag.ImageWidth, 123);
      value = exifProfile.GetValue(ExifTag.ImageWidth);
      Assert.AreEqual("123", value.ToString());
    }
  }
}
