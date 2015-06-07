@echo off

set REPOS=https://subversion.imagemagick.org/subversion
set REVISION=18800

if exist ImageMagick goto update

mkdir ImageMagick
cd ImageMagick
svn checkout %REPOS%/bzlib/trunk -r %REVISION% bzlib
svn checkout %REPOS%/cairo/trunk -r %REVISION% cairo
svn checkout %REPOS%/croco/trunk -r %REVISION% croco
svn checkout %REPOS%/exr/trunk -r %REVISION% exr
svn checkout %REPOS%/ffi/trunk -r %REVISION% ffi
svn checkout %REPOS%/glib/trunk -r %REVISION% glib
svn checkout %REPOS%/ImageMagick/trunk -r %REVISION% ImageMagick
svn checkout %REPOS%/jp2/trunk -r %REVISION% jp2
svn checkout %REPOS%/jpeg-turbo/trunk -r %REVISION% jpeg
svn checkout %REPOS%/lcms/trunk -r %REVISION% lcms
svn checkout %REPOS%/libxml/trunk -r %REVISION% libxml
svn checkout %REPOS%/librsvg/trunk -r %REVISION% librsvg
svn checkout %REPOS%/lqr/trunk -r %REVISION% lqr
svn checkout %REPOS%/openjpeg/trunk -r %REVISION% openjpeg
svn checkout %REPOS%/pango/trunk -r %REVISION% pango
svn checkout %REPOS%/pixman/trunk -r %REVISION% pixman
svn checkout %REPOS%/png/trunk -r %REVISION% png
svn checkout %REPOS%/tiff/trunk -r %REVISION% tiff
svn checkout %REPOS%/ttf/trunk -r %REVISION% ttf
svn checkout %REPOS%/VisualMagick/trunk -r %REVISION% VisualMagick
svn checkout %REPOS%/webp/trunk -r %REVISION% webp
svn checkout %REPOS%/zlib/trunk -r %REVISION% zlib
goto done

:update
cd ImageMagick
svn update -r %REVISION% bzlib
svn update -r %REVISION% cairo
svn update -r %REVISION% croco
svn update -r %REVISION% exr
svn update -r %REVISION% ffi
svn update -r %REVISION% glib
svn update -r %REVISION% ImageMagick
svn update -r %REVISION% jp2
svn update -r %REVISION% jpeg
svn update -r %REVISION% lcms
svn update -r %REVISION% libxml
svn update -r %REVISION% librsvg
svn update -r %REVISION% lqr
svn update -r %REVISION% openjpeg
svn update -r %REVISION% pango
svn update -r %REVISION% pixman
svn update -r %REVISION% png
svn update -r %REVISION% tiff
svn update -r %REVISION% ttf
svn update -r %REVISION% VisualMagick
svn update -r %REVISION% webp
svn update -r %REVISION% zlib

:done
rmdir /s /q VisualMagick\dcraw
rmdir /s /q VisualMagick\demos
rmdir /s /q VisualMagick\ImageMagickObject
rmdir /s /q VisualMagick\IMDisplay
rmdir /s /q VisualMagick\iptcutil
rmdir /s /q VisualMagick\NtMagick
rmdir /s /q VisualMagick\tests
rmdir /s /q VisualMagick\utilities
pause
