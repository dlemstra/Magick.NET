// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace FileGenerator.Drawables
{
    internal sealed class Program
    {
        internal static void Main()
        {
            IDrawablesGenerator.Generate();
            DrawablesGenerator.Generate();

            IPathsGenerator.Generate();
            PathsGenerator.Generate();
        }
    }
}
