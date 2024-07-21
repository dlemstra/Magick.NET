// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create various instances.
/// </summary>
public interface IMagickFactory
{
    /// <summary>
    /// Gets the configuration files.
    /// </summary>
    IConfigurationFiles ConfigurationFiles { get; }

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickGeometry"/> instances.
    /// </summary>
    IMagickGeometryFactory Geometry { get; }

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    IMagickImageInfoFactory ImageInfo { get; }

    /// <summary>
    /// Gets the MagickNET information.
    /// </summary>
    IMagickNET MagickNET { get; }

    /// <summary>
    /// Gets a factory that can be used to create various matrix instances.
    /// </summary>
    IMatrixFactory Matrix { get; }

    /// <summary>
    /// Gets the OpenCL information.
    /// </summary>
    IOpenCL OpenCL { get; }

    /// <summary>
    /// Gets the resource limits.
    /// </summary>
    IResourceLimits ResourceLimits { get; }
}
