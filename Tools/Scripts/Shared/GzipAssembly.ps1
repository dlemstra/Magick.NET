#==================================================================================================
# Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in 
# compliance with the License. You may obtain a copy of the License at
#
#   http://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
# express or implied. See the License for the specific language governing permissions and
# limitations under the License.
#==================================================================================================
function GzipAssembly($inFile,$outFile)
{
  $inFile = FullPath $inFile
  $outFile = FullPath $outFile

  if (!(Test-Path $inFile))
  {
    Write-Error "Unable to find file: $inFile."
    Exit 1
  }

  Write-Host "Compressing $inFile to $outFile."

  $input = New-Object System.IO.FileStream $inFile, ([IO.FileMode]::Open), ([IO.FileAccess]::Read), ([IO.FileShare]::Read)

  $buffer = New-Object byte[]($input.Length)
  $byteCount = $input.Read($buffer, 0, $input.Length)
  $input.Close()

  $folder = (Split-Path $outFile -Parent)
  if (!(Test-Path $folder))
  {
    New-Item -ItemType directory -Path $folder
  }
  $output = New-Object System.IO.FileStream $outFile, ([IO.FileMode]::Create), ([IO.FileAccess]::Write), ([IO.FileShare]::None)
  $gzipStream = New-Object System.IO.Compression.GzipStream $output, ([IO.Compression.CompressionMode]::Compress)

  $gzipStream.Write($buffer, 0, $buffer.Length)
  $gzipStream.Close()

  $output.Close()
}

function GzipAssemblies()
{
  GzipAssembliesQ8
  GzipAssembliesQ16
  GzipAssembliesQ16HDRI
}

function GzipAssembliesQ8()
{
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ8\Win32\Magick.NET-Q8-x86.Native.dll" "Source\Magick.NET\Resources\ReleaseQ8\Magick.NET-Q8-x86.Native.gz"
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ8\x64\Magick.NET-Q8-x64.Native.dll" "Source\Magick.NET\Resources\ReleaseQ8\Magick.NET-Q8-x64.Native.gz"
}

function GzipAssembliesQ16()
{
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ16\Win32\Magick.NET-Q16-x86.Native.dll" "Source\Magick.NET\Resources\ReleaseQ16\Magick.NET-Q16-x86.Native.gz"
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ16\x64\Magick.NET-Q16-x64.Native.dll" "Source\Magick.NET\Resources\ReleaseQ16\Magick.NET-Q16-x64.Native.gz"
}

function GzipAssembliesQ16HDRI()
{
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ16-HDRI\Win32\Magick.NET-Q16-HDRI-x86.Native.dll" "Source\Magick.NET\Resources\ReleaseQ16-HDRI\Magick.NET-Q16-HDRI-x86.Native.gz"
  GzipAssembly "Source\Magick.NET.Native\bin\ReleaseQ16-HDRI\x64\Magick.NET-Q16-HDRI-x64.Native.dll" "Source\Magick.NET\Resources\ReleaseQ16-HDRI\Magick.NET-Q16-HDRI-x64.Native.gz"
}
