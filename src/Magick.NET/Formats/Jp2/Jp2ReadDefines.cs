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

namespace ImageMagick.Formats.Jp2
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Jp2"/> image is read.
    /// </summary>
    public sealed class Jp2ReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Jp2ReadDefines"/> class.
        /// </summary>
        public Jp2ReadDefines()
          : base(MagickFormat.Jp2)
        {
        }

        /// <summary>
        /// Gets or sets the maximum number of quality layers to decode (jp2:quality-layers).
        /// </summary>
        public int? QualityLayers { get; set; }

        /// <summary>
        /// Gets or sets the number of highest resolution levels to be discarded (jp2:reduce-factor).
        /// </summary>
        public int? ReduceFactor { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (QualityLayers.HasValue)
                    yield return CreateDefine("quality-layers", QualityLayers.Value);

                if (ReduceFactor.HasValue)
                    yield return CreateDefine("reduce-factor", ReduceFactor.Value);
            }
        }
    }
}