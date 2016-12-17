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
  public partial class MagickNETTests
  {
    [TestMethod]
    public void Test_Version()
    {
#if ANYCPU
      StringAssert.Contains(MagickNET.Version, "AnyCPU");
#elif WIN64
      StringAssert.Contains(MagickNET.Version, "x64");
#else
      StringAssert.Contains(MagickNET.Version, "x86");
#endif

#if NET20
      StringAssert.Contains(MagickNET.Version, "net20");
#else
      StringAssert.Contains(MagickNET.Version, "net40-client");
#endif
    }
  }
}
