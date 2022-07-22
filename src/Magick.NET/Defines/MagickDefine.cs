// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick
{
    /// <summary>
    /// Class that implements <see cref="IDefine"/>.
    /// </summary>
    public sealed class MagickDefine : IDefine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(string name, string value)
            : this(MagickFormat.Unknown, name, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        public MagickDefine(MagickFormat format, string name, bool value)
            : this(format, name, value ? "true" : "false")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        public MagickDefine(MagickFormat format, string name, double value)
            : this(format, name, value.ToString(CultureInfo.InvariantCulture))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(MagickFormat format, string name, Enum value)
            : this(format, name, Enum.GetName(value.GetType(), value))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        public MagickDefine(MagickFormat format, string name, IMagickGeometry value)
            : this(format, name, value?.ToString()!)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickDefine"/> class.
        /// </summary>
        /// <param name="format">The format of the define.</param>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        public MagickDefine(MagickFormat format, string name, string value)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Throw.IfNullOrEmpty(nameof(value), value);

            Format = format;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the format to set the define for.
        /// </summary>
        public MagickFormat Format { get; }

        /// <summary>
        /// Gets the name of the define.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the define.
        /// </summary>
        public string Value { get; }
    }
}
