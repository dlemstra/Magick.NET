/*
  ImageMagick build feature configuration.  Please note that
  disabling a feature via this header file may not be sufficient to
  remove any library dependencies from the build.  The VisualMagick
  project files may need to be edited to remove libraries the feature
  depends on. When building a static ImageMagick, coder
  registrations are made via magick\static.c so if a format is
  removed, the call to its registration function should be commented
  out in static.c.  Note that VisualMagick configure updates
  magick\static.c so re-running configure may cause local changes to
  be lost.

  Note that by default ImageMagick is configured with a
  MAGICKCORE_QUANTUM_DEPTH of 8 and looks for all files in the directory
  where the executable is located.   The installed configuration (i.e. the
  setup.exe-style installer) is  modifying by defining
  "MAGICKCORE_INSTALLED_SUPPORT".  If you would like to install ImageMagick
  using hard-coded paths, or want to use the Windows registry to install
  ImageMagick, then "MAGICKCORE_INSTALLED_SUPPORT" should be defined.

  Enabled options are of the form:

    #define option

  while disabled options are initially in the form

    // #undef option

  so it is necessary to remove the comment, and change "undef" to "define"
  in order for the option to be enabled.
*/

/*
  When building ImageMagick using DLLs, include a DllMain()
  function which automatically invokes MagickCoreGenesis(NULL), and
  MagickCoreTerminus() so that the user doesn't need to. This is disabled
  by default.
*/
//#define ProvideDllMain

/*
  Permit enciphering and deciphering image pixels.
*/
#define MAGICKCORE_CIPHER_SUPPORT

/*
  Define to build a ImageMagick which uses registry settings or
  hard-coded paths to locate installed components.  This supports
  using the "setup.exe" style installer, or using hard-coded path
  definitions (see below).  If you want to be able to simply copy
  the built ImageMagick to any directory on any directory on any machine,
  then do not use this setting.
*/
//#define MAGICKCORE_INSTALLED_SUPPORT

/*
  Specify size of PixelPacket color Quantums (8, 16, or 32).
  A value of 8 uses half the memory than 16 and typically runs 30% faster,
  but provides 256 times less color resolution than a value of 16.
*/
#define MAGICKCORE_QUANTUM_DEPTH 16

/*
  Define to enable high dynamic range imagery (HDRI)
*/
#define MAGICKCORE_HDRI_ENABLE 1

/*
  Define to enable OpenCL
*/
#define MAGICKCORE__OPENCL
#define MAGICKCORE_HAVE_CL_CL_H

/*
    Exclude deprecated methods in MagickCore API
*/
#define MAGICKCORE_EXCLUDE_DEPRECATED

/*
  Define to use the bzip2 compression library
*/
#define MAGICKCORE_BZLIB_DELEGATE

/*
  Define to use the OpenEXR library
*/
#define MAGICKCORE_OPENEXR_DELEGATE

/*
  Define to use the FLIF library
*/
#define MAGICKCORE_FLIF_DELEGATE

/*
  Define to use the Jasper JPEG v2 library
*/
#define MAGICKCORE_JP2_DELEGATE

/*
  Define to use the TurboJPEG library
*/
#define MAGICKCORE_JPEG_DELEGATE

/*
  Define to use the "little" Color Management System (LCMS) library
*/
#define MAGICKCORE_LCMS_DELEGATE
#define MAGICKCORE_HAVE_LCMS2_H

/*
  Define to use the RSVG library
*/
#define MAGICKCORE_RSVG_DELEGATE
#define MAGICKCORE_CAIRO_DELEGATE

/*
  Define to use the GNOME XML library
*/
#define MAGICKCORE_XML_DELEGATE

/*
  Define to use the Liquid Rescale library
*/
#define MAGICKCORE_LQR_DELEGATE

/*
  Define to use the OpenJPEG library
*/
#define MAGICKCORE_LIBOPENJP2_DELEGATE

/*
  Define to use the Pango/Cairo library
*/
#define MAGICKCORE_PANGOCAIRO_DELEGATE

/*
  Define to use the PNG library
*/
#define MAGICKCORE_PNG_DELEGATE

/*
  Define to use the TIFF library
*/
#define MAGICKCORE_TIFF_DELEGATE

/*
  Define to use the FreeType (TrueType & Postscript font support) library
*/
#define MAGICKCORE_FREETYPE_DELEGATE

/*
  Define to use the WebP library
*/
#define MAGICKCORE_WEBP_DELEGATE

/*
  Define to use the zlib ZIP compression library
*/
#define MAGICKCORE_ZLIB_DELEGATE

/*
  Define to use the Windows GDI32 library (for clipboard and emf modules)
*/
#define MAGICKCORE_WINGDI32_DELEGATE

/*
  Define to only use the built-in (in-memory) settings.
*/
//#define MAGICKCORE_ZERO_CONFIGURATION_SUPPORT

/*
  Hard Coded Paths

  If hard-coded paths are defined via the the following define
  statements, then they will override any values from the Windows
  registry. It is unusual to use hard-coded paths under Windows.
*/

/*
  Optional: Specify where convert.exe and support executables are installed
*/
//#define MAGICKCORE_EXECUTABLE_PATH "c:\\ImageMagick\\"

/*
  Optional: Specify where operating system specific files are installed
*/
//#define MAGICKCORE_LIBRARY_PATH  "c:\\ImageMagick\\"

/*
  Optional: Specify name of the library that contains the xml resource files
*/
#define MAGICKCORE_LIBRARY_NAME "Magick.NET-Q16-HDRI-x64.Native.dll"

/*
  Optional: Specify where operating system independent files are installed
*/
//#define MAGICKCORE_SHARE_PATH  "c:\\ImageMagick\\"

/*
  Optional: Specify where coder modules (DLLs) are installed
*/
//#define MAGICKCORE_CODER_PATH  "c:\\ImageMagick\\"

/*
  Optional: Specify where filter modules (DLLs) are installed
*/
//#define MAGICKCORE_FILTER_PATH  "c:\\ImageMagick\\"

/*
  The remaining defines should not require user modification.
*/

/*
  Define the package name.
*/
#define MAGICKCORE_PACKAGE_NAME  "ImageMagick"

/*
  Required or InitializeCriticalSectionandSpinCount is undefined.
*/
#if !defined(_WIN32_WINNT)
#  define _WIN32_WINNT  0x0501
#endif

/*
  Use Visual C++ C inline method extension to improve performance
*/
#define _magickcore_inline __inline

/*
  Visual C++ does not define restrict by default.
*/
#if (_MSC_VER >= 1400)
#  define _magickcore_restrict __restrict
#else
#  define _magickcore_restrict
#endif

/*
  Visual C++ does not define double_t, float_t, or ssize_t by default.
*/
#if !defined(double_t)
#define MAGICKCORE_HAVE_DOUBLE_T
#if !defined(__MINGW32__) && !defined(__MINGW64__)
typedef double double_t;
#endif
#endif
#if !defined(float_t)
#define MAGICKCORE_HAVE_FLOAT_T
#if !defined(__MINGW32__) && !defined(__MINGW64__)
typedef float float_t;
#endif
#endif
#if !defined(ssize_t) && !defined(__MINGW32__) && !defined(__MINGW64__)
#if defined(_WIN64) 
typedef __int64 ssize_t;
#else
typedef long ssize_t;
#endif
#endif

#if !defined(__FUNCTION__)
  #define __FUNCTION__  "unknown"
#endif
#define __func__  __FUNCTION__

/*
  Define to 1 if you have the <ft2build.h> header file.
*/
#define MAGICKCORE_HAVE_FT2BUILD_H 1

/*
  Define to 1 if you have the `ftruncate' function.
*/
#define MAGICKCORE_HAVE_FTRUNCATE 1

/*
  Define to support memory mapping files for improved performance
*/
#define MAGICKCORE_HAVE_MMAP 1

/*
  Define to 1 if you have the `raise' function.
*/
#define MAGICKCORE_HAVE_RAISE 1

/*
  Define to 1 if you have the `memmove' function.
*/
#define MAGICKCORE_HAVE_MEMMOVE 1

/*
  Define to 1 if you have the `strtod_l' function.
*/
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_STRTOD_L 1
#endif

/*
  Define to 1 if you have the `sysconf' function.
*/
#define MAGICKCORE_HAVE_SYSCONF 1

/*
  Define to 1 if you have the `vfprintf_l' function.
*/
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_VFPRINTF_L 1
#endif

/*
  Define to 1 if you have the `vsnprintf' function.
*/
#define MAGICKCORE_HAVE_VSNPRINTF 1

/*
  Define to 1 if you have the `vsnprintf_' function.
*/
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_VSNPRINTF_L 1
#endif

/*
  Define to 1 if you have the `popen' function.
*/
#define MAGICKCORE_HAVE_POPEN 1

/*
  Define to 1 if you have the `strcasecmp' function.
*/
#define MAGICKCORE_HAVE_STRCASECMP 1

/*
  Define to 1 if you have the `strncasecmp' function.
*/
#define MAGICKCORE_HAVE_STRNCASECMP 1

/*
  Define to 1 if you have the `tempnam' function.
*/
#define MAGICKCORE_HAVE_TEMPNAM 1

/*
  Define to include the <sys/types.h> header file
*/
#define MAGICKCORE_HAVE_SYS_TYPES_H 1

/*
  Define to 1 if you have the `_wfopen' function.
*/
#define MAGICKCORE_HAVE__WFOPEN 1

/*
  Define to 1 if you have the `_wstat' function.
*/
#define MAGICKCORE_HAVE__WSTAT 1

#if defined(_VISUALC_) && (_MSC_VER >= 1310)
#define MAGICKCORE_HAVE__ALIGNED_MALLOC 1
#endif
#define MAGICKCORE_HAVE_VSNPRINTF 1
#define MAGICKCORE_HAVE_GETTIMEOFDAY
#define MAGICKCORE_HAVE_SETVBUF 1
#define MAGICKCORE_HAVE_TEMPNAM 1
#define MAGICKCORE_HAVE_RAISE 1
#define MAGICKCORE_HAVE_PROCESS_H 1
#define MAGICKCORE_HAVE_SPAWNVP 1
#define MAGICKCORE_HAVE_UTIME 1
#define MAGICKCORE_STDC_HEADERS 1
#define MAGICKCORE_HAVE_LOCALE_H 1
#define MAGICKCORE_HAVE_LOCALE_T 1
#define MAGICKCORE_HAVE_STRING_H 1
#define MAGICKCORE_HAVE_J0 1
#define MAGICKCORE_HAVE_J1 1

/*
  Tiff features.
*/

/*
  Define to 1 if you have the <tiffconf.h> header file.
*/
#define MAGICKCORE_HAVE_TIFFCONF_H 1

/*
  Define to 1 if you have the `TIFFMergeFieldInfo' function.
*/
#define MAGICKCORE_HAVE_TIFFMERGEFIELDINFO 1

/*
  Define to 1 if you have the `TIFFSetErrorHandlerExt' function.
*/
#define MAGICKCORE_HAVE_TIFFSETERRORHANDLEREXT 1

/*
  Define to 1 if you have the `TIFFSetTagExtender' function.
*/
#define MAGICKCORE_HAVE_TIFFSETTAGEXTENDER 1

/*
  Define to 1 if you have the `TIFFSetWarningHandlerExt' function.
*/
#define MAGICKCORE_HAVE_TIFFSETWARNINGHANDLEREXT 1

/*
  Define to 1 if you have the `TIFFReadEXIFDirectory' function.
*/
#define MAGICKCORE_HAVE_TIFFREADEXIFDIRECTORY 1

/*
  Define to 1 if you have the `TIFFSwabArrayOfTriples' function.
*/
#define MAGICKCORE_HAVE_TIFFSWABARRAYOFTRIPLES 1

/*
  Define to 1 if you have the `TIFFIsBigEndian' function.
*/
#define MAGICKCORE_HAVE_TIFFISBIGENDIAN  1

/*
  Png features.
*/
#define IMPNG_SETJMP_IS_THREAD_SAFE 1

/*
  Disable specific warnings.
*/
#ifdef _MSC_VER
#if _MSC_VER < 1910
#pragma warning(disable: 4054) /* 'conversion' : from function pointer 'type1' to data pointer 'type2' */
#pragma warning(disable: 4055) /* 'conversion' : from data pointer 'type1' to function pointer 'type2' */
#endif
#pragma warning(disable: 4101) /* 'identifier' : unreferenced local variable */
#pragma warning(disable: 4201) /* nonstandard extension used : nameless struct/union */
#pragma warning(disable: 4130) /* 'operator' : logical operation on address of string constant */
#pragma warning(disable: 4459) /* 'identifier' : declaration of 'foo' hides global declaration */
#endif

/* only report these warnings once per file */
#pragma warning(once: 4100) /* 'identifier' : unreferenced formal parameter */
#pragma warning(once: 4456) /* 'identifier' : declaration of 'foo' hides previous local declaration */
#pragma warning(once: 4459) /* 'identifier' : declaration of 'foo' hides global declaration */
