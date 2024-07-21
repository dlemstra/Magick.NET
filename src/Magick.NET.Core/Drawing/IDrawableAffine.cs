// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// Adjusts the current affine transformation matrix with the specified affine transformation
/// matrix. Note that the current affine transform is adjusted rather than replaced.
/// </summary>
public interface IDrawableAffine : IDrawable
{
    /// <summary>
    /// Gets or sets the X coordinate scaling element.
    /// </summary>
    double ScaleX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate scaling element.
    /// </summary>
    double ScaleY { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate shearing element.
    /// </summary>
    double ShearX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate shearing element.
    /// </summary>
    double ShearY { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate of the translation element.
    /// </summary>
    double TranslateX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the translation element.
    /// </summary>
    double TranslateY { get; set; }

    /// <summary>
    /// Reset to default.
    /// </summary>
    void Reset();

    /// <summary>
    /// Sets the origin of coordinate system.
    /// </summary>
    /// <param name="translateX">The X coordinate of the translation element.</param>
    /// <param name="translateY">The Y coordinate of the translation element.</param>
    void TransformOrigin(double translateX, double translateY);

    /// <summary>
    /// Sets the rotation to use.
    /// </summary>
    /// <param name="angle">The angle of the rotation.</param>
    void TransformRotation(double angle);

    /// <summary>
    /// Sets the scale to use.
    /// </summary>
    /// <param name="scaleX">The X coordinate scaling element.</param>
    /// <param name="scaleY">The Y coordinate scaling element.</param>
    void TransformScale(double scaleX, double scaleY);

    /// <summary>
    /// Skew to use in X axis.
    /// </summary>
    /// <param name="skewX">The X skewing element.</param>
    void TransformSkewX(double skewX);

    /// <summary>
    /// Skew to use in Y axis.
    /// </summary>
    /// <param name="skewY">The Y skewing element.</param>
    void TransformSkewY(double skewY);
}
