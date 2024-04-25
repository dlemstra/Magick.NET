// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class ResourceLimits
{
    [NativeInterop]
    private partial class NativeResourceLimits
    {
        public static partial ulong Area_Get();

        public static partial void Area_Set(ulong value);

        public static partial ulong Disk_Get();

        public static partial void Disk_Set(ulong value);

        public static partial ulong Height_Get();

        public static partial void Height_Set(ulong value);

        public static partial ulong ListLength_Get();

        public static partial void ListLength_Set(ulong value);

        public static partial ulong MaxMemoryRequest_Get();

        [Throws]
        public static partial void MaxMemoryRequest_Set(ulong value);

        public static partial ulong MaxProfileSize_Get();

        [Throws]
        public static partial void MaxProfileSize_Set(ulong value);

        public static partial ulong Memory_Get();

        public static partial void Memory_Set(ulong value);

        public static partial ulong Thread_Get();

        public static partial void Thread_Set(ulong value);

        public static partial ulong Throttle_Get();

        public static partial void Throttle_Set(ulong value);

        public static partial ulong Time_Get();

        public static partial void Time_Set(ulong value);

        public static partial ulong Width_Get();

        public static partial void Width_Set(ulong value);

        public static partial void LimitMemory(double percentage);
    }
}
