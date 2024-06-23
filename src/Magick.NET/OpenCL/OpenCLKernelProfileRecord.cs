// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Represents a kernel profile record for an OpenCL device.
/// </summary>
public sealed partial class OpenCLKernelProfileRecord : IOpenCLKernelProfileRecord
{
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
}
