#!/bin/bash
set -e

apt-get update -y

apt-get -o DPkg::Options::="--force-confold" -y install sudo