// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;

namespace ImageMagick;

/// <summary>
/// A value of the 8bim profile.
/// </summary>
public sealed class EightBimValue : IEightBimValue
{
    private readonly byte[] _data;

    internal EightBimValue(short id, byte[] data)
    {
        Id = id;
        _data = data;
    }

    /// <summary>
    /// Gets the ID of the 8bim value.
    /// </summary>
    public short Id { get; }

    /// <summary>
    /// Gets the id of the 8bim value.
    /// </summary>
    [Obsolete($"This property will be removed in the next major release, use {nameof(Id)} instead.")]
    public short ID
       => Id;

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="EightBimValue"/>.
    /// </summary>
    /// <param name="obj">The object to compare this 8bim value with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="EightBimValue"/>.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as IEightBimValue);

    /// <summary>
    /// Determines whether the specified <see cref="EightBimValue"/> is equal to the current <see cref="EightBimValue"/>.
    /// </summary>
    /// <param name="other">The <see cref="EightBimValue"/> to compare this <see cref="EightBimValue"/> with.</param>
    /// <returns>True when the specified <see cref="EightBimValue"/> is equal to the current <see cref="EightBimValue"/>.</returns>
    public bool Equals(IEightBimValue? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id != other.Id)
            return false;

        var data = other.ToByteArray();

        if (_data.Length != data.Length)
            return false;

        for (var i = 0; i < _data.Length; i++)
        {
            if (_data[i] != data[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
        => _data.GetHashCode() ^ Id.GetHashCode();

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    /// <returns>A <see cref="byte"/> array.</returns>
    public byte[] ToByteArray()
    {
        var data = new byte[_data.Length];
        Array.Copy(_data, 0, data, 0, _data.Length);
        return data;
    }

    /// <summary>
    /// Returns a string that represents the current value.
    /// </summary>
    /// <returns>A string that represents the current value.</returns>
    public override string ToString()
        => ToString(Encoding.UTF8);

    /// <summary>
    /// Returns a string that represents the current value with the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    /// <returns>A string that represents the current value with the specified encoding.</returns>
    public string ToString(Encoding encoding)
    {
        Throw.IfNull(nameof(encoding), encoding);

        return encoding.GetString(_data);
    }
}
