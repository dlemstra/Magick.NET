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

using ImageMagick;
using ImageMagick.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
  [TestClass]
  public class HandlerHelperTests
  {
    private MagickFormatInfo JpgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Jpg);
    private MagickFormatInfo SvgFormatInfo => MagickNET.GetFormatInformation(MagickFormat.Svg);

    [TestMethod]
    public void Test_CanCompress()
    {
      string config = @"<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"" enableGzip=""false""/>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      bool canCompress = HandlerHelper.CanCompress(settings, SvgFormatInfo);

      Assert.IsFalse(canCompress);
    }

    [TestMethod]
    public void Test_CanOptimize()
    {
      string config = @"
<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"">
  <optimization enabled=""false""/>
</magick.net.web>";

      MagickWebSettings settings = TestSectionLoader.Load(config);

      bool canCompress = HandlerHelper.CanOptimize(settings, JpgFormatInfo);

      Assert.IsFalse(canCompress);
    }
  }
}

#endif