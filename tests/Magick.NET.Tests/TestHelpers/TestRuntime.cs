// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.InteropServices;
using ImageMagick;

namespace Magick.NET.Tests;

internal static class TestRuntime
{
    static TestRuntime()
    {
        var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        var isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        IsLinuxArm64 = isLinux && Runtime.Architecture == Architecture.Arm64;
        IsMacOSArm64 = isMacOS && Runtime.Architecture == Architecture.Arm64;
    }

    public static bool IsLinuxArm64 { get; }

    public static bool IsMacOSArm64 { get; }
}
