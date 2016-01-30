//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Runtime.InteropServices;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  public partial class MagickImage : IDisposable
  {
    private static class NativeMethods
    {
      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      public delegate bool ProgressDelegate(IntPtr origin, long offset, ulong extent, IntPtr userData);
      public static class X64
      {
        static X64() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Create(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AlphaColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AlphaColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_AnimationDelay_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnimationDelay_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_AnimationIterations_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnimationIterations_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BackgroundColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BackgroundColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_BaseHeight_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_BaseWidth_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_BlackPointCompensation_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BlackPointCompensation_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BorderColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BorderColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BoundingBox_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ChannelCount_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaBluePrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaBluePrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaGreenPrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaGreenPrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaRedPrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaRedPrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaWhitePoint_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaWhitePoint_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ClassType_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClassType_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ColorFuzz_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorFuzz_Set(IntPtr instance, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ColormapSize_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColormapSize_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ColorSpace_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorSpace_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ColorType_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorType_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Compose_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Compose_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_CompressionMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompressionMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Depth_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Depth_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Endian_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Endian_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_EncodingGeometry_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_FileName_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FileName_Set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long MagickImage_FileSize_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_FilterType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FilterType_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_Format_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Format_Set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_Gamma_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_GifDisposeMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_GifDisposeMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasAlpha_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_HasAlpha_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Interlace_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Interlace_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Interpolate_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Interpolate_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_IsOpaque_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_MeanErrorPerPixel_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_NormalizedMaximumError_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_NormalizedMeanError_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Orientation_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Orientation_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Page_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Page_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Quality_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Quality_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadMask_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ReadMask_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_RenderingIntent_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RenderingIntent_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ResolutionUnits_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionUnits_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ResolutionX_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionX_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ResolutionY_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionY_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_Signature_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_TotalColors_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_VirtualPixelMethod_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_VirtualPixelMethod_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_WriteMask_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WriteMask_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveBlur(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveResize(IntPtr Instance, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveSharpen(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveThreshold(IntPtr Instance, UIntPtr width, UIntPtr height, double bias, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AddNoise(IntPtr Instance, UIntPtr noiseType, double attenuate, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AddProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, byte[] datum, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AffineTransform(IntPtr Instance, double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Annotate(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string boundingArea, UIntPtr gravity, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnnotateGravity(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, UIntPtr gravity, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AutoGamma(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AutoLevel(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AutoOrient(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BlackThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BlueShift(IntPtr Instance, double factor, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Blur(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Border(IntPtr Instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BrightnessContrast(IntPtr Instance, double brightness, double contrast, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_CannyEdge(IntPtr Instance, double radius, double sigma, double lower, double upper, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ChannelOffset(IntPtr Instance, UIntPtr channel);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Charcoal(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Chop(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clamp(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClampChannel(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClipPath(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string pathName, [MarshalAs(UnmanagedType.Bool)] bool inside, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Clone(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clut(IntPtr Instance, IntPtr image, UIntPtr method, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorDecisionList(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fileName, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Colorize(IntPtr Instance, IntPtr color, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string blend, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Compare(IntPtr Instance, IntPtr image, UIntPtr metric, UIntPtr channels, out double distortion, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Contrast(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool enhance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ContrastStretch(IntPtr Instance, double blackPoint, double whitePoint, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ColorMatrix(IntPtr Instance, IntPtr matrix, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_CompareDistortion(IntPtr Instance, IntPtr image, UIntPtr metric, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Composite(IntPtr Instance, IntPtr image, IntPtr x, IntPtr y, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompositeGeometry(IntPtr Instance, IntPtr image, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompositeGravity(IntPtr Instance, IntPtr image, UIntPtr gravity, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ConnectedComponents(IntPtr Instance, UIntPtr connectivity, out IntPtr objects, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Convolve(IntPtr Instance, IntPtr matrix, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CopyPixels(IntPtr Instance, IntPtr image, IntPtr geometry, IntPtr offset, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Crop(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_CropToTiles(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CycleColormap(IntPtr Instance, IntPtr amount, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Decipher(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string passphrase, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Deskew(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Despeckle(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_DetermineColorType(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Distort(IntPtr Instance, UIntPtr method, [MarshalAs(UnmanagedType.Bool)] bool bestfit, double[] arguments, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Edge(IntPtr Instance, double radius, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Emboss(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Encipher(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string passphrase, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Enhance(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Equalize(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_Equals(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateFunction(IntPtr Instance, UIntPtr channels, UIntPtr evaluateFunction, double[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateGeometry(IntPtr Instance, UIntPtr channels, IntPtr geometry, UIntPtr evaluateOperator, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateOperator(IntPtr Instance, UIntPtr channels, UIntPtr evaluateOperator, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Extent(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ExtentGravity(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, UIntPtr gravity, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Flip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FloodFill(IntPtr Instance, IntPtr settings, IntPtr x, IntPtr y, IntPtr target, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Flop(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_FontTypeMetrics(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, [MarshalAs(UnmanagedType.Bool)] bool ignoreNewLines, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NativeMarshaler))]
        public static extern string MagickImage_FormatExpression(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string expression, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Frame(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Fx(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string expression, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_GammaCorrect(IntPtr Instance, double gamma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GaussianBlur(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_GetBitDepth(IntPtr Instance, UIntPtr channels);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetColormap(IntPtr Instance, UIntPtr index);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetNext(IntPtr image);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextArtifactName(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextAttributeName(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextProfileName(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Grayscale(IntPtr Instance, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_HaldClut(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasChannel(IntPtr Instance, UIntPtr channel);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Histogram(IntPtr Instance, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_HoughLine(IntPtr Instance, UIntPtr width, UIntPtr height, UIntPtr threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Implode(IntPtr Instance, double amount, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Kuwahara(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Level(IntPtr Instance, double blackPoint, double whitePoint, double gamma, UIntPtr channels);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_LevelColors(IntPtr Instance, IntPtr blackColor, IntPtr whiteColor, UIntPtr channels, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Levelize(IntPtr Instance, double blackPoint, double whitePoint, double gamma, UIntPtr channels);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_LinearStretch(IntPtr Instance, double blackPoint, double whitePoint, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_LiquidRescale(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_LocalContrast(IntPtr Instance, double radius, double strength, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Magnify(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_Map(IntPtr Instance, IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Minify(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Moments(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Modulate(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string modulate, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Morphology(IntPtr Instance, UIntPtr method, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string kernel, UIntPtr channels, UIntPtr iterations, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_MotionBlur(IntPtr Instance, double radius, double sigma, double angle, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Negate(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool onlyGrayscale, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Normalize(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_OilPaint(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Opaque(IntPtr Instance, IntPtr target, IntPtr fill, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_OrderedDither(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string thresholdMap, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Perceptible(IntPtr Instance, double epsilon, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_PerceptualHash(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Polaroid(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string caption, double angle, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Posterize(IntPtr Instance, UIntPtr levels, UIntPtr method, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Quantize(IntPtr Instance, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RaiseOrLower(IntPtr Instance, UIntPtr size, [MarshalAs(UnmanagedType.Bool)] bool raise, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RandomThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadBlob(IntPtr settings, byte[] data, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadFile(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadPixels(UIntPtr width, UIntPtr height, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string map, UIntPtr storageType, byte[] data, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetArtifactIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetAttributeIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetProfileIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Resample(IntPtr Instance, double resolutionX, double resolutionY, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Resize(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Roll(IntPtr Instance, IntPtr x, IntPtr y, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Rotate(IntPtr Instance, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_RotationalBlur(IntPtr Instance, double angle, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sample(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Scale(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Segment(IntPtr Instance, UIntPtr colorSpace, double clusterThreshold, double smoothingThreshold, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SelectiveBlur(IntPtr Instance, double radius, double sigma, double threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Separate(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SepiaTone(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetAlpha(IntPtr Instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetBitDepth(IntPtr Instance, UIntPtr channels, UIntPtr value);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetColormap(IntPtr Instance, UIntPtr index, IntPtr color, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_SetColorMetric(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetNext(IntPtr Instance, IntPtr image);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetProgressDelegate(IntPtr Instance, NativeMethods.ProgressDelegate method);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shade(IntPtr Instance, double azimuth, double elevation, [MarshalAs(UnmanagedType.Bool)] bool colorShading, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shadow(IntPtr Instance, IntPtr x, IntPtr y, double sigma, double alphaPercentage, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sharpen(IntPtr Instance, double radius, double sigma, UIntPtr channel, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shave(IntPtr Instance, UIntPtr leftRight, UIntPtr topBottom, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shear(IntPtr Instance, double xAngle, double yAngle, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SigmoidalContrast(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool sharpen, double contrast, double midpoint, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SparseColor(IntPtr Instance, UIntPtr channel, UIntPtr method, double[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sketch(IntPtr Instance, double radius, double sigma, double angle, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Solarize(IntPtr Instance, double factor, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Splice(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Spread(IntPtr Instance, UIntPtr method, double radius, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Statistic(IntPtr Instance, UIntPtr type, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Statistics(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Stegano(IntPtr Instance, IntPtr watermark, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Stereo(IntPtr Instance, IntPtr rightImage, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Strip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SubImageSearch(IntPtr Instance, IntPtr reference, UIntPtr metric, double similarityThreshold, IntPtr offset, out double similarityMetric, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Swirl(IntPtr Instance, UIntPtr method, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Texture(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Threshold(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Thumbnail(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Tint(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string opacity, IntPtr tint, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transform(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string cropGeometry, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string imageGeometry, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Transparent(IntPtr Instance, IntPtr color, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_TransparentChroma(IntPtr Instance, IntPtr colorLow, IntPtr colorHigh, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transpose(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transverse(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Trim(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_UniqueColors(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_UnsharpMask(IntPtr Instance, double radius, double sigma, double amount, double threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Vignette(IntPtr Instance, double radius, double sigma, IntPtr x, IntPtr y, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Wave(IntPtr Instance, UIntPtr method, double amplitude, double length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WhiteThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_WriteBlob(IntPtr Instance, IntPtr settings, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WriteFile(IntPtr Instance, IntPtr settings, out IntPtr exception);
      }
      public static class X86
      {
        static X86() { NativeLibrary.DoInitialize(); }
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Create(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Dispose(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AlphaColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AlphaColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_AnimationDelay_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnimationDelay_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_AnimationIterations_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnimationIterations_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BackgroundColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BackgroundColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_BaseHeight_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_BaseWidth_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_BlackPointCompensation_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BlackPointCompensation_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BorderColor_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BorderColor_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BoundingBox_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ChannelCount_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaBluePrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaBluePrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaGreenPrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaGreenPrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaRedPrimary_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaRedPrimary_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ChromaWhitePoint_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ChromaWhitePoint_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ClassType_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClassType_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ColorFuzz_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorFuzz_Set(IntPtr instance, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ColormapSize_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColormapSize_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ColorSpace_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorSpace_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ColorType_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorType_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Compose_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Compose_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_CompressionMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompressionMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Depth_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Depth_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Endian_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Endian_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_EncodingGeometry_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_FileName_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FileName_Set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern long MagickImage_FileSize_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_FilterType_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FilterType_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_Format_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Format_Set(IntPtr instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_Gamma_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_GifDisposeMethod_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_GifDisposeMethod_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Height_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasAlpha_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_HasAlpha_Set(IntPtr instance, [MarshalAs(UnmanagedType.Bool)] bool value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Interlace_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Interlace_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Interpolate_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Interpolate_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_IsOpaque_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_MeanErrorPerPixel_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_NormalizedMaximumError_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_NormalizedMeanError_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Orientation_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Orientation_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Page_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Page_Set(IntPtr instance, IntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Quality_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Quality_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadMask_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ReadMask_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_RenderingIntent_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RenderingIntent_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ResolutionUnits_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionUnits_Set(IntPtr instance, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ResolutionX_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionX_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_ResolutionY_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResolutionY_Set(IntPtr instance, double value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_Signature_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_TotalColors_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_VirtualPixelMethod_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_VirtualPixelMethod_Set(IntPtr instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_Width_Get(IntPtr instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_WriteMask_Get(IntPtr instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WriteMask_Set(IntPtr instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveBlur(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveResize(IntPtr Instance, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveSharpen(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AdaptiveThreshold(IntPtr Instance, UIntPtr width, UIntPtr height, double bias, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AddNoise(IntPtr Instance, UIntPtr noiseType, double attenuate, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AddProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, byte[] datum, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AffineTransform(IntPtr Instance, double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Annotate(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string boundingArea, UIntPtr gravity, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AnnotateGravity(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, UIntPtr gravity, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AutoGamma(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_AutoLevel(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_AutoOrient(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BlackThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_BlueShift(IntPtr Instance, double factor, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Blur(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Border(IntPtr Instance, IntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_BrightnessContrast(IntPtr Instance, double brightness, double contrast, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_CannyEdge(IntPtr Instance, double radius, double sigma, double lower, double upper, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_ChannelOffset(IntPtr Instance, UIntPtr channel);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Charcoal(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Chop(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clamp(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClampChannel(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ClipPath(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string pathName, [MarshalAs(UnmanagedType.Bool)] bool inside, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Clone(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Clut(IntPtr Instance, IntPtr image, UIntPtr method, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ColorDecisionList(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string fileName, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Colorize(IntPtr Instance, IntPtr color, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string blend, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Compare(IntPtr Instance, IntPtr image, UIntPtr metric, UIntPtr channels, out double distortion, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Contrast(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool enhance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ContrastStretch(IntPtr Instance, double blackPoint, double whitePoint, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ColorMatrix(IntPtr Instance, IntPtr matrix, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern double MagickImage_CompareDistortion(IntPtr Instance, IntPtr image, UIntPtr metric, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Composite(IntPtr Instance, IntPtr image, IntPtr x, IntPtr y, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompositeGeometry(IntPtr Instance, IntPtr image, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CompositeGravity(IntPtr Instance, IntPtr image, UIntPtr gravity, UIntPtr compose, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ConnectedComponents(IntPtr Instance, UIntPtr connectivity, out IntPtr objects, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Convolve(IntPtr Instance, IntPtr matrix, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CopyPixels(IntPtr Instance, IntPtr image, IntPtr geometry, IntPtr offset, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Crop(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_CropToTiles(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_CycleColormap(IntPtr Instance, IntPtr amount, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Decipher(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string passphrase, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Deskew(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Despeckle(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_DetermineColorType(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Distort(IntPtr Instance, UIntPtr method, [MarshalAs(UnmanagedType.Bool)] bool bestfit, double[] arguments, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Edge(IntPtr Instance, double radius, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Emboss(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Encipher(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string passphrase, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Enhance(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Equalize(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_Equals(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateFunction(IntPtr Instance, UIntPtr channels, UIntPtr evaluateFunction, double[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateGeometry(IntPtr Instance, UIntPtr channels, IntPtr geometry, UIntPtr evaluateOperator, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_EvaluateOperator(IntPtr Instance, UIntPtr channels, UIntPtr evaluateOperator, double value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Extent(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ExtentGravity(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, UIntPtr gravity, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Flip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_FloodFill(IntPtr Instance, IntPtr settings, IntPtr x, IntPtr y, IntPtr target, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Flop(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_FontTypeMetrics(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string text, [MarshalAs(UnmanagedType.Bool)] bool ignoreNewLines, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NativeMarshaler))]
        public static extern string MagickImage_FormatExpression(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string expression, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Frame(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Fx(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string expression, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_GammaCorrect(IntPtr Instance, double gamma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GaussianBlur(IntPtr Instance, double radius, double sigma, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr MagickImage_GetBitDepth(IntPtr Instance, UIntPtr channels);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetColormap(IntPtr Instance, UIntPtr index);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetNext(IntPtr image);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextArtifactName(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextAttributeName(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8NoCleanupMarshaler))]
        public static extern string MagickImage_GetNextProfileName(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_GetProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Grayscale(IntPtr Instance, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_HaldClut(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasChannel(IntPtr Instance, UIntPtr channel);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_HasProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Histogram(IntPtr Instance, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_HoughLine(IntPtr Instance, UIntPtr width, UIntPtr height, UIntPtr threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Implode(IntPtr Instance, double amount, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Kuwahara(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Level(IntPtr Instance, double blackPoint, double whitePoint, double gamma, UIntPtr channels);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_LevelColors(IntPtr Instance, IntPtr blackColor, IntPtr whiteColor, UIntPtr channels, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Levelize(IntPtr Instance, double blackPoint, double whitePoint, double gamma, UIntPtr channels);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_LinearStretch(IntPtr Instance, double blackPoint, double whitePoint, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_LiquidRescale(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_LocalContrast(IntPtr Instance, double radius, double strength, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Magnify(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_Map(IntPtr Instance, IntPtr image, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Minify(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Moments(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Modulate(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string modulate, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Morphology(IntPtr Instance, UIntPtr method, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string kernel, UIntPtr channels, UIntPtr iterations, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_MotionBlur(IntPtr Instance, double radius, double sigma, double angle, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Negate(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool onlyGrayscale, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Normalize(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_OilPaint(IntPtr Instance, double radius, double sigma, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Opaque(IntPtr Instance, IntPtr target, IntPtr fill, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_OrderedDither(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string thresholdMap, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Perceptible(IntPtr Instance, double epsilon, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_PerceptualHash(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Polaroid(IntPtr Instance, IntPtr settings, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string caption, double angle, UIntPtr method, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Posterize(IntPtr Instance, UIntPtr levels, UIntPtr method, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Quantize(IntPtr Instance, IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RaiseOrLower(IntPtr Instance, UIntPtr size, [MarshalAs(UnmanagedType.Bool)] bool raise, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RandomThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadBlob(IntPtr settings, byte[] data, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadFile(IntPtr settings, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_ReadPixels(UIntPtr width, UIntPtr height, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string map, UIntPtr storageType, byte[] data, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_RemoveProfile(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetArtifactIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetAttributeIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_ResetProfileIterator(IntPtr Instance);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Resample(IntPtr Instance, double resolutionX, double resolutionY, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Resize(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Roll(IntPtr Instance, IntPtr x, IntPtr y, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Rotate(IntPtr Instance, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_RotationalBlur(IntPtr Instance, double angle, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sample(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Scale(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Segment(IntPtr Instance, UIntPtr colorSpace, double clusterThreshold, double smoothingThreshold, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SelectiveBlur(IntPtr Instance, double radius, double sigma, double threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Separate(IntPtr Instance, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SepiaTone(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetAlpha(IntPtr Instance, UIntPtr value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetArtifact(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetAttribute(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string name, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string value, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetBitDepth(IntPtr Instance, UIntPtr channels, UIntPtr value);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetColormap(IntPtr Instance, UIntPtr index, IntPtr color, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MagickImage_SetColorMetric(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetNext(IntPtr Instance, IntPtr image);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SetProgressDelegate(IntPtr Instance, NativeMethods.ProgressDelegate method);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shade(IntPtr Instance, double azimuth, double elevation, [MarshalAs(UnmanagedType.Bool)] bool colorShading, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shadow(IntPtr Instance, IntPtr x, IntPtr y, double sigma, double alphaPercentage, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sharpen(IntPtr Instance, double radius, double sigma, UIntPtr channel, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shave(IntPtr Instance, UIntPtr leftRight, UIntPtr topBottom, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Shear(IntPtr Instance, double xAngle, double yAngle, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_SigmoidalContrast(IntPtr Instance, [MarshalAs(UnmanagedType.Bool)] bool sharpen, double contrast, double midpoint, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SparseColor(IntPtr Instance, UIntPtr channel, UIntPtr method, double[] values, UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Sketch(IntPtr Instance, double radius, double sigma, double angle, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Solarize(IntPtr Instance, double factor, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Splice(IntPtr Instance, IntPtr geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Spread(IntPtr Instance, UIntPtr method, double radius, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Statistic(IntPtr Instance, UIntPtr type, UIntPtr width, UIntPtr height, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Statistics(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Stegano(IntPtr Instance, IntPtr watermark, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Stereo(IntPtr Instance, IntPtr rightImage, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Strip(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_SubImageSearch(IntPtr Instance, IntPtr reference, UIntPtr metric, double similarityThreshold, IntPtr offset, out double similarityMetric, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Swirl(IntPtr Instance, UIntPtr method, double degrees, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Texture(IntPtr Instance, IntPtr image, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Threshold(IntPtr Instance, double threshold, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Thumbnail(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string geometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Tint(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string opacity, IntPtr tint, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transform(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string cropGeometry, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string imageGeometry, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_Transparent(IntPtr Instance, IntPtr color, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_TransparentChroma(IntPtr Instance, IntPtr colorLow, IntPtr colorHigh, [MarshalAs(UnmanagedType.Bool)] bool invert, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transpose(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Transverse(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Trim(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_UniqueColors(IntPtr Instance, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_UnsharpMask(IntPtr Instance, double radius, double sigma, double amount, double threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Vignette(IntPtr Instance, double radius, double sigma, IntPtr x, IntPtr y, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_Wave(IntPtr Instance, UIntPtr method, double amplitude, double length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WhiteThreshold(IntPtr Instance, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string threshold, UIntPtr channels, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MagickImage_WriteBlob(IntPtr Instance, IntPtr settings, out UIntPtr length, out IntPtr exception);
        [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MagickImage_WriteFile(IntPtr Instance, IntPtr settings, out IntPtr exception);
      }
    }
    private NativeMagickImage _NativeInstance;
    private sealed class NativeMagickImage : NativeInstance
    {
      private IntPtr _Instance = IntPtr.Zero;
      protected override void Dispose(IntPtr instance)
      {
        DisposeInstance(instance);
      }
      public static void DisposeInstance(IntPtr instance)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Dispose(instance);
        else
          NativeMethods.X86.MagickImage_Dispose(instance);
      }
      public NativeMagickImage(MagickSettings settings)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          _Instance = NativeMethods.X64.MagickImage_Create(MagickSettings.GetInstance(settings), out exception);
        else
          _Instance = NativeMethods.X86.MagickImage_Create(MagickSettings.GetInstance(settings), out exception);
        CheckException(exception, _Instance);
        if (_Instance == IntPtr.Zero)
          throw new InvalidOperationException();
      }
      public NativeMagickImage(IntPtr instance)
      {
        _Instance = instance;
      }
      public override IntPtr Instance
      {
        get
        {
          if (_Instance == IntPtr.Zero)
            throw new ObjectDisposedException(typeof(MagickImage).ToString());
          return _Instance;
        }
        set
        {
          if (_Instance != IntPtr.Zero)
            Dispose(_Instance);
          _Instance = value;
        }
      }
      public MagickColor AlphaColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_AlphaColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_AlphaColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_AlphaColor_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_AlphaColor_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public int AnimationDelay
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_AnimationDelay_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_AnimationDelay_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_AnimationDelay_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_AnimationDelay_Set(Instance, (UIntPtr)value);
        }
      }
      public int AnimationIterations
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_AnimationIterations_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_AnimationIterations_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_AnimationIterations_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_AnimationIterations_Set(Instance, (UIntPtr)value);
        }
      }
      public MagickColor BackgroundColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BackgroundColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BackgroundColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_BackgroundColor_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_BackgroundColor_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public int BaseHeight
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BaseHeight_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BaseHeight_Get(Instance);
          return (int)result;
        }
      }
      public int BaseWidth
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BaseWidth_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BaseWidth_Get(Instance);
          return (int)result;
        }
      }
      public bool BlackPointCompensation
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BlackPointCompensation_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BlackPointCompensation_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_BlackPointCompensation_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_BlackPointCompensation_Set(Instance, value);
        }
      }
      public MagickColor BorderColor
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BorderColor_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BorderColor_Get(Instance);
          return MagickColor.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = MagickColor.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_BorderColor_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_BorderColor_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public MagickRectangle BoundingBox
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_BoundingBox_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_BoundingBox_Get(Instance);
          return MagickRectangle.CreateInstance(result);
        }
      }
      public int ChannelCount
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ChannelCount_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ChannelCount_Get(Instance);
          return (int)result;
        }
      }
      public PrimaryInfo ChromaBluePrimary
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ChromaBluePrimary_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ChromaBluePrimary_Get(Instance);
          return PrimaryInfo.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = PrimaryInfo.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_ChromaBluePrimary_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_ChromaBluePrimary_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public PrimaryInfo ChromaGreenPrimary
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ChromaGreenPrimary_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ChromaGreenPrimary_Get(Instance);
          return PrimaryInfo.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = PrimaryInfo.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_ChromaGreenPrimary_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_ChromaGreenPrimary_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public PrimaryInfo ChromaRedPrimary
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ChromaRedPrimary_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ChromaRedPrimary_Get(Instance);
          return PrimaryInfo.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = PrimaryInfo.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_ChromaRedPrimary_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_ChromaRedPrimary_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public PrimaryInfo ChromaWhitePoint
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ChromaWhitePoint_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ChromaWhitePoint_Get(Instance);
          return PrimaryInfo.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = PrimaryInfo.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_ChromaWhitePoint_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_ChromaWhitePoint_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public ClassType ClassType
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ClassType_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ClassType_Get(Instance, out exception);
          CheckException(exception);
          return (ClassType)result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ClassType_Set(Instance, (UIntPtr)value, out exception);
          else
            NativeMethods.X86.MagickImage_ClassType_Set(Instance, (UIntPtr)value, out exception);
          CheckException(exception);
        }
      }
      public double ColorFuzz
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ColorFuzz_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ColorFuzz_Get(Instance, out exception);
          CheckException(exception);
          return result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ColorFuzz_Set(Instance, value, out exception);
          else
            NativeMethods.X86.MagickImage_ColorFuzz_Set(Instance, value, out exception);
          CheckException(exception);
        }
      }
      public int ColormapSize
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ColormapSize_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ColormapSize_Get(Instance, out exception);
          CheckException(exception);
          return (int)result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ColormapSize_Set(Instance, (IntPtr)value, out exception);
          else
            NativeMethods.X86.MagickImage_ColormapSize_Set(Instance, (IntPtr)value, out exception);
          CheckException(exception);
        }
      }
      public ColorSpace ColorSpace
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ColorSpace_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ColorSpace_Get(Instance, out exception);
          CheckException(exception);
          return (ColorSpace)result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ColorSpace_Set(Instance, (UIntPtr)value, out exception);
          else
            NativeMethods.X86.MagickImage_ColorSpace_Set(Instance, (UIntPtr)value, out exception);
          CheckException(exception);
        }
      }
      public ColorType ColorType
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ColorType_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ColorType_Get(Instance, out exception);
          CheckException(exception);
          return (ColorType)result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ColorType_Set(Instance, (UIntPtr)value, out exception);
          else
            NativeMethods.X86.MagickImage_ColorType_Set(Instance, (UIntPtr)value, out exception);
          CheckException(exception);
        }
      }
      public CompositeOperator Compose
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Compose_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Compose_Get(Instance);
          return (CompositeOperator)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Compose_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Compose_Set(Instance, (UIntPtr)value);
        }
      }
      public CompressionMethod CompressionMethod
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_CompressionMethod_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_CompressionMethod_Get(Instance);
          return (CompressionMethod)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_CompressionMethod_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_CompressionMethod_Set(Instance, (UIntPtr)value);
        }
      }
      public int Depth
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Depth_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Depth_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Depth_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Depth_Set(Instance, (UIntPtr)value);
        }
      }
      public Endian Endian
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Endian_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Endian_Get(Instance);
          return (Endian)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Endian_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Endian_Set(Instance, (UIntPtr)value);
        }
      }
      public string EncodingGeometry
      {
        get
        {
          string result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_EncodingGeometry_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_EncodingGeometry_Get(Instance);
          return result;
        }
      }
      public string FileName
      {
        get
        {
          string result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_FileName_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_FileName_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_FileName_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_FileName_Set(Instance, value);
        }
      }
      public long FileSize
      {
        get
        {
          long result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_FileSize_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_FileSize_Get(Instance);
          return result;
        }
      }
      public FilterType FilterType
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_FilterType_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_FilterType_Get(Instance);
          return (FilterType)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_FilterType_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_FilterType_Set(Instance, (UIntPtr)value);
        }
      }
      public string Format
      {
        get
        {
          string result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Format_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Format_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Format_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_Format_Set(Instance, value);
        }
      }
      public double Gamma
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Gamma_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Gamma_Get(Instance);
          return result;
        }
      }
      public GifDisposeMethod GifDisposeMethod
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_GifDisposeMethod_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_GifDisposeMethod_Get(Instance);
          return (GifDisposeMethod)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_GifDisposeMethod_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_GifDisposeMethod_Set(Instance, (UIntPtr)value);
        }
      }
      public int Height
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Height_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Height_Get(Instance);
          return (int)result;
        }
      }
      public bool HasAlpha
      {
        get
        {
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_HasAlpha_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_HasAlpha_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_HasAlpha_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_HasAlpha_Set(Instance, value);
        }
      }
      public Interlace Interlace
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Interlace_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Interlace_Get(Instance);
          return (Interlace)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Interlace_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Interlace_Set(Instance, (UIntPtr)value);
        }
      }
      public PixelInterpolateMethod Interpolate
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Interpolate_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Interpolate_Get(Instance);
          return (PixelInterpolateMethod)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Interpolate_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Interpolate_Set(Instance, (UIntPtr)value);
        }
      }
      public bool IsOpaque
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_IsOpaque_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_IsOpaque_Get(Instance, out exception);
          CheckException(exception);
          return result;
        }
      }
      public double MeanErrorPerPixel
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_MeanErrorPerPixel_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_MeanErrorPerPixel_Get(Instance);
          return result;
        }
      }
      public double NormalizedMaximumError
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_NormalizedMaximumError_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_NormalizedMaximumError_Get(Instance);
          return result;
        }
      }
      public double NormalizedMeanError
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_NormalizedMeanError_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_NormalizedMeanError_Get(Instance);
          return result;
        }
      }
      public OrientationType Orientation
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Orientation_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Orientation_Get(Instance);
          return (OrientationType)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Orientation_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Orientation_Set(Instance, (UIntPtr)value);
        }
      }
      public MagickRectangle Page
      {
        get
        {
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Page_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Page_Get(Instance);
          return MagickRectangle.CreateInstance(result);
        }
        set
        {
          using (INativeInstance valueDynamic = MagickRectangle.CreateInstance(value))
          {
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_Page_Set(Instance, valueDynamic.Instance);
            else
              NativeMethods.X86.MagickImage_Page_Set(Instance, valueDynamic.Instance);
          }
        }
      }
      public int Quality
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Quality_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Quality_Get(Instance);
          return (int)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Quality_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_Quality_Set(Instance, (UIntPtr)value);
        }
      }
      public MagickImage ReadMask
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ReadMask_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ReadMask_Get(Instance, out exception);
          CheckException(exception);
          return MagickImage.Create(result);
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ReadMask_Set(Instance, MagickImage.GetInstance(value), out exception);
          else
            NativeMethods.X86.MagickImage_ReadMask_Set(Instance, MagickImage.GetInstance(value), out exception);
          CheckException(exception);
        }
      }
      public RenderingIntent RenderingIntent
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_RenderingIntent_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_RenderingIntent_Get(Instance);
          return (RenderingIntent)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_RenderingIntent_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_RenderingIntent_Set(Instance, (UIntPtr)value);
        }
      }
      public Resolution ResolutionUnits
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ResolutionUnits_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ResolutionUnits_Get(Instance);
          return (Resolution)result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ResolutionUnits_Set(Instance, (UIntPtr)value);
          else
            NativeMethods.X86.MagickImage_ResolutionUnits_Set(Instance, (UIntPtr)value);
        }
      }
      public double ResolutionX
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ResolutionX_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ResolutionX_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ResolutionX_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_ResolutionX_Set(Instance, value);
        }
      }
      public double ResolutionY
      {
        get
        {
          double result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ResolutionY_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_ResolutionY_Get(Instance);
          return result;
        }
        set
        {
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_ResolutionY_Set(Instance, value);
          else
            NativeMethods.X86.MagickImage_ResolutionY_Set(Instance, value);
        }
      }
      public string Signature
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          string result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Signature_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Signature_Get(Instance, out exception);
          CheckException(exception);
          return result;
        }
      }
      public int TotalColors
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_TotalColors_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_TotalColors_Get(Instance, out exception);
          CheckException(exception);
          return (int)result;
        }
      }
      public VirtualPixelMethod VirtualPixelMethod
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_VirtualPixelMethod_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_VirtualPixelMethod_Get(Instance, out exception);
          CheckException(exception);
          return (VirtualPixelMethod)result;
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_VirtualPixelMethod_Set(Instance, (UIntPtr)value, out exception);
          else
            NativeMethods.X86.MagickImage_VirtualPixelMethod_Set(Instance, (UIntPtr)value, out exception);
          CheckException(exception);
        }
      }
      public int Width
      {
        get
        {
          UIntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Width_Get(Instance);
          else
            result = NativeMethods.X86.MagickImage_Width_Get(Instance);
          return (int)result;
        }
      }
      public MagickImage WriteMask
      {
        get
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_WriteMask_Get(Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_WriteMask_Get(Instance, out exception);
          CheckException(exception);
          return MagickImage.Create(result);
        }
        set
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_WriteMask_Set(Instance, MagickImage.GetInstance(value), out exception);
          else
            NativeMethods.X86.MagickImage_WriteMask_Set(Instance, MagickImage.GetInstance(value), out exception);
          CheckException(exception);
        }
      }
      public void AdaptiveBlur(double radius, double sigma)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AdaptiveBlur(Instance, radius, sigma, out exception);
        else
          result = NativeMethods.X86.MagickImage_AdaptiveBlur(Instance, radius, sigma, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void AdaptiveResize(int width, int height)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AdaptiveResize(Instance, (UIntPtr)width, (UIntPtr)height, out exception);
        else
          result = NativeMethods.X86.MagickImage_AdaptiveResize(Instance, (UIntPtr)width, (UIntPtr)height, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void AdaptiveSharpen(double radius, double sigma, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AdaptiveSharpen(Instance, radius, sigma, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_AdaptiveSharpen(Instance, radius, sigma, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void AdaptiveThreshold(int width, int height, double bias)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AdaptiveThreshold(Instance, (UIntPtr)width, (UIntPtr)height, bias, out exception);
        else
          result = NativeMethods.X86.MagickImage_AdaptiveThreshold(Instance, (UIntPtr)width, (UIntPtr)height, bias, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void AddNoise(NoiseType noiseType, double attenuate, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AddNoise(Instance, (UIntPtr)noiseType, attenuate, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_AddNoise(Instance, (UIntPtr)noiseType, attenuate, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void AddProfile(string name, byte[] datum, int length)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_AddProfile(Instance, name, datum, (UIntPtr)length, out exception);
        else
          NativeMethods.X86.MagickImage_AddProfile(Instance, name, datum, (UIntPtr)length, out exception);
        CheckException(exception);
      }
      public void AffineTransform(double scaleX, double scaleY, double shearX, double shearY, double translateX, double translateY)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AffineTransform(Instance, scaleX, scaleY, shearX, shearY, translateX, translateY, out exception);
        else
          result = NativeMethods.X86.MagickImage_AffineTransform(Instance, scaleX, scaleY, shearX, shearY, translateX, translateY, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Annotate(DrawingSettings settings, string text, string boundingArea, Gravity gravity, double degrees)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Annotate(Instance, DrawingSettings.GetInstance(settings), text, boundingArea, (UIntPtr)gravity, degrees, out exception);
        else
          NativeMethods.X86.MagickImage_Annotate(Instance, DrawingSettings.GetInstance(settings), text, boundingArea, (UIntPtr)gravity, degrees, out exception);
        CheckException(exception);
      }
      public void AnnotateGravity(DrawingSettings settings, string text, Gravity gravity)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_AnnotateGravity(Instance, DrawingSettings.GetInstance(settings), text, (UIntPtr)gravity, out exception);
        else
          NativeMethods.X86.MagickImage_AnnotateGravity(Instance, DrawingSettings.GetInstance(settings), text, (UIntPtr)gravity, out exception);
        CheckException(exception);
      }
      public void AutoGamma(Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_AutoGamma(Instance, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_AutoGamma(Instance, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void AutoLevel(Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_AutoLevel(Instance, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_AutoLevel(Instance, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void AutoOrient()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_AutoOrient(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_AutoOrient(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void BlackThreshold(string threshold, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_BlackThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_BlackThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void BlueShift(double factor)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_BlueShift(Instance, factor, out exception);
        else
          result = NativeMethods.X86.MagickImage_BlueShift(Instance, factor, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Blur(double radius, double sigma, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Blur(Instance, radius, sigma, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_Blur(Instance, radius, sigma, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Border(MagickRectangle value)
      {
        using (INativeInstance valueDynamic = MagickRectangle.CreateInstance(value))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Border(Instance, valueDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Border(Instance, valueDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void BrightnessContrast(double brightness, double contrast, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_BrightnessContrast(Instance, brightness, contrast, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_BrightnessContrast(Instance, brightness, contrast, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void CannyEdge(double radius, double sigma, double lower, double upper)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_CannyEdge(Instance, radius, sigma, lower, upper, out exception);
        else
          result = NativeMethods.X86.MagickImage_CannyEdge(Instance, radius, sigma, lower, upper, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public int ChannelOffset(PixelChannel channel)
      {
        if (NativeLibrary.Is64Bit)
          return (int)NativeMethods.X64.MagickImage_ChannelOffset(Instance, (UIntPtr)channel);
        else
          return (int)NativeMethods.X86.MagickImage_ChannelOffset(Instance, (UIntPtr)channel);
      }
      public void Charcoal(double radius, double sigma)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Charcoal(Instance, radius, sigma, out exception);
        else
          result = NativeMethods.X86.MagickImage_Charcoal(Instance, radius, sigma, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Chop(MagickRectangle geometry)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Chop(Instance, geometryDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Chop(Instance, geometryDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void Clamp()
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Clamp(Instance, out exception);
        else
          NativeMethods.X86.MagickImage_Clamp(Instance, out exception);
        CheckException(exception);
      }
      public void ClampChannel(Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ClampChannel(Instance, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_ClampChannel(Instance, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void Clip()
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Clip(Instance, out exception);
        else
          NativeMethods.X86.MagickImage_Clip(Instance, out exception);
        CheckException(exception);
      }
      public void ClipPath(string pathName, bool inside)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ClipPath(Instance, pathName, inside, out exception);
        else
          NativeMethods.X86.MagickImage_ClipPath(Instance, pathName, inside, out exception);
        CheckException(exception);
      }
      public IntPtr Clone()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Clone(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Clone(Instance, out exception);
        CheckException(exception, result);
        return result;
      }
      public void Clut(MagickImage image, PixelInterpolateMethod method, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Clut(Instance, MagickImage.GetInstance(image), (UIntPtr)method, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_Clut(Instance, MagickImage.GetInstance(image), (UIntPtr)method, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void ColorDecisionList(string fileName)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ColorDecisionList(Instance, fileName, out exception);
        else
          NativeMethods.X86.MagickImage_ColorDecisionList(Instance, fileName, out exception);
        CheckException(exception);
      }
      public void Colorize(MagickColor color, string blend)
      {
        using (INativeInstance colorDynamic = MagickColor.CreateInstance(color))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Colorize(Instance, colorDynamic.Instance, blend, out exception);
          else
            result = NativeMethods.X86.MagickImage_Colorize(Instance, colorDynamic.Instance, blend, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public IntPtr Compare(MagickImage image, ErrorMetric metric, Channels channels, out double distortion)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Compare(Instance, MagickImage.GetInstance(image), (UIntPtr)metric, (UIntPtr)channels, out distortion, out exception);
        else
          result = NativeMethods.X86.MagickImage_Compare(Instance, MagickImage.GetInstance(image), (UIntPtr)metric, (UIntPtr)channels, out distortion, out exception);
        CheckException(exception, result);
        return result;
      }
      public void Contrast(bool enhance)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Contrast(Instance, enhance, out exception);
        else
          NativeMethods.X86.MagickImage_Contrast(Instance, enhance, out exception);
        CheckException(exception);
      }
      public void ContrastStretch(double blackPoint, double whitePoint, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ContrastStretch(Instance, blackPoint, whitePoint, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_ContrastStretch(Instance, blackPoint, whitePoint, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void ColorMatrix(DoubleMatrix matrix)
      {
        using (INativeInstance matrixDynamic = DoubleMatrix.CreateInstance(matrix))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_ColorMatrix(Instance, matrixDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_ColorMatrix(Instance, matrixDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public double CompareDistortion(MagickImage image, ErrorMetric metric, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        double result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_CompareDistortion(Instance, MagickImage.GetInstance(image), (UIntPtr)metric, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_CompareDistortion(Instance, MagickImage.GetInstance(image), (UIntPtr)metric, (UIntPtr)channels, out exception);
        CheckException(exception);
        return result;
      }
      public void Composite(MagickImage image, int x, int y, CompositeOperator compose)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Composite(Instance, MagickImage.GetInstance(image), (IntPtr)x, (IntPtr)y, (UIntPtr)compose, out exception);
        else
          NativeMethods.X86.MagickImage_Composite(Instance, MagickImage.GetInstance(image), (IntPtr)x, (IntPtr)y, (UIntPtr)compose, out exception);
        CheckException(exception);
      }
      public void CompositeGeometry(MagickImage image, string geometry, CompositeOperator compose)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_CompositeGeometry(Instance, MagickImage.GetInstance(image), geometry, (UIntPtr)compose, out exception);
        else
          NativeMethods.X86.MagickImage_CompositeGeometry(Instance, MagickImage.GetInstance(image), geometry, (UIntPtr)compose, out exception);
        CheckException(exception);
      }
      public void CompositeGravity(MagickImage image, Gravity gravity, CompositeOperator compose)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_CompositeGravity(Instance, MagickImage.GetInstance(image), (UIntPtr)gravity, (UIntPtr)compose, out exception);
        else
          NativeMethods.X86.MagickImage_CompositeGravity(Instance, MagickImage.GetInstance(image), (UIntPtr)gravity, (UIntPtr)compose, out exception);
        CheckException(exception);
      }
      public void ConnectedComponents(int connectivity, out IntPtr objects)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_ConnectedComponents(Instance, (UIntPtr)connectivity, out objects, out exception);
        else
          result = NativeMethods.X86.MagickImage_ConnectedComponents(Instance, (UIntPtr)connectivity, out objects, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Convolve(DoubleMatrix matrix)
      {
        using (INativeInstance matrixDynamic = DoubleMatrix.CreateInstance(matrix))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Convolve(Instance, matrixDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Convolve(Instance, matrixDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void CopyPixels(MagickImage image, MagickRectangle geometry, OffsetInfo offset)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          using (INativeInstance offsetDynamic = OffsetInfo.CreateInstance(offset))
          {
            IntPtr exception = IntPtr.Zero;
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_CopyPixels(Instance, MagickImage.GetInstance(image), geometryDynamic.Instance, offsetDynamic.Instance, out exception);
            else
              NativeMethods.X86.MagickImage_CopyPixels(Instance, MagickImage.GetInstance(image), geometryDynamic.Instance, offsetDynamic.Instance, out exception);
            CheckException(exception);
          }
        }
      }
      public void Crop(MagickRectangle geometry)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Crop(Instance, geometryDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Crop(Instance, geometryDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public IntPtr CropToTiles(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_CropToTiles(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_CropToTiles(Instance, geometry, out exception);
        CheckException(exception);
        return result;
      }
      public void CycleColormap(int amount)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_CycleColormap(Instance, (IntPtr)amount, out exception);
        else
          NativeMethods.X86.MagickImage_CycleColormap(Instance, (IntPtr)amount, out exception);
        CheckException(exception);
      }
      public void Decipher(string passphrase)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Decipher(Instance, passphrase, out exception);
        else
          NativeMethods.X86.MagickImage_Decipher(Instance, passphrase, out exception);
        CheckException(exception);
      }
      public void Deskew(double threshold)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Deskew(Instance, threshold, out exception);
        else
          result = NativeMethods.X86.MagickImage_Deskew(Instance, threshold, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Despeckle()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Despeckle(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Despeckle(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public ColorType DetermineColorType()
      {
        IntPtr exception = IntPtr.Zero;
        UIntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_DetermineColorType(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_DetermineColorType(Instance, out exception);
        CheckException(exception);
        return (ColorType)result;
      }
      public void Distort(DistortMethod method, bool bestfit, double[] arguments, int length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Distort(Instance, (UIntPtr)method, bestfit, arguments, (UIntPtr)length, out exception);
        else
          result = NativeMethods.X86.MagickImage_Distort(Instance, (UIntPtr)method, bestfit, arguments, (UIntPtr)length, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Edge(double radius)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Edge(Instance, radius, out exception);
        else
          result = NativeMethods.X86.MagickImage_Edge(Instance, radius, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Emboss(double radius, double sigma)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Emboss(Instance, radius, sigma, out exception);
        else
          result = NativeMethods.X86.MagickImage_Emboss(Instance, radius, sigma, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Encipher(string passphrase)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Encipher(Instance, passphrase, out exception);
        else
          NativeMethods.X86.MagickImage_Encipher(Instance, passphrase, out exception);
        CheckException(exception);
      }
      public void Enhance()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Enhance(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Enhance(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Equalize()
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Equalize(Instance, out exception);
        else
          NativeMethods.X86.MagickImage_Equalize(Instance, out exception);
        CheckException(exception);
      }
      public bool Equals(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        bool result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Equals(Instance, MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImage_Equals(Instance, MagickImage.GetInstance(image), out exception);
        CheckException(exception);
        return result;
      }
      public void EvaluateFunction(Channels channels, EvaluateFunction evaluateFunction, double[] values, int length)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_EvaluateFunction(Instance, (UIntPtr)channels, (UIntPtr)evaluateFunction, values, (UIntPtr)length, out exception);
        else
          NativeMethods.X86.MagickImage_EvaluateFunction(Instance, (UIntPtr)channels, (UIntPtr)evaluateFunction, values, (UIntPtr)length, out exception);
        CheckException(exception);
      }
      public void EvaluateGeometry(Channels channels, MagickRectangle geometry, EvaluateOperator evaluateOperator, double value)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_EvaluateGeometry(Instance, (UIntPtr)channels, geometryDynamic.Instance, (UIntPtr)evaluateOperator, value, out exception);
          else
            NativeMethods.X86.MagickImage_EvaluateGeometry(Instance, (UIntPtr)channels, geometryDynamic.Instance, (UIntPtr)evaluateOperator, value, out exception);
          CheckException(exception);
        }
      }
      public void EvaluateOperator(Channels channels, EvaluateOperator evaluateOperator, double value)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_EvaluateOperator(Instance, (UIntPtr)channels, (UIntPtr)evaluateOperator, value, out exception);
        else
          NativeMethods.X86.MagickImage_EvaluateOperator(Instance, (UIntPtr)channels, (UIntPtr)evaluateOperator, value, out exception);
        CheckException(exception);
      }
      public void Extent(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Extent(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_Extent(Instance, geometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void ExtentGravity(string geometry, Gravity gravity)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_ExtentGravity(Instance, geometry, (UIntPtr)gravity, out exception);
        else
          result = NativeMethods.X86.MagickImage_ExtentGravity(Instance, geometry, (UIntPtr)gravity, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Flip()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Flip(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Flip(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void FloodFill(DrawingSettings settings, int x, int y, MagickColor target, bool invert)
      {
        using (INativeInstance targetDynamic = MagickColor.CreateInstance(target))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_FloodFill(Instance, DrawingSettings.GetInstance(settings), (IntPtr)x, (IntPtr)y, targetDynamic.Instance, invert, out exception);
          else
            NativeMethods.X86.MagickImage_FloodFill(Instance, DrawingSettings.GetInstance(settings), (IntPtr)x, (IntPtr)y, targetDynamic.Instance, invert, out exception);
          CheckException(exception);
        }
      }
      public void Flop()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Flop(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Flop(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public IntPtr FontTypeMetrics(DrawingSettings settings, string text, bool ignoreNewLines)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_FontTypeMetrics(Instance, DrawingSettings.GetInstance(settings), text, ignoreNewLines, out exception);
        else
          result = NativeMethods.X86.MagickImage_FontTypeMetrics(Instance, DrawingSettings.GetInstance(settings), text, ignoreNewLines, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            ImageMagick.TypeMetric.Dispose(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public string FormatExpression(MagickSettings settings, string expression)
      {
        IntPtr exception = IntPtr.Zero;
        string result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_FormatExpression(Instance, MagickSettings.GetInstance(settings), expression, out exception);
        else
          result = NativeMethods.X86.MagickImage_FormatExpression(Instance, MagickSettings.GetInstance(settings), expression, out exception);
        CheckException(exception);
        return result;
      }
      public void Frame(MagickRectangle geometry)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Frame(Instance, geometryDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Frame(Instance, geometryDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void Fx(string expression, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Fx(Instance, expression, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_Fx(Instance, expression, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void GammaCorrect(double gamma, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_GammaCorrect(Instance, gamma, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_GammaCorrect(Instance, gamma, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void GaussianBlur(double radius, double sigma, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_GaussianBlur(Instance, radius, sigma, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_GaussianBlur(Instance, radius, sigma, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public string GetArtifact(string name)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_GetArtifact(Instance, name);
        else
          return NativeMethods.X86.MagickImage_GetArtifact(Instance, name);
      }
      public string GetAttribute(string name)
      {
        IntPtr exception = IntPtr.Zero;
        string result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_GetAttribute(Instance, name, out exception);
        else
          result = NativeMethods.X86.MagickImage_GetAttribute(Instance, name, out exception);
        CheckException(exception);
        return result;
      }
      public int GetBitDepth(Channels channels)
      {
        if (NativeLibrary.Is64Bit)
          return (int)NativeMethods.X64.MagickImage_GetBitDepth(Instance, (UIntPtr)channels);
        else
          return (int)NativeMethods.X86.MagickImage_GetBitDepth(Instance, (UIntPtr)channels);
      }
      public MagickColor GetColormap(int index)
      {
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_GetColormap(Instance, (UIntPtr)index);
        else
          result = NativeMethods.X86.MagickImage_GetColormap(Instance, (UIntPtr)index);
        return MagickColor.CreateInstance(result);
      }
      public static IntPtr GetNext(IntPtr image)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_GetNext(image);
        else
          return NativeMethods.X86.MagickImage_GetNext(image);
      }
      public string GetNextArtifactName()
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_GetNextArtifactName(Instance);
        else
          return NativeMethods.X86.MagickImage_GetNextArtifactName(Instance);
      }
      public string GetNextAttributeName()
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_GetNextAttributeName(Instance);
        else
          return NativeMethods.X86.MagickImage_GetNextAttributeName(Instance);
      }
      public string GetNextProfileName()
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_GetNextProfileName(Instance);
        else
          return NativeMethods.X86.MagickImage_GetNextProfileName(Instance);
      }
      public StringInfo GetProfile(string name)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_GetProfile(Instance, name, out exception);
        else
          result = NativeMethods.X86.MagickImage_GetProfile(Instance, name, out exception);
        CheckException(exception);
        return StringInfo.CreateInstance(result);
      }
      public void Grayscale(PixelIntensityMethod method)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Grayscale(Instance, (UIntPtr)method, out exception);
        else
          NativeMethods.X86.MagickImage_Grayscale(Instance, (UIntPtr)method, out exception);
        CheckException(exception);
      }
      public void HaldClut(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_HaldClut(Instance, MagickImage.GetInstance(image), out exception);
        else
          NativeMethods.X86.MagickImage_HaldClut(Instance, MagickImage.GetInstance(image), out exception);
        CheckException(exception);
      }
      public bool HasChannel(PixelChannel channel)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_HasChannel(Instance, (UIntPtr)channel);
        else
          return NativeMethods.X86.MagickImage_HasChannel(Instance, (UIntPtr)channel);
      }
      public bool HasProfile(string name)
      {
        if (NativeLibrary.Is64Bit)
          return NativeMethods.X64.MagickImage_HasProfile(Instance, name);
        else
          return NativeMethods.X86.MagickImage_HasProfile(Instance, name);
      }
      public IntPtr Histogram(out UIntPtr length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Histogram(Instance, out length, out exception);
        else
          result = NativeMethods.X86.MagickImage_Histogram(Instance, out length, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            ImageMagick.MagickColorCollection.DisposeList(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void HoughLine(int width, int height, int threshold)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_HoughLine(Instance, (UIntPtr)width, (UIntPtr)height, (UIntPtr)threshold, out exception);
        else
          result = NativeMethods.X86.MagickImage_HoughLine(Instance, (UIntPtr)width, (UIntPtr)height, (UIntPtr)threshold, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Implode(double amount, PixelInterpolateMethod method)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Implode(Instance, amount, (UIntPtr)method, out exception);
        else
          result = NativeMethods.X86.MagickImage_Implode(Instance, amount, (UIntPtr)method, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Kuwahara(double radius, double sigma)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Kuwahara(Instance, radius, sigma, out exception);
        else
          result = NativeMethods.X86.MagickImage_Kuwahara(Instance, radius, sigma, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Level(double blackPoint, double whitePoint, double gamma, Channels channels)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Level(Instance, blackPoint, whitePoint, gamma, (UIntPtr)channels);
        else
          NativeMethods.X86.MagickImage_Level(Instance, blackPoint, whitePoint, gamma, (UIntPtr)channels);
      }
      public void LevelColors(MagickColor blackColor, MagickColor whiteColor, Channels channels, bool invert)
      {
        using (INativeInstance blackColorDynamic = MagickColor.CreateInstance(blackColor))
        {
          using (INativeInstance whiteColorDynamic = MagickColor.CreateInstance(whiteColor))
          {
            IntPtr exception = IntPtr.Zero;
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_LevelColors(Instance, blackColorDynamic.Instance, whiteColorDynamic.Instance, (UIntPtr)channels, invert, out exception);
            else
              NativeMethods.X86.MagickImage_LevelColors(Instance, blackColorDynamic.Instance, whiteColorDynamic.Instance, (UIntPtr)channels, invert, out exception);
            CheckException(exception);
          }
        }
      }
      public void Levelize(double blackPoint, double whitePoint, double gamma, Channels channels)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Levelize(Instance, blackPoint, whitePoint, gamma, (UIntPtr)channels);
        else
          NativeMethods.X86.MagickImage_Levelize(Instance, blackPoint, whitePoint, gamma, (UIntPtr)channels);
      }
      public void LinearStretch(double blackPoint, double whitePoint)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_LinearStretch(Instance, blackPoint, whitePoint, out exception);
        else
          NativeMethods.X86.MagickImage_LinearStretch(Instance, blackPoint, whitePoint, out exception);
        CheckException(exception);
      }
      public void LiquidRescale(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_LiquidRescale(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_LiquidRescale(Instance, geometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void LocalContrast(double radius, double strength)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_LocalContrast(Instance, radius, strength, out exception);
        else
          result = NativeMethods.X86.MagickImage_LocalContrast(Instance, radius, strength, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Magnify()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Magnify(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Magnify(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public bool Map(MagickImage image, QuantizeSettings settings)
      {
        using (INativeInstance settingsDynamic = QuantizeSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          bool result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Map(Instance, MagickImage.GetInstance(image), settingsDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Map(Instance, MagickImage.GetInstance(image), settingsDynamic.Instance, out exception);
          CheckException(exception);
          return result;
        }
      }
      public void Minify()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Minify(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Minify(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public IntPtr Moments()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Moments(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Moments(Instance, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            ImageMagick.Moments.DisposeList(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void Modulate(string modulate)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Modulate(Instance, modulate, out exception);
        else
          NativeMethods.X86.MagickImage_Modulate(Instance, modulate, out exception);
        CheckException(exception);
      }
      public void Morphology(MorphologyMethod method, string kernel, Channels channels, int iterations)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Morphology(Instance, (UIntPtr)method, kernel, (UIntPtr)channels, (UIntPtr)iterations, out exception);
        else
          result = NativeMethods.X86.MagickImage_Morphology(Instance, (UIntPtr)method, kernel, (UIntPtr)channels, (UIntPtr)iterations, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void MotionBlur(double radius, double sigma, double angle)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_MotionBlur(Instance, radius, sigma, angle, out exception);
        else
          result = NativeMethods.X86.MagickImage_MotionBlur(Instance, radius, sigma, angle, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Negate(bool onlyGrayscale, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Negate(Instance, onlyGrayscale, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_Negate(Instance, onlyGrayscale, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void Normalize()
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Normalize(Instance, out exception);
        else
          NativeMethods.X86.MagickImage_Normalize(Instance, out exception);
        CheckException(exception);
      }
      public void OilPaint(double radius, double sigma)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_OilPaint(Instance, radius, sigma, out exception);
        else
          result = NativeMethods.X86.MagickImage_OilPaint(Instance, radius, sigma, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Opaque(MagickColor target, MagickColor fill, bool invert)
      {
        using (INativeInstance targetDynamic = MagickColor.CreateInstance(target))
        {
          using (INativeInstance fillDynamic = MagickColor.CreateInstance(fill))
          {
            IntPtr exception = IntPtr.Zero;
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_Opaque(Instance, targetDynamic.Instance, fillDynamic.Instance, invert, out exception);
            else
              NativeMethods.X86.MagickImage_Opaque(Instance, targetDynamic.Instance, fillDynamic.Instance, invert, out exception);
            CheckException(exception);
          }
        }
      }
      public void OrderedDither(string thresholdMap, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_OrderedDither(Instance, thresholdMap, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_OrderedDither(Instance, thresholdMap, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void Perceptible(double epsilon, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Perceptible(Instance, epsilon, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_Perceptible(Instance, epsilon, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public IntPtr PerceptualHash()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_PerceptualHash(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_PerceptualHash(Instance, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            ImageMagick.PerceptualHash.DisposeList(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void Polaroid(DrawingSettings settings, string caption, double angle, PixelInterpolateMethod method)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Polaroid(Instance, DrawingSettings.GetInstance(settings), caption, angle, (UIntPtr)method, out exception);
        else
          result = NativeMethods.X86.MagickImage_Polaroid(Instance, DrawingSettings.GetInstance(settings), caption, angle, (UIntPtr)method, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Posterize(int levels, DitherMethod method, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Posterize(Instance, (UIntPtr)levels, (UIntPtr)method, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_Posterize(Instance, (UIntPtr)levels, (UIntPtr)method, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void Quantize(QuantizeSettings settings)
      {
        using (INativeInstance settingsDynamic = QuantizeSettings.CreateInstance(settings))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Quantize(Instance, settingsDynamic.Instance, out exception);
          else
            NativeMethods.X86.MagickImage_Quantize(Instance, settingsDynamic.Instance, out exception);
          CheckException(exception);
        }
      }
      public void RaiseOrLower(int size, bool raise)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_RaiseOrLower(Instance, (UIntPtr)size, raise, out exception);
        else
          NativeMethods.X86.MagickImage_RaiseOrLower(Instance, (UIntPtr)size, raise, out exception);
        CheckException(exception);
      }
      public void RandomThreshold(string threshold, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_RandomThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_RandomThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public void ReadBlob(MagickSettings settings, byte[] data, int length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_ReadBlob(MagickSettings.GetInstance(settings), data, (UIntPtr)length, out exception);
        else
          result = NativeMethods.X86.MagickImage_ReadBlob(MagickSettings.GetInstance(settings), data, (UIntPtr)length, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void ReadFile(MagickSettings settings)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_ReadFile(MagickSettings.GetInstance(settings), out exception);
        else
          result = NativeMethods.X86.MagickImage_ReadFile(MagickSettings.GetInstance(settings), out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void ReadPixels(int width, int height, string map, StorageType storageType, byte[] data)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_ReadPixels((UIntPtr)width, (UIntPtr)height, map, (UIntPtr)storageType, data, out exception);
        else
          result = NativeMethods.X86.MagickImage_ReadPixels((UIntPtr)width, (UIntPtr)height, map, (UIntPtr)storageType, data, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void RemoveArtifact(string name)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_RemoveArtifact(Instance, name);
        else
          NativeMethods.X86.MagickImage_RemoveArtifact(Instance, name);
      }
      public void RemoveAttribute(string name)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_RemoveAttribute(Instance, name);
        else
          NativeMethods.X86.MagickImage_RemoveAttribute(Instance, name);
      }
      public void RemoveProfile(string name)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_RemoveProfile(Instance, name);
        else
          NativeMethods.X86.MagickImage_RemoveProfile(Instance, name);
      }
      public void ResetArtifactIterator()
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ResetArtifactIterator(Instance);
        else
          NativeMethods.X86.MagickImage_ResetArtifactIterator(Instance);
      }
      public void ResetAttributeIterator()
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ResetAttributeIterator(Instance);
        else
          NativeMethods.X86.MagickImage_ResetAttributeIterator(Instance);
      }
      public void ResetProfileIterator()
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_ResetProfileIterator(Instance);
        else
          NativeMethods.X86.MagickImage_ResetProfileIterator(Instance);
      }
      public void Resample(double resolutionX, double resolutionY)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Resample(Instance, resolutionX, resolutionY, out exception);
        else
          result = NativeMethods.X86.MagickImage_Resample(Instance, resolutionX, resolutionY, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Resize(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Resize(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_Resize(Instance, geometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Roll(int x, int y)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Roll(Instance, (IntPtr)x, (IntPtr)y, out exception);
        else
          result = NativeMethods.X86.MagickImage_Roll(Instance, (IntPtr)x, (IntPtr)y, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Rotate(double degrees)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Rotate(Instance, degrees, out exception);
        else
          result = NativeMethods.X86.MagickImage_Rotate(Instance, degrees, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void RotationalBlur(double angle, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_RotationalBlur(Instance, angle, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_RotationalBlur(Instance, angle, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Sample(MagickRectangle geometry)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Sample(Instance, geometryDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Sample(Instance, geometryDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void Scale(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Scale(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_Scale(Instance, geometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Segment(ColorSpace colorSpace, double clusterThreshold, double smoothingThreshold)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Segment(Instance, (UIntPtr)colorSpace, clusterThreshold, smoothingThreshold, out exception);
        else
          NativeMethods.X86.MagickImage_Segment(Instance, (UIntPtr)colorSpace, clusterThreshold, smoothingThreshold, out exception);
        CheckException(exception);
      }
      public void SelectiveBlur(double radius, double sigma, double threshold, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_SelectiveBlur(Instance, radius, sigma, threshold, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_SelectiveBlur(Instance, radius, sigma, threshold, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public IntPtr Separate(Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Separate(Instance, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_Separate(Instance, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        return result;
      }
      public void SepiaTone(double threshold)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_SepiaTone(Instance, threshold, out exception);
        else
          result = NativeMethods.X86.MagickImage_SepiaTone(Instance, threshold, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void SetAlpha(AlphaOption value)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetAlpha(Instance, (UIntPtr)value, out exception);
        else
          NativeMethods.X86.MagickImage_SetAlpha(Instance, (UIntPtr)value, out exception);
        CheckException(exception);
      }
      public void SetArtifact(string name, string value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetArtifact(Instance, name, value);
        else
          NativeMethods.X86.MagickImage_SetArtifact(Instance, name, value);
      }
      public void SetAttribute(string name, string value)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetAttribute(Instance, name, value, out exception);
        else
          NativeMethods.X86.MagickImage_SetAttribute(Instance, name, value, out exception);
        CheckException(exception);
      }
      public void SetBitDepth(Channels channels, int value)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetBitDepth(Instance, (UIntPtr)channels, (UIntPtr)value);
        else
          NativeMethods.X86.MagickImage_SetBitDepth(Instance, (UIntPtr)channels, (UIntPtr)value);
      }
      public void SetColormap(int index, MagickColor color)
      {
        using (INativeInstance colorDynamic = MagickColor.CreateInstance(color))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_SetColormap(Instance, (UIntPtr)index, colorDynamic.Instance, out exception);
          else
            NativeMethods.X86.MagickImage_SetColormap(Instance, (UIntPtr)index, colorDynamic.Instance, out exception);
          CheckException(exception);
        }
      }
      public bool SetColorMetric(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        bool result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_SetColorMetric(Instance, MagickImage.GetInstance(image), out exception);
        else
          result = NativeMethods.X86.MagickImage_SetColorMetric(Instance, MagickImage.GetInstance(image), out exception);
        CheckException(exception);
        return result;
      }
      public void SetNext(IntPtr image)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetNext(Instance, image);
        else
          NativeMethods.X86.MagickImage_SetNext(Instance, image);
      }
      public void SetProgressDelegate(NativeMethods.ProgressDelegate method)
      {
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SetProgressDelegate(Instance, method);
        else
          NativeMethods.X86.MagickImage_SetProgressDelegate(Instance, method);
      }
      public void Shade(double azimuth, double elevation, bool colorShading)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Shade(Instance, azimuth, elevation, colorShading, out exception);
        else
          result = NativeMethods.X86.MagickImage_Shade(Instance, azimuth, elevation, colorShading, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Shadow(int x, int y, double sigma, double alphaPercentage)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Shadow(Instance, (IntPtr)x, (IntPtr)y, sigma, alphaPercentage, out exception);
        else
          result = NativeMethods.X86.MagickImage_Shadow(Instance, (IntPtr)x, (IntPtr)y, sigma, alphaPercentage, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Sharpen(double radius, double sigma, Channels channel)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Sharpen(Instance, radius, sigma, (UIntPtr)channel, out exception);
        else
          result = NativeMethods.X86.MagickImage_Sharpen(Instance, radius, sigma, (UIntPtr)channel, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Shave(int leftRight, int topBottom)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Shave(Instance, (UIntPtr)leftRight, (UIntPtr)topBottom, out exception);
        else
          result = NativeMethods.X86.MagickImage_Shave(Instance, (UIntPtr)leftRight, (UIntPtr)topBottom, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Shear(double xAngle, double yAngle)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Shear(Instance, xAngle, yAngle, out exception);
        else
          result = NativeMethods.X86.MagickImage_Shear(Instance, xAngle, yAngle, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void SigmoidalContrast(bool sharpen, double contrast, double midpoint)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_SigmoidalContrast(Instance, sharpen, contrast, midpoint, out exception);
        else
          NativeMethods.X86.MagickImage_SigmoidalContrast(Instance, sharpen, contrast, midpoint, out exception);
        CheckException(exception);
      }
      public void SparseColor(Channels channel, SparseColorMethod method, double[] values, int length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_SparseColor(Instance, (UIntPtr)channel, (UIntPtr)method, values, (UIntPtr)length, out exception);
        else
          result = NativeMethods.X86.MagickImage_SparseColor(Instance, (UIntPtr)channel, (UIntPtr)method, values, (UIntPtr)length, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Sketch(double radius, double sigma, double angle)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Sketch(Instance, radius, sigma, angle, out exception);
        else
          result = NativeMethods.X86.MagickImage_Sketch(Instance, radius, sigma, angle, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Solarize(double factor)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Solarize(Instance, factor, out exception);
        else
          NativeMethods.X86.MagickImage_Solarize(Instance, factor, out exception);
        CheckException(exception);
      }
      public void Splice(MagickRectangle geometry)
      {
        using (INativeInstance geometryDynamic = MagickRectangle.CreateInstance(geometry))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Splice(Instance, geometryDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Splice(Instance, geometryDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void Spread(PixelInterpolateMethod method, double radius)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Spread(Instance, (UIntPtr)method, radius, out exception);
        else
          result = NativeMethods.X86.MagickImage_Spread(Instance, (UIntPtr)method, radius, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Statistic(StatisticType type, int width, int height)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Statistic(Instance, (UIntPtr)type, (UIntPtr)width, (UIntPtr)height, out exception);
        else
          result = NativeMethods.X86.MagickImage_Statistic(Instance, (UIntPtr)type, (UIntPtr)width, (UIntPtr)height, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public IntPtr Statistics()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Statistics(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Statistics(Instance, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            ImageMagick.Statistics.DisposeList(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void Stegano(MagickImage watermark)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Stegano(Instance, MagickImage.GetInstance(watermark), out exception);
        else
          result = NativeMethods.X86.MagickImage_Stegano(Instance, MagickImage.GetInstance(watermark), out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Stereo(MagickImage rightImage)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Stereo(Instance, MagickImage.GetInstance(rightImage), out exception);
        else
          result = NativeMethods.X86.MagickImage_Stereo(Instance, MagickImage.GetInstance(rightImage), out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Strip()
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Strip(Instance, out exception);
        else
          NativeMethods.X86.MagickImage_Strip(Instance, out exception);
        CheckException(exception);
      }
      public IntPtr SubImageSearch(MagickImage reference, ErrorMetric metric, double similarityThreshold, out MagickRectangle offset, out double similarityMetric)
      {
        using (INativeInstance offsetDynamic = MagickRectangle.CreateInstance())
        {
          IntPtr offsetDynamicOut = offsetDynamic.Instance;
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_SubImageSearch(Instance, MagickImage.GetInstance(reference), (UIntPtr)metric, similarityThreshold, offsetDynamicOut, out similarityMetric, out exception);
          else
            result = NativeMethods.X86.MagickImage_SubImageSearch(Instance, MagickImage.GetInstance(reference), (UIntPtr)metric, similarityThreshold, offsetDynamicOut, out similarityMetric, out exception);
          offset = MagickRectangle.CreateInstance(offsetDynamicOut);
          CheckException(exception, result);
          return result;
        }
      }
      public void Swirl(PixelInterpolateMethod method, double degrees)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Swirl(Instance, (UIntPtr)method, degrees, out exception);
        else
          result = NativeMethods.X86.MagickImage_Swirl(Instance, (UIntPtr)method, degrees, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Texture(MagickImage image)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Texture(Instance, MagickImage.GetInstance(image), out exception);
        else
          NativeMethods.X86.MagickImage_Texture(Instance, MagickImage.GetInstance(image), out exception);
        CheckException(exception);
      }
      public void Threshold(double threshold)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_Threshold(Instance, threshold, out exception);
        else
          NativeMethods.X86.MagickImage_Threshold(Instance, threshold, out exception);
        CheckException(exception);
      }
      public void Thumbnail(string geometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Thumbnail(Instance, geometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_Thumbnail(Instance, geometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Tint(string opacity, MagickColor tint)
      {
        using (INativeInstance tintDynamic = MagickColor.CreateInstance(tint))
        {
          IntPtr exception = IntPtr.Zero;
          IntPtr result;
          if (NativeLibrary.Is64Bit)
            result = NativeMethods.X64.MagickImage_Tint(Instance, opacity, tintDynamic.Instance, out exception);
          else
            result = NativeMethods.X86.MagickImage_Tint(Instance, opacity, tintDynamic.Instance, out exception);
          CheckException(exception, result);
          Instance = result;
        }
      }
      public void Transform(string cropGeometry, string imageGeometry)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Transform(Instance, cropGeometry, imageGeometry, out exception);
        else
          result = NativeMethods.X86.MagickImage_Transform(Instance, cropGeometry, imageGeometry, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Transparent(MagickColor color, bool invert)
      {
        using (INativeInstance colorDynamic = MagickColor.CreateInstance(color))
        {
          IntPtr exception = IntPtr.Zero;
          if (NativeLibrary.Is64Bit)
            NativeMethods.X64.MagickImage_Transparent(Instance, colorDynamic.Instance, invert, out exception);
          else
            NativeMethods.X86.MagickImage_Transparent(Instance, colorDynamic.Instance, invert, out exception);
          CheckException(exception);
        }
      }
      public void TransparentChroma(MagickColor colorLow, MagickColor colorHigh, bool invert)
      {
        using (INativeInstance colorLowDynamic = MagickColor.CreateInstance(colorLow))
        {
          using (INativeInstance colorHighDynamic = MagickColor.CreateInstance(colorHigh))
          {
            IntPtr exception = IntPtr.Zero;
            if (NativeLibrary.Is64Bit)
              NativeMethods.X64.MagickImage_TransparentChroma(Instance, colorLowDynamic.Instance, colorHighDynamic.Instance, invert, out exception);
            else
              NativeMethods.X86.MagickImage_TransparentChroma(Instance, colorLowDynamic.Instance, colorHighDynamic.Instance, invert, out exception);
            CheckException(exception);
          }
        }
      }
      public void Transpose()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Transpose(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Transpose(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Transverse()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Transverse(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Transverse(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Trim()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Trim(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_Trim(Instance, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public IntPtr UniqueColors()
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_UniqueColors(Instance, out exception);
        else
          result = NativeMethods.X86.MagickImage_UniqueColors(Instance, out exception);
        CheckException(exception, result);
        return result;
      }
      public void UnsharpMask(double radius, double sigma, double amount, double threshold, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_UnsharpMask(Instance, radius, sigma, amount, threshold, (UIntPtr)channels, out exception);
        else
          result = NativeMethods.X86.MagickImage_UnsharpMask(Instance, radius, sigma, amount, threshold, (UIntPtr)channels, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Vignette(double radius, double sigma, int x, int y)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Vignette(Instance, radius, sigma, (IntPtr)x, (IntPtr)y, out exception);
        else
          result = NativeMethods.X86.MagickImage_Vignette(Instance, radius, sigma, (IntPtr)x, (IntPtr)y, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void Wave(PixelInterpolateMethod method, double amplitude, double length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_Wave(Instance, (UIntPtr)method, amplitude, length, out exception);
        else
          result = NativeMethods.X86.MagickImage_Wave(Instance, (UIntPtr)method, amplitude, length, out exception);
        CheckException(exception, result);
        Instance = result;
      }
      public void WhiteThreshold(string threshold, Channels channels)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_WhiteThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        else
          NativeMethods.X86.MagickImage_WhiteThreshold(Instance, threshold, (UIntPtr)channels, out exception);
        CheckException(exception);
      }
      public IntPtr WriteBlob(MagickSettings settings, out UIntPtr length)
      {
        IntPtr exception = IntPtr.Zero;
        IntPtr result;
        if (NativeLibrary.Is64Bit)
          result = NativeMethods.X64.MagickImage_WriteBlob(Instance, MagickSettings.GetInstance(settings), out length, out exception);
        else
          result = NativeMethods.X86.MagickImage_WriteBlob(Instance, MagickSettings.GetInstance(settings), out length, out exception);
        MagickException magickException = MagickExceptionHelper.Create(exception);
        if (MagickExceptionHelper.IsError(magickException))
        {
          if (result != IntPtr.Zero)
            MagickMemory.Relinquish(result);
          throw magickException;
        }
        RaiseWarning(magickException);
        return result;
      }
      public void WriteFile(MagickSettings settings)
      {
        IntPtr exception = IntPtr.Zero;
        if (NativeLibrary.Is64Bit)
          NativeMethods.X64.MagickImage_WriteFile(Instance, MagickSettings.GetInstance(settings), out exception);
        else
          NativeMethods.X86.MagickImage_WriteFile(Instance, MagickSettings.GetInstance(settings), out exception);
        CheckException(exception);
      }
    }
    internal static IntPtr GetInstance(MagickImage instance)
    {
      if (instance == null)
        return IntPtr.Zero;
      return instance._NativeInstance.Instance;
    }
  }
}
