// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.SourceGenerator;

internal sealed class CleanupInfo
{
    public CleanupInfo(string? name, string? arguments)
    {
        Name = name is null ? "UnknownName" : name;
        Arguments = arguments is null ? "UnknownArguments" : arguments;
    }

    public string Name { get; }

    public string Arguments { get; }
}
