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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Specifies which parts will be written when the profile is added to an image.
    /// </summary>
    [Flags]
    public enum ExifParts
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// IfdTags
        /// </summary>
        IfdTags = 1,

        /// <summary>
        /// ExifTags
        /// </summary>
        ExifTags = 4,

        /// <summary>
        /// GPSTags
        /// </summary>
        GPSTags = 8,

        /// <summary>
        /// All
        /// </summary>
        All = IfdTags | ExifTags | GPSTags,
    }
}