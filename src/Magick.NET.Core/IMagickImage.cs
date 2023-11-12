// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

/// <summary>
/// Interface that represents an ImageMagick image.
/// </summary>
public partial interface IMagickImage : IDisposable
{
    /// <summary>
    /// Event that will be raised when progress is reported by this image.
    /// </summary>
    event EventHandler<ProgressEventArgs> Progress;

    /// <summary>
    /// Event that will we raised when a warning is thrown by ImageMagick.
    /// </summary>
    event EventHandler<WarningEventArgs> Warning;

    /// <summary>
    /// Gets or sets the time in 1/100ths of a second which must expire before splaying the next image in an
    /// animated sequence.
    /// </summary>
    int AnimationDelay { get; set; }

    /// <summary>
    /// Gets or sets the number of iterations to loop an animation (e.g. Netscape loop extension) for.
    /// </summary>
    int AnimationIterations { get; set; }

    /// <summary>
    /// Gets or sets the ticks per seconds for the animation delay.
    /// </summary>
    int AnimationTicksPerSecond { get; set; }

    /// <summary>
    /// Gets the names of the artifacts.
    /// </summary>
    IEnumerable<string> ArtifactNames { get; }

    /// <summary>
    /// Gets the names of the attributes.
    /// </summary>
    IEnumerable<string> AttributeNames { get; }

    /// <summary>
    /// Gets the height of the image before transformations.
    /// </summary>
    int BaseHeight { get; }

    /// <summary>
    /// Gets the width of the image before transformations.
    /// </summary>
    int BaseWidth { get; }

    /// <summary>
    /// Gets or sets a value indicating whether black point compensation should be used.
    /// </summary>
    bool BlackPointCompensation { get; set; }

    /// <summary>
    /// Gets the smallest bounding box enclosing non-border pixels. The current fuzz value is used
    /// when discriminating between pixels.
    /// </summary>
    IMagickGeometry? BoundingBox { get; }

    /// <summary>
    /// Gets the number of channels that the image contains.
    /// </summary>
    int ChannelCount { get; }

    /// <summary>
    /// Gets the channels of the image.
    /// </summary>
    IEnumerable<PixelChannel> Channels { get; }

    /// <summary>
    /// Gets or sets the chromaticity blue primary point.
    /// </summary>
    IPrimaryInfo ChromaBluePrimary { get; set; }

    /// <summary>
    /// Gets or sets the chromaticity green primary point.
    /// </summary>
    IPrimaryInfo ChromaGreenPrimary { get; set; }

    /// <summary>
    /// Gets or sets the chromaticity red primary point.
    /// </summary>
    IPrimaryInfo ChromaRedPrimary { get; set; }

    /// <summary>
    /// Gets or sets the chromaticity white primary point.
    /// </summary>
    IPrimaryInfo ChromaWhitePoint { get; set; }

    /// <summary>
    /// Gets or sets the image class (DirectClass or PseudoClass)
    /// NOTE: Setting a DirectClass image to PseudoClass will result in the loss of color information
    /// if the number of colors in the image is greater than the maximum palette size (either 256 (Q8)
    /// or 65536 (Q16).
    /// </summary>
    ClassType ClassType { get; set; }

    /// <summary>
    /// Gets or sets the distance where colors are considered equal.
    /// </summary>
    Percentage ColorFuzz { get; set; }

    /// <summary>
    /// Gets or sets the colormap size (number of colormap entries).
    /// </summary>
    int ColormapSize { get; set; }

    /// <summary>
    /// Gets or sets the color space of the image.
    /// </summary>
    ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the color type of the image.
    /// </summary>
    ColorType ColorType { get; set; }

    /// <summary>
    /// Gets or sets the comment text of the image.
    /// </summary>
    string? Comment { get; set; }

    /// <summary>
    /// Gets or sets the composition operator to be used when composition is implicitly used (such as for image flattening).
    /// </summary>
    CompositeOperator Compose { get; set; }

    /// <summary>
    /// Gets the compression method of the image.
    /// </summary>
    CompressionMethod Compression { get; }

    /// <summary>
    /// Gets or sets the vertical and horizontal resolution in pixels of the image.
    /// </summary>
    Density Density { get; set; }

    /// <summary>
    /// Gets or sets the depth (bits allocated to red/green/blue components).
    /// </summary>
    int Depth { get; set; }

    /// <summary>
    /// Gets the preferred size of the image when encoding.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    [Obsolete("Will be removed in the next major release.")]
    IMagickGeometry? EncodingGeometry { get; }

    /// <summary>
    /// Gets or sets the endianness (little like Intel or big like SPARC) for image formats which support
    /// endian-specific options.
    /// </summary>
    Endian Endian { get; set; }

    /// <summary>
    /// Gets the original file name of the image (only available if read from disk).
    /// </summary>
    string? FileName { get; }

    /// <summary>
    /// Gets or sets the filter to use when resizing image.
    /// </summary>
    FilterType FilterType { get; set; }

    /// <summary>
    /// Gets or sets the format of the image.
    /// </summary>
    MagickFormat Format { get; set; }

    /// <summary>
    /// Gets the information about the format of the image.
    /// </summary>
    [Obsolete("Will be removed in the next major release.")]
    IMagickFormatInfo? FormatInfo { get; }

    /// <summary>
    /// Gets the gamma level of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Gamma { get; }

    /// <summary>
    /// Gets or sets the gif disposal method.
    /// </summary>
    GifDisposeMethod GifDisposeMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the image supports transparency (alpha channel).
    /// </summary>
    bool HasAlpha { get; set; }

    /// <summary>
    /// Gets the height of the image.
    /// </summary>
    int Height { get; }

    /// <summary>
    /// Gets or sets the type of interlacing to use.
    /// </summary>
    Interlace Interlace { get; set; }

    /// <summary>
    /// Gets or sets the pixel color interpolate method to use.
    /// </summary>
    PixelInterpolateMethod Interpolate { get; set; }

    /// <summary>
    /// Gets a value indicating whether the instance is disposed.
    /// </summary>
    [Obsolete("Will be removed in the next major release.")]
    bool IsDisposed { get; }

    /// <summary>
    /// Gets a value indicating whether none of the pixels in the image have an alpha value other
    /// than OpaqueAlpha (QuantumRange).
    /// </summary>
    bool IsOpaque { get; }

    /// <summary>
    /// Gets or sets the label of the image.
    /// </summary>
    string? Label { get; set; }

    /// <summary>
    /// Gets or sets the photo orientation of the image.
    /// </summary>
    OrientationType Orientation { get; set; }

    /// <summary>
    /// Gets or sets the preferred size and location of an image canvas.
    /// </summary>
    IMagickGeometry Page { get; set; }

    /// <summary>
    /// Gets the names of the profiles.
    /// </summary>
    IEnumerable<string> ProfileNames { get; }

    /// <summary>
    /// Gets or sets the JPEG/MIFF/PNG compression level (default 75).
    /// </summary>
    int Quality { get; set; }

    /// <summary>
    /// Gets or sets the type of rendering intent.
    /// </summary>
    RenderingIntent RenderingIntent { get; set; }

    /// <summary>
    /// Gets the signature of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string Signature { get; }

    /// <summary>
    /// Gets the number of colors in the image.
    /// </summary>
    int TotalColors { get; }

    /// <summary>
    /// Gets or sets the virtual pixel method.
    /// </summary>
    VirtualPixelMethod VirtualPixelMethod { get; set; }

    /// <summary>
    /// Gets the width of the image.
    /// </summary>
    int Width { get; }

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
    void AdaptiveResize(int width, int height);

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
    void AdaptiveThreshold(int width, int height);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(int width, int height, Channels channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(int width, int height, double bias);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="bias">Constant to subtract from pixel neighborhood mean (+/-)(0-QuantumRange).</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(int width, int height, double bias, Channels channels);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(int width, int height, Percentage biasPercentage);

    /// <summary>
    /// Local adaptive threshold image.
    /// http://www.dai.ed.ac.uk/HIPR2/adpthrsh.htm.
    /// </summary>
    /// <param name="width">The width of the pixel neighborhood.</param>
    /// <param name="height">The height of the pixel neighborhood.</param>
    /// <param name="biasPercentage">Constant to subtract from pixel neighborhood mean.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AdaptiveThreshold(int width, int height, Percentage biasPercentage, Channels channels);

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
    /// Affine Transform image.
    /// </summary>
    /// <param name="affineMatrix">The affine matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AffineTransform(IDrawableAffine affineMatrix);

    /// <summary>
    /// Applies the specified alpha option.
    /// </summary>
    /// <param name="value">The option to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Alpha(AlphaOption value);

    /// <summary>
    /// Annotate using specified text, and bounding area.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity);

    /// <summary>
    /// Annotate using specified text, bounding area, and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="boundingArea">The bounding area.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="angle">The rotation.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, IMagickGeometry boundingArea, Gravity gravity, double angle);

    /// <summary>
    /// Annotate with text (bounding area is entire image) and placement gravity.
    /// </summary>
    /// <param name="text">The text to use.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Annotate(string text, Gravity gravity);

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoGamma();

    /// <summary>
    /// Extracts the 'mean' from the image and adjust the image to try make set its gamma appropriatally.
    /// </summary>
    /// <param name="channels">The channel(s) to set the gamma for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoGamma(Channels channels);

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoLevel();

    /// <summary>
    /// Adjusts the levels of a particular image channel by scaling the minimum and maximum values
    /// to the full quantum range.
    /// </summary>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoLevel(Channels channels);

    /// <summary>
    /// Adjusts an image so that its orientation is suitable for viewing.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoOrient();

    /// <summary>
    /// Automatically selects a threshold and replaces each pixel in the image with a black pixel if
    /// the image intentsity is less than the selected threshold otherwise white.
    /// </summary>
    /// <param name="method">The threshold method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void AutoThreshold(AutoThresholdMethod method);

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>\
    void BilateralBlur(int width, int height);

    /// <summary>
    /// Applies a non-linear, edge-preserving, and noise-reducing smoothing filter.
    /// </summary>
    /// <param name="width">The width of the neighborhood in pixels.</param>
    /// <param name="height">The height of the neighborhood in pixels.</param>
    /// <param name="intensitySigma">The sigma in the intensity space.</param>
    /// <param name="spatialSigma">The sigma in the coordinate space.</param>
    void BilateralBlur(int width, int height, double intensitySigma, double spatialSigma);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlackThreshold(Percentage threshold);

    /// <summary>
    /// Forces all pixels below the threshold into black while leaving all pixels at or above
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BlackThreshold(Percentage threshold, Channels channels);

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
    /// Blur image the specified channel of the image with the default blur factor (0x1).
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
    /// Blur image with specified blur factor and channel.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="channels">The channel(s) that should be blurred.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Blur(double radius, double sigma, Channels channels);

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="size">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(int size);

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="width">The width of the border.</param>
    /// <param name="height">The height of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(int width, int height);

    /// <summary>
    /// Border image (add border to image).
    /// </summary>
    /// <param name="percentage">The size of the border.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Border(Percentage percentage);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BrightnessContrast(Percentage brightness, Percentage contrast);

    /// <summary>
    /// Changes the brightness and/or contrast of an image. It converts the brightness and
    /// contrast parameters into slope and intercept and calls a polynomical function to apply
    /// to the image.
    /// </summary>
    /// <param name="brightness">The brightness.</param>
    /// <param name="contrast">The contrast.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void BrightnessContrast(Percentage brightness, Percentage contrast, Channels channels);

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
    /// <param name="offset">The X offset from origin.</param>
    /// <param name="width">The width of the part to chop horizontally.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ChopHorizontal(int offset, int width);

    /// <summary>
    /// Chop image (remove horizontal subregion of image).
    /// </summary>
    /// <param name="offset">The Y offset from origin.</param>
    /// <param name="height">The height of the part to chop vertically.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ChopVertical(int offset, int height);

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The percentage of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The percentage of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    void Clahe(Percentage xTiles, Percentage yTiles, int numberBins, double clipLimit);

    /// <summary>
    /// A variant of adaptive histogram equalization in which the contrast amplification is limited,
    /// so as to reduce this problem of noise amplification.
    /// </summary>
    /// <param name="xTiles">The number of tile divisions to use in horizontal direction.</param>
    /// <param name="yTiles">The number of tile divisions to use in vertical direction.</param>
    /// <param name="numberBins">The number of bins for histogram ("dynamic range").</param>
    /// <param name="clipLimit">The contrast limit for localised changes in contrast. A limit less than 1
    /// results in standard non-contrast limited AHE.</param>
    void Clahe(int xTiles, int yTiles, int numberBins, double clipLimit);

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clamp();

    /// <summary>
    /// Set each pixel whose value is below zero to zero and any the pixel whose value is above
    /// the quantum range to the quantum range (Quantum.Max) otherwise the pixel value
    /// remains unchanged.
    /// </summary>
    /// <param name="channels">The channel(s) to clamp.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clamp(Channels channels);

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clip();

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect inside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clip(string pathName);

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ClipOutside();

    /// <summary>
    /// Sets the image clip mask based on any clipping path information if it exists. The clipping
    /// path can be removed with <see cref="RemoveWriteMask"/>. This operating takes effect outside
    /// the clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use
    /// clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ClipOutside(string pathName);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image, PixelInterpolateMethod method);

    /// <summary>
    /// Apply a color lookup table (CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <param name="channels">The channel(s) to clut.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Clut(IMagickImage image, PixelInterpolateMethod method, Channels channels);

    /// <summary>
    /// Applies the color decision list from the specified ASC CDL file.
    /// </summary>
    /// <param name="fileName">The file to read the ASC CDL information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ColorDecisionList(string fileName);

    /// <summary>
    /// Apply a color matrix to the image channels.
    /// </summary>
    /// <param name="matrix">The color matrix to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ColorMatrix(IMagickColorMatrix matrix);

    /// <summary>
    /// Compare current image with another image and returns error information.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Compare(IMagickImage image);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric, Channels channels);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric, IMagickImage difference);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="difference">The image that will contain the difference.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <returns>The distortion based on the specified metric.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    double Compare(IMagickImage image, ErrorMetric metric, IMagickImage difference, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Channels channels);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, int x, int y, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose);

    /// <summary>
    /// Compose an image onto another at specified offset using the 'In' operator.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, Channels channels);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args);

    /// <summary>
    /// Compose an image onto another at specified offset using the specified algorithm.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <param name="channels">The channel(s) to composite.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Composite(IMagickImage image, Gravity gravity, int x, int y, CompositeOperator compose, string? args, Channels channels);

    /// <summary>
    /// Contrast image (enhance intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Contrast();

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint, Percentage whitePoint);

    /// <summary>
    /// A simple image enhancement technique that attempts to improve the contrast in an image by
    /// 'stretching' the range of intensity values it contains to span a desired range of values.
    /// It differs from the more sophisticated histogram equalization in that it can only apply a
    /// linear scaling function to the image pixel values. As a result the 'enhancement' is less harsh.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <param name="channels">The channel(s) to constrast stretch.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ContrastStretch(Percentage blackPoint, Percentage whitePoint, Channels channels);

    /// <summary>
    /// Returns the convex hull points of an image canvas.
    /// </summary>
    /// <returns>The convex hull points of an image canvas.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IEnumerable<PointD> ConvexHull();

    /// <summary>
    /// Convolve image. Applies a user-specified convolution to the image.
    /// </summary>
    /// <param name="matrix">The convolution matrix.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Convolve(IConvolveMatrix matrix);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, Channels channels);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry);

    /// <summary>
    /// Copies pixels from the source image to the destination image.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, Channels channels);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to start the copy from.</param>
    /// <param name="y">The Y offset to start the copy from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y);

    /// <summary>
    /// Copies pixels from the source image as defined by the geometry the destination image at
    /// the specified offset.
    /// </summary>
    /// <param name="source">The source image to copy the pixels from.</param>
    /// <param name="geometry">The geometry to copy.</param>
    /// <param name="x">The X offset to copy the pixels to.</param>
    /// <param name="y">The Y offset to copy the pixels to.</param>
    /// <param name="channels">The channels to copy.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CopyPixels(IMagickImage source, IMagickGeometry geometry, int x, int y, Channels channels);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(int width, int height);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="width">The width of the subregion.</param>
    /// <param name="height">The height of the subregion.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(int width, int height, Gravity gravity);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(IMagickGeometry geometry);

    /// <summary>
    /// Crop image (subregion of original image). RePage should be called unless the Page information
    /// is needed.
    /// </summary>
    /// <param name="geometry">The subregion to crop.</param>
    /// <param name="gravity">The position where the cropping should start from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Crop(IMagickGeometry geometry, Gravity gravity);

    /// <summary>
    /// Displaces an image's colormap by a given number of positions.
    /// </summary>
    /// <param name="amount">Displace the colormap this amount.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void CycleColormap(int amount);

    /// <summary>
    /// Converts cipher pixels to plain pixels.
    /// </summary>
    /// <param name="passphrase">The password that was used to encrypt the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Decipher(string passphrase);

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
    /// from 0 to QuantumRange.
    /// </summary>
    /// <param name="settings">The deskew settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The angle that was used.</returns>
    double Deskew(IDeskewSettings settings);

    /// <summary>
    /// Despeckle image (reduce speckle noise).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Despeckle();

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components). Use the Depth
    /// property to get the current value.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components).</returns>
    int DetermineBitDepth();

    /// <summary>
    /// Determines the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="channels">The channel to get the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The bit depth (bits allocated to red/green/blue components) of the specified channel.</returns>
    int DetermineBitDepth(Channels channels);

    /// <summary>
    /// Determines the color type of the image. This method can be used to automatically make the
    /// type GrayScale.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The color type of the image.</returns>
    ColorType DetermineColorType();

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
    /// <param name="method">The distortion method to use.</param>
    /// <param name="settings">The settings for the distort operation.</param>
    /// <param name="arguments">An array containing the arguments for the distortion.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Distort(DistortMethod method, IDistortSettings settings, params double[] arguments);

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Draw(params IDrawable[] drawables);

    /// <summary>
    /// Draw on image using a collection of drawables.
    /// </summary>
    /// <param name="drawables">The drawables to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Draw(IEnumerable<IDrawable> drawables);

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
    /// Converts pixels to cipher-pixels.
    /// </summary>
    /// <param name="passphrase">The password that to encrypt the image with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Encipher(string passphrase);

    /// <summary>
    /// Applies a digital filter that improves the quality of a noisy image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Enhance();

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Equalize();

    /// <summary>
    /// Applies a histogram equalization to the image.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Equalize(Channels channels);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateFunction">The function.</param>
    /// <param name="arguments">The arguments for the function.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateFunction evaluateFunction, params double[] arguments);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateOperator evaluateOperator, double value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, EvaluateOperator evaluateOperator, Percentage percentage);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="value">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, double value);

    /// <summary>
    /// Apply an arithmetic or bitwise operator to the image pixel quantums.
    /// </summary>
    /// <param name="channels">The channel(s) to apply the operator on.</param>
    /// <param name="geometry">The geometry to use.</param>
    /// <param name="evaluateOperator">The operator.</param>
    /// <param name="percentage">The value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Evaluate(Channels channels, IMagickGeometry geometry, EvaluateOperator evaluateOperator, Percentage percentage);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(int width, int height);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(int x, int y, int width, int height);

    /// <summary>
    /// Extend the image as defined by the width and height.
    /// </summary>
    /// <param name="width">The width to extend the image to.</param>
    /// <param name="height">The height to extend the image to.</param>
    /// <param name="gravity">The placement gravity.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Extent(int width, int height, Gravity gravity);

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
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text);

    /// <summary>
    /// Obtain font metrics for text string given current font, pointsize, and density settings.
    /// </summary>
    /// <param name="text">The text to get the font metrics for.</param>
    /// <param name="ignoreNewlines">Specifies if newlines should be ignored.</param>
    /// <returns>The font metrics for text.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    ITypeMetric? FontTypeMetrics(string text, bool ignoreNewlines);

    /// <summary>
    /// Formats the specified expression, more info here: http://www.imagemagick.org/script/escape.php.
    /// </summary>
    /// <param name="expression">The expression, more info here: http://www.imagemagick.org/script/escape.php.</param>
    /// <returns>The result of the expression.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? FormatExpression(string expression);

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
    void Frame(int width, int height);

    /// <summary>
    /// Frame image with the specified with, height, innerBevel and outerBevel.
    /// </summary>
    /// <param name="width">The width of the frame.</param>
    /// <param name="height">The height of the frame.</param>
    /// <param name="innerBevel">The inner bevel of the frame.</param>
    /// <param name="outerBevel">The outer bevel of the frame.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Frame(int width, int height, int innerBevel, int outerBevel);

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Fx(string expression);

    /// <summary>
    /// Applies a mathematical expression to the image.
    /// </summary>
    /// <param name="expression">The expression to apply.</param>
    /// <param name="channels">The channel(s) to apply the expression to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Fx(string expression, Channels channels);

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GammaCorrect(double gamma);

    /// <summary>
    /// Gamma correct image.
    /// </summary>
    /// <param name="gamma">The image gamma for the channel.</param>
    /// <param name="channels">The channel(s) to gamma correct.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void GammaCorrect(double gamma, Channels channels);

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
    /// Retrieve the 8bim profile from the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    /// <returns>The 8bim profile from the image.</returns>
    IEightBimProfile? Get8BimProfile();

    /// <summary>
    /// Returns the value of the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <returns>The value of the artifact with the specified name.</returns>
    string? GetArtifact(string name);

    /// <summary>
    /// Returns the value of a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <returns>The value of a named image attribute.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetAttribute(string name);

    /// <summary>
    /// Returns the default clipping path. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <returns>The default clipping path. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetClippingPath();

    /// <summary>
    /// Returns the clipping path with the specified name. Null will be returned if the image has no clipping path.
    /// </summary>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <returns>The clipping path with the specified name. Null will be returned if the image has no clipping path.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    string? GetClippingPath(string pathName);

    /// <summary>
    /// Retrieve the color profile from the image.
    /// </summary>
    /// <returns>The color profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IColorProfile? GetColorProfile();

    /// <summary>
    /// Retrieve the exif profile from the image.
    /// </summary>
    /// <returns>The exif profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IExifProfile? GetExifProfile();

    /// <summary>
    /// Retrieve the iptc profile from the image.
    /// </summary>
    /// <returns>The iptc profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IIptcProfile? GetIptcProfile();

    /// <summary>
    /// Retrieve a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <returns>A named profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IImageProfile? GetProfile(string name);

    /// <summary>
    /// Retrieve the xmp profile from the image.
    /// </summary>
    /// <returns>The xmp profile from the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IXmpProfile? GetXmpProfile();

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Grayscale();

    /// <summary>
    /// Converts the colors in the image to gray.
    /// </summary>
    /// <param name="method">The pixel intensity method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Grayscale(PixelIntensityMethod method);

    /// <summary>
    /// Apply a color lookup table (Hald CLUT) to the image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void HaldClut(IMagickImage image);

    /// <summary>
    /// Gets a value indicating whether a profile with the specified name already exists on the image.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <returns>A value indicating whether a profile with the specified name already exists on the image.</returns>
    bool HasProfile(string name);

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
    void HoughLine(int width, int height, int threshold);

    /// <summary>
    /// Implode image (special effect).
    /// </summary>
    /// <param name="amount">The extent of the implosion.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Implode(double amount, PixelInterpolateMethod method);

    /// <summary>
    /// Import pixels from the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(byte[] data, IPixelImportSettings settings);

    /// <summary>
    /// Import pixels from the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(byte[] data, int offset, IPixelImportSettings settings);

    /// <summary>
    /// Resize image to specified size using the specified interpolation method.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InterpolativeResize(int width, int height, PixelInterpolateMethod method);

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
    /// Inverse contrast image (diminish intensity differences in image).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseContrast();

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="midpoint">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(Percentage blackPointPercentage, Percentage whitePointPercentage, double midpoint, Channels channels);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, double midpoint);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, double midpoint, Channels channels);

    /// <summary>
    /// Adjust the image contrast with an inverse non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseSigmoidalContrast(double contrast, Percentage midpointPercentage);

    /// <summary>
    /// Applies k-means color reduction to an image. This is a colorspace clustering or segmentation technique.
    /// </summary>
    /// <param name="settings">The kmeans settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Kmeans(IKmeansSettings settings);

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
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, Channels channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPointPercentage">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePointPercentage">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(Percentage blackPointPercentage, Percentage whitePointPercentage, double gamma, Channels channels);

    /// <summary>
    /// Discards any pixels below the black point and above the white point and levels the remaining pixels.
    /// </summary>
    /// <param name="blackPoint">The black point.</param>
    /// <param name="whitePoint">The white point.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LinearStretch(Percentage blackPoint, Percentage whitePoint);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(int width, int height);

    /// <summary>
    /// Rescales image with seam carving.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <param name="deltaX">Maximum seam transversal step (0 means straight seams).</param>
    /// <param name="rigidity">Introduce a bias for non-straight seams (typically 0).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LiquidRescale(int width, int height, double deltaX, double rigidity);

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
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LocalContrast(double radius, Percentage strength);

    /// <summary>
    /// Local contrast enhancement.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="strength">The strength of the blur mask.</param>
    /// <param name="channels">The channel(s) that should be changed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LocalContrast(double radius, Percentage strength, Channels channels);

    /// <summary>
    /// Lower image (darken the edges of an image to give a 3-D lowered effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Lower(int size);

    /// <summary>
    /// Magnify image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Magnify();

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Map(IMagickImage image);

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Map(IMagickImage image, IQuantizeSettings settings);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    void MeanShift(int size);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="size">The width and height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    void MeanShift(int size, Percentage colorDistance);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    void MeanShift(int width, int height);

    /// <summary>
    /// Delineate arbitrarily shaped clusters in the image.
    /// </summary>
    /// <param name="width">The width of the pixels neighborhood.</param>
    /// <param name="height">The height of the pixels neighborhood.</param>
    /// <param name="colorDistance">The color distance.</param>
    void MeanShift(int width, int height, Percentage colorDistance);

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MedianFilter();

    /// <summary>
    /// Filter image by replacing each pixel component with the median color in a circular neighborhood.
    /// </summary>
    /// <param name="radius">The radius of the pixel neighborhood.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MedianFilter(int radius);

    /// <summary>
    /// Reduce image by integral size.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Minify();

    /// <summary>
    /// Returns the points that form the minimum bounding box around the image foreground objects with
    /// the "Rotating Calipers" algorithm. he method also returns these properties: minimum-bounding-box:area,
    /// minimum-bounding-box:width, minimum-bounding-box:height, and minimum-bounding-box:angle.
    /// </summary>
    /// <returns>The points that form the minimum bounding box around the image foreground objects.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IEnumerable<PointD> MinimumBoundingBox();

    /// <summary>
    /// Modulate percent brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness);

    /// <summary>
    /// Modulate percent saturation and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness, Percentage saturation);

    /// <summary>
    /// Modulate percent hue, saturation, and brightness of an image.
    /// </summary>
    /// <param name="brightness">The brightness percentage.</param>
    /// <param name="saturation">The saturation percentage.</param>
    /// <param name="hue">The hue percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Modulate(Percentage brightness, Percentage saturation, Percentage hue);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, Channels channels);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, Channels channels, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, string? arguments);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, Channels channels);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, Channels channels, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="kernel">Built-in kernel.</param>
    /// <param name="arguments">Kernel arguments.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, Kernel kernel, string? arguments, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, string userKernel);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, string userKernel, Channels channels);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="channels">The channels to apply the kernel to.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, string userKernel, Channels channels, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology method.
    /// </summary>
    /// <param name="method">The morphology method.</param>
    /// <param name="userKernel">User suplied kernel.</param>
    /// <param name="iterations">The number of iterations.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(MorphologyMethod method, string userKernel, int iterations);

    /// <summary>
    /// Applies a kernel to the image according to the given mophology settings.
    /// </summary>
    /// <param name="settings">The morphology settings.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Morphology(IMorphologySettings settings);

    /// <summary>
    /// Returns the normalized moments of one or more image channels.
    /// </summary>
    /// <returns>The normalized moments of one or more image channels.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMoments Moments();

    /// <summary>
    /// Motion blur image with specified blur factor.
    /// </summary>
    /// <param name="radius">The radius of the Gaussian, in pixels, not counting the center pixel.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <param name="angle">The angle the object appears to be comming from (zero degrees is from the right).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void MotionBlur(double radius, double sigma, double angle);

    /// <summary>
    /// Negate colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Negate();

    /// <summary>
    /// Negate colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Negate(Channels channels);

    /// <summary>
    /// Negate the grayscale colors in image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void NegateGrayscale();

    /// <summary>
    /// Negate the grayscale colors in image for the specified channel.
    /// </summary>
    /// <param name="channels">The channel(s) that should be negated.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void NegateGrayscale(Channels channels);

    /// <summary>
    /// Normalize image (increase contrast by normalizing the pixel values to span the full range
    /// of color values).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Normalize();

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    void OilPaint();

    /// <summary>
    /// Oilpaint image (image looks like oil painting).
    /// </summary>
    /// <param name="radius">The radius of the circular neighborhood.</param>
    /// <param name="sigma">The standard deviation of the Laplacian, in pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OilPaint(double radius, double sigma);

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OrderedDither(string thresholdMap);

    /// <summary>
    /// Perform a ordered dither based on a number of pre-defined dithering threshold maps, but over
    /// multiple intensity levels.
    /// </summary>
    /// <param name="thresholdMap">A string containing the name of the threshold dither map to use,
    /// followed by zero or more numbers representing the number of color levels tho dither between.</param>
    /// <param name="channels">The channel(s) to dither.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void OrderedDither(string thresholdMap, Channels channels);

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Perceptible(double epsilon);

    /// <summary>
    /// Set each pixel whose value is less than epsilon to epsilon or -epsilon (whichever is closer)
    /// otherwise the pixel value remains unchanged.
    /// </summary>
    /// <param name="epsilon">The epsilon threshold.</param>
    /// <param name="channels">The channel(s) to perceptible.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Perceptible(double epsilon, Channels channels);

    /// <summary>
    /// Returns the perceptual hash of this image.
    /// </summary>
    /// <returns>The perceptual hash of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IPerceptualHash? PerceptualHash();

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, int offset, int count);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(FileInfo file);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(Stream stream);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(string fileName);

    /// <summary>
    /// Simulates a polaroid picture.
    /// </summary>
    /// <param name="caption">The caption to put on the image.</param>
    /// <param name="angle">The angle of image.</param>
    /// <param name="method">Pixel interpolate method.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Polaroid(string caption, double angle, PixelInterpolateMethod method);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, Channels channels);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, DitherMethod method);

    /// <summary>
    /// Reduces the image to a limited number of colors for a "poster" effect.
    /// </summary>
    /// <param name="levels">Number of color levels allowed in each channel.</param>
    /// <param name="method">Dither method to use.</param>
    /// <param name="channels">The channel(s) to posterize.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Posterize(int levels, DitherMethod method, Channels channels);

    /// <summary>
    /// Sets an internal option to preserve the color type.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void PreserveColorType();

    /// <summary>
    /// Quantize image (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error information.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo? Quantize(IQuantizeSettings settings);

    /// <summary>
    /// Raise image (lighten the edges of an image to give a 3-D raised effect).
    /// </summary>
    /// <param name="size">The size of the edges.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Raise(int size);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(Percentage percentageLow, Percentage percentageHigh);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="percentageLow">The low threshold.</param>
    /// <param name="percentageHigh">The high threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(Percentage percentageLow, Percentage percentageHigh, Channels channels);

    /// <summary>
    /// Applies soft and hard thresholding.
    /// </summary>
    /// <param name="percentageLowBlack">Defines the minimum black threshold value.</param>
    /// <param name="percentageLowWhite">Defines the minimum white threshold value.</param>
    /// <param name="percentageHighWhite">Defines the maximum white threshold value.</param>
    /// <param name="percentageHighBlack">Defines the maximum black threshold value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RangeThreshold(Percentage percentageLowBlack, Percentage percentageLowWhite, Percentage percentageHighWhite, Percentage percentageHighBlack);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, int offset, int count);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, int offset, int count, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, int width, int height);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, int width, int height);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReduceNoise();

    /// <summary>
    /// Reduce noise in image using a noise peak elimination filter.
    /// </summary>
    /// <param name="order">The order to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReduceNoise(int order);

    /// <summary>
    /// Associates a mask with the image as defined by the specified region.
    /// </summary>
    /// <param name="region">The mask region.</param>
    void RegionMask(IMagickGeometry region);

    /// <summary>
    /// Removes the artifact with the specified name.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    void RemoveArtifact(string name);

    /// <summary>
    /// Removes the attribute with the specified name.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    void RemoveAttribute(string name);

    /// <summary>
    /// Removes the region mask of the image.
    /// </summary>
    void RemoveRegionMask();

    /// <summary>
    /// Remove a profile from the image.
    /// </summary>
    /// <param name="profile">The profile to remove.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    public void RemoveProfile(IImageProfile profile);

    /// <summary>
    /// Remove a named profile from the image.
    /// </summary>
    /// <param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RemoveProfile(string name);

    /// <summary>
    /// Removes the associated read mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RemoveReadMask();

    /// <summary>
    /// Removes the associated write mask of the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RemoveWriteMask();

    /// <summary>
    /// Resets the page property of this image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RePage();

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
    /// <param name="density">The density to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resample(PointD density);

    /// <summary>
    /// Resize image to specified size.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Resize(int width, int height);

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

    /// <summary>
    /// Roll image (rolls image vertically and horizontally).
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
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
    void Sample(int width, int height);

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
    void Scale(int width, int height);

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
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Segment();

    /// <summary>
    /// Segment (coalesce similar image components) by analyzing the histograms of the color
    /// components and identifying units that are homogeneous with the fuzzy c-means technique.
    /// Also uses QuantizeColorSpace and Verbose image attributes.
    /// </summary>
    /// <param name="quantizeColorSpace">Quantize colorspace.</param>
    /// <param name="clusterThreshold">This represents the minimum number of pixels contained in
    /// a hexahedra before it can be considered valid (expressed as a percentage).</param>
    /// <param name="smoothingThreshold">The smoothing threshold eliminates noise in the second
    /// derivative of the histogram. As the value is increased, you can expect a smoother second
    /// derivative.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Segment(ColorSpace quantizeColorSpace, double clusterThreshold, double smoothingThreshold);

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
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="value">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetArtifact(string name, string value);

    /// <summary>
    /// Inserts the artifact with the specified name and value into the artifact tree of the image.
    /// </summary>
    /// <param name="name">The name of the artifact.</param>
    /// <param name="flag">The value of the artifact.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetArtifact(string name, bool flag);

    /// <summary>
    /// Lessen (or intensify) when adding noise to an image.
    /// </summary>
    /// <param name="attenuate">The attenuate value.</param>
    void SetAttenuate(double attenuate);

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetAttribute(string name, string value);

    /// <summary>
    /// Sets a named image attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="flag">The value of the attribute.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetAttribute(string name, bool flag);

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components).
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetBitDepth(int value);

    /// <summary>
    /// Set the bit depth (bits allocated to red/green/blue components) of the specified channel.
    /// </summary>
    /// <param name="value">The depth.</param>
    /// <param name="channels">The channel to set the depth for.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetBitDepth(int value, Channels channels);

    /// <summary>
    /// Sets the default clipping path.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetClippingPath(string value);

    /// <summary>
    /// Sets the clipping path with the specified name.
    /// </summary>
    /// <param name="value">The clipping path.</param>
    /// <param name="pathName">Name of clipping path resource. If name is preceded by #, use clipping path numbered by name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetClippingPath(string value, string pathName);

    /// <summary>
    /// Gets the compression of the image. This method should only be used when the encoder uses the compression of the image. For
    /// most usecases Setting.Compression should be used instead.
    /// </summary>
    /// <param name="compression">The compression method.</param>
    void SetCompression(CompressionMethod compression);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IImageProfile profile);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IColorProfile profile);

    /// <summary>
    /// Set the specified profile of the image. If a profile with the same name already exists it will be overwritten.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetProfile(IColorProfile profile, ColorTransformMode mode);

    /// <summary>
    /// Sets the associated read mask of the image. The mask must be the same dimensions as the image and
    /// only contain the colors black and white.
    /// </summary>
    /// <param name="image">The image that contains the read mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetReadMask(IMagickImage image);

    /// <summary>
    /// Sets the associated write mask of the image. The mask must be the same dimensions as the image and
    /// only contains the colors black and white or have grayscale values that will cause blended updates of
    /// the image.
    /// </summary>
    /// <param name="image">The image that contains the write mask.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetWriteMask(IMagickImage image);

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
    void Shave(int size);

    /// <summary>
    /// Shave pixels from image edges.
    /// </summary>
    /// <param name="leftRight">The number of pixels to shave left and right.</param>
    /// <param name="topBottom">The number of pixels to shave top and bottom.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shave(int leftRight, int topBottom);

    /// <summary>
    /// Shear image (create parallelogram by sliding image by X or Y axis).
    /// </summary>
    /// <param name="xAngle">Specifies the number of x degrees to shear the image.</param>
    /// <param name="yAngle">Specifies the number of y degrees to shear the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Shear(double xAngle, double yAngle);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, double midpoint);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpoint">The midpoint to use.</param>
    /// <param name="channels">The channel(s) that should be adjusted.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, double midpoint, Channels channels);

    /// <summary>
    /// Adjust the image contrast with a non-linear sigmoidal contrast algorithm.
    /// </summary>
    /// <param name="contrast">The contrast to use.</param>
    /// <param name="midpointPercentage">The midpoint to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SigmoidalContrast(double contrast, Percentage midpointPercentage);

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
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize();

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factor">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize(double factor);

    /// <summary>
    /// Sort pixels within each scanline in ascending order of intensity.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SortPixels();

    /// <summary>
    /// Solarize image (similar to effect seen when exposing a photographic film to light during
    /// the development process).
    /// </summary>
    /// <param name="factorPercentage">The factor to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Solarize(Percentage factorPercentage);

    /// <summary>
    /// Splice the background color into the image.
    /// </summary>
    /// <param name="geometry">The geometry to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Splice(IMagickGeometry geometry);

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
    void Statistic(StatisticType type, int width, int height);

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IStatistics Statistics();

    /// <summary>
    /// Returns the image statistics.
    /// </summary>
    /// <returns>The image statistics.</returns>
    /// <param name="channels">The channel(s) that should be used.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IStatistics Statistics(Channels channels);

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
    /// Strips an image of all profiles and comments.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Strip();

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
    /// Channel a texture on image background.
    /// </summary>
    /// <param name="image">The image to use as a texture on the image background.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Texture(IMagickImage image);

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Threshold(Percentage percentage);

    /// <summary>
    /// Threshold image.
    /// </summary>
    /// <param name="percentage">The threshold percentage.</param>
    /// <param name="channels">The channel(s) that should be thresholded.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Threshold(Percentage percentage, Channels channels);

    /// <summary>
    /// Resize image to thumbnail size and remove all the image profiles except the icc/icm profile.
    /// <para />
    /// Resize will fit the image into the requested size. It does NOT fill, the requested box size.
    /// Use the <see cref="IMagickGeometry"/> overload for more control over the resulting size.
    /// </summary>
    /// <param name="width">The new width.</param>
    /// <param name="height">The new height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Thumbnail(int width, int height);

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
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Tile(IMagickImage image, CompositeOperator compose);

    /// <summary>
    /// Compose an image repeated across and down the image.
    /// </summary>
    /// <param name="image">The image to composite with this image.</param>
    /// <param name="compose">The algorithm to use.</param>
    /// <param name="args">The arguments for the algorithm (compose:args).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Tile(IMagickImage image, CompositeOperator compose, string? args);

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64();

    /// <summary>
    /// Converts this instance to a base64 <see cref="string"/>.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A base64 <see cref="string"/>.</returns>
    string ToBase64(MagickFormat format);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    byte[] ToByteArray();

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(IWriteDefines defines);

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="byte"/> array.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    byte[] ToByteArray(MagickFormat format);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile target);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. This
    /// requires the image to have a color profile. Nothing will happen if the image has no color profile.
    /// </summary>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile target, ColorTransformMode mode);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile source, IColorProfile target);

    /// <summary>
    /// Transforms the image from the colorspace of the source profile to the target profile. The
    /// source profile will only be used if the image does not contain a color profile. Nothing
    /// will happen if the source profile has a different colorspace then that of the image.
    /// </summary>
    /// <param name="source">The source color profile.</param>
    /// <param name="target">The target color profile.</param>
    /// <param name="mode">The color transformation mode.</param>
    /// <returns>True when the colorspace was transformed otherwise false.</returns>
    bool TransformColorSpace(IColorProfile source, IColorProfile target, ColorTransformMode mode);

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
    /// Trim edges that are the background color from the image.
    /// </summary>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim();

    /// <summary>
    /// Trim the specified edges that are the background color from the image.
    /// </summary>
    /// <param name="edges">The edges that need to be trimmed.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim(params Gravity[] edges);

    /// <summary>
    /// Trim edges that are the background color from the image.
    /// </summary>
    /// <param name="percentBackground">The percentage of background pixels permitted in the outer rows and columns.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Trim(Percentage percentBackground);

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
    /// <param name="method">Pixel interpolate method.</param>
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

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    void WhiteBalance();

    /// <summary>
    /// Apply a white balancing to an image according to a grayworld assumption in the LAB colorspace.
    /// </summary>
    /// <param name="vibrance">The vibrance.</param>
    void WhiteBalance(Percentage vibrance);

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void WhiteThreshold(Percentage threshold);

    /// <summary>
    /// Forces all pixels above the threshold into white while leaving all pixels at or below
    /// the threshold unchanged.
    /// </summary>
    /// <param name="threshold">The threshold to use.</param>
    /// <param name="channels">The channel(s) to make black.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void WhiteThreshold(Percentage threshold, Channels channels);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Write(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(FileInfo file, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(Stream stream, MagickFormat format, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, IWriteDefines defines, CancellationToken cancellationToken);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format);

    /// <summary>
    /// Writes the image to the specified file name.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="format">The format to use.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task WriteAsync(string fileName, MagickFormat format, CancellationToken cancellationToken);
}
