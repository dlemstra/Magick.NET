cmake_minimum_required (VERSION 2.8.8)

project(Magick.NET)

add_definitions(-DMAGICK_NET_${PLATFORM} -DMAGICKCORE_QUANTUM_DEPTH=${DEPTH} -DMAGICKCORE_HDRI_ENABLE=${HDRI_ENABLE})

include_directories(. /usr/local/include /usr/local/include/ImageMagick-7)

set(LIB_DIR /usr/local/lib/)
set(CMAKE_CXX_FLAGS "-Wall -O3")

file(GLOB_RECURSE ALL_FILES . *.c)
file(GLOB_RECURSE CMAKE_FILES **/CMakeFiles/*.c)
list(REMOVE_ITEM ALL_FILES ${CMAKE_FILES})

if (APPLE)
    find_library(LIBICONV NAMES libiconv.a PATHS /usr/local/opt/libiconv/lib)

    set(STDCPLUSPLUS stdc++)
else()
    find_package(Threads REQUIRED)
    set(THREADLIB Threads::Threads)

    list(APPEND ALL_FILES foo.cxx)

    set(STDCPLUSPLUS -static-libstdc++ -static-libgcc)
endif()

add_library(Magick.NET-${QUANTUM}-x64.Native.dll SHARED ${ALL_FILES})

target_link_libraries(
    Magick.NET-${QUANTUM}-x64.Native.dll

    ${LIB_DIR}/libMagickWand-7.${QUANTUM_NAME}.a
    ${LIB_DIR}/libMagickCore-7.${QUANTUM_NAME}.a

    ${LIB_DIR}/libz.a
    ${LIB_DIR}/libxml2.a
    ${LIB_DIR}/libpng.a
    ${LIB_DIR}/libfreetype.a
    ${LIB_DIR}/libfontconfig.a
    ${LIB_DIR}/libturbojpeg.a
    ${LIB_DIR}/libtiff.a
    ${LIB_DIR}/libwebp.a
    ${LIB_DIR}/libwebpmux.a
    ${LIB_DIR}/libwebpdemux.a
    ${LIB_DIR}/libopenjp2.a
    ${LIB_DIR}/liblcms2.a
    ${LIB_DIR}/libheif.a
    ${LIB_DIR}/libde265.a
    ${LIB_DIR}/libraw_r.a

    ${THREADLIB}

    ${LIBICONV}

    ${STDCPLUSPLUS}
)