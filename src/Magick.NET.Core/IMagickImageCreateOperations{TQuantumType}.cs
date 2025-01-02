// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that represents ImageMagick operations that create a new image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public interface IMagickImageCreateOperations<TQuantumType> : IMagickImageCreateOperations
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

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shadow(IMagickColor<TQuantumType> color);

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x ">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <param name="color">The color of the shadow.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shadow(int x, int y, double sigma, Percentage alpha, IMagickColor<TQuantumType> color);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SparseColor(SparseColorMethod method, IEnumerable<ISparseColorArg<TQuantumType>> args);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SparseColor(SparseColorMethod method, params ISparseColorArg<TQuantumType>[] args);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="channels">The channel(s) to use.</param>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SparseColor(Channels channels, SparseColorMethod method, IEnumerable<ISparseColorArg<TQuantumType>> args);

    /// <summary>
    /// Sparse color image, given a set of coordinates, interpolates the colors found at those
    /// coordinates, across the whole image, using various methods.
    /// </summary>
    /// <param name="channels">The channel(s) to use.</param>
    /// <param name="method">The sparse color method to use.</param>
    /// <param name="args">The sparse color arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SparseColor(Channels channels, SparseColorMethod method, params ISparseColorArg<TQuantumType>[] args);

    /// <summary>
    /// Applies a color vector to each pixel in the image. The length of the vector is 0 for black
    /// and white and at its maximum for the midtones. The vector weighting function is
    /// f(x)=(1-(4.0*((x-0.5)*(x-0.5)))).
    /// </summary>
    /// <param name="opacity">An opacity value used for tinting.</param>
    /// <param name="color">A color value used for tinting.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Tint(IMagickGeometry opacity, IMagickColor<TQuantumType> color);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing.</param>
    void WaveletDenoise(TQuantumType threshold);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="threshold">The threshold for smoothing.</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
    void WaveletDenoise(TQuantumType threshold, double softness);
}
