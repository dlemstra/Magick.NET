// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Pdf"/> image is read.
    /// </summary>
    public sealed class PdfReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfReadDefines"/> class.
        /// </summary>
        public PdfReadDefines()
          : base(MagickFormat.Pdf)
        {
        }

        /// <summary>
        /// Gets or sets the size where the image should be scaled to fit the page (pdf:fit-page).
        /// </summary>
        public IMagickGeometry? FitPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all images are forced to be interpolated at full device resolution.
        /// </summary>
        public bool? Interpolate { get; set; }

        /// <summary>
        /// Gets or sets the password that should be used to open the pdf (authenticate).
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use of the cropbox should be forced (pdf:use-trimbox).
        /// </summary>
        public bool? UseCropBox { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use of the trimbox should be forced (pdf:use-trimbox).
        /// </summary>
        public bool? UseTrimBox { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (FitPage != null)
                    yield return CreateDefine("fit-page", FitPage);

                if (Interpolate == true)
                    yield return CreateDefine("interpolate", Interpolate.Value);

                if (Password != null)
                    yield return new MagickDefine("authenticate", Password);

                if (UseCropBox.HasValue)
                    yield return CreateDefine("use-cropbox", UseCropBox.Value);

                if (UseTrimBox.HasValue)
                    yield return CreateDefine("use-trimbox", UseTrimBox.Value);
            }
        }
    }
}