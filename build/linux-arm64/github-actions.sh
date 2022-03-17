#!/bin/bash
set -e

cd /Magick.NET
build/linux-arm64/install.dependencies.sh
build/linux-arm64/test.Magick.NET.sh
