// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.InteropServices;
using ImageMagick;

namespace Magick.NET.Tests;

internal static class TestRuntime
{
    static TestRuntime()
    {
        HasFlakyLinuxArm64Result = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && Runtime.Architecture is Architecture.Arm64;
        HasFlakyMacOSResult = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        HasFlakyMacOSArm64Result = HasFlakyMacOSResult && Runtime.Architecture == Architecture.Arm64;
    }

    public static bool HasFlakyLinuxArm64Result { get; }

    public static bool HasFlakyMacOSResult { get; }

    public static bool HasFlakyMacOSArm64Result { get; }

    private static bool IsMacOS { get; }
}
