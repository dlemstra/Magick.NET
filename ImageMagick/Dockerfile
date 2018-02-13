# Quick script to run ldd against binaries on a variety of platforms. 

FROM ubuntu as base
RUN apt-get update
RUN apt-get install -y dos2unix
COPY verify ./verify
RUN dos2unix ./verify

FROM ubuntu:16.04
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM ubuntu:17.10
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM ubuntu:18.04
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM ubuntu:latest
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM centos:7
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM microsoft/dotnet:2.0-runtime
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify

FROM microsoft/dotnet:latest
COPY ./Q8/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./Q16/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./Q16-HDRI/lib/Release/CrossPlatform/ubuntu.16.04/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY --from=base verify ./verify
RUN ./verify
