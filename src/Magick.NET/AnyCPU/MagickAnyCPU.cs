// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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