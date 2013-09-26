Breaking changes.

Magick.NET 6.8.7.001:
  - ToBitmap method of MagickImage returns a png instead of a bmp.
  - Changed the value for full transparency from 255(Q8)/65535(Q16) to 0.
  - MagickColor now uses floats instead of Byte/UInt16.

Magick.NET 6.8.6.801:
  - Renamed Attribute method of MagickImage to GetAttribute and SetAttribute.
  - Renamed Copy method of MagickImage to Clone.
  - Renamed HasMatte attribute to HasAlpha.

Magick.NET 6.8.6.301:
  - Removed Debug property of MagickImage (use MagickNET.SetLogEvents).
  - MagickImage.Separate no longer modifies the image, it returns an IEnumerable<MagickImage>.
  - LayerMethod enum is no longer public.
  - Removed LayerMethod argument from MagickImageCollection.Merge.

Magick.NET 6.8.5.1001:
  - MagickNET.Initialize has been made obsolete because the ImageMagick files in the  directory are no longer necessary.
  - MagickGeometry is no longer IDisposable.
  - Renamed dll's so they include the platform name.
  - Image profiles can now only be accessed and modified with ImageProfile classes.
  - Renamed DrawableBase to Drawable.
  - Removed Args part of PathArc/PathCurvetoArgs/PathQuadraticCurvetoArgs classes.

Magick.NET 6.8.5.401:
  - Renamed ImageType enum to MagickFormat.
  - Renamed ImageType property of MagickImage to Format.
  - Reordered constructor MagickImage(int width, int height, MagickColor color).
  - Replaced Width/Height, ColorSpace with MagickReadSettings object in MagickImage Read methods.
