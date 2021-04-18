// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to initialize OpenCL.
    /// </summary>
    public static partial class OpenCL
    {
        private static bool? _isEnabled;

        /// <summary>
        /// Gets or sets a value indicating whether OpenCL is enabled.
        /// </summary>
        public static bool IsEnabled
        {
            get
            {
                if (!_isEnabled.HasValue)
                    _isEnabled = NativeOpenCL.GetEnabled();

                return _isEnabled.Value;
            }

            set => _isEnabled = NativeOpenCL.SetEnabled(value);
        }

        /// <summary>
        /// Gets all the OpenCL devices.
        /// </summary>
        /// <returns>A <see cref="OpenCLDevice"/> iteration.</returns>
        public static IEnumerable<OpenCLDevice> Devices
        {
            get
            {
                var devices = NativeOpenCL.GetDevices(out var length);
                var result = new Collection<OpenCLDevice>();

                if (devices == IntPtr.Zero)
                    return result;

                for (int i = 0; i < (int)length; i++)
                {
                    var instance = NativeOpenCL.GetDevice(devices, i);
                    var device = OpenCLDevice.CreateInstance(instance);
                    if (device != null)
                        result.Add(device);
                }

                return result;
            }
        }

        /// <summary>
        /// Sets the directory that will be used by ImageMagick to store OpenCL cache files.
        /// </summary>
        /// <param name="path">The path of the OpenCL cache directory.</param>
        public static void SetCacheDirectory(string path)
            => Environment.SetEnv("MAGICK_OPENCL_CACHE_DIR", FileHelper.GetFullPath(path));
    }
}
