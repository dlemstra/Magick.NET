//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
  /// Class for defines that are used when a dds image is written.
  ///</summary>
  public sealed class DdsWriteDefines : DefineCreator
  {
    ///<summary>
    /// Initializes a new instance of the DdsWriteDefines class.
    ///</summary>
    public DdsWriteDefines()
      : base(MagickFormat.Dds)
    {
    }

    ///<summary>
    /// Enables or disables cluser fit (dds:cluster-fit).
    ///</summary>
    public bool? ClusterFit
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the compression that will be used (dds:compression).
    ///</summary>
    public DdsCompression? Compression
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the number of mipmaps, zero will disable writing mipmaps (dds:mipmaps).
    ///</summary>
    public int? Mipmaps
    {
      get;
      set;
    }

    ///<summary>
    /// Enables or disables weight by alpha when cluster fit is used (dds:weight-by-alpha).
    ///</summary>
    public bool? WeightByAlpha
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
        if (ClusterFit.HasValue)
          yield return CreateDefine("cluster-fit", ClusterFit.Value);

        if (Compression.HasValue)
          yield return CreateDefine("compression", Compression.Value);

        if (Mipmaps.HasValue)
          yield return CreateDefine("mipmaps", Mipmaps.Value);

        if (WeightByAlpha.HasValue)
          yield return CreateDefine("weight-by-alpha", WeightByAlpha.Value);
      }
    }
  }
}