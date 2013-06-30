// ImageMagick build feature configuration.  Please note that
// disabling a feature via this header file may not be sufficient to
// remove any library dependencies from the build.  The VisualMagick
// project files may need to be edited to remove libraries the feature
// depends on. When building a static ImageMagick, coder
// registrations are made via magick\static.c so if a format is
// removed, the call to its registration function should be commented
// out in static.c.  Note that VisualMagick configure updates
// magick\static.c so re-running configure may cause local changes to
// be lost.
//
// Note that by default ImageMagick is configured with a
// MAGICKCORE_QUANTUM_DEPTH of 8 and looks for all files in the directory
// where the executable is located.   The installed configuration (i.e. the
// setup.exe-style installer) is  modifying by defining
// "MAGICKCORE_INSTALLED_SUPPORT".  If you would like to install ImageMagick
// using hard-coded paths, or want to use the Windows registry to install
// ImageMagick, then "MAGICKCORE_INSTALLED_SUPPORT" should be defined.
//
// Enabled options are of the form:
//
//   #define option
//
// while disabled options are initially in the form
//
//   // #undef option
//
// so it is necessary to remove the comment, and change "undef" to "define"
// in order for the option to be enabled.

// Specify size of PixelPacket color Quantums (8, 16, or 32).
// A value of 8 uses half the memory than 16 and typically runs 30% faster,
// but provides 256 times less color resolution than a value of 16.
//
#define MAGICKCORE_QUANTUM_DEPTH 8

// Define to build a ImageMagick which uses registry settings or
// hard-coded paths to locate installed components.  This supports
// using the "setup.exe" style installer, or using hard-coded path
// definitions (see below).  If you want to be able to simply copy
// the built ImageMagick to any directory on any directory on any machine,
// then do not use this setting.
//
// #undef MAGICKCORE_INSTALLED_SUPPORT

// When building ImageMagick using DLLs, include a DllMain()
// function which automatically invokes MagickCoreGenesis(NULL), and
// MagickCoreTerminus() so that the user doesn't need to. This is enabled
// by default.
//
#undef ProvideDllMain

// Define if MIT X11 is available (or stubbed).  It is not actually
// necessary to use X11 or the X11 stubs library. The VisualMagick configure
// program assumes that X11 stubs is being used if X11 is not supported.
// To achieve a slimmer ImageMagick, undefine MAGICKCORE_X11_DELEGATE and
// remove the 'xlib' project from the ImageMagick workspace.
//
#undef MAGICKCORE_X11_DELEGATE

// Define to enable high dynamic range imagery (HDRI)
//
#define MAGICKCORE_HDRI_ENABLE 0

// Exclude deprecated methods in MagickCore API 
//
#define MAGICKCORE_EXCLUDE_DEPRECATED

// Permit enciphering and deciphering image pixels.
//
#define MAGICKCORE_CIPHER_SUPPORT

/////////////
//
// Optional packages
//
// All packages except autotrace are included by default in the build.

// Define to use the bzip2 compression library
#define MAGICKCORE_BZLIB_DELEGATE

// Define to use the FlashPIX library (fpx module/subdirectory)
// #undef MAGICKCORE_FPX_DELEGATE

// Define to use the FreeType (TrueType & Postscript font support) library
#define MAGICKCORE_FREETYPE_DELEGATE

// Define to use the JBIG library
#define MAGICKCORE_JBIG_DELEGATE

// Define to use the Jasper JPEG v2 library
#define MAGICKCORE_JP2_DELEGATE

// Define to use the IJG JPEG v1 library
#define MAGICKCORE_JPEG_DELEGATE

// Define to use the "little" Color Management System (LCMS) library
#define MAGICKCORE_LCMS_DELEGATE
#define MAGICKCORE_HAVE_LCMS2_H

// Define to use the PNG library
#define MAGICKCORE_PNG_DELEGATE

// Define to use the TIFF library
#define MAGICKCORE_TIFF_DELEGATE

// Define to use the Windows GDI32 library (for clipboard and emf modules)
#define MAGICKCORE_WINGDI32_DELEGATE

// Define to use the GNOME XML library
#define MAGICKCORE_XML_DELEGATE

// Define to use the zlib ZIP compression library
#define MAGICKCORE_ZLIB_DELEGATE

// Define to use the autotrace library (obtain sources seperately)
//
// #undef MAGICKCORE_AUTOTRACE_DELEGATE

// Define to enable self-contained, embeddable, zero-configuration ImageMagick (experimental)
//
#define MAGICKCORE_WEBP_DELEGATE
// #undef MAGICKCORE_EMBEDDABLE_SUPPORT
/////////////

//
// Hard Coded Paths
// 
// If hard-coded paths are defined via the the following define
// statements, then they will override any values from the Windows
// registry.    It is unusual to use hard-coded paths under Windows.

// Optional: Specify where X11 application resource files are installed
// #define X11_APPLICATION_PATH "c:\\ImageMagick\\"

// Optional: Specify where user-specific X11 application resource files are installed
// #define X11_PREFERENCES_PATH  "~\\."

// Optional: Specify where convert.exe and support executables are installed
// #define MAGICKCORE_EXECUTABLE_PATH  "c:\\ImageMagick\\"

// Optional: Specify where operating system specific files are installed
// #define MAGICKCORE_LIBRARY_PATH  "c:\\ImageMagick\\"

// Optional: Specify where operating system independent files are installed
// #define MAGICKCORE_SHARE_PATH  "c:\\ImageMagick\\"

// Optional: Specify where coder modules (DLLs) are installed
// #define MAGICKCORE_CODER_PATH  "c:\\ImageMagick\\"

// Optional: Specify where filter modules (DLLs) are installed
// #define MAGICKCORE_FILTER_PATH  "c:\\ImageMagick\\"

// Magick API method prefix.  Define to add a unique prefix to all API methods.
// #undef MAGICKCORE_NAMESPACE_PREFIX

/////////////
//
// The remaining defines should not require user modification.
//

// Define the package name.
#define MAGICKCORE_PACKAGE_NAME  "ImageMagick"

// Required or InitializeCriticalSectionandSpinCount is undefined.
#if !defined(_WIN32_WINNT)
#  define _WIN32_WINNT  0x0501
#endif

// Use Visual C++ C inline method extension to improve performance
#define inline __inline

// Visual C++ does not define restrict by default.
#if !defined(restrict)
  #define restrict
#endif

// Visual C++ does not define ssize_t by default.
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
#define nearbyint(x)  ((ssize_t) ((x)+0.5))

/* Define to 1 if you have the <ft2build.h> header file. */
#define MAGICKCORE_HAVE_FT2BUILD_H 1

/* Define to 1 if you have the `ftruncate' function. */
#define MAGICKCORE_HAVE_FTRUNCATE 1

// Define to support memory mapping files for improved performance
#define MAGICKCORE_HAVE_MMAP_FILEIO 1

/* Define to 1 if you have the `raise' function. */
#define MAGICKCORE_HAVE_RAISE 1

/* Define to 1 if you have the `memmove' function. */
#define MAGICKCORE_HAVE_MEMMOVE 1

/* Define to 1 if you have the `strtod_l' function. */
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_STRTOD_L 1
#endif

/* Define to 1 if you have the `sysconf' function. */
#define MAGICKCORE_HAVE_SYSCONF 1

/* Define to 1 if you have the `vfprintf_l' function. */
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_VFPRINTF_L 1
#endif

/* Define to 1 if you have the `vsnprintf' function. */
#define MAGICKCORE_HAVE_VSNPRINTF 1

/* Define to 1 if you have the `vsnprintf_' function. */
#if defined(_VISUALC_) && (_MSC_VER >= 1400)
#define MAGICKCORE_HAVE_VSNPRINTF_L 1
#endif

/* Define to 1 if you have the `popen' function. */
#define MAGICKCORE_HAVE_POPEN 1

/* Define to   if you have the `strcasecmp' function. */
#define MAGICKCORE_HAVE_STRCASECMP 1

/* Define to 1 if you have the `strncasecmp' function. */
#define MAGICKCORE_HAVE_STRNCASECMP 1

/* Define to 1 if you have the `tempnam' function. */
#define MAGICKCORE_HAVE_TEMPNAM 1

// Define to include the <sys/types.h> header file
#define MAGICKCORE_HAVE_SYS_TYPES_H 1

/* Define to 1 if you have the `_wfopen' function. */
#define MAGICKCORE_HAVE__WFOPEN 1

/* Define to 1 if you have the `_wstat' function. */
#define MAGICKCORE_HAVE__WSTAT 1

#define MAGICKCORE_HAVE__ALIGNED_MALLOC 1
#define MAGICKCORE_HAVE_VSNPRINTF 1
#define MAGICKCORE_HAVE_GETTIMEOFDAY
#define MAGICKCORE_HAVE_SETVBUF 1
#define MAGICKCORE_HAVE_TEMPNAM 1
#define MAGICKCORE_HAVE_RAISE 1
#define MAGICKCORE_HAVE_PROCESS_H 1
#define MAGICKCORE_HAVE_SPAWNVP 1
#define MAGICKCORE_STDC_HEADERS 1
#define MAGICKCORE_HAVE_STRING_H 1
#define MAGICKCORE_HAVE_J0 1
#define MAGICKCORE_HAVE_J1 1

/*
  Tiff features.
*/

/* Define to 1 if you have the <tiffconf.h> header file. */
#define MAGICKCORE_HAVE_TIFFCONF_H 1

/* Define to 1 if you have the `TIFFMergeFieldInfo' function. */
#define MAGICKCORE_HAVE_TIFFMERGEFIELDINFO 1

/* Define to 1 if you have the `TIFFSetErrorHandlerExt' function. */
#define MAGICKCORE_HAVE_TIFFSETERRORHANDLEREXT 1

/* Define to 1 if you have the `TIFFSetTagExtender' function. */
#define MAGICKCORE_HAVE_TIFFSETTAGEXTENDER 1

/* Define to 1 if you have the `TIFFSetWarningHandlerExt' function. */
#define MAGICKCORE_HAVE_TIFFSETWARNINGHANDLEREXT 1

/* Define to 1 if you have the `TIFFReadEXIFDirectory' function. */
#define MAGICKCORE_HAVE_TIFFREADEXIFDIRECTORY

/* Define to 1 if you have the `TIFFSwabArrayOfTriples' function. */
#define MAGICKCORE_HAVE_TIFFSWABARRAYOFTRIPLES 1

/* Define to 1 if you have the `TIFFIsBigEndian' function. */
#define MAGICKCORE_HAVE_TIFFISBIGENDIAN  1
