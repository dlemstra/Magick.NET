# Quick script to run ldd against binaries on a variety of platforms. 
# Quantum folders from <Magick.NET project>/ImageMagick must be copied to this folder before building.

FROM ubuntu:16.04
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify

FROM ubuntu:17.10
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify

FROM ubuntu:18.04
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify

FROM ubuntu:latest
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify

FROM centos:7
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify

FROM alpine:3.5
#RUN apk update; apk add libgomp xz-libs libbz2 libgcc
RUN ln -s /usr/lib/libbz2.so.1.0.6 /usr/lib/libbz2.so.1.0
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY verify ./verify
RUN ./verify
