// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.Factories;

/// <summary>
/// Class that can be used to create various settings.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface ISettingsFactory<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Initializes a new instance that implements <see cref="ICompareSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="ICompareSettings{TQuantumType}"/> instance.</returns>
    ICompareSettings<TQuantumType> CreateCompareSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IComplexSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IComplexSettings"/> instance.</returns>
    IComplexSettings CreateComplexSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IConnectedComponentsSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IConnectedComponentsSettings"/> instance.</returns>
    IConnectedComponentsSettings CreateConnectedComponentsSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IDistortSettings"/>.
    /// </summary>
    /// <param name="method">The distort method to use.</param>
    /// <returns>A new <see cref="IDistortSettings"/> instance.</returns>
    IDistortSettings CreateDistortSettings(DistortMethod method);

    /// <summary>
    /// Initializes a new instance that implements <see cref="IKmeansSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IKmeansSettings"/> instance.</returns>
    IKmeansSettings CreateKmeansSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMagickReadSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
    IMagickReadSettings<TQuantumType> CreateMagickReadSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMontageSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IMagickReadSettings{TQuantumType}"/> instance.</returns>
    IMontageSettings<TQuantumType> CreateMontageSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IMorphologySettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IMorphologySettings"/> instance.</returns>
    IMorphologySettings CreateMorphologySettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IPixelReadSettings{TQuantumType}"/>.
    /// </summary>
    /// <returns>A new <see cref="IPixelReadSettings{TQuantumType}"/> instance.</returns>
    IPixelReadSettings<TQuantumType> CreatePixelReadSettings();

    /// <summary>
    /// Initializes a new instance that implements <see cref="IQuantizeSettings"/>.
    /// </summary>
    /// <returns>A new <see cref="IQuantizeSettings"/> instance.</returns>
    IQuantizeSettings CreateQuantizeSettings();
}
