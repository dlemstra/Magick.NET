@echo off

set /p GITHUB_TOKEN=<../../src/Magick.Native/api.key.txt
docker build ../.. -f Dockerfile --build-arg GITHUB_TOKEN=%GITHUB_TOKEN% -t magick-net-linux