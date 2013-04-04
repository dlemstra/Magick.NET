#if defined(WIN32)

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
// Note that by default ImageMagick is configured with a QuantumDepth of 8
// and looks for all files in the directory where the executable is located.
// The installed configuration (i.e. the setup.exe-style installer) is
// modifying by defining "UseInstalledMagick".  If you would like to install
// ImageMagick using hard-coded paths, or want to use the Windows registry to
// install ImageMagick, then "UseInstalledMagick" should be defined.
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

// Define to build a ImageMagick which uses registry settings or
// hard-coded paths to locate installed components.  This supports
// using the "setup.exe" style installer, or using hard-coded path
// definitions (see below).  If you want to be able to simply copy
// the built ImageMagick to any directory on any directory on any machine,
// then do not use this setting.

//
// #undef UseInstalledMagick

// When building ImageMagick using DLLs, include a DllMain()
// function which automatically invokes InitializeMagick(NULL), and
// DestroyMagick() so that the user doesn't need to. This is enabled
// by default.
//
#define ProvideDllMain

// Define if MIT X11 is available (or stubbed).  It is not actually
// necessary to use X11 or the X11 stubs library. The VisualMagick configure
// program assumes that X11 stubs is being used if X11 is not supported.
// To achieve a slimmer ImageMagick, undefine HasX11 and remove the 'xlib'
// project from the ImageMagick workspace.
//
#define HasX11

/////////////
//
// Optional packages
//
// All packages except autotrace are included by default in the build.

// Define to use the bzip2 compression library
#define HasBZLIB

// Define to use the FlashPIX library (fpx module/subdirectory)
// #undef HasFPX

// Define to use the JBIG library
#define HasJBIG

// Define to use the Jasper JPEG v2 library
#define HasJP2

// Define to use the IJG JPEG v1 library
#define HasJPEG

// Define to use the "little" Color Management System (LCMS) library
#define HasLCMS

// Define to use the PNG library
#define HasPNG

// Define to use the TIFF library
#define HasTIFF

// Define to use the FreeType (TrueType & Postscript font support) library
#define HasTTF

// Define to use the Windows GDI32 library (for clipboard and emf modules)
#define HasWINGDI32

// Define to use the libwmf WMF parsing library
#define HasWMFlite

// Define to use the GNOME XML library
#define HasXML

// Define to use the zlib ZIP compression library
#define HasZLIB

// Define to use the autotrace library (obtain sources seperately)
//
// #undef HasAUTOTRACE

// Define to enable self-contained, embeddable, zero-configuration ImageMagick (experimental)
//
// #undef UseEmbeddableMagick
/////////////

//
// Hard Coded Paths
// 
// If hard-coded paths are defined via the the following define
// statements, then they will override any values from the Windows
// registry.    It is unusual to use hard-coded paths under Windows.

// Optional: Specify where X11 application resource files are installed
// #define ApplicationDefaults "c:\\ImageMagick\\"

// Optional: Specify where user-specific X11 application resource files are installed
// #define PreferencesDefaults  "~\\."

// Optional: Specify where convert.exe and support executables are installed
// #define MagickBinPath       "c:\\ImageMagick\\"

// Optional: Specify where operating system specific files are installed
// #define MagickLibPath       "c:\\ImageMagick\\"

// Optional: Specify where operating system independent files are installed
// #define MagickSharePath     "c:\\ImageMagick\\"

// Optional: Specify where coder modules (DLLs) are installed
// #define MagickImageCodersPath   "c:\\ImageMagick\\"

// Optional: Specify where filter modules (DLLs) are installed
// #define MagickImageFiltersPath   "c:\\ImageMagick\\"

// Magick API method prefix.  Define to add a unique prefix to all API methods.
// #undef MagickMethodPrefix

/////////////
//
// The remaining defines should not require user modification.
//

// Use Visual C++ C inline method extension to improve performance
#define inline __inline

// Visual C++ does not define ssize_t by default.
#if !defined(ssize_t)
#if defined(_WIN64) 
#  define ssize_t  __int64
#else
#  define ssize_t  long
#endif
#endif

/* Define to 1 if you have the <ft2build.h> header file. */
#define HAVE_FT2BUILD_H 1

// Define to support memory mapping files for improved performance
#define HAVE_MMAP_FILEIO 1

/* Define to 1 if you have the `raise' function. */
#define HAVE_RAISE 1

/* Define to 1 if you have the `memmove' function. */
#define HAVE_MEMMOVE 1

/* Define to 1 if you have the `vsnprintf' function. */
#define HAVE_VSNPRINTF 1

/* Define to 1 if you have the `popen' function. */
#define HAVE_POPEN 1

/* Define to   if you have the `strcasecmp' function. */
#define HAVE_STRCASECMP 1

/* Define to 1 if you have the `strncasecmp' function. */
#define HAVE_STRNCASECMP 1

/* Define to 1 if you have the `tempnam' function. */
#define HAVE_TEMPNAM 1

// Define to include the <sys/types.h> header file
#define HAVE_SYS_TYPES_H 1

#endif
