// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Tiff"/> image is written.
    /// </summary>
    public sealed class TiffWriteDefines : IWriteDefines
    {
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
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Tiff;

        /// <summary>
        /// Gets or sets the jpeg tables mode (tiff:jpeg-tables-mode).
        /// </summary>
        public TiffJpegTablesMode? JpegTablesMode { get; set; }

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
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (Alpha.HasValue)
                    yield return new MagickDefine(Format, "alpha", Alpha.Value);

                if (Endian.HasValue && Endian.Value != ImageMagick.Endian.Undefined)
                    yield return new MagickDefine(Format, "endian", Endian.Value);

                if (FillOrder.HasValue && FillOrder.Value != ImageMagick.Endian.Undefined)
                    yield return new MagickDefine(Format, "fill-order", FillOrder.Value);

                if (JpegTablesMode.HasValue)
                    yield return new MagickDefine(Format, "jpeg-tables-mode", (int)JpegTablesMode.Value);

                if (Predictor.HasValue)
                    yield return new MagickDefine(Format, "predictor", Predictor.Value);

                if (PreserveCompression)
                    yield return new MagickDefine(Format, "preserve-compression", PreserveCompression);

                if (RowsPerStrip.HasValue)
                    yield return new MagickDefine(Format, "rows-per-strip", RowsPerStrip.Value);

                if (TileGeometry is not null)
                    yield return new MagickDefine(Format, "tile-geometry", TileGeometry);

                if (WriteLayers)
                    yield return new MagickDefine(Format, "write-layers", WriteLayers);
            }
        }
    }
}
