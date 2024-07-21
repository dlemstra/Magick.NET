// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using ImageMagick.Drawing;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create various instances.
/// </summary>
public sealed partial class MagickFactory : IMagickFactory<QuantumType>
{
    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickColorFactory{TQuantumType}"/> instances.
    /// </summary>
    public IMagickColorFactory<QuantumType> Color { get; } = new MagickColorFactory();

    /// <summary>
    /// Gets a factory that can be used to create color instances by name.
    /// </summary>
    public IMagickColors<QuantumType> Colors { get; } = new MagickColors();

    /// <summary>
    /// Gets the configuration files.
    /// </summary>
    public IConfigurationFiles ConfigurationFiles
        => Configuration.ConfigurationFiles.Default;

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IDrawables{QuantumType}"/> instances.
    /// </summary>
    public IDrawablesFactory<QuantumType> Drawables { get; } = new DrawablesFactory();

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickGeometry"/> instances.
    /// </summary>
    public IMagickGeometryFactory Geometry { get; } = new MagickGeometryFactory();

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickImage{QuantumType}"/> instances.
    /// </summary>
    public IMagickImageFactory<QuantumType> Image { get; } = new MagickImageFactory();

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickImageCollection{QuantumType}"/> instances.
    /// </summary>
    public IMagickImageCollectionFactory<QuantumType> ImageCollection { get; } = new MagickImageCollectionFactory();

    /// <summary>
    /// Gets a factory that can be used to create <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    public IMagickImageInfoFactory ImageInfo { get; } = new MagickImageInfoFactory();

    /// <summary>
    /// Gets the MagickNET information.
    /// </summary>
    public IMagickNET MagickNET { get; } = new MagickNET();

    /// <summary>
    /// Gets a factory that can be used to create various matrix instances.
    /// </summary>
    public IMatrixFactory Matrix { get; } = new MatrixFactory();

    /// <summary>
    /// Gets the OpenCL information.
    /// </summary>
    public IOpenCL OpenCL { get; } = new OpenCL();

    /// <summary>
    /// Gets the quantum information.
    /// </summary>
    public IQuantum<QuantumType> Quantum { get; } = new Quantum();

    /// <summary>
    /// Gets the resource limits.
    /// </summary>
    public IResourceLimits ResourceLimits { get; } = new ResourceLimits();

    /// <summary>
    /// Gets a factory that can be used to create various settings.
    /// </summary>
    public ISettingsFactory<QuantumType> Settings { get; } = new SettingsFactory();
}
