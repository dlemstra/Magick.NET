// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Adjusts the current affine transformation matrix with the specified affine transformation
/// matrix. Note that the current affine transform is adjusted rather than replaced.
/// </summary>
public sealed partial class DrawableAffine : IDrawableAffine, IDrawingWand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableAffine"/> class.
    /// </summary>
    public DrawableAffine()
        => Reset();

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableAffine"/> class.
    /// </summary>
    /// <param name="scaleX">The X coordinate scaling element.</param>
    /// <param name="scaleY">The Y coordinate scaling element.</param>
    /// <param name="shearX">The X coordinate shearing element.</param>
    /// <param name="shearY">The Y coordinate shearing element.</param>
    /// <param name="translateX">The X coordinate of the translation element.</param>
    /// <param name="translateY">The Y coordinate of the translation element.</param>
    public DrawableAffine(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY)
    {
        ScaleX = scaleX;
        ScaleY = scaleY;
        ShearX = shearX;
        ShearY = shearY;
        TranslateX = translateX;
        TranslateY = translateY;
    }

    /// <summary>
    /// Gets or sets the X coordinate scaling element.
    /// </summary>
    public double ScaleX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate scaling element.
    /// </summary>
    public double ScaleY { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate shearing element.
    /// </summary>
    public double ShearX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate shearing element.
    /// </summary>
    public double ShearY { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate of the translation element.
    /// </summary>
    public double TranslateX { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the translation element.
    /// </summary>
    public double TranslateY { get; set; }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
        => wand?.Affine(ScaleX, ScaleY, ShearX, ShearY, TranslateX, TranslateY);

    /// <summary>
    /// Reset to default.
    /// </summary>
    public void Reset()
    {
        ScaleX = 1.0;
        ScaleY = 1.0;
        ShearX = 0.0;
        ShearY = 0.0;
        TranslateX = 0.0;
        TranslateY = 0.0;
    }

    /// <summary>
    /// Sets the origin of coordinate system.
    /// </summary>
    /// <param name="translateX">The X coordinate of the translation element.</param>
    /// <param name="translateY">The Y coordinate of the translation element.</param>
    public void TransformOrigin(double translateX, double translateY)
        => Transform(new DrawableAffine
        {
            TranslateX = translateX,
            TranslateY = translateY,
        });

    /// <summary>
    /// Sets the rotation to use.
    /// </summary>
    /// <param name="angle">The angle of the rotation.</param>
    public void TransformRotation(double angle)
        => Transform(new DrawableAffine
        {
            ScaleX = Math.Cos(DegreesToRadians(Math.IEEERemainder(angle, 360.0))),
            ScaleY = Math.Cos(DegreesToRadians(Math.IEEERemainder(angle, 360.0))),
            ShearX = -Math.Sin(DegreesToRadians(Math.IEEERemainder(angle, 360.0))),
            ShearY = Math.Sin(DegreesToRadians(Math.IEEERemainder(angle, 360.0))),
        });

    /// <summary>
    /// Sets the scale to use.
    /// </summary>
    /// <param name="scaleX">The X coordinate scaling element.</param>
    /// <param name="scaleY">The Y coordinate scaling element.</param>
    public void TransformScale(double scaleX, double scaleY)
        => Transform(new DrawableAffine
        {
            ScaleX = scaleX,
            ScaleY = scaleY,
        });

    /// <summary>
    /// Skew to use in X axis.
    /// </summary>
    /// <param name="skewX">The X skewing element.</param>
    public void TransformSkewX(double skewX)
        => Transform(new DrawableAffine
        {
            ShearX = Math.Tan(DegreesToRadians(Math.IEEERemainder(skewX, 360.0))),
        });

    /// <summary>
    /// Skew to use in Y axis.
    /// </summary>
    /// <param name="skewY">The Y skewing element.</param>
    public void TransformSkewY(double skewY)
        => Transform(new DrawableAffine
        {
            ShearY = Math.Tan(DegreesToRadians(Math.IEEERemainder(skewY, 360.0))),
        });

    private static double DegreesToRadians(double x)
        => Math.PI * x / 180.0;

    private void Transform(DrawableAffine affine)
    {
        var scaleX = ScaleX;
        var scaleY = ScaleY;
        var shearX = ShearX;
        var shearY = ShearY;
        var translateX = TranslateX;
        var translateY = TranslateY;

        ScaleX = (scaleX * affine.ScaleX) + (shearY * affine.ShearX);
        ScaleY = (shearX * affine.ShearY) + (scaleY * affine.ScaleY);
        ShearX = (shearX * affine.ScaleX) + (scaleY * affine.ShearX);
        ShearY = (scaleX * affine.ShearY) + (shearY * affine.ScaleY);
        TranslateX = (scaleX * affine.TranslateX) + (shearY * affine.TranslateY) + translateX;
        TranslateY = (shearX * affine.TranslateX) + (scaleY * affine.TranslateY) + translateY;
    }
}
