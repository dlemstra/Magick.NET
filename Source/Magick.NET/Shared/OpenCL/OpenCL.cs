// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

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
                    _isEnabled = NativeOpenCL.SetEnabled(true);

                return _isEnabled.Value;
            }
            set
            {
                _isEnabled = NativeOpenCL.SetEnabled(value);
            }
        }

        /// <summary>
        /// Gets all the OpenCL devices.
        /// </summary>
        /// <returns>A <see cref="OpenCLDevice"/> iteration.</returns>
        public static IEnumerable<OpenCLDevice> Devices
        {
            get
            {
                UIntPtr length;
                IntPtr devices = NativeOpenCL.GetDevices(out length);
                Collection<OpenCLDevice> result = new Collection<OpenCLDevice>();

                if (devices == IntPtr.Zero)
                    return result;

                for (int i = 0; i < (int)length; i++)
                {
                    IntPtr instance = NativeOpenCL.GetDevice(devices, i);
                    OpenCLDevice device = OpenCLDevice.CreateInstance(instance);
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
        {
            Environment.SetEnv("MAGICK_OPENCL_CACHE_DIR", FileHelper.GetFullPath(path));
        }
    }
}
