Breaking changes.

Magick.NET 6.8.5.403:
  - Renamed dll's so they include the platform name.
  - Renamed DrawableBase to Drawable.
  - Removed Args part of PathArc/PathCurvetoArgs/PathQuadraticCurvetoArgs classes.

Magick.NET 6.8.5.401:
  - Renamed ImageType enum to MagickFormat.
  - Renamed ImageType property of MagickImage to Format.
  - Reordered constructor MagickImage(int width, int height, MagickColor color).
  - Replaced Width/Height, ColorSpace with MagickReadSettings object in MagickImage Read methods.