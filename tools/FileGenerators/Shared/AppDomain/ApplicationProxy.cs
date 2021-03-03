// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Reflection;

namespace FileGenerator
{
    public abstract class ApplicationProxy : MarshalByRefObject
    {
        protected Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }
    }
}
