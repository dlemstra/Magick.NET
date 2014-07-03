Breaking changes.

Magick.NET 6.8.9.501:
  - Changed arguments for the SparseColor method of MagickImage.

Magick.NET 6.8.9.101:
  - Int/short Set methods of WritablePixelCollection are now unsigned.
  - The Q16 build no longer uses HDRI, switch to the new Q16-HDRI build if you need HDRI.

Magick.NET 6.8.8.901:
  - Changed datatype of MagickImage.ColorFuzz to Percentage.

Magick.NET 6.8.8.801:
  - Fixed double value of Percentage (50% is 50.0 instead of 0.5).
  - Refactored Quantize methods and properties to only one method that use a QuantizeSettings paramater.

Magick.NET 6.8.8.501:
  - To reduce memory usage the Q8 version of Magick.NET no longer uses HDRI.
  - Removed MagickNET.SetCacheThreshold (Use ResourceLimits).
  - Renamed MagickImage.QuantumOperator to MagickImage.Evaluate.

Magick.NET 6.8.8.201:
  - Removed MagickImage.ModulusDepth property and moved functionality to BitDepth method.

Magick.NET 6.8.7.901:
  - Renamed MagickGeometry.Aspect to IgnoreAspectRatio.

Magick.NET 6.8.7.501:
  - Refactored MagickImageStatistics to prepare for upcoming changes in ImageMagick 7.
  - Renamed MagickImage.SetOption to SetDefine.

Magick.NET 6.8.7.101:
  - Renamed Matrix classes: MatrixColor = ColorMatrix and MatrixConvolve = ConvolveMatrix.
  - Renamed Depth method with Channels parameter to BitDepth and changed the other method into a property.

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
