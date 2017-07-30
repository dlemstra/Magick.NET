FROM ubuntu:16.04

# Install packages
RUN apt-get update
RUN apt-get install -y gcc g++ make autoconf libjpeg-turbo8-dev git openssh-server

# Initialize the sshd server
RUN mkdir /var/run/sshd;chmod 755 /var/run/sshd
RUN sed -i -e 's/#PasswordAuthentication yes/PasswordAuthentication no/g' /etc/ssh/sshd_config
RUN mkdir ~/.ssh;echo "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAwFyerjwW+MpAIV056qHP0pJLAJn2acRQNtFnmQiCXP6AukRMr7rJ23w3TsyGEKzTGCi/jQMHTWzGYGj7OtNNTN+OSFvx9OVg+4wr6kYPQmPZBnPvUg727nqggAGZ90j7bL/B+7qiUJxoHOBqPSS56A7oMmsA19Iy2kGaYlBLB0J+YY1wHlRyVa4vyxZox6n9ORZ6Wat+OGc6fyo9sNYWBCVzufUWAhQM2JCctCO5htglytD3vk/wJnJ5k/VEajYueM90m3V7fv4DucWvPJk75TlSJUkh81yujQttPmar74k4vcTf/zfUpkcYBHucvMATUrBfpE+e1OL013JM470RuQ==" > ~/.ssh/authorized_keys;chmod 600 ~/.ssh/authorized_keys

# Build ImageMagick
RUN mkdir Magick.NET
RUN git clone --depth 1 https://github.com/dlemstra/Magick.NET.git Magick.NET
RUN cd Magick.NET/ImageMagick/Source;sh Checkout.sh
RUN cd Magick.NET/ImageMagick/Source/ImageMagick/ImageMagick;./configure --with-magick-plus-plus=no --with-quantum-depth=8 --enable-hdri=false;make install
RUN cd Magick.NET/ImageMagick/Source/ImageMagick/ImageMagick;./configure --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=false;make install
RUN cd Magick.NET/ImageMagick/Source/ImageMagick/ImageMagick;./configure --with-magick-plus-plus=no --with-quantum-depth=16 --enable-hdri=true;make install

# Start sshd on port 22
EXPOSE 22
CMD ["/usr/sbin/sshd", "-D"]
