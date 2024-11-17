// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface that can be used to efficiently clone and mutate an image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickImageCloneMutator<TQuantumType> : IMagickImageCreateOperations<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alpha">The alpha percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Colorize(IMagickColor<TQuantumType> color, Percentage alpha);

    /// <summary>
    /// Colorize image with the specified color, using specified percent alpha for red, green,
    /// and blue quantums.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="alphaRed">The alpha percentage for red.</param>
    /// <param name="alphaGreen">The alpha percentage for green.</param>
    /// <param name="alphaBlue">The alpha percentage for blue.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Colorize(IMagickColor<TQuantumType> color, Percentage alphaRed, Percentage alphaGreen, Percentage alphaBlue);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(uint width, uint height, IMagickColor<TQuantumType> backgroundColor);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(uint width, uint height, Gravity gravity, IMagickColor<TQuantumType> backgroundColor);

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(IMagickGeometry geometry, IMagickColor<TQuantumType> backgroundColor);

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="backgroundColor">The background color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(IMagickGeometry geometry, Gravity gravity, IMagickColor<TQuantumType> backgroundColor);
}
