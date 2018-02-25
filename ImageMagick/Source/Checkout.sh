#!/bin/bash

clone() {
  local repo=$1
  local dir=$2
  local root="https://github.com/ImageMagick"

  echo ''
  echo "Cloning $1"

  if [ ! -d "$dir" ]; then
    git clone $root/$repo.git $dir
    if [ $? != 0 ]; then echo "Error during checkout"; exit; fi
  fi
  cd $dir
  git pull origin master
  cd ..
}

#clone and check out a specific commit
clone_commit()
{
  local repo=$1
  local commit=$2
  local dir=$3
  if [ -z $dir ]; then dir=$repo; fi

  clone $repo $dir

  cd $dir
  git checkout $commit
  cd ..
}

#clone and check out a specific date
clone_date()
{
  local repo=$1
  local date=$2
  local dir=$3
  if [ -z $dir ]; then dir=$repo; fi

  clone $repo $dir

  cd $dir
  git checkout `git rev-list -n 1 --before="$date" origin/master`
  cd ..
}

if [ ! -d "ImageMagick" ]; then
  mkdir ImageMagick
fi

cd ImageMagick

clone_commit 'ImageMagick' '5c3fbf6c9eb9ca8bf608ffa6d8ca463e166b3d8f'

# get a commit date from the current ImageMagick checkout
cd ImageMagick
declare -r commitDate=`git log -1 --format=%ci`
echo "Set latest commit date as $commitDate" 
cd ..

if [ "$1" != "Linux" ] && [ "$1" != "Windows" ]; then
  exit
fi

clone_date 'bzlib' "$commitDate"
clone_date 'jpeg-turbo' "$commitDate" 'jpeg'
clone_date 'libxml' "$commitDate"
clone_date 'png' "$commitDate"
clone_date 'tiff' "$commitDate"
clone_date 'webp' "$commitDate"
clone_date 'zlib' "$commitDate"

if [ "$1" != "Windows" ]; then
  exit
fi

clone_date 'cairo' "$commitDate"
clone_date 'croco' "$commitDate"
clone_date 'exr' "$commitDate"
clone_date 'ffi' "$commitDate"
clone_date 'flif' "$commitDate"
clone_date 'glib' "$commitDate"
clone_date 'jp2' "$commitDate"
clone_date 'lcms' "$commitDate"
clone_date 'libde265' "$commitDate"
clone_date 'libraw' "$commitDate"
clone_date 'librsvg' "$commitDate"
clone_date 'lqr' "$commitDate"
clone_date 'openjpeg' "$commitDate"
clone_date 'pango' "$commitDate"
clone_date 'pixman' "$commitDate"
clone_date 'ttf' "$commitDate"
clone_date 'VisualMagick' "$commitDate"

rm -rf VisualMagick/dcraw
rm -rf VisualMagick/demos
rm -rf VisualMagick/fuzz
rm -rf VisualMagick/ImageMagickObject
rm -rf VisualMagick/IMDisplay
rm -rf VisualMagick/iptcutil
rm -rf VisualMagick/Magick++
rm -rf VisualMagick/NtMagick
rm -rf VisualMagick/tests
rm -rf VisualMagick/utilities
