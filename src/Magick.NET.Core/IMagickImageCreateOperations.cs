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
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveSharpen();

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveSharpen(Channels channels);

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveSharpen(double radius, double sigma);

    /// <summary>
    /// Adaptively sharpens the image by sharpening more intensely near image edges and less
    /// intensely far from edges.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    void AdaptiveSharpen(double radius, double sigma, Channels channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height, Channels channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height, double bias);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height, double bias, Channels channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height, Percentage biasPercentage);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(uint width, uint height, Percentage biasPercentage, Channels channels);

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddNoise(NoiseType noiseType);

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddNoise(NoiseType noiseType, Channels channels);

    /// <summary>
    /// Add noise to image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddNoise(NoiseType noiseType, double attenuate);

    /// <summary>
    /// Add noise to the specified channel of the image with the specified noise type.
    /// </summary>
    /// <param name="noiseType">The type of noise that should be added to the image.</param>
    /// <param name="attenuate">Attenuate the random distribution.</param>
    /// <param name="channels">The channel(s) where the noise should be added.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AddNoise(NoiseType noiseType, double attenuate, Channels channels);

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
