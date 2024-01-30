// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ImageMagick.SourceGenerator;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Class that can be used to chain path actions.
/// </summary>
[Paths]
public sealed partial class Paths : IPaths<QuantumType>
{
    private readonly Drawables? _drawables;
    private readonly Collection<IPath> _paths;

    /// <summary>
    /// Initializes a new instance of the <see cref="Paths"/> class.
    /// </summary>
    public Paths()
        => _paths = new Collection<IPath>();

    internal Paths(Drawables drawables)
      : this()
        => _drawables = drawables;

    /// <summary>
    /// Applies the PathCurveToAbs operation to the <see cref="Paths" />.
    /// </summary>
    /// <param name="x1">X coordinate of control point for curve beginning.</param>
    /// <param name="y1">Y coordinate of control point for curve beginning.</param>
    /// <param name="x2">X coordinate of control point for curve ending.</param>
    /// <param name="y2">Y coordinate of control point for curve ending.</param>
    /// <param name="x">X coordinate of the end of the curve.</param>
    /// <param name="y">Y coordinate of the end of the curve.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> CurveToAbs(double x1, double y1, double x2, double y2, double x, double y)
        => CurveToAbs(new PointD(x1, y1), new PointD(x2, y2), new PointD(x, y));

    /// <summary>
    /// Applies the PathCurveToRel operation to the <see cref="Paths" />.
    /// </summary>
    /// <param name="x1">X coordinate of control point for curve beginning.</param>
    /// <param name="y1">Y coordinate of control point for curve beginning.</param>
    /// <param name="x2">X coordinate of control point for curve ending.</param>
    /// <param name="y2">Y coordinate of control point for curve ending.</param>
    /// <param name="x">X coordinate of the end of the curve.</param>
    /// <param name="y">Y coordinate of the end of the curve.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> CurveToRel(double x1, double y1, double x2, double y2, double x, double y)
        => CurveToRel(new PointD(x1, y1), new PointD(x2, y2), new PointD(x, y));

    /// <summary>
    /// Converts this instance to a <see cref="IDrawables{TQuantumType}"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Drawables"/> instance.</returns>
    public IDrawables<QuantumType> Drawables()
    {
        if (_drawables is null)
            return new Drawables().Path(this);

        return _drawables.Path(this);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that iterates through the collection.</returns>
    public IEnumerator<IPath> GetEnumerator()
        => _paths.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that iterates through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
        => _paths.GetEnumerator();

    /// <summary>
    /// Draws a line path from the current point to the given coordinate using absolute coordinates.
    /// The coordinate then becomes the new current point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> LineToAbs(double x, double y)
        => LineToAbs(new PointD(x, y));

    /// <summary>
    /// Draws a line path from the current point to the given coordinate using absolute coordinates.
    /// The coordinate then becomes the new current point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> LineToRel(double x, double y)
        => LineToRel(new PointD(x, y));

    /// <summary>
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> MoveToAbs(double x, double y)
        => MoveToAbs(new PointD(x, y));

    /// <summary>
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> MoveToRel(double x, double y)
        => MoveToRel(new PointD(x, y));

    /// <summary>
    /// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
    /// point using relative coordinates. At the end of the command, the new current point becomes
    /// the final (x, y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x1">X coordinate of control point.</param>
    /// <param name="y1">Y coordinate of control point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> QuadraticCurveToAbs(double x1, double y1, double x, double y)
        => QuadraticCurveToAbs(new PointD(x1, y1), new PointD(x, y));

    /// <summary>
    /// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
    /// point using relative coordinates. At the end of the command, the new current point becomes
    /// the final (x, y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x1">X coordinate of control point.</param>
    /// <param name="y1">Y coordinate of control point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> QuadraticCurveToRel(double x1, double y1, double x, double y)
        => QuadraticCurveToRel(new PointD(x1, y1), new PointD(x, y));

    /// <summary>
    /// Draws a cubic Bezier curve from the current point to (x,y) using absolute coordinates. The
    /// first control point is assumed to be the reflection of the second control point on the
    /// previous command relative to the current point. (If there is no previous command or if the
    /// previous command was not an PathCurveToAbs, PathCurveToRel, PathSmoothCurveToAbs or
    /// PathSmoothCurveToRel, assume the first control point is coincident with the current point.)
    /// (x2,y2) is the second control point (i.e., the control point at the end of the curve). At
    /// the end of the command, the new current point becomes the final (x,y) coordinate pair used
    /// in the polybezier.
    /// </summary>
    /// <param name="x2">X coordinate of second point.</param>
    /// <param name="y2">Y coordinate of second point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> SmoothCurveToAbs(double x2, double y2, double x, double y)
        => SmoothCurveToAbs(new PointD(x2, y2), new PointD(x, y));

    /// <summary>
    /// Draws a cubic Bezier curve from the current point to (x,y) using absolute coordinates. The
    /// first control point is assumed to be the reflection of the second control point on the
    /// previous command relative to the current point. (If there is no previous command or if the
    /// previous command was not an PathCurveToAbs, PathCurveToRel, PathSmoothCurveToAbs or
    /// PathSmoothCurveToRel, assume the first control point is coincident with the current point.)
    /// (x2,y2) is the second control point (i.e., the control point at the end of the curve). At
    /// the end of the command, the new current point becomes the final (x,y) coordinate pair used
    /// in the polybezier.
    /// </summary>
    /// <param name="x2">X coordinate of second point.</param>
    /// <param name="y2">Y coordinate of second point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> SmoothCurveToRel(double x2, double y2, double x, double y)
        => SmoothCurveToRel(new PointD(x2, y2), new PointD(x, y));

    /// <summary>
    /// Draws a quadratic Bezier curve (using absolute coordinates) from the current point to (X, Y).
    /// The control point is assumed to be the reflection of the control point on the previous
    /// command relative to the current point. (If there is no previous command or if the previous
    /// command was not a PathQuadraticCurveToAbs, PathQuadraticCurveToRel,
    /// PathSmoothQuadraticCurveToAbs or PathSmoothQuadraticCurveToRel, assume the control point is
    /// coincident with the current point.). At the end of the command, the new current point becomes
    /// the final (X,Y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> SmoothQuadraticCurveToAbs(double x, double y)
        => SmoothQuadraticCurveToAbs(new PointD(x, y));

    /// <summary>
    /// Draws a quadratic Bezier curve (using absolute coordinates) from the current point to (X, Y).
    /// The control point is assumed to be the reflection of the control point on the previous
    /// command relative to the current point. (If there is no previous command or if the previous
    /// command was not a PathQuadraticCurveToAbs, PathQuadraticCurveToRel,
    /// PathSmoothQuadraticCurveToAbs or PathSmoothQuadraticCurveToRel, assume the control point is
    /// coincident with the current point.). At the end of the command, the new current point becomes
    /// the final (X,Y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="Paths" /> instance.</returns>
    public IPaths<QuantumType> SmoothQuadraticCurveToRel(double x, double y)
        => SmoothQuadraticCurveToRel(new PointD(x, y));
}
