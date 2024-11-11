// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
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

/// <content />
public partial class MagickImage
{
    private NativeMagickImage _nativeInstance;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate bool ProgressDelegate(IntPtr origin, long offset, ulong extent, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long ReadWriteStreamDelegate(IntPtr data, UIntPtr length, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long SeekStreamDelegate(long offset, IntPtr whence, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate long TellStreamDelegate(IntPtr user_data);

    [NativeInterop(RaiseWarnings = true, StaticDispose = true)]
    private unsafe sealed partial class NativeMagickImage : NativeInstance, INativeMagickImage
    {
        [Throws]
        [Cleanup(Name = nameof(DisposeInstance))]
        public static partial NativeMagickImage Create(IMagickSettings<QuantumType>? settings);

        public static partial IntPtr GetNext(IntPtr image);

        public partial nuint AnimationDelay_Get();

        public partial void AnimationDelay_Set(nuint value);

        public partial nuint AnimationIterations_Get();

        public partial void AnimationIterations_Set(nuint value);

        public partial nint AnimationTicksPerSecond_Get();

        public partial void AnimationTicksPerSecond_Set(nint value);

        public partial IMagickColor<QuantumType>? BackgroundColor_Get();

        public partial void BackgroundColor_Set(IMagickColor<QuantumType>? value);

        public partial nuint BaseHeight_Get();

        public partial nuint BaseWidth_Get();

        public partial bool BlackPointCompensation_Get();

        public partial void BlackPointCompensation_Set(bool value);

        public partial IMagickColor<QuantumType>? BorderColor_Get();

        public partial void BorderColor_Set(IMagickColor<QuantumType>? value);

        [Throws]
        public partial MagickRectangle? BoundingBox_Get();

        public partial nuint ChannelCount_Get();

        public partial IPrimaryInfo? ChromaBlue_Get();

        public partial void ChromaBlue_Set(IPrimaryInfo? value);

        public partial IPrimaryInfo? ChromaGreen_Get();

        public partial void ChromaGreen_Set(IPrimaryInfo? value);

        public partial IPrimaryInfo? ChromaRed_Get();

        public partial void ChromaRed_Set(IPrimaryInfo? value);

        public partial IPrimaryInfo? ChromaWhite_Get();

        public partial void ChromaWhite_Set(IPrimaryInfo? value);

        public partial ClassType ClassType_Get();

        [Throws]
        public partial void ClassType_Set(ClassType value);

        public partial double ColorFuzz_Get();

        public partial void ColorFuzz_Set(double value);

        public partial nint ColormapSize_Get();

        [Throws]
        public partial void ColormapSize_Set(nint value);

        public partial ColorSpace ColorSpace_Get();

        [Throws]
        public partial void ColorSpace_Set(ColorSpace value);

        public partial ColorType ColorType_Get();

        [Throws]
        public partial void ColorType_Set(ColorType value);

        public partial CompositeOperator Compose_Get();

        public partial void Compose_Set(CompositeOperator value);

        public partial CompressionMethod Compression_Get();

        public partial void Compression_Set(CompressionMethod value);

        public partial nuint Depth_Get();

        public partial void Depth_Set(nuint value);

        public partial string? EncodingGeometry_Get();

        public partial Endian Endian_Get();

        public partial void Endian_Set(Endian value);

        public partial string? FileName_Get();

        public partial void FileName_Set(string value);

        public partial FilterType FilterType_Get();

        public partial void FilterType_Set(FilterType value);

        public partial string? Format_Get();

        public partial void Format_Set(string? value);

        public partial double Gamma_Get();

        public partial GifDisposeMethod GifDisposeMethod_Get();

        public partial void GifDisposeMethod_Set(GifDisposeMethod value);

        public partial bool HasAlpha_Get();

        [Throws]
        public partial bool HasAlpha_Set(bool value);

        public partial nuint Height_Get();

        public partial Interlace Interlace_Get();

        public partial void Interlace_Set(Interlace value);

        public partial PixelInterpolateMethod Interpolate_Get();

        public partial void Interpolate_Set(PixelInterpolateMethod value);

        [Throws]
        public partial bool IsOpaque_Get();

        public partial IMagickColor<QuantumType>? MatteColor_Get();

        public partial void MatteColor_Set(IMagickColor<QuantumType>? value);

        public partial nuint MetaChannelCount_Get();

        [Throws]
        public partial void MetaChannelCount_Set(nuint value);

        public partial double MeanErrorPerPixel_Get();

        public partial double NormalizedMaximumError_Get();

        public partial double NormalizedMeanError_Get();

        public partial OrientationType Orientation_Get();

        public partial void Orientation_Set(OrientationType value);

        public partial MagickRectangle? Page_Get();

        public partial void Page_Set(MagickRectangle? value);

        public partial nuint Quality_Get();

        public partial void Quality_Set(nuint value);

        public partial RenderingIntent RenderingIntent_Get();

        public partial void RenderingIntent_Set(RenderingIntent value);

        public partial DensityUnit ResolutionUnits_Get();

        public partial void ResolutionUnits_Set(DensityUnit value);

        public partial double ResolutionX_Get();

        public partial void ResolutionX_Set(double value);

        public partial double ResolutionY_Get();

        public partial void ResolutionY_Set(double value);

        [Throws]
        public partial string Signature_Get();

        [Throws]
        public partial nuint TotalColors_Get();

        public partial VirtualPixelMethod VirtualPixelMethod_Get();

        [Throws]
        public partial void VirtualPixelMethod_Set(VirtualPixelMethod value);

        public partial nuint Width_Get();

        [Throws]
        public partial IntPtr AdaptiveBlur(double radius, double sigma);

        [Throws]
        public partial IntPtr AdaptiveResize(string geometry);

        [Throws]
        public partial IntPtr AdaptiveSharpen(double radius, double sigma, Channels channels);

        [Throws]
        public partial IntPtr AdaptiveThreshold(nuint width, nuint height, double bias, Channels channels);

        [Throws]
        public partial IntPtr AddNoise(NoiseType noiseType, double attenuate, Channels channels);

        [Throws]
        public partial IntPtr AffineTransform(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY);

        [Throws]
        public partial void Annotate(DrawingSettings settings, string text, string boundingArea, Gravity gravity, double degrees);

        [Throws]
        public partial void AnnotateGravity(DrawingSettings settings, string text, Gravity gravity);

        [Throws]
        public partial void AutoGamma(Channels channels);

        [Throws]
        public partial void AutoLevel(Channels channels);

        [Throws]
        public partial IntPtr AutoOrient();

        [Throws]
        public partial void AutoThreshold(AutoThresholdMethod method);

        [Throws]
        public partial IntPtr BilateralBlur(nuint width, nuint height, double intensitySigma, double spatialSigma);

        [Throws]
        public partial void BlackThreshold(string threshold, Channels channels);

        [Throws]
        public partial IntPtr BlueShift(double factor);

        [Throws]
        public partial IntPtr Blur(double radius, double sigma, Channels channels);

        [Throws]
        public partial IntPtr Border(MagickRectangle value);

        [Throws]
        public partial void BrightnessContrast(double brightness, double contrast, Channels channels);

        [Throws]
        public partial IntPtr CannyEdge(double radius, double sigma, double lower, double upper);

        public partial nuint ChannelOffset(PixelChannel channel);

        [Throws]
        public partial IntPtr Charcoal(double radius, double sigma);

        [Throws]
        public partial IntPtr Chop(MagickRectangle geometry);

        [Throws]
        public partial void Clahe(nuint xTiles, nuint yTiles, nuint numberBins, double clipLimit);

        [Throws]
        public partial IntPtr Clamp(Channels channels);

        [Throws]
        public partial void ClipPath(string pathName, bool inside);

        [Throws]
        public partial NativeMagickImage Clone();

        [Throws]
        public partial NativeMagickImage CloneArea(nuint width, nuint height);

        [Throws]
        public partial void Clut(IMagickImage image, PixelInterpolateMethod method, Channels channels);

        [Throws]
        public partial void ColorDecisionList(string fileName);

        [Throws]
        public partial IntPtr Colorize(IMagickColor<QuantumType>? color, string blend);

        [Throws]
        [SetInstance]
        public partial void ColorMatrix(IDoubleMatrix matrix);

        [Throws]
        public partial void ColorThreshold(IMagickColor<QuantumType>? startColor, IMagickColor<QuantumType>? stopColor);

        [Throws]
        public partial IntPtr Compare(IMagickImage image, ErrorMetric metric, Channels channels, out double distortion);

        [Throws]
        public partial double CompareDistortion(IMagickImage image, ErrorMetric metric, Channels channels);

        [Throws]
        public partial void Composite(IMagickImage image, nint x, nint y, CompositeOperator compose, Channels channels);

        [Throws]
        public partial void CompositeGravity(IMagickImage image, Gravity gravity, nint x, nint y, CompositeOperator compose, Channels channels);

        [Throws]
        [SetInstance]
        public partial void ConnectedComponents(nuint connectivity, out IntPtr objects);

        [Throws]
        public partial void Contrast(bool enhance);

        [Throws]
        public partial void ContrastStretch(double blackPoint, double whitePoint, Channels channels);

        [Throws]
        public partial IntPtr ConvexHull(out nuint length);

        [Throws]
        [SetInstance]
        public partial void Convolve(IDoubleMatrix matrix);

        [Throws]
        public partial void CopyPixels(IMagickImage image, MagickRectangle geometry, OffsetInfo offset, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Crop(string geometry, Gravity gravity);

        [Throws]
        public partial IntPtr CropToTiles(string geometry);

        [Throws]
        [SetInstance]
        public partial void CycleColormap(nint amount);

        [Throws]
        public partial void Decipher(string passphrase);

        [Throws]
        [SetInstance]
        public partial void Deskew(double threshold);

        [Throws]
        [SetInstance]
        public partial void Despeckle();

        [Throws]
        public partial nuint DetermineBitDepth(Channels channels);

        [Throws]
        public partial ColorType DetermineColorType();

        [Throws]
        [SetInstance]
        public partial void Distort(DistortMethod method, bool bestfit, double[] arguments, nuint length);

        [Throws]
        [SetInstance]
        public partial void Edge(double radius);

        [Throws]
        [SetInstance]
        public partial void Emboss(double radius, double sigma);

        [Throws]
        [SetInstance]
        public partial void Encipher(string passphrase);

        [Throws]
        [SetInstance]
        public partial void Enhance();

        [Throws]
        public partial void Equalize(Channels channels);

        [Throws]
        public partial void Equals(IMagickImage image);

        [Throws]
        public partial void EvaluateFunction(Channels channels, EvaluateFunction evaluateFunction, double[] values, nuint length);

        [Throws]
        public partial void EvaluateGeometry(Channels channels, MagickRectangle geometry, EvaluateOperator evaluateOperator, double value);

        [Throws]
        public partial void EvaluateOperator(Channels channels, EvaluateOperator evaluateOperator, double value);

        [Throws]
        [SetInstance]
        public partial void Extent(string geometry, Gravity gravity);

        [Throws]
        [SetInstance]
        public partial void Flip();

        [Throws]
        public partial void FloodFill(DrawingSettings settings, nint x, nint y, IMagickColor<QuantumType>? target, bool invert);

        [Throws]
        [SetInstance]
        public partial void Flop();

        [Throws]
        [Cleanup(Name = "TypeMetric.Dispose")]
        public partial TypeMetric FontTypeMetrics(DrawingSettings settings, bool ignoreNewlines);

        [Throws]
        public partial string? FormatExpression(IMagickSettings<QuantumType>? settings, string expression);

        [Throws]
        [SetInstance]
        public partial void Frame(MagickRectangle geometry);

        [Throws]
        public partial IntPtr Fx(string expression, Channels channels);

        [Throws]
        public partial void GammaCorrect(double gamma, Channels channels);

        [Throws]
        [SetInstance]
        public partial void GaussianBlur(double radius, double sigma, Channels channels);

        [Throws]
        public partial string? GetArtifact(string name);

        [Throws]
        public partial string? GetAttribute(string name);

        public partial IMagickColor<QuantumType>? GetColormapColor(nuint index);

        public partial string? GetNextArtifactName();

        public partial string? GetNextAttributeName();

        public partial string? GetNextProfileName();

        public partial StringInfo? GetProfile(string name);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr GetReadMask();

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr GetWriteMask();

        [Throws]
        public partial void Grayscale(PixelIntensityMethod method);

        [Throws]
        public partial void HaldClut(IMagickImage image, Channels channels);

        public partial bool HasChannel(PixelChannel channel);

        public partial bool HasProfile(string name);

        [Throws]
        public partial IntPtr Histogram(out nuint length);

        [Throws]
        [SetInstance]
        public partial void HoughLine(nuint width, nuint height, nuint threshold);

        [Throws]
        [SetInstance]
        public partial void Implode(double amount, PixelInterpolateMethod method);

        [Throws]
        public partial void ImportPixels(nint x, nint y, nuint width, nuint height, string map, StorageType storageType, void* data, nuint offsetInBytes);

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr Integral();

        [Throws]
        [SetInstance]
        public partial void InterpolativeResize(string geometry, PixelInterpolateMethod method);

        [Throws]
        public partial void InverseLevel(double blackPoint, double whitePoint, double gamma, Channels channels);

        [Throws]
        public partial void Kmeans(nuint numberColors, nuint maxIterations, double tolerance);

        [Throws]
        [SetInstance]
        public partial void Kuwahara(double radius, double sigma);

        [Throws]
        public partial void Level(double blackPoint, double whitePoint, double gamma, Channels channels);

        [Throws]
        public partial void LevelColors(IMagickColor<QuantumType>? blackColor, IMagickColor<QuantumType>? whiteColor, Channels channels, bool invert);

        [Throws]
        public partial void LinearStretch(double blackPoint, double whitePoint);

        [Throws]
        [SetInstance]
        public partial void LiquidRescale(string geometry, double deltaX, double rigidity);

        [Throws]
        [SetInstance]
        public partial void LocalContrast(double radius, double strength, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Magnify();

        [Throws]
        [SetInstance]
        public partial void MeanShift(nuint width, nuint height, double colorDistance);

        [Throws]
        [SetInstance]
        public partial void Minify();

        [Throws]
        [Cleanup(Name = "ImageMagick.Moments.DisposeList")]
        public partial IntPtr Moments();

        [Throws]
        public partial void Modulate(string modulate);

        [Throws]
        [Cleanup(Name = "PointInfoCollection.DisposeList")]
        public partial IntPtr MinimumBoundingBox(out nuint length);

        [Throws]
        [SetInstance]
        public partial void Morphology(MorphologyMethod method, string kernel, Channels channels, nint iterations);

        [Throws]
        [SetInstance]
        public partial void MotionBlur(double radius, double sigma, double angle);

        [Throws]
        public partial void Negate(bool onlyGrayscale, Channels channels);

        [Throws]
        public partial void Normalize();

        [Throws]
        [SetInstance]
        public partial void OilPaint(double radius, double sigma);

        [Throws]
        public partial void Opaque(IMagickColor<QuantumType>? target, IMagickColor<QuantumType>? fill, bool invert);

        [Throws]
        public partial void OrderedDither(string thresholdMap, Channels channels);

        [Throws]
        public partial void Perceptible(double epsilon, Channels channels);

        [Throws]
        [Cleanup(Name = "ImageMagick.PerceptualHash.DisposeList")]
        public partial IntPtr PerceptualHash();

        [Throws]
        [SetInstance]
        public partial void Polaroid(DrawingSettings settings, string caption, double angle, PixelInterpolateMethod method);

        [Throws]
        public partial void Posterize(nuint levels, DitherMethod method, Channels channels);

        [Throws]
        public partial void Quantize(IQuantizeSettings settings);

        [Throws]
        public partial void RaiseOrLower(nuint size, bool raise);

        [Throws]
        public partial void RandomThreshold(double low, double high, Channels channels);

        [Throws]
        public partial void RangeThreshold(double low_black, double low_white, double high_white, double high_black);

        [Throws]
        [ReadInstance]
        public partial void ReadBlob(IMagickSettings<QuantumType>? settings, byte[] data, nuint offset, nuint length);

#if !NETSTANDARD2_0
        [Throws]
        [ReadInstance]
        public partial void ReadBlob(IMagickSettings<QuantumType>? settings, ReadOnlySpan<byte> data, nuint offset, nuint length);
#endif

        [Throws]
        [ReadInstance]
        public partial void ReadFile(IMagickSettings<QuantumType>? settings);

        [Throws]
        [ReadInstance]
        public partial void ReadPixels(nuint width, nuint height, string map, StorageType storageType, void* data, nuint offsetInBytes);

        [Throws]
        [ReadInstance]
        public partial void ReadStream(IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate reader, SeekStreamDelegate? seeker, TellStreamDelegate? teller, void* data);

        [Throws]
        public partial void RegionMask(MagickRectangle? region);

        [Throws]
        public partial bool Remap(IMagickImage image, IQuantizeSettings settings);

        public partial void RemoveArtifact(string name);

        public partial void RemoveAttribute(string name);

        public partial void RemoveProfile(string name);

        public partial void ResetArtifactIterator();

        public partial void ResetAttributeIterator();

        public partial void ResetProfileIterator();

        [Throws]
        [SetInstance]
        public partial void Resample(double resolutionX, double resolutionY);

        [Throws]
        public partial IntPtr Resize(string geometry);

        [Throws]
        [SetInstance]
        public partial void Roll(nint x, nint y);

        [Throws]
        [SetInstance]
        public partial void Rotate(double degrees);

        [Throws]
        [SetInstance]
        public partial void RotationalBlur(double angle, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Sample(string geometry);

        [Throws]
        [SetInstance]
        public partial void Scale(string geometry);

        [Throws]
        public partial void Segment(ColorSpace colorSpace, double clusterThreshold, double smoothingThreshold);

        [Throws]
        [SetInstance]
        public partial void SelectiveBlur(double radius, double sigma, double threshold, Channels channels);

        [Throws]
        [Cleanup(Name = "MagickImageCollection.DisposeList")]
        public partial IntPtr Separate(Channels channels);

        [Throws]
        [SetInstance]
        public partial void SepiaTone(double threshold);

        [Throws]
        public partial void SetAlpha(AlphaOption value);

        [Throws]
        public partial void SetArtifact(string name, string value);

        [Throws]
        public partial void SetAttribute(string name, string value);

        [Throws]
        public partial void SetBitDepth(nuint value, Channels channels);

        [Throws]
        public partial void SetColormapColor(nuint index, IMagickColor<QuantumType> color);

        [Throws]
        public partial bool SetColorMetric(IMagickImage image);

        [Throws]
        public partial void SetNext(IntPtr image);

        [Throws]
        public partial void SetProfile(string name, byte[] datum, nuint length);

        [Throws]
        public partial void SetProgressDelegate(ProgressDelegate? method);

        [Throws]
        public partial void SetReadMask(IMagickImage? image);

        [Throws]
        public partial void SetWriteMask(IMagickImage? image);

        [Throws]
        [SetInstance]
        public partial void Shade(double azimuth, double elevation, bool colorShading, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Shadow(nint x, nint y, double sigma, double alphaPercentage);

        [Throws]
        [SetInstance]
        public partial void Sharpen(double radius, double sigma, Channels channel);

        [Throws]
        [SetInstance]
        public partial void Shave(nuint leftRight, nuint topBottom);

        [Throws]
        [SetInstance]
        public partial void Shear(double xAngle, double yAngle);

        [Throws]
        public partial void SigmoidalContrast(bool sharpen, double contrast, double midpoint, Channels channels);

        [Throws]
        [SetInstance]
        public partial void SparseColor(Channels channel, SparseColorMethod method, double[] values, nuint length);

        [Throws]
        [SetInstance]
        public partial void Sketch(double radius, double sigma, double angle);

        [Throws]
        public partial void Solarize(double factor);

        [Throws]
        public partial void SortPixels();

        [Throws]
        [SetInstance]
        public partial void Splice(MagickRectangle geometry);

        [Throws]
        [SetInstance]
        public partial void Spread(PixelInterpolateMethod method, double radius);

        [Throws]
        [SetInstance]
        public partial void Statistic(StatisticType type, nuint width, nuint height);

        [Throws]
        [Cleanup(Name = "ImageMagick.Statistics.DisposeList")]
        public partial IntPtr Statistics(Channels channels);

        [Throws]
        [SetInstance]
        public partial void Stegano(IMagickImage watermark);

        [Throws]
        [SetInstance]
        public partial void Stereo(IMagickImage rightImage);

        [Throws]
        public partial void Strip();

        [Throws]
        [Cleanup(Name = nameof(Dispose))]
        public partial IntPtr SubImageSearch(IMagickImage reference, ErrorMetric metric, double similarityThreshold, out MagickRectangle offset, out double similarityMetric);

        [Throws]
        [SetInstance]
        public partial void Swirl(PixelInterpolateMethod method, double degrees);

        [Throws]
        public partial void Texture(IMagickImage image);

        [Throws]
        public partial void Threshold(double threshold, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Thumbnail(string geometry);

        [Throws]
        [SetInstance]
        public partial void Tint(string opacity, IMagickColor<QuantumType>? tint);

        [Throws]
        public partial void Transparent(IMagickColor<QuantumType>? color, bool invert);

        [Throws]
        public partial void TransparentChroma(IMagickColor<QuantumType>? colorLow, IMagickColor<QuantumType>? colorHigh, bool invert);

        [Throws]
        [SetInstance]
        public partial void Transpose();

        [Throws]
        [SetInstance]
        public partial void Transverse();

        [Throws]
        [SetInstance]
        public partial void Trim();

        [Throws]
        public partial IntPtr UniqueColors();

        [Throws]
        [SetInstance]
        public partial void UnsharpMask(double radius, double sigma, double amount, double threshold, Channels channels);

        [Throws]
        [SetInstance]
        public partial void Vignette(double radius, double sigma, nint x, nint y);

        [Throws]
        [SetInstance]
        public partial void Wave(PixelInterpolateMethod method, double amplitude, double length);

        [Throws]
        public partial void WhiteBalance();

        [Throws]
        [SetInstance]
        public partial void WaveletDenoise(double threshold, double softness);

        [Throws]
        public partial void WhiteThreshold(string threshold, Channels channels);

        [Throws]
        public partial void WriteFile(IMagickSettings<QuantumType>? settings);

        [Throws]
        public partial void WriteStream(IMagickSettings<QuantumType>? settings, ReadWriteStreamDelegate? writer, SeekStreamDelegate? seeker, TellStreamDelegate? teller, ReadWriteStreamDelegate? reader, void* data);
    }
}
