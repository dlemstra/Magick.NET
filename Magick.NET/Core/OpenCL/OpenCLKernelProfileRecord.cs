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
  /// Represents a kernel profile record for an OpenCL device.
  /// </summary>
  public sealed partial class OpenCLKernelProfileRecord
  {
    private OpenCLKernelProfileRecord(NativeOpenCLKernelProfileRecord instance)
    {
      Name = instance.Name;
      Count = instance.Count;
      MaximumDuration = instance.MaximumDuration;
      MinimumDuration = instance.MinimumDuration;
      TotalDuration = instance.TotalDuration;
    }

    internal static OpenCLKernelProfileRecord CreateInstance(IntPtr instance)
    {
      if (instance == IntPtr.Zero)
        return null;

      NativeOpenCLKernelProfileRecord nativeInstance = new NativeOpenCLKernelProfileRecord();
      nativeInstance.Instance = instance;

      return new OpenCLKernelProfileRecord(nativeInstance);
    }

    /// <summary>
    /// The average duration of all executions in microseconds.
    /// </summary>
    public long AverageDuration
    {
      get
      {
        if (Count == 0)
          return 0;

        return TotalDuration / Count;
      }
    }

    /// <summary>
    /// The number of times that this kernel was executed.
    /// </summary>
    public long Count
    {
      get;
      private set;
    }

    /// <summary>
    /// The maximum duration of a single execution in microseconds.
    /// </summary>
    public long MaximumDuration
    {
      get;
      private set;
    }

    /// <summary>
    /// The minimum duration of a single execution in microseconds.
    /// </summary>
    public long MinimumDuration
    {
      get;
      private set;
    }

    /// <summary>
    /// The name of the device.
    /// </summary>
    public string Name
    {
      get;
      private set;
    }

    /// <summary>
    /// The total duration of all executions in microseconds.
    /// </summary>
    public long TotalDuration
    {
      get;
      private set;
    }
  }
}
