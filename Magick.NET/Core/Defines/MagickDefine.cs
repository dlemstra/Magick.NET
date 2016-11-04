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

namespace ImageMagick
{
  /// <summary>
  /// Class that implements IDefine
  /// </summary>
  public sealed class MagickDefine : IDefine
  {
    /// <summary>
    /// Initializes a new instance of the MagickDefine class.
    /// </summary>
    public MagickDefine(string name, string value)
    {
      Format = MagickFormat.Unknown;
      Name = name;
      Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the MagickDefine class.
    /// </summary>
    public MagickDefine(MagickFormat format, string name, string value)
    {
      Format = format;
      Name = name;
      Value = value;
    }

    /// <summary>
    /// The format to set the define for.
    /// </summary>
    public MagickFormat Format
    {
      get;
      private set;
    }

    /// <summary>
    /// The name of the define.
    /// </summary>
    public string Name
    {
      get;
      private set;
    }

    /// <summary>
    /// The value of the define.
    /// </summary>
    public string Value
    {
      get;
      private set;
    }
  }
}