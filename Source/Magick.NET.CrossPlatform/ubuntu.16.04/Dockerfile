FROM ubuntu:16.04

# Install packages
RUN apt-get update
RUN apt-get install -y gcc g++ make autoconf pkg-config libjpeg-turbo8-dev libtiff5-dev libpng16-dev libwebp-dev git openssh-server

# Initialize the sshd server
RUN mkdir /var/run/sshd; chmod 755 /var/run/sshd
RUN sed -i -e 's/#PasswordAuthentication yes/PasswordAuthentication no/g' /etc/ssh/sshd_config
COPY authorized_keys /root/.ssh/authorized_keys
RUN chmod 600 ~/.ssh/authorized_keys

# Build ImageMagick
RUN git clone -n https://github.com/ImageMagick/ImageMagick ~/ImageMagick
RUN cd ~/ImageMagick; git checkout tags/7.0.7-21;
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=8 --enable-hdri=no; make install
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=no; make install
RUN cd ~/ImageMagick; ./configure CFLAGS="-fPIC -Wall -O3" CXXFLAGS="-fPIC -Wall -O3" --disable-shared --enable-static --enable-delegate-build --with-magick-plus-plus=no --with-quantum-depth=16; make install

# Start sshd on port 22
EXPOSE 22
CMD ["/usr/sbin/sshd", "-D"]
