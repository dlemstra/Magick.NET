﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;

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
    /// Applies an affine transformation to the image.
    /// </summary>
    /// <param name="affineMatrix">The affine matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AffineTransform(IDrawableAffine affineMatrix);

    /// <summary>
    /// Adjusts an image so that its orientation is suitable for viewing.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoOrient();

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BilateralBlur(uint width, uint height);

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>
    /// <param name="intensitySigma">The sigma in the intensity space.</param>
    /// <param name="spatialSigma">The sigma in the coordinate space.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BilateralBlur(uint width, uint height, double intensitySigma, double spatialSigma);

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlueShift();

    /// <summary>
    /// Simulate a scene at nighttime in the moonlight.
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlueShift(double factor);

    /// <summary>
    /// Blur image with the default blur factor (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Blur();

    /// <summary>
    /// Blur the the specified channel(s) of the image with the default blur factor (0x1).
    /// </summary>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Blur(Channels channels);

    /// <summary>
    /// Blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Blur(double radius, double sigma);

    /// <summary>
    /// Blur the specified channel(s) of the image with the specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Blur(double radius, double sigma, Channels channels);

    /// <summary>
    /// Add a border to the image.
    /// </summary>
    /// <param name="size">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(uint size);

    /// <summary>
    /// Add a border to the image.
    /// </summary>
    /// <param name="width">The width of the border.</param>
    /// <param name="height">The height of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(uint width, uint height);

    /// <summary>
    /// Add a border to the image.
    /// </summary>
    /// <param name="percentage">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(Percentage percentage);

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CannyEdge();

    /// <summary>
    /// Uses a multi-stage algorithm to detect a wide range of edges in images.
    /// </summary>
    /// <param name="radius">The radius of the gaussian smoothing filter.</param>
    /// <param name="sigma">The sigma of the gaussian smoothing filter.</param>
    /// <param name="lower">Percentage of edge pixels in the lower threshold.</param>
    /// <param name="upper">Percentage of edge pixels in the upper threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CannyEdge(double radius, double sigma, Percentage lower, Percentage upper);

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Charcoal();

    /// <summary>
    /// Charcoal effect image (looks like charcoal sketch).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Charcoal(double radius, double sigma);

    /// <summary>
    /// Chop image (remove vertical or horizontal subregion of image) using the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Chop(IMagickGeometry geometry);

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="width">The width of the part to chop horizontally.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ChopHorizontal(int x, uint width);

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="height">The height of the part to chop vertically.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ChopVertical(int y, uint height);

    /// <summary>
    /// Apply a color matrix to the image channels.
    /// </summary>
    /// <param name="matrix">The color matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ColorMatrix(IMagickColorMatrix matrix);

    /// <summary>
    /// Convolve image. Applies a user-specified convolution to the image.
    /// </summary>
    /// <param name="matrix">The convolution matrix.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Convolve(IConvolveMatrix matrix);

    /// <summary>
    /// Crop image (subregion of original image). <see cref="IMagickImage.ResetPage"/> should be called unless
    /// the <see cref="IMagickImage.Page"/> information is needed.
    /// </summary>
    /// <param name="width">The width of the subregion to crop.</param>
    /// <param name="height">The height of the subregion to crop.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(uint width, uint height);

    /// <summary>
    /// Crop image (subregion of original image). <see cref="IMagickImage.ResetPage"/> should be called unless
    /// the <see cref="IMagickImage.Page"/> information is needed.
    /// </summary>
    /// <param name="width">The width of the subregion to crop.</param>
    /// <param name="height">The height of the subregion to crop.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(uint width, uint height, Gravity gravity);

    /// <summary>
    /// Crop image (subregion of original image). <see cref="IMagickImage.ResetPage"/> should be called unless
    /// the <see cref="IMagickImage.Page"/> information is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(IMagickGeometry geometry);

    /// <summary>
    /// Crop image (subregion of original image). <see cref="IMagickImage.ResetPage"/> should be called unless
    /// the <see cref="IMagickImage.Page"/> information is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(IMagickGeometry geometry, Gravity gravity);

    /// <summary>
    /// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
    /// the camera being misaligned, imperfections in the scanning or surface, or simply because
    /// the paper was not placed completely flat when scanned. The value of threshold ranges
    /// from 0 to QuantumRange.
    /// </summary>
    /// <param name="threshold">The threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The angle that was used.</returns>
    double Deskew(Percentage threshold);

    /// <summary>
    /// Removes skew from the image. Skew is an artifact that occurs in scanned images because of
    /// the camera being misaligned, imperfections in the scanning or surface, or simply because
    /// the paper was not placed completely flat when scanned. The value of threshold ranges
    /// from 0 to QuantumRange. After the image is deskewed, it is cropped.
    /// </summary>
    /// <param name="threshold">The threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The angle that was used.</returns>
    double DeskewAndCrop(Percentage threshold);

    /// <summary>
    /// Despeckle image (reduce speckle noise).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Despeckle();

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image of the same size as the source image.
    /// </summary>
    /// <param name="method">The distortion method to use.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Distort(DistortMethod method, params double[] arguments);

    /// <summary>
    /// Distorts an image using various distortion methods, by mapping color lookups of the source
    /// image to a new destination image usually of the same size as the source image, unless
    /// 'bestfit' is set to true.
    /// </summary>
    /// <param name="settings">The settings for the distort operation.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Distort(IDistortSettings settings, params double[] arguments);

    /// <summary>
    /// Edge image (highlight edges in image).
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Edge(double radius);

    /// <summary>
    /// Emboss image (highlight edges with 3D effect) with default value (0x1).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Emboss();

    /// <summary>
    /// Emboss image (highlight edges with 3D effect).
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Emboss(double radius, double sigma);

    /// <summary>
    /// Applies a digital filter that improves the quality of a noisy image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Enhance();

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(uint width, uint height);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(int x, int y, uint width, uint height);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(uint width, uint height, Gravity gravity);

    /// <summary>
    /// Extend the image as defined by the rectangle.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(IMagickGeometry geometry);

    /// <summary>
    /// Extend the image as defined by the geometry.
    /// </summary>
    /// <param name="geometry">The geometry to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(IMagickGeometry geometry, Gravity gravity);

    /// <summary>
    /// Flip image (reflect each scanline in the vertical direction).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Flip();

    /// <summary>
    /// Flop image (reflect each scanline in the horizontal direction).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Flop();

    /// <summary>
    /// Frame image with the default geometry (25x25+6+6).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Frame();

    /// <summary>
    /// Frame image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Frame(IMagickGeometry geometry);

    /// <summary>
    /// Frame image with the specified with and height.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Frame(uint width, uint height);

    /// <summary>
    /// Frame image with the specified with, height, innerBevel and outerBevel.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="innerBevel">The inner bevel of the frame.</param>
    /// <param name="outerBevel">The outer bevel of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Frame(uint width, uint height, int innerBevel, int outerBevel);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GaussianBlur(double radius);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GaussianBlur(double radius, Channels channels);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GaussianBlur(double radius, double sigma);

    /// <summary>
    /// Gaussian blur image.
    /// </summary>
    /// <param name="radius">The number of neighbor pixels to be included in the convolution.</param>
    /// <param name="sigma">The standard deviation of the gaussian bell curve.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GaussianBlur(double radius, double sigma, Channels channels);

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void HoughLine();

    /// <summary>
    /// Identifies lines in the image.
    /// </summary>
    /// <param name="width">The width of the neighborhood.</param>
    /// <param name="height">The height of the neighborhood.</param>
    /// <param name="threshold">The line count threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void HoughLine(uint width, uint height, uint threshold);

    /// <summary>
    /// Implode image (special effect).
    /// </summary>
    /// <param name="amount">The extent of the implosion.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Implode(double amount, PixelInterpolateMethod method);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InterpolativeResize(uint width, uint height, PixelInterpolateMethod method);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InterpolativeResize(IMagickGeometry geometry, PixelInterpolateMethod method);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InterpolativeResize(Percentage percentage, PixelInterpolateMethod method);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InterpolativeResize(Percentage percentageWidth, Percentage percentageHeight, PixelInterpolateMethod method);

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Kuwahara();

    /// <summary>
    /// An edge preserving noise reduction filter.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Kuwahara(double radius, double sigma);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(uint width, uint height);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="deltaX">Maximum seam transversal step (0 means straight seams).</param>
    /// <param name="rigidity">Introduce a bias for non-straight seams (typically 0).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(uint width, uint height, double deltaX, double rigidity);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(IMagickGeometry geometry);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(Percentage percentage);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <param name="deltaX">Maximum seam transversal step (0 means straight seams).</param>
    /// <param name="rigidity">Introduce a bias for non-straight seams (typically 0).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(Percentage percentageWidth, Percentage percentageHeight, double deltaX, double rigidity);

    /// <summary>
    /// Magnify image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Magnify();

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    void MeanShift(uint size);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    void MeanShift(uint size, Percentage colorDistance);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    void MeanShift(uint width, uint height);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    void MeanShift(uint width, uint height, Percentage colorDistance);

    /// <summary>
    /// Reduce image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Minify();

    /// <summary>
    /// Applies a kernel to the image according to the given mophology settings.
    /// </summary>
    /// <param name="settings">The morphology settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(IMorphologySettings settings);

    /// <summary>
    /// Motion blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MotionBlur(double radius, double sigma, double angle);

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OilPaint();

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    /// <param name="radius">The radius of the circular neighborhood.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OilPaint(double radius, double sigma);

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="resolutionX">The new X resolution.</param>
    /// <param name="resolutionY">The new Y resolution.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resample(double resolutionX, double resolutionY);

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="resolutionX">The new X resolution.</param>
    /// <param name="resolutionY">The new Y resolution.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resample(double resolutionX, double resolutionY, FilterType filter);

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="density">The density to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resample(PointD density);

    /// <summary>
    /// Resize image in terms of its pixel size.
    /// </summary>
    /// <param name="density">The density to use.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resample(PointD density, FilterType filter);

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
    /// Resize image to specified size.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(uint width, uint height, FilterType filter);

    /// <summary>
    /// Resize image to specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(IMagickGeometry geometry);

    /// <summary>
    /// Resize image to specified geometry.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(IMagickGeometry geometry, FilterType filter);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentage);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentage, FilterType filter);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Resize image to specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <param name="filter">The filter to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(Percentage percentageWidth, Percentage percentageHeight, FilterType filter);

    /// <summary>
    /// Roll image (rolls image vertically and horizontally).
    /// </summary>
    /// <param name="x">The number of columns to roll in the horizontal direction.</param>
    /// <param name="y">The number of rows to roll in the vertical direction.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Roll(int x, int y);

    /// <summary>
    /// Rotate image clockwise by specified number of degrees.
    /// </summary>
    /// <remarks>Specify a negative number for <paramref name="degrees"/> to rotate counter-clockwise.</remarks>
    /// <param name="degrees">The number of degrees to rotate (positive to rotate clockwise, negative to rotate counter-clockwise).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Rotate(double degrees);

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RotationalBlur(double angle);

    /// <summary>
    /// Rotational blur image.
    /// </summary>
    /// <param name="angle">The angle to use.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RotationalBlur(double angle, Channels channels);

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sample(uint width, uint height);

    /// <summary>
    /// Resize image by using pixel sampling algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sample(IMagickGeometry geometry);

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sample(Percentage percentage);

    /// <summary>
    /// Resize image by using pixel sampling algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sample(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Scale(uint width, uint height);

    /// <summary>
    /// Resize image by using simple ratio algorithm.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Scale(IMagickGeometry geometry);

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Scale(Percentage percentage);

    /// <summary>
    /// Resize image by using simple ratio algorithm to the specified percentage.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Scale(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SelectiveBlur(double radius, double sigma, double threshold);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="threshold">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SelectiveBlur(double radius, double sigma, double threshold, Channels channels);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="thresholdPercentage">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SelectiveBlur(double radius, double sigma, Percentage thresholdPercentage);

    /// <summary>
    /// Selectively blur pixels within a contrast threshold. It is similar to the unsharpen mask
    /// that sharpens everything with contrast above a certain threshold.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="thresholdPercentage">Only pixels within this contrast threshold are included in the blur operation.</param>
    /// <param name="channels">The channel(s) to blur.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SelectiveBlur(double radius, double sigma, Percentage thresholdPercentage, Channels channels);

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SepiaTone();

    /// <summary>
    /// Applies a special effect to the image, similar to the effect achieved in a photo darkroom
    /// by sepia toning.
    /// </summary>
    /// <param name="threshold">The tone threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SepiaTone(Percentage threshold);

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shade();

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shade(double azimuth, double elevation);

    /// <summary>
    /// Shade image using distant light source.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <param name="channels">The channel(s) that should be shaded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shade(double azimuth, double elevation, Channels channels);

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ShadeGrayscale();

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ShadeGrayscale(double azimuth, double elevation);

    /// <summary>
    /// Shade image using distant light source and make it grayscale.
    /// </summary>
    /// <param name="azimuth">The azimuth of the light source direction.</param>
    /// <param name="elevation">The elevation of the light source direction.</param>
    /// <param name="channels">The channel(s) that should be shaded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ShadeGrayscale(double azimuth, double elevation, Channels channels);

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shadow();

    /// <summary>
    /// Simulate an image shadow.
    /// </summary>
    /// <param name="x">the shadow x-offset.</param>
    /// <param name="y">the shadow y-offset.</param>
    /// <param name="sigma">The standard deviation of the Gaussian, in pixels.</param>
    /// <param name="alpha">Transparency percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shadow(int x, int y, double sigma, Percentage alpha);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sharpen();

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sharpen(Channels channels);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sharpen(double radius, double sigma);

    /// <summary>
    /// Sharpen pixels in image.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    void Sharpen(double radius, double sigma, Channels channels);

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="size">The size of to shave of the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shave(uint size);

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="leftRight">The number of pixels to shave left and right.</param>
    /// <param name="topBottom">The number of pixels to shave top and bottom.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shave(uint leftRight, uint topBottom);

    /// <summary>
    /// Shear image (create parallelogram by sliding image by X or Y axis).
    /// </summary>
    /// <param name="xAngle">Specifies the number of x degrees to shear the image.</param>
    /// <param name="yAngle">Specifies the number of y degrees to shear the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shear(double xAngle, double yAngle);

    /// <summary>
    /// Simulates a pencil sketch.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sketch();

    /// <summary>
    /// Simulates a pencil sketch. We convolve the image with a Gaussian operator of the given
    /// radius and standard deviation (sigma). For reasonable results, radius should be larger than sigma.
    /// Use a radius of 0 and sketch selects a suitable radius for you.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">Apply the effect along this angle.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Sketch(double radius, double sigma, double angle);

    /// <summary>
    /// Splice the background color into the image.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Splice(IMagickGeometry geometry);

    /// <summary>
    /// Splice the background color into the image.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="gravity">The gravity to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Splice(IMagickGeometry geometry, Gravity gravity);

    /// <summary>
    /// Spread pixels randomly within image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Spread();

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Spread(double radius);

    /// <summary>
    /// Spread pixels randomly within image by specified amount.
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="radius">Choose a random pixel in a neighborhood of this extent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Spread(PixelInterpolateMethod method, double radius);

    /// <summary>
    /// Makes each pixel the min / max / median / mode / etc. of the neighborhood of the specified width
    /// and height.
    /// </summary>
    /// <param name="type">The statistic type.</param>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Statistic(StatisticType type, uint width, uint height);

    /// <summary>
    /// Add a digital watermark to the image (based on second image).
    /// </summary>
    /// <param name="watermark">The image to use as a watermark.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Stegano(IMagickImage watermark);

    /// <summary>
    /// Create an image which appears in stereo when viewed with red-blue glasses (Red image on
    /// left, blue on right).
    /// </summary>
    /// <param name="rightImage">The image to use as the right part of the resulting image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Stereo(IMagickImage rightImage);

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Swirl(double degrees);

    /// <summary>
    /// Swirl image (image pixels are rotated by degrees).
    /// </summary>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="degrees">The number of degrees.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Swirl(PixelInterpolateMethod method, double degrees);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Thumbnail(uint width, uint height);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Thumbnail(IMagickGeometry geometry);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Thumbnail(Percentage percentage);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Thumbnail(Percentage percentageWidth, Percentage percentageHeight);

    /// <summary>
    /// Creates a horizontal mirror image by reflecting the pixels around the central y-axis while
    /// rotating them by 90 degrees.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Transpose();

    /// <summary>
    /// Creates a vertical mirror image by reflecting the pixels around the central x-axis while
    /// rotating them by 270 degrees.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Transverse();

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void UnsharpMask(double radius, double sigma);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void UnsharpMask(double radius, double sigma, Channels channels);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void UnsharpMask(double radius, double sigma, double amount, double threshold);

    /// <summary>
    /// Replace image with a sharpened version of the original image using the unsharp mask algorithm.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="amount">The percentage of the difference between the original and the blur image
    /// that is added back into the original.</param>
    /// <param name="threshold">The threshold in pixels needed to apply the diffence amount.</param>
    /// <param name="channels">The channel(s) that should be sharpened.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void UnsharpMask(double radius, double sigma, double amount, double threshold, Channels channels);

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Vignette();

    /// <summary>
    /// Softens the edges of the image in vignette style.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="x">The x ellipse offset.</param>
    /// <param name="y">the y ellipse offset.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Vignette(double radius, double sigma, int x, int y);

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Wave();

    /// <summary>
    /// Map image pixels to a sine wave.
    /// </summary>
    /// <param name="method">The pixel interpolate method.</param>
    /// <param name="amplitude">The amplitude.</param>
    /// <param name="length">The length of the wave.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Wave(PixelInterpolateMethod method, double amplitude, double length);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing.</param>
    void WaveletDenoise(Percentage thresholdPercentage);

    /// <summary>
    /// Removes noise from the image using a wavelet transform.
    /// </summary>
    /// <param name="thresholdPercentage">The threshold for smoothing.</param>
    /// <param name="softness">Attenuate the smoothing threshold.</param>
    void WaveletDenoise(Percentage thresholdPercentage, double softness);
}
