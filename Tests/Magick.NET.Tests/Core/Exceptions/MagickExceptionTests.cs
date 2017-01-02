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
using System.Linq;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickExceptionTests
  {
    [TestMethod]
    public void Test_IgnoreTags()
    {
      using (MagickImage image = new MagickImage())
      {
        var exception = ExceptionAssert.Throws<MagickCoderErrorException>(() =>
        {
          image.Read(Files.Coders.IgnoreTagTIF);
        });

        var relatedExceptions = exception.RelatedExceptions.ToArray();
        Assert.AreEqual(1, relatedExceptions.Length);

        var warning = relatedExceptions[0] as MagickCoderWarningException;
        Assert.IsNotNull(warning);
        Assert.AreEqual(0, warning.RelatedExceptions.Count());
      }
    }
  }
}
