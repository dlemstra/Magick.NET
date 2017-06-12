//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
    /// Represents an OpenCL device.
    /// </summary>
    public sealed partial class OpenCLDevice
    {
        private NativeOpenCLDevice _Instance;
        private bool _ProfileKernels;

        private OpenCLDevice(IntPtr instance)
        {
            _Instance = new NativeOpenCLDevice();
            _Instance.Instance = instance;
            _ProfileKernels = false;
        }

        internal static OpenCLDevice CreateInstance(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            return new OpenCLDevice(instance);
        }

        /// <summary>
        /// Gets the benchmark score of the device.
        /// </summary>
        public double BenchmarkScore
        {
            get
            {
                return _Instance.BenchmarkScore;
            }
        }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        public OpenCLDeviceType DeviceType
        {
            get
            {
                return _Instance.DeviceType;
            }
        }

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string Name
        {
            get
            {
                return _Instance.Name;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the device is enabled or disabled.
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
        /// Gets all the kernel profile records for this devices.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{OpenCLKernelProfileRecord}"/>.</returns>
        public IEnumerable<OpenCLKernelProfileRecord> KernelProfileRecords
        {
            get
            {
                UIntPtr length;
                IntPtr records = _Instance.GetKernelProfileRecords(out length);
                Collection<OpenCLKernelProfileRecord> result = new Collection<OpenCLKernelProfileRecord>();

                if (records == IntPtr.Zero)
                    return result;

                for (int i = 0; i < (int)length; i++)
                {
                    IntPtr instance = NativeOpenCLDevice.GetKernelProfileRecord(records, i);
                    OpenCLKernelProfileRecord record = OpenCLKernelProfileRecord.CreateInstance(instance);
                    if (record != null)
                        result.Add(record);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether kernel profiling is enabled.
        /// This can be used to get information about the OpenCL performance.
        /// </summary>
        public bool ProfileKernels
        {
            get
            {
                return _ProfileKernels;
            }
            set
            {
                _Instance.SetProfileKernels(value);
                _ProfileKernels = value;
            }
        }

        /// <summary>
        /// Gets the OpenCL version supported by the device.
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
