COPY Magick.NET-Q8-x64.Native.dll.so ./Magick.NET-Q8-x64.Native.dll.so
COPY Magick.NET-Q16-x64.Native.dll.so ./Magick.NET-Q16-x64.Native.dll.so
COPY Magick.NET-Q16-HDRI-x64.Native.dll.so ./Magick.NET-Q16-HDRI-x64.Native.dll.so
COPY RunLdd.sh ./RunLdd.sh
RUN  chmod +x RunLdd.sh && ./RunLdd.sh