// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.InteropServices;

namespace Magick.NET.Tests;

internal static class TestRuntime
{
    public static bool HasFlakyLinuxArm64Result
        => IsLinux && IsArm64;

    public static bool HasFlakyMacOSResult
        => IsMacOS;

    public static bool HasFlakyMacOSArm64Result
        => IsMacOS && IsArm64;

    private static bool IsArm64 { get; } = RuntimeInformation.ProcessArchitecture == Architecture.Arm64;

    private static bool IsLinux { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    private static bool IsMacOS { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
}
