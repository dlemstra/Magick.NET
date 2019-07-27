#!/bin/bash
set -e

apt update

sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections"

apt install msttcorefonts