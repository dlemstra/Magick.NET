// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Interface that represents ImageMagick operations that create a new image.
/// </summary>
public interface IMagickImageCreateOperations
{
    /// <summary>
    /// Adaptive-blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveBlur();

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveBlur(double radius);

    /// <summary>
    /// Adaptive-blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveBlur(double radius, double sigma);

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveResize(uint width, uint height);

    /// <summary>
    /// Resize using mesh interpolation. It works well for small resizes of less than +/- 50%
    /// of the original image size. For larger resizing on images a full filtered and slower resize
    /// function should be used instead.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveResize(IMagickGeometry geometry);

    /// <summary>
    /// Resize image to specified size.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(uint width, uint height);

    /// <summary>
    /// Resize image to specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(IMagickGeometry geometry);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentage);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentageWidth, Percentage percentageHeight);
}
