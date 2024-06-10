// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.SourceGenerator;

internal sealed class CleanupInfo
{
    public CleanupInfo(string? name, string? arguments)
    {
        Name = name is null ? "UnknownName" : name
            .Replace("nameof(", string.Empty)
            .Replace(")", string.Empty);

        Arguments = arguments is null ? "UnknownArguments" : arguments
            .Replace("\"", string.Empty);
    }

    public string Name { get; }

    public string Arguments { get; }
}
