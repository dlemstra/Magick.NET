// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Interface that represents the resource limits of ImageMagick.
/// </summary>
public interface IResourceLimits
{
    /// <summary>
    /// Gets or sets the maximum width * height of an image that can reside in the pixel cache memory.
    /// Images that exceed the area limit are cached to disk.
    /// </summary>
    ulong Area { get; set; }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Requests for memory above this limit will fail.
    /// </summary>
    ulong Disk { get; set; }

    /// <summary>
    /// Gets or sets the maximum height of an image.
    /// </summary>
    ulong Height { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of images in an image list.
    /// </summary>
    ulong ListLength { get; set; }

    /// <summary>
    /// Gets or sets the max memory request in bytes. ImageMagick maintains a separate memory pool for large
    /// resource requests. If the limit is exceeded when allocating pixels, the allocation is instead memory-mapped
    /// on disk.
    /// </summary>
    ulong MaxMemoryRequest { get; set; }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
    /// operations are to/from disk. The default value of this is 50% of the available memory on the machine in 64-bit mode.
    /// When running in 32-bit mode this is 50% of the limit of the operating system.
    /// </summary>
    ulong Memory { get; set; }

    /// <summary>
    /// Gets or sets the number of threads used in multithreaded operations.
    /// </summary>
    ulong Thread { get; set; }

    /// <summary>
    /// Gets or sets the time specified in milliseconds to periodically yield the CPU for.
    /// </summary>
    ulong Throttle { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of seconds that the process is permitted to execute. Exceed this limit and
    /// an exception is thrown and processing stops.
    /// </summary>
    ulong Time { get; set; }

    /// <summary>
    /// Gets or sets the maximum width of an image.
    /// </summary>
    ulong Width { get; set; }

    /// <summary>
    /// Set the maximum percentage of memory that can be used for image data. This also changes
    /// the <see cref="Area"/> limit to four times the number of bytes.
    /// </summary>
    /// <param name="percentage">The percentage to use.</param>
    void LimitMemory(Percentage percentage);
}
