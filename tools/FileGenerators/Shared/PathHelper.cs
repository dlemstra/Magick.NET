// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace FileGenerator
{
    public static class PathHelper
    {
        public static string GetFullPath(string path)
        {
            return Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\..\" + path);
        }
    }
}
