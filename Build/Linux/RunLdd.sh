#!/bin/sh
for f in Magick.NET-Q8-x64.Native.dll.so Magick.NET-Q16-x64.Native.dll.so Magick.NET-Q16-HDRI-x64.Native.dll.so
do
  if ldd $f 2>&1 | grep "not found"
  then
    exit 1
  else
    echo "Verified ldd status for $f"
  fi
  if ld $f 2>&1 | grep "undefined reference"
  then
    exit 1
  else
    echo "Verified ld status for $f"
  fi
done