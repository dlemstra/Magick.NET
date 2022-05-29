FROM mcr.microsoft.com/dotnet/sdk:6.0

ARG GITHUB_TOKEN

RUN mkdir /magick

WORKDIR /magick

RUN mkdir build && mkdir build/linux-x64

COPY build/shared build/shared

COPY build/linux-x64/install.dependencies.sh build/linux-x64

RUN build/linux-x64/install.dependencies.sh

COPY src src

RUN build/shared/install.Magick.Native.sh dlemstra ${GITHUB_TOKEN} linux x64

COPY stylecop.json stylecop.json

COPY logo logo

COPY tests tests

RUN build/shared/build.Magick.NET.sh

RUN build/shared/test.Magick.NET.sh
