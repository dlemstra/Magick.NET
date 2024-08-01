// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick.Drawing;

namespace ImageMagick;

/// <summary>
/// Interface that represents an ImageMagick image.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickImage<TQuantumType> : IMagickImage, IComparable<IMagickImage<TQuantumType>?>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets or sets the background color of the image.
    /// </summary>
    IMagickColor<TQuantumType>? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border color of the image.
    /// </summary>
    IMagickColor<TQuantumType>? BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the matte color.
    /// </summary>
    IMagickColor<TQuantumType>? MatteColor { get; set; }

    /// <summary>
    /// Gets the settings for this instance.
    /// </summary>
    IMagickSettings<TQuantumType> Settings { get; }

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <returns>A clone of the current image.</returns>
    IMagickImage<TQuantumType> Clone();

    /// <summary>
    /// Creates a clone of the current image with the specified geometry.
    /// </summary>
    /// <param name="geometry">The area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Clone(IMagickGeometry geometry);

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="width">The width of the area to clone.</param>
    /// <param name="height">The height of the area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    IMagickImage<TQuantumType> Clone(uint width, uint height);

    /// <summary>
    /// Creates a clone of the current image.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width of the area to clone.</param>
    /// <param name="height">The height of the area to clone.</param>
    /// <returns>A clone of the current image.</returns>
    IMagickImage<TQuantumType> Clone(int x, int y, uint width, uint height);

    /// <summary>
    /// Sets the alpha channel to the specified color.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ColorAlpha(IMagickColor<TQuantumType> color);

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
    /// Forces all pixels in the color range to white otherwise black.
    /// </summary>
    /// <param name="startColor">The start color of the color range.</param>
    /// <param name="stopColor">The stop color of the color range.</param>
    void ColorThreshold(IMagickColor<TQuantumType> startColor, IMagickColor<TQuantumType> stopColor);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="distortion">The distortion based on the specified metric.</param>
    /// <returns>The image that contains the difference.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Compare(IMagickImage image, ErrorMetric metric, out double distortion);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <param name="distortion">The distortion based on the specified metric.</param>
    /// <returns>The image that contains the difference.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Compare(IMagickImage image, ErrorMetric metric, Channels channels, out double distortion);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="settings">The settings to use.</param>
    /// <param name="distortion">The distortion based on the specified metric.</param>
    /// <returns>The image that contains the difference.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Compare(IMagickImage image, ICompareSettings<TQuantumType> settings, out double distortion);

    /// <summary>
    /// Returns the distortion based on the specified metric.
    /// </summary>
    /// <param name="image">The other image to compare with this image.</param>
    /// <param name="settings">The settings to use.</param>
    /// <param name="channels">The channel(s) to compare.</param>
    /// <param name="distortion">The distortion based on the specified metric.</param>
    /// <returns>The image that contains the difference.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType> Compare(IMagickImage image, ICompareSettings<TQuantumType> settings, Channels channels, out double distortion);

    /// <summary>
    /// Determines the connected-components of the image.
    /// </summary>
    /// <param name="connectivity">How many neighbors to visit, choose from 4 or 8.</param>
    /// <returns>The connected-components of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IReadOnlyList<IConnectedComponent<TQuantumType>> ConnectedComponents(uint connectivity);

    /// <summary>
    /// Determines the connected-components of the image.
    /// </summary>
    /// <param name="settings">The settings for this operation.</param>
    /// <returns>The connected-components of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IReadOnlyList<IConnectedComponent<TQuantumType>> ConnectedComponents(IConnectedComponentsSettings settings);

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="width">The width of the tile.</param>
    /// <param name="height">The height of the tile.</param>
    /// <returns>New title of the current image.</returns>
    IReadOnlyList<IMagickImage<TQuantumType>> CropToTiles(uint width, uint height);

    /// <summary>
    /// Creates tiles of the current image in the specified dimension.
    /// </summary>
    /// <param name="geometry">The size of the tile.</param>
    /// <returns>New title of the current image.</returns>
    IReadOnlyList<IMagickImage<TQuantumType>> CropToTiles(IMagickGeometry geometry);

    /// <summary>
    /// Draw on image using one or more drawables.
    /// </summary>
    /// <param name="drawables">The drawable(s) to draw on the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Draw(IDrawables<TQuantumType> drawables);

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
    /// Floodfill pixels matching color (within fuzz factor) of target pixel(x,y) with replacement
    /// alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void FloodFill(TQuantumType alpha, int x, int y);

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void FloodFill(IMagickColor<TQuantumType> color, int x, int y);

    /// <summary>
    /// Flood-fill color across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void FloodFill(IMagickColor<TQuantumType> color, int x, int y, IMagickColor<TQuantumType> target);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void FloodFill(IMagickImage<TQuantumType> image, int x, int y);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void FloodFill(IMagickImage<TQuantumType> image, int x, int y, IMagickColor<TQuantumType> target);

    /// <summary>
    /// Returns the color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <returns>The color at colormap position index.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickColor<TQuantumType>? GetColormapColor(int index);

    /// <summary>
    /// Returns a pixel collection that can be used to read or modify the pixels of this image.
    /// </summary>
    /// <returns>A pixel collection that can be used to read or modify the pixels of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IPixelCollection<TQuantumType> GetPixels();

    /// <summary>
    /// Returns a pixel collection that can be used to read or modify the pixels of this image. This instance
    /// will not do any bounds checking and directly call ImageMagick.
    /// </summary>
    /// <returns>A pixel collection that can be used to read or modify the pixels of this image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IUnsafePixelCollection<TQuantumType> GetPixelsUnsafe();

    /// <summary>
    /// Gets the associated read mask of the image.
    /// </summary>
    /// <returns>The associated read mask of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType>? GetReadMask();

    /// <summary>
    /// Gets the associated write mask of the image.
    /// </summary>
    /// <returns>The associated write mask of the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType>? GetWriteMask();

    /// <summary>
    /// Creates a color histogram.
    /// </summary>
    /// <returns>A color histogram.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IReadOnlyDictionary<IMagickColor<TQuantumType>, uint> Histogram();

#if !Q8
    /// <summary>
    /// Import pixels from the specified quantum array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(TQuantumType[] data, IPixelImportSettings settings);

    /// <summary>
    /// Import pixels from the specified quantum array into the current image.
    /// </summary>
    /// <param name="data">The quantum array to read the pixels from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="settings">The import settings to use when importing the pixels.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ImportPixels(TQuantumType[] data, uint offset, IPixelImportSettings settings);
#endif

    /// <summary>
    /// Returns the sum of values (pixel values) in the image.
    /// </summary>
    /// <returns>The sum of values (pixel values) in the image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType>? Integral();

    /// <summary>
    /// Floodfill pixels not matching color (within fuzz factor) of target pixel(x,y) with
    /// replacement alpha value using method.
    /// </summary>
    /// <param name="alpha">The alpha to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseFloodFill(TQuantumType alpha, int x, int y);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseFloodFill(IMagickColor<TQuantumType> color, int x, int y);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="color">The color to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseFloodFill(IMagickColor<TQuantumType> color, int x, int y, IMagickColor<TQuantumType> target);

    /// <summary>
    /// Flood-fill texture across pixels that do not match the color of the target pixel and are
    /// neighbors of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseFloodFill(IMagickImage<TQuantumType> image, int x, int y);

    /// <summary>
    /// Flood-fill texture across pixels that match the color of the target pixel and are neighbors
    /// of the target pixel. Uses current fuzz setting when determining color match.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="target">The target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseFloodFill(IMagickImage<TQuantumType> image, int x, int y, IMagickColor<TQuantumType> target);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(TQuantumType blackPoint, TQuantumType whitePoint);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(TQuantumType blackPoint, TQuantumType whitePoint, Channels channels);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(TQuantumType blackPoint, TQuantumType whitePoint, double gamma);

    /// <summary>
    /// Applies the reversed level operation to just the specific channels specified. It compresses
    /// the full range of color values, so that they lie between the given black and white points.
    /// Gamma is applied before the values are mapped.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevel(TQuantumType blackPoint, TQuantumType whitePoint, double gamma, Channels channels);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevelColors(IMagickColor<TQuantumType> blackColor, IMagickColor<TQuantumType> whiteColor);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseLevelColors(IMagickColor<TQuantumType> blackColor, IMagickColor<TQuantumType> whiteColor, Channels channels);

    /// <summary>
    /// Changes any pixel that does not match the target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseOpaque(IMagickColor<TQuantumType> target, IMagickColor<TQuantumType> fill);

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't match the specified color to transparent.
    /// </summary>
    /// <param name="color">The color that should not be made transparent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseTransparent(IMagickColor<TQuantumType> color);

    /// <summary>
    /// Add alpha channel to image, setting pixels that don't lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <param name="colorLow">The low target color.</param>
    /// <param name="colorHigh">The high target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void InverseTransparentChroma(IMagickColor<TQuantumType> colorLow, IMagickColor<TQuantumType> colorHigh);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(TQuantumType blackPoint, TQuantumType whitePoint);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range. Uses a midpoint of 1.0.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(TQuantumType blackPoint, TQuantumType whitePoint, Channels channels);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(TQuantumType blackPoint, TQuantumType whitePoint, double gamma);

    /// <summary>
    /// Adjust the levels of the image by scaling the colors falling between specified white and
    /// black points to the full available quantum range.
    /// </summary>
    /// <param name="blackPoint">The darkest color in the image. Colors darker are set to zero.</param>
    /// <param name="whitePoint">The lightest color in the image. Colors brighter are set to the maximum quantum value.</param>
    /// <param name="gamma">The gamma correction to apply to the image. (Useful range of 0 to 10).</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Level(TQuantumType blackPoint, TQuantumType whitePoint, double gamma, Channels channels);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LevelColors(IMagickColor<TQuantumType> blackColor, IMagickColor<TQuantumType> whiteColor);

    /// <summary>
    /// Maps the given color to "black" and "white" values, linearly spreading out the colors, and
    /// level values on a channel by channel bases, as per level(). The given colors allows you to
    /// specify different level ranges for each of the color channels separately.
    /// </summary>
    /// <param name="blackColor">The color to map black to/from.</param>
    /// <param name="whiteColor">The color to map white to/from.</param>
    /// <param name="channels">The channel(s) to level.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void LevelColors(IMagickColor<TQuantumType> blackColor, IMagickColor<TQuantumType> whiteColor, Channels channels);

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Map(IEnumerable<IMagickColor<TQuantumType>> colors);

    /// <summary>
    /// Remap image colors with closest color from the specified colors.
    /// </summary>
    /// <param name="colors">The colors to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <returns>The error informaton.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickErrorInfo Map(IEnumerable<IMagickColor<TQuantumType>> colors, IQuantizeSettings settings);

    /// <summary>
    /// Changes any pixel that matches target with the color defined by fill.
    /// </summary>
    /// <param name="target">The color to replace.</param>
    /// <param name="fill">The color to replace opaque color with.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Opaque(IMagickColor<TQuantumType> target, IMagickColor<TQuantumType> fill);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the information from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Reads only metadata and not the pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Ping(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The high threshold.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(TQuantumType low, TQuantumType high);

    /// <summary>
    /// Changes the value of individual pixels based on the intensity of each pixel compared to a
    /// random threshold. The result is a low-contrast, two color image.
    /// </summary>
    /// <param name="low">The low threshold.</param>
    /// <param name="high">The high threshold.</param>
    /// <param name="channels">The channel(s) to use.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RandomThreshold(TQuantumType low, TQuantumType high, Channels channels);

    /// <summary>
    /// Applies soft and hard thresholding.
    /// </summary>
    /// <param name="lowBlack">Defines the minimum black threshold value.</param>
    /// <param name="lowWhite">Defines the minimum white threshold value.</param>
    /// <param name="highWhite">Defines the maximum white threshold value.</param>
    /// <param name="highBlack">Defines the maximum black threshold value.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void RangeThreshold(TQuantumType lowBlack, TQuantumType lowWhite, TQuantumType highWhite, TQuantumType highBlack);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, uint offset, uint count, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(byte[] data, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="color">The color to fill the image with.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(IMagickColor<TQuantumType> color, uint width, uint height);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Read(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(FileInfo file, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(Stream stream, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, IMagickReadSettings<TQuantumType>? readSettings);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadAsync(string fileName, IMagickReadSettings<TQuantumType>? readSettings, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(byte[] data, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of bytes to read.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(byte[] data, uint offset, uint count, IPixelReadSettings<TQuantumType> settings);

#if !Q8
    /// <summary>
    /// Read single image frame.
    /// </summary>
    /// <param name="data">The quantum array to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(TQuantumType[] data, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="data">The quantum array to read the image data from.</param>
    /// <param name="offset">The offset at which to begin reading data.</param>
    /// <param name="count">The maximum number of items to read.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(TQuantumType[] data, uint offset, uint count, IPixelReadSettings<TQuantumType> settings);
#endif

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(FileInfo file, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(Stream stream, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void ReadPixels(string fileName, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(FileInfo file, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(Stream stream, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(Stream stream, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(string fileName, IPixelReadSettings<TQuantumType> settings);

    /// <summary>
    /// Read single image frame from pixel data.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="settings">The pixel settings to use when reading the image.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    Task ReadPixelsAsync(string fileName, IPixelReadSettings<TQuantumType> settings, CancellationToken cancellationToken);

    /// <summary>
    /// Separates the channels from the image and returns it as grayscale images.
    /// </summary>
    /// <returns>The channels from the image as grayscale images.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IReadOnlyList<IMagickImage<TQuantumType>> Separate();

    /// <summary>
    /// Separates the specified channels from the image and returns it as grayscale images.
    /// </summary>
    /// <param name="channels">The channel(s) to separates.</param>
    /// <returns>The channels from the image as grayscale images.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IReadOnlyList<IMagickImage<TQuantumType>> Separate(Channels channels);

    /// <summary>
    /// Set color at colormap position index.
    /// </summary>
    /// <param name="index">The position index.</param>
    /// <param name="color">The color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void SetColormapColor(int index, IMagickColor<TQuantumType> color);

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
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickSearchResult<TQuantumType>? SubImageSearch(IMagickImage<TQuantumType> image);

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickSearchResult<TQuantumType>? SubImageSearch(IMagickImage<TQuantumType> image, ErrorMetric metric);

    /// <summary>
    /// Search for the specified image at EVERY possible location in this image. This is slow!
    /// very very slow.. It returns a similarity image such that an exact match location is
    /// completely white and if none of the pixels match, black, otherwise some gray level in-between.
    /// </summary>
    /// <param name="image">The image to search for.</param>
    /// <param name="metric">The metric to use.</param>
    /// <param name="similarityThreshold">Minimum distortion for (sub)image match.</param>
    /// <returns>The result of the search action.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickSearchResult<TQuantumType>? SubImageSearch(IMagickImage<TQuantumType> image, ErrorMetric metric, double similarityThreshold);

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
    /// Add alpha channel to image, setting pixels matching color to transparent.
    /// </summary>
    /// <param name="color">The color to make transparent.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void Transparent(IMagickColor<TQuantumType> color);

    /// <summary>
    /// Add alpha channel to image, setting pixels that lie in between the given two colors to
    /// transparent.
    /// </summary>
    /// <param name="colorLow">The low target color.</param>
    /// <param name="colorHigh">The high target color.</param>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    void TransparentChroma(IMagickColor<TQuantumType> colorLow, IMagickColor<TQuantumType> colorHigh);

    /// <summary>
    /// Returns the unique colors of an image.
    /// </summary>
    /// <returns>The unique colors of an image.</returns>
    /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
    IMagickImage<TQuantumType>? UniqueColors();

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
