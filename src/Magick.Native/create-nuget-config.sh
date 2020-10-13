#!/bin/sh
set -e

cp nuget.config.template nuget.config
dotnet nuget update source "github" --source "https://nuget.pkg.github.com/dlemstra/index.json" --username "$1" --password "$2" --store-password-in-clear-text
