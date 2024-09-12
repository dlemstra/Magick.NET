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

        HasFlakyLinuxArm64Result = isLinux && Runtime.Architecture == Architecture.Arm64;
        HasFlakyMacOSResult = isMacOS;
        HasFlakyMacOSArm64Result = isMacOS && Runtime.Architecture == Architecture.Arm64;
    }

    public static bool HasFlakyLinuxArm64Result { get; }

    public static bool HasFlakyMacOSResult { get; }

    public static bool HasFlakyMacOSArm64Result { get; }
}
