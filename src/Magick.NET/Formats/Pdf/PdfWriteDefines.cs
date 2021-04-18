// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Pdf"/> image is written.
    /// </summary>
    public sealed class PdfWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfWriteDefines"/> class.
        /// </summary>
        public PdfWriteDefines()
          : base(MagickFormat.Pdf)
        {
        }

        /// <summary>
        /// Gets or sets the author of the pdf document (pdf:author).
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets the producer of the pdf document (pdf:producer).
        /// </summary>
        public string? Producer { get; set; }

        /// <summary>
        /// Gets or sets the title of the pdf document (pdf:title).
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (Author?.Length > 0)
                    yield return CreateDefine("author", Author);

                if (Producer?.Length > 0)
                    yield return CreateDefine("producer", Producer);

                if (Title?.Length > 0)
                    yield return CreateDefine("title", Title);
            }
        }
    }
}