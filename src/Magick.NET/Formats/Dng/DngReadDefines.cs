// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Dng"/> image is read.
    /// </summary>
    public sealed class DngReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DngReadDefines"/> class.
        /// </summary>
        public DngReadDefines()
          : base(MagickFormat.Dng)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating wether auto whitebalance should be used (dng:use_auto_wb).
        /// </summary>
        public bool? DisableAutoBrightness { get; set; }

        /// <summary>
        /// Gets or sets the output color.
        /// </summary>
        public DngOutputColor? OutputColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether auto whitebalance should be used (dng:use_auto_wb).
        /// </summary>
        public bool? UseAutoWhitebalance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether the whitebalance of the camera should be used (dng:use_camera_wb).
        /// </summary>
        public bool? UseCameraWhitebalance { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (DisableAutoBrightness.HasValue)
                    yield return CreateDefine("no_auto_bright", DisableAutoBrightness.Value);

                if (OutputColor.HasValue)
                    yield return CreateDefine("output_color", (int)OutputColor.Value);

                if (UseCameraWhitebalance.HasValue)
                    yield return CreateDefine("use_camera_wb", UseCameraWhitebalance.Value);

                if (UseAutoWhitebalance.HasValue)
                    yield return CreateDefine("use_auto_wb", UseAutoWhitebalance.Value);
            }
        }
    }
}