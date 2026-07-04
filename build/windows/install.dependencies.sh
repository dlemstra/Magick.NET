#!/bin/bash
set -e

script_dir="$(cd "$(dirname "$0")" && pwd)"

echo "Downloading Ghostscript 10.00.0"
gh release download build-binaries-2025-08-30 \
  --repo dlemstra/Magick.NET.BuildDependencies \
  --pattern gs1000w32.exe \
  --output "$script_dir/../../tools/windows/gs1000w32.exe" \
  --clobber

echo "Installing Ghostscript"
MSYS_NO_PATHCONV=1 "$script_dir/../../tools/windows/gs1000w32.exe" /S

echo "Downloading FFmpeg 4.2.3"
gh release download build-binaries-2025-08-30 \
  --repo dlemstra/Magick.NET.BuildDependencies \
  --pattern ffmpeg-4.2.3-win64.exe \
  --output /c/vcpkg/ffmpeg.exe \
  --clobber
