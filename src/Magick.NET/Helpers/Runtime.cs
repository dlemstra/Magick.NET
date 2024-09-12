// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick;

internal static partial class Runtime
{
    public static bool Is64Bit { get; } = Architecture is Architecture.X64 or Architecture.Arm64;

    public static Architecture Architecture { get; } = GetArchitecture();

    public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    private static Architecture GetArchitecture()
    {
        var processArchitecture = RuntimeInformation.ProcessArchitecture;

        return processArchitecture switch
        {
            Architecture.X64 or Architecture.Arm64 or Architecture.X86 => processArchitecture,
            _ => throw new NotSupportedException($"{processArchitecture} is an unsupported architecture, only {nameof(Architecture.X64)}, {nameof(Architecture.Arm64)} and {nameof(Architecture.X86)} are supported."),
        };
    }
}
