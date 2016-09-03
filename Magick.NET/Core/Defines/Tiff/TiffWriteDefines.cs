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
  /// Class for defines that are used when a tiff image is written.
  ///</summary>
  public sealed class TiffWriteDefines : DefineCreator, IWriteDefines
  {
    ///<summary>
    /// Initializes a new instance of the <see cref="TiffWriteDefines"/> class.
    ///</summary>
    public TiffWriteDefines()
      : base(MagickFormat.Tiff)
    {
    }

    MagickFormat IWriteDefines.Format
    {
      get
      {
        return Format;
      }
    }

    ///<summary>
    /// Specifies the tiff alpha (tiff:alpha).
    ///</summary>
    public TiffAlpha? Alpha
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the endianness of the tiff file (tiff:endian).
    ///</summary>
    public Endian? Endian
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the endianness of the tiff file (tiff:fill-order).
    ///</summary>
    public Endian? FillOrder
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the rows per strip (tiff:rows-per-strip).
    ///</summary>
    public int? RowsPerStrip
    {
      get;
      set;
    }

    ///<summary>
    /// Specifies the tile geometry (tiff:tile-geometry).
    ///</summary>
    public MagickGeometry TileGeometry
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
        if (Alpha.HasValue)
          yield return CreateDefine("alpha", Alpha.Value);

        if (Endian.HasValue && Endian.Value != ImageMagick.Endian.Undefined)
          yield return CreateDefine("endian", Endian.Value);

        if (FillOrder.HasValue && FillOrder.Value != ImageMagick.Endian.Undefined)
          yield return CreateDefine("fill-order", FillOrder.Value);

        if (RowsPerStrip.HasValue)
          yield return CreateDefine("rows-per-strip", RowsPerStrip.Value);

        if (TileGeometry != null)
          yield return CreateDefine("tile-geometry", TileGeometry);
      }
    }
  }
}