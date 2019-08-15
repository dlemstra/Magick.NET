#!/bin/bash
set -e

sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"

apt update

apt install msttcorefonts -y