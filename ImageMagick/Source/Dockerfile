#
# Creates an image suitable for building the Magick.NET.CrossPlatform project for Linux.
# 
# This image requires that ImageMagick and it's dependencies already be checked out to your
# machine under ImageMagick/Source, ideally using Checkout.cmd.

FROM ubuntu:16.04

# Install packages
RUN apt-get update
RUN apt-get install -y gcc g++ make autoconf autopoint pkg-config libtool nasm git openssh-server dos2unix

# Initialize the sshd server
RUN mkdir /var/run/sshd; chmod 755 /var/run/sshd
RUN sed -i -e 's/#PasswordAuthentication yes/PasswordAuthentication no/g' /etc/ssh/sshd_config
COPY authorized_keys /root/.ssh/authorized_keys
RUN chmod 600 ~/.ssh/authorized_keys

# Build libbz2
COPY /ImageMagick/bzlib /bzlib
WORKDIR /bzlib
RUN make CFLAGS="-Wall -Winline -O3 -fPIC -g -D_FILE_OFFSET_BITS=64"; \
    make install;

# Build zlib
COPY /ImageMagick/zlib /zlib
WORKDIR /zlib
RUN export CFLAGS="-O3 -fPIC"; \
    dos2unix configure; \
    ./configure --static; \
    make; \
    make install

# Build libjpeg-turbo
COPY /ImageMagick/jpeg /jpeg
WORKDIR /jpeg
RUN find . -type f -print0 | xargs -0 dos2unix; \
    chmod +x ./simd/nasm_lt.sh; \
    autoreconf -fiv; \
    ./configure --with-jpeg8 CFLAGS="-O3 -fPIC"; \
    make; \
    make install prefix=/usr/local libdir=/usr/local/lib64

# Build libpng16
COPY /ImageMagick/png /png
WORKDIR /png
#RUN git clone -n https://github.com/glennrp/libpng.git ~/libpng; cd ~/libpng; git checkout tags/v1.6.34
RUN find . -type f -print0 | xargs -0 dos2unix; \
    autoreconf -fiv; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --enable-mips-msa=off --enable-arm-neon=off --enable-powerpc-vsx=off; \
    make; \
    make install

# Build libtiff
COPY /ImageMagick/tiff /tiff
WORKDIR /tiff
RUN find . -type f -print0 | xargs -0 dos2unix; \
    autoreconf -fiv; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure; \
    make; \
    make install

# Build libwebpmux/demux
COPY /ImageMagick/webp /webp
WORKDIR /webp
RUN find . -type f -print0 | xargs -0 dos2unix; \
    autoreconf -fiv; \
    export CFLAGS="-O3 -fPIC"; \
    ./configure --enable-libwebpmux --enable-libwebpdemux; \
    make; \
    make install;


# Build ImageMagick
COPY /ImageMagick/ImageMagick /ImageMagick
WORKDIR /ImageMagick
# Note several scripts need forced line-ending conversion due to odd characters
RUN find . -type f -print0 | xargs -0 dos2unix; \
    dos2unix -f configure Makefile.in config/ltmain.sh;
RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=8 --enable-hdri=no; \
    make install
RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=no; \
    make install
RUN ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16; \
    make install

# Start sshd on port 22
EXPOSE 22
CMD ["/usr/sbin/sshd", "-D"]
