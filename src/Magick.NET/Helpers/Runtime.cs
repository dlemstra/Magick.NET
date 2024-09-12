// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;

namespace ImageMagick;

internal static class Runtime
{
    static Runtime()
    {
        Architecture = GetArchitecture();
        Is64Bit = Architecture is Architecture.X64 or Architecture.Arm64;
        IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public static bool Is64Bit { get; }

    public static Architecture Architecture { get; }

    public static bool IsWindows { get; }

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
