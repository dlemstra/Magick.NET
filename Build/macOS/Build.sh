#!/bin/bash
set -e

export HOMEBREW_NO_AUTO_UPDATE=1
brew install nasm
brew install pkg-config
brew install autoconf
brew install automake
brew install libtool
brew install libiconv
brew install ossp-uuid

# Uninstall already installed development libraries
brew uninstall --force libpng
brew uninstall --ignore-dependencies xz

cd ../../ImageMagick/Source

./Checkout.sh macOS

cd ImageMagick

# Clone freetype
if [ ! -d freetype ]; then
  git clone git://git.sv.nongnu.org/freetype/freetype2.git freetype
fi
cd freetype
git reset --hard
git fetch
git checkout VER-2-9
cd ../

# Clone fontconfig
if [ ! -d fontconfig ]; then
  git clone git://anongit.freedesktop.org/fontconfig fontconfig
fi
cd fontconfig
git reset --hard
git fetch
git checkout 2.13.0
cd ../

# Build zlib
cd zlib
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
./configure
make install

# Build libxml
cd ../libxml
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure --with-python=no --with-iconv=/usr/local/opt/libiconv
make install

# Build libpng
cd ../png
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure --enable-mips-msa=off --enable-arm-neon=off --enable-powerpc-vsx=off
make install

# Build freetype
cd ../freetype
./autogen.sh
export CFLAGS="-O3 -fPIC"
./configure --without-bzip2
make install

# Build fontconfig
cd ../fontconfig
export PATH=${PATH}:/usr/local/opt/gettext/bin
autoreconf -fiv
sudo easy_install pip
sudo python -m pip install lxml
sudo python -m pip install six
export CFLAGS="-O3 -fPIC"
./configure --enable-libxml2 --enable-static=yes --with-add-fonts=/System/Library/Fonts,/Library/Fonts,~/Library/Fonts
make install

# Build libjpeg-turbo
cd ../jpeg
cmake . -DCMAKE_INSTALL_PREFIX=/usr/local -DENABLE_SHARED=off -DCMAKE_BUILD_TYPE=Release -DCMAKE_C_FLAGS="-O3 -fPIC"
make install

# Build libtiff
cd ../tiff
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure
make install

# Build libwebp
cd ../webp
autoreconf -fiv
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
./configure --enable-libwebpmux --enable-libwebpdemux
make install

# Build openjpeg
cd ../openjpeg
cmake -DCMAKE_INSTALL_PREFIX=/usr/local -DBUILD_SHARED_LIBS=off -DCMAKE_BUILD_TYPE=Release -DCMAKE_C_FLAGS="-O3 -fPIC" .
make install
cp bin/libopenjp2.a /usr/local/lib

# Build lcms
cd ../lcms
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure --disable-shared --prefix=/usr/local
make install

# Build libde265
cd ../libde265
autoreconf -fiv
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
export CXXFLAGS="-O3 -fPIC"
./configure --disable-shared --prefix=/usr/local
make install

# Build libheif
cd ../libheif
autoreconf -fiv
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
export CXXFLAGS="-O3 -fPIC"
./configure --disable-shared --disable-go --prefix=/usr/local
make install

# Build libraw
cd ../libraw
chmod +x ./version.sh
chmod +x ./shlib-version.sh
autoreconf -fiv
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
export CXXFLAGS="-O3 -fPIC"
./configure --disable-shared --disable-examples --disable-openmp  --disable-jpeg --disable-jasper --prefix=/usr/local
make install

cd ../../../../
mkdir Output

buildMagickNET() {
    local quantum=$1

    # Set ImageMagick variables
    local quantum_name=$quantum
    local hdri=no
    local hdri_enable=0
    local depth=8
    if [ "$quantum" == "Q16" ]; then
        depth=16
    elif [ "$quantum" == "Q16-HDRI" ]; then
        quantum_name=Q16HDRI
        depth=16
        hdri=yes
        hdri_enable=1
    fi

    # Build ImageMagick
    cd ImageMagick/Source/ImageMagick/ImageMagick
    ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --disable-docs --with-bzlib=no --with-lzma=no --with-x=no --with-quantum-depth=$depth --enable-hdri=$hdri
    make install

    # Build Magick.NET
    cd ../../../../Source/Magick.NET.Native
    mkdir $quantum
    cd $quantum

    cmake -D DEPTH=$depth -D QUANTUM=$quantum -D HDRI_ENABLE=$hdri_enable -DQUANTUM_NAME=$quantum_name -DPLATFORM=MACOS ..
    make
    cp libMagick.NET-$quantum-x64.Native.dll.dylib ../../../Output/Magick.NET-$quantum-x64.Native.dll.dylib
    cd ../../../
}

buildMagickNET "Q8"
buildMagickNET "Q16"
buildMagickNET "Q16-HDRI"

testMagickNET() {
    local quantum=$1

    dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test${quantum}

    cp Output/Magick.NET-$quantum-x64.Native.dll.dylib Tests/Magick.NET.Tests/bin/Test${quantum}/AnyCPU/netcoreapp2.0

    dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test${quantum}
}

testMagickNET "Q8"
testMagickNET "Q16"
testMagickNET "Q16-HDRI"