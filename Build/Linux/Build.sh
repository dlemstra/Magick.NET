#!/bin/bash
set -e

export FLAGS="-O3 -fPIC"

# Uninstall already installed development libraries
apt-get update

apt-get remove --autoremove zlib1g-dev -y
apt-get remove --autoremove imagemagick -y

apt-get install pkg-config -y
apt-get install gperf -y
apt-get install nasm -y
apt-get install autoconf -y

sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"
apt-get install ttf-mscorefonts-installer -y

cd ../../ImageMagick/Source

./Checkout.sh Linux

cd ImageMagick

# Build zlib
cd zlib
chmod +x ./configure
./configure
make install CFLAGS="$FLAGS"

# Build libxml
cd ../libxml
autoreconf -fiv
./configure --with-python=no --enable-static --disable-shared CFLAGS="$FLAGS"
make install

# Build libpng
cd ../png
autoreconf -fiv
./configure --enable-mips-msa=off --enable-arm-neon=off --enable-powerpc-vsx=off CFLAGS="$FLAGS"
make install

# Build freetype
cd ../freetype
./autogen.sh
./configure --disable-shared CFLAGS="$FLAGS"
make install

# Build fontconfig
cd ../fontconfig
autoreconf -fiv
pip install lxml
pip install six
./configure --enable-libxml2 --enable-static=yes CFLAGS="$FLAGS"
make install

# Build libjpeg-turbo
cd ../jpeg
cmake . -DCMAKE_INSTALL_PREFIX=/usr/local -DENABLE_SHARED=off -DCMAKE_BUILD_TYPE=Release -DCMAKE_C_FLAGS="$FLAGS"
make install

# Build libtiff
cd ../tiff
autoreconf -fiv
./configure CFLAGS="$FLAGS"
make install

# Build libwebp
cd ../webp
autoreconf -fiv
./configure --enable-libwebpmux --enable-libwebpdemux CFLAGS="$FLAGS"
make install

# Build openjpeg
cd ../openjpeg
cmake . -DCMAKE_INSTALL_PREFIX=/usr/local -DBUILD_SHARED_LIBS=off -DCMAKE_BUILD_TYPE=Release -DCMAKE_C_FLAGS="$FLAGS"
make install
cp bin/libopenjp2.a /usr/local/lib

# Build lcms
cd ../lcms
autoreconf -fiv
./configure --disable-shared --prefix=/usr/local CFLAGS="$FLAGS"
make install

# Build libde265
cd ../libde265
autoreconf -fiv
./configure --disable-shared --prefix=/usr/local CFLAGS="$FLAGS" CXXFLAGS="$FLAGS"
make install

# Build libheif
cd ../libheif
autoreconf -fiv
./configure --disable-shared --disable-go --prefix=/usr/local CFLAGS="$FLAGS" CXXFLAGS="$FLAGS"
make install

# Build libraw
cd ../libraw
chmod +x ./version.sh
chmod +x ./shlib-version.sh
autoreconf -fiv
./configure --disable-shared --disable-examples --disable-openmp --disable-jpeg --disable-jasper --prefix=/usr/local CFLAGS="$FLAGS" CXXFLAGS="$FLAGS"
make install

cd ../../../../
mkdir Output

cd ImageMagick/Source/ImageMagick/ImageMagick
autoreconf -fiv
cd ../../../

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
    ./configure CFLAGS="$FLAGS -Wall" CXXFLAGS="$FLAGS -Wall" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --disable-docs --with-bzlib=no --with-lzma=no --with-x=no --with-quantum-depth=$depth --enable-hdri=$hdri
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

verifyPlatform() {
    local platform=$1

    echo "FROM ${platform}" | cat - Verify.Dockerfile > Verify.Platform.Dockerfile
    docker build -f Verify.Platform.Dockerfile .
    docker image prune -a -f
}

cd Build/Linux
cp ../../Output/Magick.NET-Q8-x64.Native.dll.so .
cp ../../Output/Magick.NET-Q16-x64.Native.dll.so .
cp ../../Output/Magick.NET-Q16-HDRI-x64.Native.dll.so .

platforms=("ubuntu:16.04" "ubuntu:17.10" "ubuntu:18.04" "ubuntu:latest" "centos:7" "microsoft/dotnet:2.0-runtime" "microsoft/dotnet:latest" "lambci/lambda:dotnetcore2.0")
for platform in "${platforms[@]}"
do
    verifyPlatform ${platform}
done