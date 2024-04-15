// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that can be used to set the limits to the resources that are being used.
/// </summary>
public partial class ResourceLimits : IResourceLimits
{
    /// <summary>
    /// Gets or sets the maximum width * height of an image that can reside in the pixel cache memory.
    /// Images that exceed the area limit are cached to disk.
    /// </summary>
    public static ulong Area
    {
        get => NativeResourceLimits.Area;
        set => NativeResourceLimits.Area = value;
    }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Requests for memory above this limit will fail.
    /// </summary>
    public static ulong Disk
    {
        get => NativeResourceLimits.Disk;
        set => NativeResourceLimits.Disk = value;
    }

    /// <summary>
    /// Gets or sets the maximum height of an image.
    /// </summary>
    public static ulong Height
    {
        get => NativeResourceLimits.Height;
        set => NativeResourceLimits.Height = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of images in an image list.
    /// </summary>
    public static ulong ListLength
    {
        get => NativeResourceLimits.ListLength;
        set => NativeResourceLimits.ListLength = value;
    }

    /// <summary>
    /// Gets or sets the max memory request in bytes. ImageMagick maintains a separate memory pool for large
    /// resource requests. If the limit is exceeded when allocating pixels, the allocation is instead memory-mapped
    /// on disk.
    /// </summary>
    public static ulong MaxMemoryRequest
    {
        get => NativeResourceLimits.MaxMemoryRequest;
        set => NativeResourceLimits.MaxMemoryRequest = value;
    }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
    /// operations are to/from disk. The default value of this is 50% of the available memory on the machine in 64-bit mode.
    /// When running in 32-bit mode this is 50% of the limit of the operating system.
    /// </summary>
    public static ulong Memory
    {
        get => NativeResourceLimits.Memory;
        set => NativeResourceLimits.Memory = value;
    }

    /// <summary>
    /// Gets or sets the number of threads used in multithreaded operations.
    /// </summary>
    public static ulong Thread
    {
        get => NativeResourceLimits.Thread;
        set => NativeResourceLimits.Thread = value;
    }

    /// <summary>
    /// Gets or sets the time specified in milliseconds to periodically yield the CPU for.
    /// </summary>
    public static ulong Throttle
    {
        get => NativeResourceLimits.Throttle;
        set => NativeResourceLimits.Throttle = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of seconds that the process is permitted to execute. Exceed this limit and
    /// an exception is thrown and processing stops.
    /// </summary>
    public static ulong Time
    {
        get => NativeResourceLimits.Time;
        set => NativeResourceLimits.Time = value;
    }

    /// <summary>
    /// Gets or sets the maximum width of an image.
    /// </summary>
    public static ulong Width
    {
        get => NativeResourceLimits.Width;
        set => NativeResourceLimits.Width = value;
    }

    /// <summary>
    /// Gets or sets the maximum width * height of an image that can reside in the pixel cache memory.
    /// Images that exceed the area limit are cached to disk.
    /// </summary>
    ulong IResourceLimits.Area
    {
        get => Area;
        set => Area = value;
    }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Requests for memory above this limit will fail.
    /// </summary>
    ulong IResourceLimits.Disk
    {
        get => Disk;
        set => Disk = value;
    }

    /// <summary>
    /// Gets or sets the maximum height of an image.
    /// </summary>
    ulong IResourceLimits.Height
    {
        get => Height;
        set => Height = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of images in an image list.
    /// </summary>
    ulong IResourceLimits.ListLength
    {
        get => ListLength;
        set => ListLength = value;
    }

    /// <summary>
    /// Gets or sets the max memory request in bytes. ImageMagick maintains a separate memory pool for large
    /// resource requests. If the limit is exceeded, the allocation is instead memory-mapped on disk.
    /// </summary>
    ulong IResourceLimits.MaxMemoryRequest
    {
        get => MaxMemoryRequest;
        set => MaxMemoryRequest = value;
    }

    /// <summary>
    /// Gets or sets the pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
    /// operations are to/from disk. The default value of this is 50% of the available memory on the machine in 64-bit mode.
    /// When running in 32-bit mode this is 50% of the limit of the operating system.
    /// </summary>
    ulong IResourceLimits.Memory
    {
        get => Memory;
        set => Memory = value;
    }

    /// <summary>
    /// Gets or sets the number of threads used in multithreaded operations.
    /// </summary>
    ulong IResourceLimits.Thread
    {
        get => Thread;
        set => Thread = value;
    }

    /// <summary>
    /// Gets or sets the time specified in milliseconds to periodically yield the CPU for.
    /// </summary>
    ulong IResourceLimits.Throttle
    {
        get => Throttle;
        set => Throttle = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of seconds that the process is permitted to execute. Exceed this limit and
    /// an exception is thrown and processing stops.
    /// </summary>
    ulong IResourceLimits.Time
    {
        get => Time;
        set => Time = value;
    }

    /// <summary>
    /// Gets or sets the maximum width of an image.
    /// </summary>
    ulong IResourceLimits.Width
    {
        get => Width;
        set => Width = value;
    }

    /// <summary>
    /// Set the maximum percentage of memory that can be used for image data. This also changes
    /// the <see cref="Area"/> limit to four times the number of bytes.
    /// </summary>
    /// <param name="percentage">The percentage to use.</param>
    public static void LimitMemory(Percentage percentage)
        => NativeResourceLimits.LimitMemory((double)percentage / 100.0);

    /// <summary>
    /// Set the maximum percentage of memory that can be used for image data. This also changes
    /// the <see cref="Area"/> limit to four times the number of bytes.
    /// </summary>
    /// <param name="percentage">The percentage to use.</param>
    void IResourceLimits.LimitMemory(Percentage percentage)
        => LimitMemory(percentage);
}
