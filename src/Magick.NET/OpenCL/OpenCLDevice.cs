// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageMagick;

/// <summary>
/// Represents an OpenCL device.
/// </summary>
public sealed partial class OpenCLDevice : IOpenCLDevice
{
    private readonly NativeOpenCLDevice _instance;
    private bool _profileKernels;

    private OpenCLDevice(NativeOpenCLDevice instance)
    {
        _instance = instance;
        _profileKernels = false;
    }

    /// <summary>
    /// Gets the benchmark score of the device.
    /// </summary>
    public double BenchmarkScore
        => _instance.BenchmarkScore_Get();

    /// <summary>
    /// Gets the type of the device.
    /// </summary>
    public OpenCLDeviceType DeviceType
        => _instance.DeviceType_Get();

    /// <summary>
    /// Gets or sets a value indicating whether the device is enabled or disabled.
    /// </summary>
    public bool IsEnabled
    {
        get => _instance.IsEnabled_Get();
        set => _instance.IsEnabled_Set(value);
    }

    /// <summary>
    /// Gets all the kernel profile records for this devices.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{OpenCLKernelProfileRecord}"/>.</returns>
    public IReadOnlyCollection<IOpenCLKernelProfileRecord> KernelProfileRecords
    {
        get
        {
            var records = _instance.GetKernelProfileRecords(out var length);
            var result = new Collection<IOpenCLKernelProfileRecord>();

            if (records == IntPtr.Zero)
                return result;

            for (var i = 0U; i < length; i++)
            {
                var record = NativeOpenCLDevice.GetKernelProfileRecord(records, i);
                result.Add(record);
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the name of the device.
    /// </summary>
    public string Name
        => _instance.Name_Get();

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
        => _instance.Version_Get();
}
