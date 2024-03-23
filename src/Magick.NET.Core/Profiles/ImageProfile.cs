// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick;

/// <summary>
/// Class that contains an image profile.
/// </summary>
public partial class ImageProfile : IImageProfile
{
    private byte[]? _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <param name="data">A byte array containing the profile.</param>
    public ImageProfile(string name, byte[] data)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNull(nameof(data), data);

        Name = name;
        _data = Copy(data);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <param name="stream">A stream containing the profile.</param>
    public ImageProfile(string name, Stream stream)
    {
        Throw.IfNullOrEmpty(nameof(name), name);

        Name = name;

        var bytes = Bytes.Create(stream, allowEmptyStream: true);
        _data = bytes.GetData();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public ImageProfile(string name, string fileName)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Throw.IfNullOrEmpty(nameof(fileName), fileName);

        Name = name;

        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        _data = File.ReadAllBytes(filePath);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    protected ImageProfile(string name)
    {
        Throw.IfNullOrEmpty(nameof(name), name);
        Name = name;
    }

    /// <summary>
    /// Gets the name of the profile.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="ImageProfile"/>.
    /// </summary>
    /// <param name="obj">The object to compare this <see cref="ImageProfile"/> with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="ImageProfile"/>.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as IImageProfile);

    /// <summary>
    /// Determines whether the specified image compare is equal to the current <see cref="ImageProfile"/>.
    /// </summary>
    /// <param name="other">The image profile to compare this <see cref="ImageProfile"/> with.</param>
    /// <returns>True when the specified image compare is equal to the current <see cref="ImageProfile"/>.</returns>
    public bool Equals(IImageProfile? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Name != other.Name)
            return false;

        UpdateData();

        var data = other.ToByteArray();
        if (data is null || data.Length == 0)
            return _data is null || _data.Length == 0;

        if (_data?.Length != data.Length)
            return false;

        for (var i = 0; i < _data.Length; i++)
        {
            if (_data[i] != data[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Returns the <see cref="byte"/> array of this profile.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
#if NETSTANDARD2_1
    [Obsolete($"This property will be removed in the next major release, use {nameof(ToByteArray)} or {nameof(ToReadOnlySpan)} instead.")]
#else
    [Obsolete($"This property will be removed in the next major release, use {nameof(ToByteArray)} instead.")]
#endif
    public byte[]? GetData() // When removed GetDataProtected should be renamed to GetData
        => _data;

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        if (_data is null)
            return Name.GetHashCode();

        return _data.GetHashCode() ^ Name.GetHashCode();
    }

    /// <summary>
    /// Converts this instance to a <see cref="byte"/> array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    public byte[]? ToByteArray()
    {
        UpdateData();
        return Copy(_data);
    }

    /// <summary>
    /// Returns the <see cref="byte"/> array of this profile.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    protected byte[]? GetDataProtected()
        => _data;

    /// <summary>
    /// Sets the data of the profile.
    /// </summary>
    /// <param name="data">The new data of the profile.</param>
    protected void SetData(byte[]? data)
        => _data = data;

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected virtual void UpdateData()
    {
    }

    private static byte[] Copy(byte[]? data)
    {
        if (data is null || data.Length == 0)
            return Array.Empty<byte>();

        var result = new byte[data.Length];
        data.CopyTo(result, 0);
        return result;
    }
}
