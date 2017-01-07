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
using ImageMagick.Web;
using ImageMagick.Web.Handlers;
using System.Web;

namespace Magick.NET.Tests
{
  [ExcludeFromCodeCoverage]
  public sealed class TestMagickHandler : MagickHandler
  {
    public TestMagickHandler(MagickWebSettings settings, IUrlResolver urlResolver, MagickFormatInfo formatInfo)
      : base(settings, urlResolver, formatInfo)
    {
      FileName = Files.MagickNETIconPNG;
    }

    protected override void WriteFile(HttpContext context)
    {
      WriteFile(context, FileName);
    }

    public string FileName { get; set; }
  }
}