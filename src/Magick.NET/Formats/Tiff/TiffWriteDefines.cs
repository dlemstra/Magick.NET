// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
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
        /// Gets or sets the prediction scheme with LZW (tiff:predictor).
        /// </summary>
        public int? Predictor { get; set; }

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
        public IMagickGeometry? TileGeometry { get; set; }

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

                if (Predictor.HasValue)
                    yield return CreateDefine("predictor", Predictor.Value);

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