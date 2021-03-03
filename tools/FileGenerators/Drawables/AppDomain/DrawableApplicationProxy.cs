// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace FileGenerator.Drawables
{
    internal sealed class DrawableApplicationProxy : ApplicationProxy
    {
        public void GenerateIDrawables()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            IDrawablesGenerator.Generate();
        }

        public void GenerateDrawables()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            DrawablesGenerator.Generate();
        }

        public void GenerateIPaths()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            IPathsGenerator.Generate();
        }

        public void GeneratePaths()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

            PathsGenerator.Generate();
        }
    }
}
