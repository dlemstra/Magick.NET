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

namespace ImageMagick
{
  /// <summary>
  /// Represents an OpenCL device.
  /// </summary>
  public sealed partial class OpenCLDevice
  {
    private NativeOpenCLDevice _Instance;

    private OpenCLDevice(IntPtr instance)
    {
      _Instance = new NativeOpenCLDevice();
      _Instance.Instance = instance;
    }

    internal static OpenCLDevice CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;

      return new OpenCLDevice(instance);
    }

    /// <summary>
    /// The name of the device.
    /// </summary>
    public string Name
    {
      get
      {
        return _Instance.Name;
      }
    }

    /// <summary>
    /// Specifies if the device is enabled or disabled.
    /// </summary>
    public bool IsEnabled
    {
      get
      {
        return _Instance.IsEnabled;
      }
      set
      {
        _Instance.IsEnabled = value;
      }
    }

    /// <summary>
    /// The type of the device.
    /// </summary>
    public OpenCLDeviceType DeviceType
    {
      get
      {
        return _Instance.Type;
      }
    }

    /// <summary>
    /// The OpenCL version supported by the device.
    /// </summary>
    public string Version
    {
      get
      {
        return _Instance.Version;
      }
    }
  }
}
