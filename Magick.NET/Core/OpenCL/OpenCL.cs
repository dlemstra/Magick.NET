//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageMagick
{
  /// <summary>
  /// Class that can be used to initialize OpenCL.
  /// </summary>
  public static partial class OpenCL
  {
    private static bool? _IsEnabled;

    ///<summary>
    /// Sets the directory that will be used by ImageMagick to store OpenCL cache files.
    ///</summary>
    ///<param name="path">The path of the OpenCL cache directory.</param>
    public static void SetCacheDirectory(string path)
    {
      Environment.SetEnv("MAGICK_OPENCL_CACHE_DIR", FileHelper.GetFullPath(path));
    }

    /// <summary>
    /// Returns all the OpenCL devices.
    /// </summary>
    /// <returns></returns>
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

    ///<summary>
    /// Gets or sets wether OpenCL is enabled.
    ///</summary>
    public static bool IsEnabled
    {
      get
      {
        if (!_IsEnabled.HasValue)
          _IsEnabled = NativeOpenCL.SetEnabled(true);

        return _IsEnabled.Value;
      }
      set
      {
        _IsEnabled = NativeOpenCL.SetEnabled(value);
      }
    }
  }
}
