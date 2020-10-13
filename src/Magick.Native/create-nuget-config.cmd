@echo off

if not [%1] == [] (
    copy nuget.config.template nuget.config
    ..\..\tools\windows\nuget.exe sources update -Name "github" -Source "https://nuget.pkg.github.com/dlemstra/index.json" -UserName "%1" -Password "%2"
)