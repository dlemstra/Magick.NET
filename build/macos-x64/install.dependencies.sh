#!/bin/bash
set -e

export HOMEBREW_NO_AUTO_UPDATE=1

fc-list

brew install ffmpeg
