FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN mkdir /build

COPY build/linux-x64/install.dependencies.sh /build
COPY src/Magick.Native/api.key.tx[t] /build

RUN build/install.dependencies.sh

