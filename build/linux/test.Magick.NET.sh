#!/bin/bash
set -e

buildAndTest() {
    local quantum=$1

    dotnet build tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f netcoreapp3.1 -c Test$quantum
    dotnet test tests/Magick.NET.Core.Tests/Magick.NET.Core.Tests.csproj -f netcoreapp3.1 -c Test$quantum
    dotnet build tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp3.1 -c Test$quantum
    dotnet test tests/Magick.NET.Tests/Magick.NET.Tests.csproj -f netcoreapp3.1 -c Test$quantum
}

filename=tests/Magick.NET.Tests/Images/Coders/sample.pdf
echo gs -q -dQUIET -dSAFER -dBATCH -dNOPAUSE -dNOPROMPT --permit-file-read="$filename" -c "($filename) (r) file runpdfbegin pdfpagecount = quit"
gs -q -dQUIET -dSAFER -dBATCH -dNOPAUSE -dNOPROMPT --permit-file-read="$filename" -c "($filename) (r) file runpdfbegin pdfpagecount = quit"

gs -o fixed.pdf -sDEVICE=pdfwrite -dPDFSETTINGS=/prepress $filename
cp fixed.pdf $filename

gs -q -dQUIET -dSAFER -dBATCH -dNOPAUSE -dNOPROMPT --permit-file-read="$filename" -c "($filename) (r) file runpdfbegin pdfpagecount = quit"

buildAndTest "Q8"
buildAndTest "Q16"
buildAndTest "Q16-HDRI"
