// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace FileGenerator.Drawables
{
    [Serializable]
    internal static class DrawableAppDomainHelper
    {
        private static void GenerateIDrawables()
        {
            AppDomain domain = AppDomainHelper.CreateDomain();
            DrawableApplicationProxy proxy = AppDomainHelper.CreateProxy<DrawableApplicationProxy>(domain);

            proxy.GenerateIDrawables();

            AppDomain.Unload(domain);
        }

        private static void GenerateDrawables()
        {
            AppDomain domain = AppDomainHelper.CreateDomain();
            DrawableApplicationProxy proxy = AppDomainHelper.CreateProxy<DrawableApplicationProxy>(domain);

            proxy.GenerateDrawables();

            AppDomain.Unload(domain);
        }

        private static void GenerateIPaths()
        {
            AppDomain domain = AppDomainHelper.CreateDomain();
            DrawableApplicationProxy proxy = AppDomainHelper.CreateProxy<DrawableApplicationProxy>(domain);

            proxy.GenerateIPaths();

            AppDomain.Unload(domain);
        }

        private static void GeneratePaths()
        {
            AppDomain domain = AppDomainHelper.CreateDomain();
            DrawableApplicationProxy proxy = AppDomainHelper.CreateProxy<DrawableApplicationProxy>(domain);

            proxy.GeneratePaths();

            AppDomain.Unload(domain);
        }

        public static void Execute()
        {
            GenerateIDrawables();
            GenerateDrawables();
            GenerateIPaths();
            GeneratePaths();
        }
    }
}
