#!/bin/bash
set -e

cd /Magick.NET
build/linux-arm64/install.dependencies.sh
build/shared/build.Magick.NET.sh
build/shared/test.Magick.NET.sh
build/shared/build.Magick.NET.sh OpenMP
build/shared/test.Magick.NET.sh OpenMP
