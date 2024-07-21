// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <summary>
/// Class that can be used to chain path actions.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
[Paths]
public partial interface IPaths<TQuantumType> : IEnumerable<IPath>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Applies the PathCurveToAbs operation to the <see cref="IPaths{TQuantumType}" />.
    /// </summary>
    /// <param name="x1">X coordinate of control point for curve beginning.</param>
    /// <param name="y1">Y coordinate of control point for curve beginning.</param>
    /// <param name="x2">X coordinate of control point for curve ending.</param>
    /// <param name="y2">Y coordinate of control point for curve ending.</param>
    /// <param name="x">X coordinate of the end of the curve.</param>
    /// <param name="y">Y coordinate of the end of the curve.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> CurveToAbs(double x1, double y1, double x2, double y2, double x, double y);

    /// <summary>
    /// Applies the PathCurveToRel operation to the <see cref="IPaths{TQuantumType}" />.
    /// </summary>
    /// <param name="x1">X coordinate of control point for curve beginning.</param>
    /// <param name="y1">Y coordinate of control point for curve beginning.</param>
    /// <param name="x2">X coordinate of control point for curve ending.</param>
    /// <param name="y2">Y coordinate of control point for curve ending.</param>
    /// <param name="x">X coordinate of the end of the curve.</param>
    /// <param name="y">Y coordinate of the end of the curve.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> CurveToRel(double x1, double y1, double x2, double y2, double x, double y);

    /// <summary>
    /// Converts this instance to a <see cref="IDrawables{TQuantumType}"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Drawables"/> instance.</returns>
    IDrawables<TQuantumType> Drawables();

    /// <summary>
    /// Draws a line path from the current point to the given coordinate using absolute coordinates.
    /// The coordinate then becomes the new current point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> LineToAbs(double x, double y);

    /// <summary>
    /// Draws a line path from the current point to the given coordinate using absolute coordinates.
    /// The coordinate then becomes the new current point.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> LineToRel(double x, double y);

    /// <summary>
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> MoveToAbs(double x, double y);

    /// <summary>
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> MoveToRel(double x, double y);

    /// <summary>
    /// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
    /// point using relative coordinates. At the end of the command, the new current point becomes
    /// the final (x, y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x1">X coordinate of control point.</param>
    /// <param name="y1">Y coordinate of control point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> QuadraticCurveToAbs(double x1, double y1, double x, double y);

    /// <summary>
    /// Draws a quadratic Bezier curve from the current point to (x, y) using (x1, y1) as the control
    /// point using relative coordinates. At the end of the command, the new current point becomes
    /// the final (x, y) coordinate pair used in the polybezier.
    /// </summary>
    /// <param name="x1">X coordinate of control point.</param>
    /// <param name="y1">Y coordinate of control point.</param>
    /// <param name="x">X coordinate of final point.</param>
    /// <param name="y">Y coordinate of final point.</param>
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> QuadraticCurveToRel(double x1, double y1, double x, double y);

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
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> SmoothCurveToAbs(double x2, double y2, double x, double y);

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
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> SmoothCurveToRel(double x2, double y2, double x, double y);

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
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> SmoothQuadraticCurveToAbs(double x, double y);

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
    /// <returns>The <see cref="IPaths{TQuantumType}" /> instance.</returns>
    IPaths<TQuantumType> SmoothQuadraticCurveToRel(double x, double y);
}
