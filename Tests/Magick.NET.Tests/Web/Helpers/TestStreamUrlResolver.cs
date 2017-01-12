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
using ImageMagick;
using ImageMagick.Web;
using System.IO;

namespace Magick.NET.Tests
{
  [ExcludeFromCodeCoverage]
  public sealed class TestStreamUrlResolver : IStreamUrlResolver
  {
    public static bool Result = false;

    private string _FileName;

    public TestStreamUrlResolver()
    {
      _FileName = "foo.jpg";
    }

    public TestStreamUrlResolver(string fileName)
    {
      _FileName = fileName;
    }

    public MagickFormat Format => MagickFormatInfo.Create(_FileName).Format;

    public string ImageId => _FileName;

    public DateTime ModifiedTimeUtc => File.GetLastWriteTime(_FileName);

    public Stream OpenStream() => File.OpenRead(_FileName);

    public bool Resolve(Uri url)
    {
      return Result;
    }
  }
}
