cmake_minimum_required (VERSION 2.8.8)

project(Magick.NET)

add_definitions(-DMAGICK_NET_${PLATFORM} -DMAGICKCORE_QUANTUM_DEPTH=${DEPTH} -DMAGICKCORE_HDRI_ENABLE=${HDRI_ENABLE})

include_directories(.)
include_directories(/usr/local/include/ImageMagick-7/)

find_library(MAGICKCORE libMagickCore-7.${QUANTUM_NAME}.a)
find_library(MAGICKWAND libMagickWand-7.${QUANTUM_NAME}.a)

find_library(LIBZ libz.a)
find_library(LIBXML libxml2.a)
find_library(LIBPNG libpng.a)
find_library(LIBFREETYPE libfreetype.a)
find_library(LIBFONTCONFIG libfontconfig.a)
find_library(LIBTIFF libtiff.a)
find_library(LIBWEBP libwebp.a)
find_library(LIBWEBPMUX libwebpmux.a)
find_library(LIBWEBPDEMUX libwebpdemux.a)
find_library(LIBOPENJP2 libopenjp2.a)
find_library(LIBLCMS liblcms2.a)
find_library(LIBLDE265 libde265.a)
find_library(LIBLHEIF libheif.a)
find_library(LIBGCC libheif.a)

if (APPLE)
    find_library(LIBICONV NAMES libiconv.a PATHS /usr/local/opt/libiconv/lib)
    find_library(LIBLZMA liblzma.a)

    find_library(LIBJPEG libjpeg.a)

    set(STDCPLUSPLUS stdc++)
else()
    find_library(LIBJPEG libjpeg.a PATHS /usr/local/lib64)

    set(STDCPLUSPLUS -static-libstdc++ -static-libgcc)
endif()

file(GLOB_RECURSE ALL_FILES . *.c)
file(GLOB_RECURSE CMAKE_FILES **/CMakeFiles/*.c)
list(REMOVE_ITEM ALL_FILES ${CMAKE_FILES})

add_library(Magick.NET-${QUANTUM}-x64.Native.dll SHARED ${ALL_FILES})

target_link_libraries(
    Magick.NET-${QUANTUM}-x64.Native.dll
    ${MAGICKCORE}
    ${MAGICKWAND}    

    ${LIBZ}
    ${LIBXML}
    ${LIBPNG}
    ${LIBFONTCONFIG}
    ${LIBFREETYPE}
    ${LIBJPEG}
    ${LIBTIFF}
    ${LIBWEBP}
    ${LIBWEBPMUX}
    ${LIBWEBPDEMUX}
    ${LIBOPENJP2}
    ${LIBLCMS}
    ${LIBLDE265}
    ${LIBLHEIF}

    ${LIBICONV}
    ${LIBLZMA}

    ${STDCPLUSPLUS}
    -fPIC
)