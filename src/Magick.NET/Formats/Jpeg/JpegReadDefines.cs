// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jpeg"/> image is read.
    /// </summary>
    public sealed class JpegReadDefines : IReadDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JpegReadDefines"/> class.
        /// </summary>
        public JpegReadDefines()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether block smoothing is enabled or disabled (jpeg:block-smoothing).
        /// </summary>
        public bool? BlockSmoothing { get; set; }

        /// <summary>
        /// Gets or sets the desired number of colors (jpeg:colors).
        /// </summary>
        public int? Colors { get; set; }

        /// <summary>
        /// Gets or sets the dtc method that will be used (jpeg:dct-method).
        /// </summary>
        public JpegDctMethod? DctMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fancy upsampling is enabled or disabled (jpeg:fancy-upsampling).
        /// </summary>
        public bool? FancyUpsampling { get; set; }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        public MagickFormat Format
            => MagickFormat.Jpeg;

        /// <summary>
        /// Gets or sets the size the scale the image to (jpeg:size). The output image won't be exactly
        /// the specified size. More information can be found here: http://jpegclub.org/djpeg/.
        /// </summary>
        public IMagickGeometry? Size { get; set; }

        /// <summary>
        /// Gets or sets the profile(s) that should be skipped when the image is read (profile:skip).
        /// </summary>
        public JpegProfileTypes? SkipProfiles { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public IEnumerable<IDefine> Defines
        {
            get
            {
                if (BlockSmoothing.HasValue)
                    yield return new MagickDefine(Format, "block-smoothing", BlockSmoothing.Value);

                if (Colors.HasValue)
                    yield return new MagickDefine(Format, "colors", Colors.Value);

                if (DctMethod.HasValue)
                    yield return new MagickDefine(Format, "dct-method", DctMethod.Value);

                if (FancyUpsampling.HasValue)
                    yield return new MagickDefine(Format, "fancy-upsampling", FancyUpsampling.Value);

                if (Size is not null)
                    yield return new MagickDefine(Format, "size", Size);

                if (SkipProfiles.HasValue)
                {
                    var value = EnumHelper.ConvertFlags(SkipProfiles.Value);

                    if (!string.IsNullOrEmpty(value))
                        yield return new MagickDefine("profile:skip", value);
                }
            }
        }
    }
}
