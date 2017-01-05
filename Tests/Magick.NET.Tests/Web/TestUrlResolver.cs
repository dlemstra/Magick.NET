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
using System.Xml.XPath;
using ImageMagick;
using ImageMagick.Web;

namespace Magick.NET.Tests
{
  public sealed class TestUrlResolver : IUrlResolver
  {
    public string FileName
    {
      get;
    }

    public MagickFormat Format
    {
      get;
    }

    public IXPathNavigable Script
    {
      get;
    }

    public bool Resolve(Uri url)
    {
      return false;
    }
  }
}
