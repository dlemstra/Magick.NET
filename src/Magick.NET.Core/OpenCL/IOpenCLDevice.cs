// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that represents an OpenCL device.
/// </summary>
public interface IOpenCLDevice
{
    /// <summary>
    /// Gets the benchmark score of the device.
    /// </summary>
    double BenchmarkScore { get; }

    /// <summary>
    /// Gets the type of the device.
    /// </summary>
    OpenCLDeviceType DeviceType { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the device is enabled or disabled.
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// Gets all the kernel profile records for this devices.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{OpenCLKernelProfileRecord}"/>.</returns>
    IReadOnlyList<IOpenCLKernelProfileRecord> KernelProfileRecords { get; }

    /// <summary>
    /// Gets the name of the device.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether kernel profiling is enabled.
    /// This can be used to get information about the OpenCL performance.
    /// </summary>
    bool ProfileKernels { get; set; }

    /// <summary>
    /// Gets the OpenCL version supported by the device.
    /// </summary>
    string Version { get; }
}
