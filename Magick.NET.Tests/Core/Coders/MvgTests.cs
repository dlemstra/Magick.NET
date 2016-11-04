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
  public class MvgTests
  {
    private const string _Category = "MvgTests";

    [TestMethod, TestCategory(_Category)]
    public void Test_Disabled()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        using (StreamWriter writer = new StreamWriter(memStream))
        {
          writer.Write(@"push graphic-context
              viewbox 0 0 640 480
              image over 0,0 0,0 ""label:Magick.NET""
              pop graphic-context");

          writer.Flush();

          memStream.Position = 0;

          using (MagickImage image = new MagickImage())
          {
            ExceptionAssert.Throws<MagickMissingDelegateErrorException>(delegate ()
            {
              image.Read(memStream);
            });

            ExceptionAssert.Throws<MagickPolicyErrorException>(delegate ()
            {
              MagickReadSettings settings = new MagickReadSettings()
              {
                Format = MagickFormat.Mvg
              };

              image.Read(memStream, settings);
            });
          }
        }
      }
    }
  }
}