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

using System.Xml.XPath;

namespace ImageMagick
{
  /// <summary>
  /// A value of the exif profile.
  /// </summary>
  public sealed class ClipPath
  {
    internal ClipPath(string name, IXPathNavigable path)
    {
      Name = name;
      Path = path;
    }

    /// <summary>
    /// The name of the clipping path.
    /// </summary>
    public string Name
    {
      get;
      private set;
    }

    /// <summary>
    /// The path of the clipping path.
    /// </summary>
    public IXPathNavigable Path
    {
      get;
      private set;
    }
  }
}
