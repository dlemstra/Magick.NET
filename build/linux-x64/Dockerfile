FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN mkdir /magick

WORKDIR /magick

RUN mkdir build && mkdir build/linux-x64

COPY build/linux-x64/install.dependencies.sh build/linux-x64

RUN build/linux-x64/install.dependencies.sh

COPY src src

COPY build/shared build/shared

ARG GITHUB_TOKEN

RUN build/shared/install.Magick.Native.sh dlemstra ${GITHUB_TOKEN} linux x64

COPY stylecop.json stylecop.json

COPY logo logo

COPY tests tests

ARG OpenMP

RUN build/shared/build.Magick.NET.sh $OpenMP

RUN build/shared/test.Magick.NET.sh $OpenMP
