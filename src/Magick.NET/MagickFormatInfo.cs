// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that contains information about an image format.
/// </summary>
public sealed partial class MagickFormatInfo : IMagickFormatInfo
{
    private static readonly Dictionary<MagickFormat, IMagickFormatInfo> _all = LoadFormats();

    private MagickFormatInfo(NativeMagickFormatInfo instance)
    {
        Format = GetFormat(instance.Format_Get());
        Description = instance.Description_Get();
        CanReadMultithreaded = instance.CanReadMultithreaded_Get();
        CanWriteMultithreaded = instance.CanWriteMultithreaded_Get();
        SupportsMultipleFrames = instance.SupportsMultipleFrames_Get();
        SupportsReading = instance.SupportsReading_Get();
        SupportsWriting = instance.SupportsWriting_Get();
        MimeType = instance.MimeType_Get();
        ModuleFormat = GetFormat(instance.Module_Get());
    }

    /// <summary>
    /// Gets a value indicating whether the format can be read multithreaded.
    /// </summary>
    public bool CanReadMultithreaded { get; }

    /// <summary>
    /// Gets a value indicating whether the format can be written multithreaded.
    /// </summary>
    public bool CanWriteMultithreaded { get; }

    /// <summary>
    /// Gets the description of the format.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the format.
    /// </summary>
    public MagickFormat Format { get; }

    /// <summary>
    /// Gets the mime type.
    /// </summary>
    public string? MimeType { get; }

    /// <summary>
    /// Gets the module.
    /// </summary>
    public MagickFormat ModuleFormat { get; }

    /// <summary>
    /// Gets a value indicating whether the format supports multiple frames.
    /// </summary>
    public bool SupportsMultipleFrames { get; }

    /// <summary>
    /// Gets a value indicating whether the format can be read.
    /// </summary>
    public bool SupportsReading { get; }

    /// <summary>
    /// Gets a value indicating whether the format can be written.
    /// </summary>
    public bool SupportsWriting { get; }

    internal static IReadOnlyCollection<IMagickFormatInfo> All
        => _all.Values;

    /// <summary>
    /// Returns the format information. The extension of the supplied file is used to determine
    /// the format.
    /// </summary>
    /// <param name="file">The file to check.</param>
    /// <returns>The format information.</returns>
    public static IMagickFormatInfo? Create(FileInfo file)
    {
        Throw.IfNull(nameof(file), file);

        var format = EnumHelper.ParseMagickFormatFromExtension(file);

        if (format == MagickFormat.Unknown)
            return null;

        return Create(format);
    }

    /// <summary>
    /// Returns the format information of the specified format.
    /// </summary>
    /// <param name="format">The image format.</param>
    /// <returns>The format information.</returns>
    public static IMagickFormatInfo? Create(MagickFormat format)
    {
        if (!_all.ContainsKey(format))
            return null;

        return _all[format];
    }

    /// <summary>
    /// Returns the format information. The header of the image in the array of bytes is used to
    /// determine the format. Null will be returned when the format could not be determined.
    /// </summary>
    /// <param name="data">The array of bytes to read the image header from.</param>
    /// <returns>The format information.</returns>
    public static IMagickFormatInfo? Create(byte[] data)
    {
        Throw.IfNullOrEmpty(nameof(data), data);

        var instance = new NativeMagickFormatInfo();
        instance.GetInfoWithBlob(data, (uint)data.Length);

        return Create(instance);
    }

    /// <summary>
    /// Returns the format information. The extension of the supplied file name is used to
    /// determine the format.
    /// </summary>
    /// <param name="fileName">The name of the file to check.</param>
    /// <returns>The format information.</returns>
    public static IMagickFormatInfo? Create(string fileName)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);

        return Create(new FileInfo(filePath));
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="MagickFormatInfo"/>.
    /// </summary>
    /// <param name="obj">The object to compare this <see cref="MagickFormatInfo"/> with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="MagickFormatInfo"/>.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as IMagickFormatInfo);

    /// <summary>
    /// Determines whether the specified <see cref="IMagickFormatInfo"/> is equal to the current <see cref="MagickFormatInfo"/>.
    /// </summary>
    /// <param name="other">The <see cref="IMagickFormatInfo"/> to compare this <see cref="MagickFormatInfo"/> with.</param>
    /// <returns>True when the specified <see cref="IMagickFormatInfo"/> is equal to the current <see cref="MagickFormatInfo"/>.</returns>
    public bool Equals(IMagickFormatInfo? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Format == other.Format;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
        => ModuleFormat.GetHashCode();

    /// <summary>
    /// Returns a string that represents the current format.
    /// </summary>
    /// <returns>A string that represents the current format.</returns>
    public override string ToString()
        => string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2}R{3}W{4}M)", Format, Description, SupportsReading ? "+" : "-", SupportsWriting ? "+" : "-", SupportsMultipleFrames ? "+" : "-");

    /// <summary>
    /// Unregisters this format.
    /// </summary>
    /// <returns>True when the format was found and unregistered.</returns>
    public bool Unregister()
        => NativeMagickFormatInfo.Unregister(Enum.GetName(Format.GetType(), Format));

    private static MagickFormatInfo? Create(NativeMagickFormatInfo instance)
    {
        if (!instance.HasInstance)
            return null;

        return new MagickFormatInfo(instance);
    }

    private static MagickFormatInfo? Create(NativeMagickFormatInfo instance, string name)
    {
        instance.GetInfoByName(name);
        return Create(instance);
    }

    private static MagickFormat GetFormat(string? format)
    {
        if (format is null)
            return MagickFormat.Unknown;

        format = format.Replace("-", string.Empty);
        if (format == "3FR")
            format = "ThreeFr";
        else if (format == "3G2")
            format = "ThreeG2";
        else if (format == "3GP")
            format = "ThreeGp";

        return EnumHelper.Parse(format, MagickFormat.Unknown);
    }

    private static Dictionary<MagickFormat, IMagickFormatInfo> LoadFormats()
    {
        var formats = new Dictionary<MagickFormat, IMagickFormatInfo>();

        var list = IntPtr.Zero;
        nuint length = 0;
        try
        {
            list = NativeMagickFormatInfo.CreateList(out length);

            var instance = new NativeMagickFormatInfo();

            var ptr = list;
            for (var i = 0U; i < length; i++)
            {
                instance.GetInfo(list, i);

                var formatInfo = Create(instance);
                if (formatInfo is not null)
                    formats[formatInfo.Format] = formatInfo;

                ptr = new IntPtr(ptr.ToInt64() + i);
            }

            AddStealthCoders(instance, formats);
        }
        finally
        {
            if (list != IntPtr.Zero)
                NativeMagickFormatInfo.DisposeList(list, length);
        }

        return formats;
    }

    private static void AddStealthCoders(NativeMagickFormatInfo instance, Dictionary<MagickFormat, IMagickFormatInfo> formats)
    {
        var formatInfo = Create(instance, "DIB");
        if (formatInfo is not null)
            formats[formatInfo.Format] = formatInfo;

        formatInfo = Create(instance, "TIF");
        if (formatInfo is not null)
            formats[formatInfo.Format] = formatInfo;
    }
}
