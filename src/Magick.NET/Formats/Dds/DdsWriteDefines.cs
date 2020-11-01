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

namespace ImageMagick.Formats.Dds
{
    /// <summary>
    /// Class for defines that are used when a <see cref="MagickFormat.Dds"/> image is written.
    /// </summary>
    public sealed class DdsWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DdsWriteDefines"/> class.
        /// </summary>
        public DdsWriteDefines()
          : base(MagickFormat.Dds)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether cluser fit is enabled or disabled (dds:cluster-fit).
        /// </summary>
        public bool? ClusterFit { get; set; }

        /// <summary>
        /// Gets or sets the compression that will be used (dds:compression).
        /// </summary>
        public DdsCompression? Compression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mipmaps should be resized faster but with a lower quality (dds:fast-mipmaps).
        /// </summary>
        public bool? FastMipmaps { get; set; }

        /// <summary>
        /// Gets or sets the the number of mipmaps, zero will disable writing mipmaps (dds:mipmaps).
        /// </summary>
        public int? Mipmaps { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mipmaps should be created from the images in the collection (dds:mipmaps=fromlist).
        /// </summary>
        public bool? MipmapsFromCollection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only the raw pixels should be written (dds:raw).
        /// </summary>
        public bool? Raw { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether weight by alpha is enabled or disabled when cluster fit is used (dds:weight-by-alpha).
        /// </summary>
        public bool? WeightByAlpha { get; set; }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (ClusterFit.HasValue)
                    yield return CreateDefine("cluster-fit", ClusterFit.Value);

                if (Compression.HasValue)
                    yield return CreateDefine("compression", Compression.Value);

                if (FastMipmaps.HasValue)
                    yield return CreateDefine("fast-mipmaps", FastMipmaps.Value);

                if (MipmapsFromCollection == true)
                    yield return CreateDefine("mipmaps", "fromlist");
                else if (Mipmaps.HasValue)
                    yield return CreateDefine("mipmaps", Mipmaps.Value);

                if (Raw.HasValue)
                    yield return CreateDefine("raw", Raw.Value);

                if (WeightByAlpha.HasValue)
                    yield return CreateDefine("weight-by-alpha", WeightByAlpha.Value);
            }
        }
    }
}