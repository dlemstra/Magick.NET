// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class for defines that are used when a <see cref="MagickFormat.Tiff"/> image is read.
    /// </summary>
    public sealed class TiffReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TiffReadDefines"/> class.
        /// </summary>
        public TiffReadDefines()
          : base(MagickFormat.Tiff)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the exif profile should be ignored (tiff:exif-properties).
        /// </summary>
        public bool? IgnoreExifPoperties { get; set; }

        /// <summary>
        /// Gets or sets the tiff tags that should be ignored (tiff:ignore-tags).
        /// </summary>
        public IEnumerable<string> IgnoreTags { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (IgnoreExifPoperties.Equals(true))
                    yield return CreateDefine("exif-properties", false);

                MagickDefine ignoreTags = CreateDefine("ignore-tags", IgnoreTags);
                if (ignoreTags != null)
                    yield return ignoreTags;
            }
        }
    }
}