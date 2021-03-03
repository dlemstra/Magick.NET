// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal sealed class ExifTagDescriptionAttribute : Attribute
    {
        public ExifTagDescriptionAttribute(object value, string description)
        {
            Value = value;
            Description = description;
        }

        public object Value { get; }

        public string Description { get; }
    }
}
