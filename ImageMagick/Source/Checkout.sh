#/bin/bash

clone_repository()
{
  repos="http://git.imagemagick.org/repos"
  date="2017-07-11 01:35"

  echo ''
  echo "Cloning $1 at $date"

  dir="$1"
  if [ ! -z "$2" ]; then
    dir="$2"
  fi
  if [ ! -d "$dir" ]; then
    git clone $repos/$1.git $dir
  fi
  cd $dir
  git pull origin master
  git checkout `git rev-list -n 1 --before="$date" origin/master`
  cd ..
}

if [ ! -d "ImageMagick" ]; then
  mkdir ImageMagick
fi

cd ImageMagick

clone_repository 'bzlib'
clone_repository 'cairo'
clone_repository 'croco'
clone_repository 'exr'
clone_repository 'ffi'
clone_repository 'flif'
clone_repository 'glib'
clone_repository 'ImageMagick'
clone_repository 'jp2'
clone_repository 'jpeg-turbo' 'jpeg'
clone_repository 'lcms'
clone_repository 'librsvg'
clone_repository 'libxml'
clone_repository 'lqr'
clone_repository 'openjpeg'
clone_repository 'pango'
clone_repository 'pixman'
clone_repository 'png'
clone_repository 'tiff'
clone_repository 'ttf'
clone_repository 'VisualMagick'
clone_repository 'webp'
clone_repository 'zlib'

rm -rf VisualMagick/dcraw
rm -rf VisualMagick/demos
rm -rf VisualMagick/ImageMagickObject
rm -rf VisualMagick/IMDisplay
rm -rf VisualMagick/iptcutil
rm -rf VisualMagick/Magick++
rm -rf VisualMagick/NtMagick
rm -rf VisualMagick/tests
rm -rf VisualMagick/utilities
