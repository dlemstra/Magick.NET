FROM mcr.microsoft.com/dotnet/core/sdk:latest

ARG GITHUB_TOKEN

RUN mkdir /build

COPY build/linux/install.dependencies.sh /build

RUN cd /build; ./install.dependencies.sh

COPY src /build/src

COPY build/linux/install.Magick.Native.sh /build

RUN cd /build; ./install.Magick.Native.sh dlemstra ${GITHUB_TOKEN}

COPY stylecop.json /build/stylecop.json

COPY logo /build/logo

COPY tests /build/tests

COPY build/linux/test.Magick.NET.sh /build

RUN cd /build; ./test.Magick.NET.sh
