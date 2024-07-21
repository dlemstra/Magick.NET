// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
/// Class that can be used to create various settings.
/// </summary>
public sealed class SettingsFactory : ISettingsFactory<QuantumType>
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="ICompareSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="ICompareSettings{TQuantumType}"/> instance.</returns>
    public ICompareSettings<QuantumType> CreateCompareSettings()
        => new CompareSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IComplexSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IComplexSettings"/> instance.</returns>
    public IComplexSettings CreateComplexSettings()
        => new ComplexSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IConnectedComponentsSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IConnectedComponentsSettings"/> instance.</returns>
    public IConnectedComponentsSettings CreateConnectedComponentsSettings()
        => new ConnectedComponentsSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IDeskewSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IDeskewSettings"/> instance.</returns>
    public IDeskewSettings CreateDeskewSettings()
        => new DeskewSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IDistortSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IDistortSettings"/> instance.</returns>
    public IDistortSettings CreateDistortSettings()
        => new DistortSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IKmeansSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IKmeansSettings"/> instance.</returns>
    public IKmeansSettings CreateKmeansSettings()
        => new KmeansSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickReadSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
    public IMagickReadSettings<QuantumType> CreateMagickReadSettings()
        => new MagickReadSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMontageSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
    public IMontageSettings<QuantumType> CreateMontageSettings()
        => new MontageSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMorphologySettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IMorphologySettings"/> instance.</returns>
    public IMorphologySettings CreateMorphologySettings()
        => new MorphologySettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IPixelReadSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IPixelReadSettings{TQuantumType}"/> instance.</returns>
    public IPixelReadSettings<QuantumType> CreatePixelReadSettings()
        => new PixelReadSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IQuantizeSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IQuantizeSettings"/> instance.</returns>
    public IQuantizeSettings CreateQuantizeSettings()
        => new QuantizeSettings();
}
