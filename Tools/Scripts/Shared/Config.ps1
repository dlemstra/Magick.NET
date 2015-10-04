#==================================================================================================
$builds = @(
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q8";       Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $true}
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q8";       Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $false}
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16";      Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $true}
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16";      Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $false}
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $true}
  @{Name = "Magick.NET.net20"; Suffix = ".net20"; Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v2.0"; FrameworkName = "net20";        RunTests = $false}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q8";       Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q8";       Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q16";      Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q16";      Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q16-HDRI"; Platform = "x86"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  @{Name = "Magick.NET";       Suffix = "";       Quantum = "Q16-HDRI"; Platform = "x64"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $false}
  )
$anyCPUbuilds = @(
  @{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q8";       Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  @{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q16";      Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  @{Name = "Magick.NET.AnyCPU"; Suffix=""; Quantum = "Q16-HDRI"; Platform = "AnyCPU"; Framework = "v4.0"; FrameworkName = "net40-client"; RunTests = $true}
  )
#==================================================================================================
function GetBuilds($quantum, $platform)
{
  if ($quantum -eq "Q8") 
  {
    if ($platform -eq "x86")
    {
      return @($builds[6], $builds[0])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[7], $builds[1])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[0])
    }
  }
  elseif ($quantum -eq "Q16")
  {
    if ($platform -eq "x86")
    {
      return @($builds[8], $builds[2])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[9], $builds[3])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[1])
    }
  }
  elseif ($quantum -eq "Q16-HDRI")
  {
    if ($platform -eq "x86")
    {
      return @($builds[10], $builds[4])
    }
    elseif ($platform -eq "x64")
    {
      return @($builds[11], $builds[5])
    }
    elseif ($platform -eq "AnyCPU")
    {
      return @($anyCPUbuilds[2])
    }
  }
  return $null
}
#==================================================================================================