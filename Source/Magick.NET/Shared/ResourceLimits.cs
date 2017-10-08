// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that can be used to set the limits to the resources that are being used.
    /// </summary>
    public static partial class ResourceLimits
    {
        /// <summary>
        /// Gets or sets the pixel cache limit in bytes. Requests for memory above this limit will fail.
        /// </summary>
        public static ulong Disk
        {
            get { return NativeResourceLimits.Disk; }
            set { NativeResourceLimits.Disk = value; }
        }

        /// <summary>
        /// Gets or sets the maximum height of an image.
        /// </summary>
        public static ulong Height
        {
            get { return NativeResourceLimits.Height; }
            set { NativeResourceLimits.Height = value; }
        }

        /// <summary>
        /// Gets or sets the pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
        /// operations are to/from disk.
        /// </summary>
        public static ulong Memory
        {
            get { return NativeResourceLimits.Memory; }
            set { NativeResourceLimits.Memory = value; }
        }

        /// <summary>
        /// Gets or sets the number of threads used in multithreaded operations.
        /// </summary>
        public static ulong Thread
        {
            get { return NativeResourceLimits.Thread; }
            set { NativeResourceLimits.Thread = value; }
        }

        /// <summary>
        /// Gets or sets the time specified in milliseconds to periodically yield the CPU for.
        /// </summary>
        public static ulong Throttle
        {
            get { return NativeResourceLimits.Throttle; }
            set { NativeResourceLimits.Throttle = value; }
        }

        /// <summary>
        /// Gets or sets the maximum width of an image.
        /// </summary>
        public static ulong Width
        {
            get { return NativeResourceLimits.Width; }
            set { NativeResourceLimits.Width = value; }
        }
    }
}