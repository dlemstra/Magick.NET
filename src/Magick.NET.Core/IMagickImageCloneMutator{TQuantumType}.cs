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
}
