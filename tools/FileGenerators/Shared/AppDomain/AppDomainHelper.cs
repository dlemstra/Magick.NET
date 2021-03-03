// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Reflection;

namespace FileGenerator
{
    public static class AppDomainHelper
    {
        public static AppDomain CreateDomain()
        {
            return AppDomain.CreateDomain("AppDomainHelper", null, new AppDomainSetup()
            {
                ApplicationName = "AppDomainHelper"
            });
        }

        public static TProxy CreateProxy<TProxy>(AppDomain domain)
          where TProxy : ApplicationProxy
        {
            Type activator = typeof(TProxy);
            TProxy proxy = domain.CreateInstanceAndUnwrap(
                  Assembly.GetAssembly(activator).FullName,
                  activator.ToString()) as TProxy;
            return proxy;
        }
    }
}
