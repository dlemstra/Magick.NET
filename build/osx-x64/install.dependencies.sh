#!/bin/bash
set -e

export HOMEBREW_NO_AUTO_UPDATE=1

brew install ffmpeg fontconfig
fc-list
