// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that represents the OpenCL information of ImageMagick.
/// </summary>
public interface IOpenCL
{
    /// <summary>
    /// Gets or sets a value indicating whether OpenCL is enabled.
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// Gets all the OpenCL devices.
    /// </summary>
    /// <returns>A <see cref="IOpenCLDevice"/> iteration.</returns>
    IReadOnlyList<IOpenCLDevice> Devices { get; }

    /// <summary>
    /// Sets the directory that will be used by ImageMagick to store OpenCL cache files.
    /// </summary>
    /// <param name="path">The path of the OpenCL cache directory.</param>
    void SetCacheDirectory(string path);
}
