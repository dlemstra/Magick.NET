#!/bin/bash

docker build . --no-cache -t dlemstra/magick.net-ubuntu.16.04
docker push dlemstra/magick.net-ubuntu.16.04