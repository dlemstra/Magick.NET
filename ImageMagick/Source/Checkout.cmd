@echo off

set REPOS=https://subversion.imagemagick.org/subversion
set REVISION=15437

if not exist ImageMagick goto checkout
rmdir /s /q ImageMagick
if exist ImageMagick goto done

:checkout
mkdir ImageMagick
cd ImageMagick
svn checkout %REPOS%/bzlib/trunk -r %REVISION% bzlib
svn checkout %REPOS%/cairo/trunk -r %REVISION% cairo
svn checkout %REPOS%/croco/trunk -r %REVISION% croco
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/coders -r %REVISION% coders
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/config -r %REVISION% config
svn checkout %REPOS%/ffi/trunk -r %REVISION% ffi
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/filters -r %REVISION% filters
svn checkout %REPOS%/glib/trunk -r %REVISION% glib
svn checkout %REPOS%/jbig/trunk -r %REVISION% jbig
svn checkout %REPOS%/jp2/trunk -r %REVISION% jp2
svn checkout %REPOS%/jpeg-turbo/trunk -r %REVISION% jpeg
svn checkout %REPOS%/lcms/trunk -r %REVISION% lcms
svn checkout %REPOS%/libxml/trunk -r %REVISION% libxml
svn checkout %REPOS%/librsvg/trunk -r %REVISION% librsvg
svn checkout %REPOS%/lqr/trunk -r %REVISION% lqr
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/magick -r %REVISION% magick
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/Magick++ -r %REVISION% Magick++
svn checkout %REPOS%/openjpeg/trunk -r %REVISION% openjpeg
svn checkout %REPOS%/pango/trunk -r %REVISION% pango
svn checkout %REPOS%/pixman/trunk -r %REVISION% pixman
svn checkout %REPOS%/png/trunk -r %REVISION% png
svn checkout %REPOS%/tiff/trunk -r %REVISION% tiff
svn checkout %REPOS%/ttf/trunk -r %REVISION% ttf
svn checkout %REPOS%/VisualMagick/branches/VisualMagick-6 -r %REVISION% VisualMagick
svn checkout %REPOS%/ImageMagick/branches/ImageMagick-6/wand -r %REVISION% wand
svn checkout %REPOS%/webp/trunk -r %REVISION% webp
svn checkout %REPOS%/zlib/trunk -r %REVISION% zlib

:done
pause
