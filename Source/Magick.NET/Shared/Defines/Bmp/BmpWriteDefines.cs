﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// Class for defines that are used when a bmp image is written.
    /// </summary>
    public sealed class BmpWriteDefines : WriteDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BmpWriteDefines"/> class.
        /// </summary>
        public BmpWriteDefines()
          : base(MagickFormat.Bmp)
        {
        }

        /// <summary>
        /// Gets or sets the subtype that will be used (bmp:subtype).
        /// </summary>
        public BmpSubtype? Subtype
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public override IEnumerable<IDefine> Defines
        {
            get
            {
                if (Subtype.HasValue)
                    yield return CreateDefine("subtype", Subtype.Value);
            }
        }
    }
}