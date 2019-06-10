#!/bin/bash
set -e

apt update

apt install cabextract nuget xfonts-utils  -y

curl -s -o ttf-mscorefonts-installer_3.6_all.deb http://ftp.us.debian.org/debian/pool/contrib/m/msttcorefonts/ttf-mscorefonts-installer_3.6_all.deb

sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"

dpkg -i ttf-mscorefonts-installer_3.6_all.deb