#==================================================================================================
function Build($build)
{
  $config = "Release"

  if ($build.RunTests -eq $true)
  {
    $config = "Tests"
  }

  BuildSolution "$($build.Name).sln" "Configuration=$config$($build.Quantum),RunCodeAnalysis=false,Platform=$($build.Platform)"
}
#==================================================================================================
