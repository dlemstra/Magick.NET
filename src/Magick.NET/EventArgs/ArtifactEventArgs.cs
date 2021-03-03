// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal sealed class ArtifactEventArgs : EventArgs
    {
        internal ArtifactEventArgs(string key, string? value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string? Value { get; }
    }
}