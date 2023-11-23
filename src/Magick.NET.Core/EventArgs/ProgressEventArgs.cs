// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// EventArgs for Progress events.
/// </summary>
/// <param name="origin">The originator of this event.</param>
/// <param name="offset">The offset.</param>
/// <param name="extent">The extent.</param>
public sealed class ProgressEventArgs(string? origin, int offset, int extent) : EventArgs
{
    /// <summary>
    /// Gets the originator of this event.
    /// </summary>
    public string? Origin { get; } = origin;

    /// <summary>
    /// Gets the progress percentage.
    /// </summary>
    public Percentage Progress { get; } = new Percentage(((offset + 1) / (double)extent) * 100);

    /// <summary>
    /// Gets or sets a value indicating whether the current operation will be canceled.
    /// </summary>
    public bool Cancel { get; set; }
}
