FROM mcr.microsoft.com/dotnet/sdk:6.0-focal-arm64v8

ARG GITHUB_TOKEN

RUN mkdir /build

COPY build/linux-arm64/install.dependencies.sh /build

RUN cd /build; ./install.dependencies.sh

COPY src /build/src

COPY build/linux-arm64/install.Magick.Native.sh /build

RUN cd /build; ./install.Magick.Native.sh dlemstra ${GITHUB_TOKEN}

COPY stylecop.json /build/stylecop.json

COPY logo /build/logo

COPY tests /build/tests

COPY build/linux-arm64/build.Magick.NET.sh /build

RUN cd /build; ./build.Magick.NET.sh

COPY build/linux-arm64/test.Magick.NET.sh /build

RUN cd /build; ./test.Magick.NET.sh
