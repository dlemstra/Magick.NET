#/bin/bash

clone_repository()
{
  echo ''
  echo "Cloning $3 at $2"

  dir="$3"
  if [ ! -z "$4" ]; then
    dir="$4"
  fi
  if [ ! -d "$dir" ]; then
    git clone $1/$3.git $dir
  fi
  cd $dir
  git pull origin master
  git checkout `git rev-list -n 1 --before="$2" origin/master`
  cd ..
}

if [ ! -d "ImageMagick" ]; then
  mkdir ImageMagick
fi

cd ImageMagick

clone_repository $1 "$2" 'bzlib'
clone_repository $1 "$2" 'cairo'
clone_repository $1 "$2" 'croco'
clone_repository $1 "$2" 'exr'
clone_repository $1 "$2" 'ffi'
clone_repository $1 "$2" 'flif'
clone_repository $1 "$2" 'glib'
clone_repository $1 "$2" 'ImageMagick'
clone_repository $1 "$2" 'jp2'
clone_repository $1 "$2" 'jpeg-turbo' 'jpeg'
clone_repository $1 "$2" 'lcms'
clone_repository $1 "$2" 'librsvg'
clone_repository $1 "$2" 'libxml'
clone_repository $1 "$2" 'lqr'
clone_repository $1 "$2" 'openjpeg'
clone_repository $1 "$2" 'pango'
clone_repository $1 "$2" 'pixman'
clone_repository $1 "$2" 'png'
clone_repository $1 "$2" 'tiff'
clone_repository $1 "$2" 'ttf'
clone_repository $1 "$2" 'VisualMagick'
clone_repository $1 "$2" 'webp'
clone_repository $1 "$2" 'zlib'

rm -rf VisualMagick/dcraw
rm -rf VisualMagick/demos
rm -rf VisualMagick/ImageMagickObject
rm -rf VisualMagick/IMDisplay
rm -rf VisualMagick/iptcutil
rm -rf VisualMagick/Magick++
rm -rf VisualMagick/NtMagick
rm -rf VisualMagick/tests
rm -rf VisualMagick/utilities
