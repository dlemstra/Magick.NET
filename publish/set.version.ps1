# Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in
# compliance with the License. You may obtain a copy of the License at
#
#   https://www.imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
# either express or implied. See the License for the specific language governing permissions
# and limitations under the License.

& cmd /c 'git describe --exact-match --tags HEAD > tag.txt 2> nul'

$tag = [IO.File]::ReadAllText("tag.txt").Trim()

if ($tag.Length -eq 0) {
    $tag = Get-Date -Format "0.yyyy.MMdd.HHmm"
}

Write-Host "::set-env name=NuGetVersion::$tag"

& cmd /c 'git rev-parse HEAD > commit.txt 2> nul'

$commit = [IO.File]::ReadAllText("commit.txt").Trim()

Write-Host "::set-env name=GitCommitId::$commit"

Exit 0
