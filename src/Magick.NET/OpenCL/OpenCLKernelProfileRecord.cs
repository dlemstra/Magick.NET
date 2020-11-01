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

        /// <summary>
        /// Gets the average duration of all executions in microseconds.
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
        /// Gets the number of times that this kernel was executed.
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// Gets the maximum duration of a single execution in microseconds.
        /// </summary>
        public long MaximumDuration { get; }

        /// <summary>
        /// Gets the minimum duration of a single execution in microseconds.
        /// </summary>
        public long MinimumDuration { get; }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the total duration of all executions in microseconds.
        /// </summary>
        public long TotalDuration { get; }

        internal static OpenCLKernelProfileRecord CreateInstance(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            NativeOpenCLKernelProfileRecord nativeInstance = new NativeOpenCLKernelProfileRecord();
            nativeInstance.Instance = instance;

            return new OpenCLKernelProfileRecord(nativeInstance);
        }
    }
}
