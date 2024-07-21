// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick exception object.
/// </summary>
public abstract class MagickException : Exception
{
    private List<MagickException>? _relatedExceptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public MagickException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Gets the exceptions that are related to this exception.
    /// </summary>
    public IReadOnlyList<MagickException> RelatedExceptions
    {
        get
        {
            if (_relatedExceptions is null)
                return new MagickException[0];

            return _relatedExceptions;
        }
    }

    /// <summary>
    /// Sets the related exceptions of this exception.
    /// </summary>
    /// <param name="relatedExceptions">The related exceptions.</param>
    public void SetRelatedException(List<MagickException> relatedExceptions)
        => _relatedExceptions = relatedExceptions;
}
