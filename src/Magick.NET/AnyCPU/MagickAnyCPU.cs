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

#if PLATFORM_AnyCPU
using System;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to initialize the AnyCPU version of Magick.NET.
    /// </summary>
    public static class MagickAnyCPU
    {
        private static string _cacheDirectory = Path.GetTempPath();

        /// <summary>
        /// Gets or sets the directory that will be used by Magick.NET to store the embedded assemblies.
        /// </summary>
        public static string CacheDirectory
        {
            get => _cacheDirectory;
            set
            {
                if (!Directory.Exists(value))
                    throw new InvalidOperationException("The specified directory does not exist.");
                _cacheDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the security permissions of the embeded library
        /// should be changed when it is written to disk. Only set this to true when multiple
        /// application pools with different idententies need to execute the same library.
        /// </summary>
        public static bool HasSharedCacheDirectory { get; set; }

        internal static bool UsesDefaultCacheDirectory => _cacheDirectory == Path.GetTempPath();
    }
}
#endif