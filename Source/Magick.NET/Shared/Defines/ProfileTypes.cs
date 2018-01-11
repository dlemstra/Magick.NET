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

namespace ImageMagick.Defines
{
    /// <summary>
    /// Specifies profile types
    /// </summary>
    [Flags]
    public enum ProfileTypes
    {
        /// <summary>
        /// App profile
        /// </summary>
        App = 1,

        /// <summary>
        /// 8bim profile
        /// </summary>
        EightBim = 2,

        /// <summary>
        /// Exif profile
        /// </summary>
        Exif = 4,

        /// <summary>
        /// Icc profile
        /// </summary>
        Icc = 8,

        /// <summary>
        /// Iptc profile
        /// </summary>
        Iptc = 16,

        /// <summary>
        /// Iptc profile
        /// </summary>
        Xmp = 32,
    }
}