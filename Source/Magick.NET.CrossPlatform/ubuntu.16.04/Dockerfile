FROM ubuntu:16.04

# Install packages
RUN apt-get update
RUN apt-get install -y gcc g++ make autoconf autopoint pkg-config libtool nasm git openssh-server

# Initialize the sshd server
RUN mkdir /var/run/sshd; chmod 755 /var/run/sshd
RUN sed -i -e 's/#PasswordAuthentication yes/PasswordAuthentication no/g' /etc/ssh/sshd_config
COPY authorized_keys /root/.ssh/authorized_keys
RUN chmod 600 ~/.ssh/authorized_keys

# Build libbz2
RUN cd ~; wget -q http://www.bzip.org/1.0.6/bzip2-1.0.6.tar.gz; tar xf bzip2-1.0.6.tar.gz;
RUN cd ~/bzip2-1.0.6; make CFLAGS="-Wall -Winline -O3 -fPIC -g -D_FILE_OFFSET_BITS=64"; make install;

# Build zlib
RUN git clone -n https://github.com/madler/zlib.git ~/libz; cd ~/libz; git checkout tags/v1.2.11
RUN cd ~/libz; export CFLAGS="-O3 -fPIC"; ./configure --static; make; make install

# Build lzma
RUN git clone -n https://git.tukaani.org/xz.git ~/lzma; cd ~/lzma; git checkout tags/v5.2.3
RUN cd ~/lzma; autoreconf -fiv; export CFLAGS="-O3 -fPIC"; ./configure; make; make install;

# Build libjpeg-turbo
RUN git clone -n https://github.com/libjpeg-turbo/libjpeg-turbo.git ~/libjpeg; cd ~/libjpeg; git checkout tags/1.5.3
RUN cd ~/libjpeg; autoreconf -fiv; ./configure --with-jpeg8 CFLAGS="-O3 -fPIC"; make; make install prefix=/usr/local libdir=/usr/local/lib64

# Build libpng16
RUN git clone -n https://github.com/glennrp/libpng.git ~/libpng; cd ~/libpng; git checkout tags/v1.6.34
RUN cd ~/libpng; autoreconf -fiv; ./configure CFLAGS="-O3 -fPIC"; make; make install

# Build libtiff
RUN git clone -n https://gitlab.com/libtiff/libtiff.git ~/libtiff; cd ~/libtiff; git checkout tags/Release-v4-0-9
RUN cd ~/libtiff; autoreconf -fiv; export CFLAGS="-O3 -fPIC"; ./configure; make; make install

# Build libwebpmux/demux
RUN git clone -n https://chromium.googlesource.com/webm/libwebp ~/libwebp; cd ~/libwebp; git checkout tags/v0.6.1
RUN cd ~/libwebp; autoreconf -fiv; export CFLAGS="-O3 -fPIC"; ./configure --enable-libwebpmux --enable-libwebpdemux; make; make install;


# Build ImageMagick
RUN git clone -n https://github.com/ImageMagick/ImageMagick ~/ImageMagick
RUN cd ~/ImageMagick; git checkout tags/7.0.7-21;
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=8 --enable-hdri=no; make install
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=no; make install
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --disable-openmp --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16; make install

# Start sshd on port 22
EXPOSE 22
CMD ["/usr/sbin/sshd", "-D"]
