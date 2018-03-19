# Quick script to run ldd against binaries on a variety of platforms. 

FROM ubuntu:16.04
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM ubuntu:17.10
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM ubuntu:18.04
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM ubuntu:latest
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM centos:7
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM microsoft/dotnet:2.0-runtime
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM microsoft/dotnet:latest
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify

FROM lambci/lambda:dotnetcore2.0
COPY ./verify/Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY ./verify/Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY Linux.Verify ./Linux.Verify
RUN ./Linux.Verify