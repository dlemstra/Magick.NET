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
$builds = @(
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x86"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "x64"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x86"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "x64"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x86"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "x64"; FrameworkName = "netstandard13";}
)
$anyCPUbuilds = @(
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "AnyCPU"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q8";       Platform = "AnyCPU"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "AnyCPU"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16";      Platform = "AnyCPU"; FrameworkName = "netstandard13";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "AnyCPU"; Framework = "v2.0"; FrameworkName = "net20";}
  @{Name = "Magick.NET"; Quantum = "Q16-HDRI"; Platform = "AnyCPU"; FrameworkName = "netstandard13";}
)

function GetBuilds($quantum, $platform)
{
  if ($quantum -eq "Q8") 
  {
    if ($platform -eq "x86")
    {
      return @($builds[0], $builds[1], $builds[2])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[3], $builds[4], $builds[5])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[0], $anyCPUbuilds[1], $anyCPUbuilds[2])
    }
  }
  elseif ($quantum -eq "Q16")
  {
    if ($platform -eq "x86")
    {
      return @($builds[6], $builds[7], $builds[8])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[9], $builds[10], $builds[11])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[3], $anyCPUbuilds[4], $anyCPUbuilds[5])
    }
  }
  elseif ($quantum -eq "Q16-HDRI")
  {
    if ($platform -eq "x86")
    {
      return @($builds[12], $builds[13], $builds[14])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[15], $builds[16], $builds[17])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[6], $anyCPUbuilds[7], $anyCPUbuilds[8])
    }
  }
  return $null
}

function GetDevVersion()
{
  $fileName = "C:\DevVersion.txt"
  If (Test-Path $fileName)
  {
    return [IO.File]::ReadAllText($fileName, [System.Text.Encoding]::Default)
  }
  else
  {
    $utcNow = [System.DateTime]::Now.ToUniversalTime()
    $build = [int]($utcNow.Date - (New-Object System.DateTime –ArgumentList 2000, 1, 1).Date).TotalDays
    $revision = [int]($utcNow.TimeOfDay.TotalSeconds / 2)
    $version = "1.0.$build.$revision"
    [IO.File]::WriteAllText($fileName, $version, [System.Text.Encoding]::Default)
    return $version
  }
}
