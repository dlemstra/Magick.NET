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
using ImageMagick.Defines;

namespace ImageMagick
{
  ///<summary>
  /// Class for defines that are used when a tiff image is read.
  ///</summary>
  public sealed class TiffReadDefines : DefineCreator, IReadDefines
  {
    ///<summary>
    /// Initializes a new instance of the TiffReadDefines class.
    ///</summary>
    public TiffReadDefines()
      : base(MagickFormat.Tiff)
    {
    }

    ///<summary>
    /// Specifies if the exif profile should be ignored (tiff:exif-properties).
    ///</summary>
    public bool? IgnoreExifPoperties
    {
      get;
      set;
    }

    ///<summary>
    /// The defines that should be set as an define on an image
    ///</summary>
    public override IEnumerable<IDefine> Defines
    {
      get
      {
        if (IgnoreExifPoperties.Equals(true))
          yield return CreateDefine("exif-properties", "false");
      }
    }
  }
}