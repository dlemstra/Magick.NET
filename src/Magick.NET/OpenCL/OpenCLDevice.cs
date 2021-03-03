// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
        private readonly NativeOpenCLDevice _instance;
        private bool _profileKernels;

        private OpenCLDevice(IntPtr instance)
        {
            _instance = new NativeOpenCLDevice();
            _instance.Instance = instance;
            _profileKernels = false;
        }

        /// <summary>
        /// Gets the benchmark score of the device.
        /// </summary>
        public double BenchmarkScore
            => _instance.BenchmarkScore;

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        public OpenCLDeviceType DeviceType
            => _instance.DeviceType;

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string Name
            => _instance.Name;

        /// <summary>
        /// Gets or sets a value indicating whether the device is enabled or disabled.
        /// </summary>
        public bool IsEnabled
        {
            get => _instance.IsEnabled;
            set => _instance.IsEnabled = value;
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
                var records = _instance.GetKernelProfileRecords(out length);
                var result = new Collection<OpenCLKernelProfileRecord>();

                if (records == IntPtr.Zero)
                    return result;

                for (int i = 0; i < (int)length; i++)
                {
                    var instance = NativeOpenCLDevice.GetKernelProfileRecord(records, i);
                    var record = OpenCLKernelProfileRecord.CreateInstance(instance);
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
            get => _profileKernels;
            set
            {
                _instance.SetProfileKernels(value);
                _profileKernels = value;
            }
        }

        /// <summary>
        /// Gets the OpenCL version supported by the device.
        /// </summary>
        public string Version
            => _instance.Version;

        internal static OpenCLDevice? CreateInstance(IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            return new OpenCLDevice(instance);
        }
    }
}
