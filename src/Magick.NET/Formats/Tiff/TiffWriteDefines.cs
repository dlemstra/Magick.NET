// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats.Tiff
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Tiff"/> image is written.
    /// </summary>
    public sealed class TiffWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TiffWriteDefines"/> class.
        /// </summary>
        public TiffWriteDefines()
          : base(MagickFormat.Tiff)
        {
        }

        /// <summary>
        /// Gets or sets the tiff alpha (tiff:alpha).
        /// </summary>
        public TiffAlpha? Alpha { get; set; }

        /// <summary>
        /// Gets or sets the endianness of the tiff file (tiff:endian).
        /// </summary>
        public Endian? Endian { get; set; }

        /// <summary>
        /// Gets or sets the endianness of the tiff file (tiff:fill-order).
        /// </summary>
        public Endian? FillOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the compression of the image should be preserved (tiff:preserve-compression).
        /// </summary>
        public bool PreserveCompression { get; set; }

        /// <summary>
        /// Gets or sets the rows per strip (tiff:rows-per-strip).
        /// </summary>
        public int? RowsPerStrip { get; set; }

        /// <summary>
        /// Gets or sets the tile geometry (tiff:tile-geometry).
        /// </summary>
        public IMagickGeometry TileGeometry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether photoshop layers should be written (tiff:write-layers).
        /// </summary>
        public bool WriteLayers { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
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

                if (PreserveCompression)
                    yield return CreateDefine("preserve-compression", PreserveCompression);

                if (RowsPerStrip.HasValue)
                    yield return CreateDefine("rows-per-strip", RowsPerStrip.Value);

                if (TileGeometry != null)
                    yield return CreateDefine("tile-geometry", TileGeometry);

                if (WriteLayers)
                    yield return CreateDefine("write-layers", WriteLayers);
            }
        }
    }
}