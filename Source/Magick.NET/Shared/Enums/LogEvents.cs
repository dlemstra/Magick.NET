//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;

namespace ImageMagick
{
    /// <summary>
    /// Specifies log events.
    /// </summary>
    [Flags]
    public enum LogEvents
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0x000000,

        /// <summary>
        /// Accelerate
        /// </summary>
        Accelerate = 0x00001,

        /// <summary>
        /// Annotate
        /// </summary>
        Annotate = 0x00002,

        /// <summary>
        /// Blob
        /// </summary>
        Blob = 0x00004,

        /// <summary>
        /// Cache
        /// </summary>
        Cache = 0x00008,

        /// <summary>
        /// Coder
        /// </summary>
        Coder = 0x00010,

        /// <summary>
        /// Configure
        /// </summary>
        Configure = 0x00020,

        /// <summary>
        /// Deprecate
        /// </summary>
        Deprecate = 0x00040,

        /// <summary>
        /// Draw
        /// </summary>
        Draw = 0x00080,

        /// <summary>
        /// Exception
        /// </summary>
        Exception = 0x00100,

        /// <summary>
        /// Image
        /// </summary>
        Image = 0x00200,

        /// <summary>
        /// Locale
        /// </summary>
        Locale = 0x00400,

        /// <summary>
        /// Module
        /// </summary>
        Module = 0x00800,

        /// <summary>
        /// Pixel
        /// </summary>
        Pixel = 0x01000,

        /// <summary>
        /// Policy
        /// </summary>
        Policy = 0x02000,

        /// <summary>
        /// Resource
        /// </summary>
        Resource = 0x04000,

        /// <summary>
        /// Resource
        /// </summary>
        Trace = 0x08000,

        /// <summary>
        /// Transform
        /// </summary>
        Transform = 0x10000,

        /// <summary>
        /// User
        /// </summary>
        User = 0x20000,

        /// <summary>
        /// Wand
        /// </summary>
        Wand = 0x40000,

        /// <summary>
        /// All log events except Trace.
        /// </summary>
        All = 0x7fff7fff
    }
}