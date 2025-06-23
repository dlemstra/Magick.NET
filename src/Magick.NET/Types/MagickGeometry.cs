// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Globalization;

namespace ImageMagick;

/// <summary>
/// Encapsulation of the ImageMagick geometry object.
/// </summary>
public sealed partial class MagickGeometry : IMagickGeometry
{
    private uint _width;
    private uint _height;
    private int _x;
    private int _y;
    private GeometryFlags _flags;

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class.
    /// </summary>
    public MagickGeometry()
        => Initialize(0, 0, 0, 0, GeometryFlags.NoValue);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified width and height.
    /// </summary>
    /// <param name="widthAndHeight">The width and height.</param>
    public MagickGeometry(uint widthAndHeight)
        => Initialize(0, 0, widthAndHeight, widthAndHeight, GeometryFlags.WidthHeight);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified width and height.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MagickGeometry(uint width, uint height)
        => Initialize(0, 0, width, height, GeometryFlags.WidthHeight);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified offsets, width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public MagickGeometry(int x, int y, uint width, uint height)
        => Initialize(x, y, width, height, GeometryFlags.XYWidthHeight);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified width and height.
    /// </summary>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    public MagickGeometry(Percentage percentageWidth, Percentage percentageHeight)
        => InitializeFromPercentage(0, 0, percentageWidth, percentageHeight, GeometryFlags.WidthHeight);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified offsets, width and height.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="percentageWidth">The percentage of the width.</param>
    /// <param name="percentageHeight">The percentage of the height.</param>
    public MagickGeometry(int x, int y, Percentage percentageWidth, Percentage percentageHeight)
        => InitializeFromPercentage(x, y, percentageWidth, percentageHeight, GeometryFlags.XYWidthHeight);

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickGeometry"/> class using the specified geometry.
    /// </summary>
    /// <param name="value">Geometry specifications in the form: &lt;width&gt;x&lt;height&gt;
    /// {+-}&lt;xoffset&gt;{+-}&lt;yoffset&gt; (where width, height, xoffset, and yoffset are numbers).</param>
    public MagickGeometry(string value)
    {
        Throw.IfNullOrEmpty(value);

        using var instance = NativeMagickGeometry.Create();
        _flags = instance.Initialize(value);

        Throw.IfTrue(_flags == GeometryFlags.NoValue, nameof(value), "Invalid geometry specified.");

        _x = (int)instance.X_Get();
        _y = (int)instance.Y_Get();

        if (AspectRatio)
        {
            var ratio = value.Split(':');
            _width = ParseUInt(ratio[0]);
            _height = ParseUInt(ratio[1]);
        }
        else
        {
            _width = (uint)instance.Width_Get();
            _height = (uint)instance.Height_Get();
        }
    }

    /// <summary>
    /// Gets a value indicating whether the value is an aspect ratio.
    /// </summary>
    public bool AspectRatio
    {
        get => _flags.HasFlag(GeometryFlags.AspectRatio);
        internal set => _flags = value ? _flags | GeometryFlags.AspectRatio : _flags & ~GeometryFlags.AspectRatio;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized based on the smallest fitting dimension (^).
    /// </summary>
    public bool FillArea
    {
        get => _flags.HasFlag(GeometryFlags.FillArea);
        set => _flags = value ? _flags | GeometryFlags.FillArea : _flags & ~GeometryFlags.FillArea;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized if image is greater than size (&gt;).
    /// </summary>
    public bool Greater
    {
        get => _flags.HasFlag(GeometryFlags.Greater);
        set => _flags = value ? _flags | GeometryFlags.Greater : _flags & ~GeometryFlags.Greater;
    }

    /// <summary>
    /// Gets or sets the height of the geometry.
    /// </summary>
    public uint Height
    {
        get => _height;
        set
        {
            _height = value;
            _flags |= GeometryFlags.Height;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized without preserving aspect ratio (!).
    /// </summary>
    public bool IgnoreAspectRatio
    {
        get => _flags.HasFlag(GeometryFlags.IgnoreAspectRatio);
        set => _flags = value ? _flags | GeometryFlags.IgnoreAspectRatio : _flags & ~GeometryFlags.IgnoreAspectRatio;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the width and height are expressed as percentages.
    /// </summary>
    public bool IsPercentage
    {
        get => _flags.HasFlag(GeometryFlags.Percentage);
        set => _flags = value ? _flags | GeometryFlags.Percentage : _flags & ~GeometryFlags.Percentage;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized if the image is less than size (&lt;).
    /// </summary>
    public bool Less
    {
        get => _flags.HasFlag(GeometryFlags.Less);
        set => _flags = value ? _flags | GeometryFlags.Less : _flags & ~GeometryFlags.Less;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the image is resized using a pixel area count limit (@).
    /// </summary>
    public bool LimitPixels
    {
        get => _flags.HasFlag(GeometryFlags.LimitPixels);
        set => _flags = value ? _flags | GeometryFlags.LimitPixels : _flags & ~GeometryFlags.LimitPixels;
    }

    /// <summary>
    /// Gets or sets the width of the geometry.
    /// </summary>
    public uint Width
    {
        get => _width;
        set
        {
            _width = value;
            _flags |= GeometryFlags.Width;
        }
    }

    /// <summary>
    /// Gets or sets the X offset from origin.
    /// </summary>
    public int X
    {
        get => _x;
        set
        {
            _x = value;
            _flags |= GeometryFlags.X;
        }
    }

    /// <summary>
    /// Gets or sets the Y offset from origin.
    /// </summary>
    public int Y
    {
        get => _y;
        set
        {
            _y = value;
            _flags |= GeometryFlags.Y;
        }
    }

    /// <summary>
    /// Converts the specified string to an instance of this type.
    /// </summary>
    /// <param name="value">Geometry specifications in the form: &lt;width&gt;x&lt;height&gt;
    /// {+-}&lt;xoffset&gt;{+-}&lt;yoffset&gt; (where width, height, xoffset, and yoffset are numbers).</param>
    public static explicit operator MagickGeometry(string value)
        => new MagickGeometry(value);

    /// <summary>
    /// Determines whether the specified <see cref="MagickGeometry"/> instances are considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator ==(MagickGeometry left, MagickGeometry right)
        => Equals(left, right);

    /// <summary>
    /// Determines whether the specified <see cref="MagickGeometry"/> instances are not considered equal.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator !=(MagickGeometry left, MagickGeometry right)
        => !Equals(left, right);

    /// <summary>
    /// Determines whether the first <see cref="MagickGeometry"/>  is more than the second <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator >(MagickGeometry left, MagickGeometry right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) == 1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickGeometry"/> is less than the second <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator <(MagickGeometry left, MagickGeometry right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) == -1;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickGeometry"/> is more than or equal to the second <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator >=(MagickGeometry left, MagickGeometry right)
    {
        if (left is null)
            return right is null;

        return left.CompareTo(right) >= 0;
    }

    /// <summary>
    /// Determines whether the first <see cref="MagickGeometry"/> is less than or equal to the second <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="left">The first <see cref="MagickGeometry"/> to compare.</param>
    /// <param name="right">The second <see cref="MagickGeometry"/> to compare.</param>
    public static bool operator <=(MagickGeometry left, MagickGeometry right)
    {
        if (left is null)
            return right is not null;

        return left.CompareTo(right) <= 0;
    }

    /// <summary>
    /// Initializes a new <see cref="IMagickGeometry"/> instance using the specified page size.
    /// </summary>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A <see cref="IMagickGeometry"/> instance that represents the specified page size at 72 dpi.</returns>
    public static IMagickGeometry FromPageSize(string pageSize)
    {
        Throw.IfNullOrEmpty(pageSize);

        var rectangle = MagickRectangle.FromPageSize(pageSize);
        if (rectangle is null)
            throw new InvalidOperationException("Invalid page size specified.");

        return FromRectangle(rectangle);
    }

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare this geometry with.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public int CompareTo(IMagickGeometry? other)
    {
        if (other is null)
            return 1;

        var left = Width * Height;
        var right = other.Width * other.Height;

        if (left == right)
            return 0;

        return left < right ? -1 : 1;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="obj">The object to compare this <see cref="MagickGeometry"/> with.</param>
    /// <returns>True when the specified object is equal to the current <see cref="MagickGeometry"/>.</returns>
    public override bool Equals(object? obj)
        => Equals(obj as IMagickGeometry);

    /// <summary>
    /// Determines whether the specified <see cref="IMagickGeometry"/> is equal to the current <see cref="MagickGeometry"/>.
    /// </summary>
    /// <param name="other">The <see cref="IMagickGeometry"/> to compare this <see cref="MagickGeometry"/> with.</param>
    /// <returns>True when the specified <see cref="IMagickGeometry"/> is equal to the current <see cref="MagickGeometry"/>.</returns>
    public bool Equals(IMagickGeometry? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return
            Width == other.Width &&
            Height == other.Height &&
            X == other.X &&
            Y == other.Y &&
            AspectRatio == other.AspectRatio &&
            IsPercentage == other.IsPercentage &&
            IgnoreAspectRatio == other.IgnoreAspectRatio &&
            Less == other.Less &&
            Greater == other.Greater &&
            FillArea == other.FillArea &&
            LimitPixels == other.LimitPixels;
    }

    /// <summary>
    /// Serves as a hash of this type.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
        => Width.GetHashCode() ^
           Height.GetHashCode() ^
           X.GetHashCode() ^
           Y.GetHashCode() ^
           AspectRatio.GetHashCode() ^
           IsPercentage.GetHashCode() ^
           IgnoreAspectRatio.GetHashCode() ^
           Less.GetHashCode() ^
           Greater.GetHashCode() ^
           FillArea.GetHashCode() ^
           LimitPixels.GetHashCode();

    /// <summary>
    /// Initializes the geometry using the specified value.
    /// </summary>
    /// <param name="x">The X offset from origin.</param>
    /// <param name="y">The Y offset from origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void Initialize(int x, int y, uint width, uint height)
        => Initialize(x, y, width, height, GeometryFlags.XYWidthHeight);

    /// <summary>
    /// Returns a string that represents the current <see cref="IMagickGeometry"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="IMagickGeometry"/>.</returns>
    public override string ToString()
    {
        if (AspectRatio)
            return _width + ":" + _height;

        var result = string.Empty;

        if (_flags.HasFlag(GeometryFlags.Width) && _width != 0)
        {
            result += _width;
            if (IsPercentage)
                result += "%";
            else
                result += "x";
        }

        if (_flags.HasFlag(GeometryFlags.Height) && _height != 0)
        {
            if (result.Length == 0 || IsPercentage)
                result += "x";

            result += _height;
            if (IsPercentage)
                result += "%";
        }

        if (result.Length == 0 && _flags.HasFlag(GeometryFlags.WidthHeight))
            result = "0x0";

        if (_flags.HasFlag(GeometryFlags.X))
        {
            if (_x >= 0)
                result += "+";

            result += _x;
        }

        if (_flags.HasFlag(GeometryFlags.Y))
        {
            if (_y >= 0)
                result += "+";

            result += _y;
        }

        if (IgnoreAspectRatio)
            result += "!";

        if (Greater)
            result += ">";

        if (Less)
            result += "<";

        if (FillArea)
            result += "^";

        if (LimitPixels)
            result += "@";

        return result;
    }

    internal static IMagickGeometry? Clone(IMagickGeometry? value)
    {
        if (value is null)
            return null;

        return new MagickGeometry
        {
            AspectRatio = value.AspectRatio,
            FillArea = value.FillArea,
            Greater = value.Greater,
            Height = value.Height,
            IgnoreAspectRatio = value.IgnoreAspectRatio,
            IsPercentage = value.IsPercentage,
            Less = value.Less,
            LimitPixels = value.LimitPixels,
            Width = value.Width,
            X = value.X,
            Y = value.Y,
        };
    }

    internal static IMagickGeometry FromRectangle(MagickRectangle rectangle)
        => new MagickGeometry(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    internal static IMagickGeometry? FromString(string? value)
        => value is null ? null : new MagickGeometry(value);

    private static uint ParseUInt(string value)
    {
        var index = 0;
        while (index < value.Length && !char.IsNumber(value[index]))
            index++;

        var start = index;
        while (index < value.Length && char.IsNumber(value[index]))
            index++;

        return uint.Parse(value.Substring(start, index - start), CultureInfo.InvariantCulture);
    }

    private void Initialize(int x, int y, uint width, uint height, GeometryFlags flags)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _flags = flags;
    }

    private void InitializeFromPercentage(int x, int y, Percentage percentageWidth, Percentage percentageHeight, GeometryFlags flags)
    {
        Throw.IfNegative(percentageWidth);
        Throw.IfNegative(percentageHeight);

        Initialize(x, y, (uint)percentageWidth, (uint)percentageHeight, flags | GeometryFlags.Percentage);
    }
}
