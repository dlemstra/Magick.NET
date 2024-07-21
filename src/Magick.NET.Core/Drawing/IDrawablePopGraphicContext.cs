// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Drawing;

/// <summary>
/// destroys the current drawing wand and returns to the previously pushed drawing wand. Multiple
/// drawing wands may exist. It is an error to attempt to pop more drawing wands than have been
/// pushed, and it is proper form to pop all drawing wands which have been pushed.
/// </summary>
public interface IDrawablePopGraphicContext : IDrawable
{
}
