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
    /// Class for defines that are used when a psd image is read.
    /// </summary>
    public sealed class PsdReadDefines : ReadDefinesCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PsdReadDefines"/> class.
        /// </summary>
        public PsdReadDefines()
          : base(MagickFormat.Psd)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether alpha unblending should be enabled or disabled (psd:alpha-unblend).
        /// </summary>
        public bool? AlphaUnblend
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
                if (AlphaUnblend.Equals(false))
                    yield return CreateDefine("alpha-unblend", false);
            }
        }
    }
}