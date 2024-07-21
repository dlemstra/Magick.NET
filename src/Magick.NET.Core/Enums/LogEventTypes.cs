// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Specifies log event types.
/// </summary>
[Flags]
public enum LogEventTypes
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0x000000,

    /// <summary>
    /// Accelerate.
    /// </summary>
    Accelerate = 0x00001,

    /// <summary>
    /// Annotate.
    /// </summary>
    Annotate = 0x00002,

    /// <summary>
    /// Blob.
    /// </summary>
    Blob = 0x00004,

    /// <summary>
    /// Cache.
    /// </summary>
    Cache = 0x00008,

    /// <summary>
    /// Coder.
    /// </summary>
    Coder = 0x00010,

    /// <summary>
    /// Configure.
    /// </summary>
    Configure = 0x00020,

    /// <summary>
    /// Deprecate.
    /// </summary>
    Deprecate = 0x00040,

    /// <summary>
    /// Draw.
    /// </summary>
    Draw = 0x00080,

    /// <summary>
    /// Exception.
    /// </summary>
    Exception = 0x00100,

    /// <summary>
    /// Image.
    /// </summary>
    Image = 0x00200,

    /// <summary>
    /// Locale.
    /// </summary>
    Locale = 0x00400,

    /// <summary>
    /// Module.
    /// </summary>
    Module = 0x00800,

    /// <summary>
    /// Pixel.
    /// </summary>
    Pixel = 0x01000,

    /// <summary>
    /// Policy.
    /// </summary>
    Policy = 0x02000,

    /// <summary>
    /// Resource.
    /// </summary>
    Resource = 0x04000,

    /// <summary>
    /// Trace.
    /// </summary>
    Trace = 0x08000,

    /// <summary>
    /// Transform.
    /// </summary>
    Transform = 0x10000,

    /// <summary>
    /// User.
    /// </summary>
    User = 0x20000,

    /// <summary>
    /// Wand.
    /// </summary>
    Wand = 0x40000,

    /// <summary>
    /// All log events except Trace.
    /// </summary>
    Detailed = 0x7fff7fff,

    /// <summary>
    /// All log.
    /// </summary>
    All = Detailed | Trace,
}
