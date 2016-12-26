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

using System.Collections.Generic;
using ImageMagick;
using ImageMagick.Defines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class DefinesCreatorTests
  {
    private class TestDefine : DefinesCreator
    {
      public TestDefine()
        : base(MagickFormat.A)
      {
      }

      public override IEnumerable<IDefine> Defines
      {
        get
        {
          yield return CreateDefine("null", (MagickGeometry)null);
        }
      }
    }

    [TestMethod]
    public void Test_Null()
    {
      using (MagickImage image = new MagickImage())
      {
        image.Settings.SetDefines(new TestDefine());

        Assert.AreEqual(null, image.Settings.GetDefine(MagickFormat.A, "null"));
      }
    }
  }
}
