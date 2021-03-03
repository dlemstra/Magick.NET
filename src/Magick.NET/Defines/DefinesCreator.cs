// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick.Defines
{
    /// <summary>
    /// Base class that can create defines.
    /// </summary>
    public abstract class DefinesCreator : IDefines
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinesCreator"/> class.
        /// </summary>
        /// <param name="format">The format where the defines are for.</param>
        protected DefinesCreator(MagickFormat format)
            => Format = format;

        /// <summary>
        /// Gets the defines that should be set as a define on an image.
        /// </summary>
        public abstract IEnumerable<IDefine> Defines { get; }

        /// <summary>
        /// Gets the format where the defines are for.
        /// </summary>
        protected MagickFormat Format { get; }

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, bool value)
            => new MagickDefine(Format, name, value ? "true" : "false");

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, double value)
            => new MagickDefine(Format, name, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, int value)
            => new MagickDefine(Format, name, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, long value)
            => new MagickDefine(Format, name, value.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, IMagickGeometry value)
            => new MagickDefine(Format, name, value?.ToString());

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine(string name, string value)
            => new MagickDefine(Format, name, value);

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine CreateDefine<TEnum>(string name, TEnum value)
          where TEnum : struct
            => new MagickDefine(Format, name, Enum.GetName(typeof(TEnum), value));

        /// <summary>
        /// Create a define with the specified name and value.
        /// </summary>
        /// <param name="name">The name of the define.</param>
        /// <param name="value">The value of the define.</param>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <returns>A <see cref="MagickDefine"/> instance.</returns>
        protected MagickDefine? CreateDefine<T>(string name, IEnumerable<T>? value)
        {
            if (value == null)
                return null;

            var values = new List<string>();
            foreach (T val in value)
            {
                if (val != null)
                    values.Add(val.ToString());
            }

            if (values.Count == 0)
                return null;

            return new MagickDefine(Format, name, string.Join(",", values.ToArray()));
        }
    }
}