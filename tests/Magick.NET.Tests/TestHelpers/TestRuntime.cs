// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.InteropServices;
using ImageMagick;

namespace Magick.NET.Tests;

internal static class TestRuntime
{
    public static bool HasFlakyLinuxArm64Result { get; } = IsLinux && Runtime.Architecture is Architecture.Arm64;

    public static bool HasFlakyMacOSResult { get; } = IsMacOS;

    public static bool HasFlakyMacOSArm64Result { get; } = IsMacOS && Runtime.Architecture is Architecture.Arm64;

    private static bool IsLinux { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    private static bool IsMacOS { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}
