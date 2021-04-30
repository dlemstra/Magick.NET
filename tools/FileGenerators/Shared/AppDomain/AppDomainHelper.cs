// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Reflection;

namespace FileGenerator
{
    public static class AppDomainHelper
    {
        public static AppDomain CreateDomain()
            => AppDomain.CreateDomain("AppDomainHelper");

        public static TProxy CreateProxy<TProxy>(AppDomain domain)
          where TProxy : ApplicationProxy
        {
            var activator = typeof(TProxy);

            var assembly = Assembly.GetAssembly(activator);
            if (assembly == null || assembly.FullName == null)
                throw new InvalidOperationException();

            var result = domain.CreateInstanceAndUnwrap(assembly.FullName, activator.ToString()) as TProxy;
            if (result == null)
                throw new InvalidOperationException();

            return result;
        }
    }
}
