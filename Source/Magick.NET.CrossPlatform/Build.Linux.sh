#!/bin/bash
set -e

# Uninstall already installed development libraries
sudo apt-get remove zlib1g-dev -y
sudo apt-get remove libpng12-0 -y
sudo apt-get remove imagemagick -y
sudo apt-get auto-remove -y

sudo apt-get update

sudo apt-get install pkg-config -y
sudo apt-get install gperf -y
sudo apt-get install nasm -y
sudo apt-get install autoconf -y

sudo sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"
sudo apt-get install ttf-mscorefonts-installer -y

cd ../../ImageMagick/Source

./Checkout.sh Linux

cd ImageMagick

# Clone freetype
git clone git://git.sv.nongnu.org/freetype/freetype2.git freetype
cd freetype
git reset --hard
git fetch
git checkout VER-2-9
cd ../

# Clone fontconfig
git clone git://anongit.freedesktop.org/fontconfig fontconfig
cd fontconfig
git reset --hard
git fetch
git checkout 2.12.6
cd ../

# Build zlib
cd zlib
chmod +x ./configure
export CFLAGS="-O3 -fPIC"
./configure --static
make install

# Build libxml
cd ../libxml
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure --with-python=no
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
./configure
make install

# Build fontconfig
cd ../fontconfig
autoreconf -fiv
pip install lxml
pip install six
export CFLAGS="-O3 -fPIC"
./configure --enable-libxml2 --enable-static=yes
export LD_LIBRARY_PATH="/usr/local/lib"
make install

# Build libjpeg-turbo
cd ../jpeg
chmod +x ./simd/nasm_lt.sh
autoreconf -fiv
./configure --with-jpeg8 CFLAGS="-O3 -fPIC"
make install prefix=/usr/local libdir=/usr/local/lib64

# Build libtiff
cd ../tiff
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure
make install

# Build libwebpmux/demux
cd ../webp
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
./configure --enable-libwebpmux --enable-libwebpdemux
make install

# Build openjpeg
cd ../openjpeg
cmake . -DCMAKE_INSTALL_PREFIX=/usr/local -DBUILD_SHARED_LIBS=off -DCMAKE_BUILD_TYPE=Release -DCMAKE_C_FLAGS="-O3 -fPIC"
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
export CFLAGS="-O3 -fPIC"
export CXXFLAGS="-O3 -fPIC"
./configure --disable-shared --prefix=/usr/local
make install

# Build libheif
cd ../libheif
autoreconf -fiv
export CFLAGS="-O3 -fPIC"
export CXXFLAGS="-O3 -fPIC"
./configure --disable-shared --prefix=/usr/local
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
    ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --with-bzlib=no --with-quantum-depth=$depth --enable-hdri=$hdri
    make install

    # Build Magick.NET
    cd ../../../../Source/Magick.NET.Native
    echo "" > foo.cxx
    mkdir $quantum
    cd $quantum

    cmake -D DEPTH=$depth -D QUANTUM=$quantum -D HDRI_ENABLE=$hdri_enable -DQUANTUM_NAME=$quantum_name -DPLATFORM=LINUX ..
    make
    cp libMagick.NET-$quantum-x64.Native.dll.so ../../../Output/Magick.NET-$quantum-x64.Native.dll.so
    cd ../../../
}

buildMagickNET "Q8"
buildMagickNET "Q16"
buildMagickNET "Q16-HDRI"

testMagickNET() {
    local quantum=$1

    dotnet build Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test${quantum}

    cp Output/Magick.NET-$quantum-x64.Native.dll.so Tests/Magick.NET.Tests/bin/Test${quantum}/AnyCPU/netcoreapp2.0

    dotnet test Tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp2.0 -c Test${quantum}
}

testMagickNET "Q8"
testMagickNET "Q16"
testMagickNET "Q16-HDRI"

# Verify libraries
cd Source/Magick.NET.CrossPlatform
mkdir verify
cp ../../Output/Magick.NET-Q8-x64.Native.dll.so verify
cp ../../Output/Magick.NET-Q16-x64.Native.dll.so verify
cp ../../Output/Magick.NET-Q16-HDRI-x64.Native.dll.so verify
docker build -f Linux.Verify.Dockerfile .