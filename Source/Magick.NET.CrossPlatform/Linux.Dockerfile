#
# Creates an image suitable for building the Magick.NET.CrossPlatform project for Linux.
# 
# This image requires that ImageMagick and it's dependencies already be checked out to your
# machine under ImageMagick/Source, ideally using Checkout.cmd.

FROM ubuntu:rolling

# Install packages
RUN apt-get update
RUN apt-get install -y gcc g++ make cmake autoconf autopoint pkg-config libtool nasm git openssh-server
RUN apt-get install -y python-pip gperf

# Initialize the sshd server
RUN mkdir /var/run/sshd; chmod 755 /var/run/sshd
RUN sed -i -e 's/#PasswordAuthentication yes/PasswordAuthentication no/g' /etc/ssh/sshd_config
COPY /Source/Magick.NET.CrossPlatform/authorized_keys /root/.ssh/authorized_keys
RUN chmod 600 ~/.ssh/authorized_keys

# Build zlib
COPY /ImageMagick/Source/ImageMagick/zlib /zlib
WORKDIR /zlib
RUN chmod +x ./configure; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --static; \
    make; \
    make install

# Build libxml
COPY /ImageMagick/Source/ImageMagick/libxml /libxml
WORKDIR /libxml
RUN autoreconf -fiv; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure; \
    make; \
    make install;

# Build libpng
COPY /ImageMagick/Source/ImageMagick/png /png
WORKDIR /png
RUN autoreconf -fiv; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --enable-mips-msa=off --enable-arm-neon=off --enable-powerpc-vsx=off; \
    make; \
    make install

# Build freetype
COPY /ImageMagick/Source/ImageMagick/freetype /freetype
WORKDIR /freetype
RUN ./autogen.sh; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure; \
    make; \
    make install

# build fontconfig
COPY /ImageMagick/Source/ImageMagick/fontconfig /fontconfig
WORKDIR /fontconfig
RUN autoreconf -fiv; \
    sync; \
    pip install lxml; \
    pip install six; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --enable-libxml2 --enable-static=yes; \
    make; \
    export LD_LIBRARY_PATH="/usr/local/lib"; \
    make install;

# Build libjpeg-turbo
COPY /ImageMagick/Source/ImageMagick/jpeg /jpeg
WORKDIR /jpeg
RUN chmod +x ./simd/nasm_lt.sh; \
    autoreconf -fiv; \
    sync; \
    ./configure --with-jpeg8 CFLAGS="-O3 -fPIC"; \
    make; \
    make install prefix=/usr/local libdir=/usr/local/lib64

# Build libtiff
COPY /ImageMagick/Source/ImageMagick/tiff /tiff
WORKDIR /tiff
RUN autoreconf -fiv; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure; \
    make; \
    make install

# Build libwebpmux/demux
COPY /ImageMagick/Source/ImageMagick/webp /webp
WORKDIR /webp
RUN autoreconf -fiv; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --enable-libwebpmux --enable-libwebpdemux; \
    make; \
    make install;

# Build openjpeg
COPY /ImageMagick/Source/ImageMagick/openjpeg /openjpeg
WORKDIR /openjpeg
RUN cmake . -DCMAKE_INSTALL_PREFIX=/usr/local -DBUILD_SHARED_LIBS=off -DBUILD_CODEC=off -DCMAKE_BUILD_TYPE=Release; \
    sync; \
    export CFLAGS="-O3 -fPIC"; \
    make; \
    make install

# Build ImageMagick
COPY /ImageMagick/Source/ImageMagick/ImageMagick /ImageMagick
WORKDIR /ImageMagick

RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --with-quantum-depth=8 --enable-hdri=no; \
    make install
RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --with-quantum-depth=16 --enable-hdri=no; \
    make install
RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-utilities=no --with-quantum-depth=16; \
    make install

# Start sshd on port 22
EXPOSE 22
CMD ["/usr/sbin/sshd", "-D"]
