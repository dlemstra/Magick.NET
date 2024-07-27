// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace ImageMagick;

/// <summary>
/// Class that can be used to access an 8bim profile.
/// </summary>
public sealed class EightBimProfile : ImageProfile, IEightBimProfile
{
    private static readonly short ExifProfileId = 1058;
    private static readonly short IptcProfileId = 1028;
    private static readonly short XmpProfileId = 1060;

    private readonly uint _height;
    private readonly uint _width;

    private Collection<IEightBimValue>? _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
    /// </summary>
    /// <param name="data">The byte array to read the 8bim profile from.</param>
    public EightBimProfile(byte[] data)
      : base("8bim", data)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the 8bim profile file, or the relative
    /// 8bim profile file name.</param>
    public EightBimProfile(string fileName)
      : base("8bim", fileName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
    /// </summary>
    /// <param name="stream">The stream to read the 8bim profile from.</param>
    public EightBimProfile(Stream stream)
      : base("8bim", stream)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
    /// </summary>
    /// <param name="image">The image that contains the profile.</param>
    /// <param name="data">The byte array to read the 8bim profile from.</param>
    public EightBimProfile(IMagickImage image, byte[] data)
      : base("8bim", data)
    {
        Throw.IfNull(nameof(image), image);

        _width = image.Width;
        _height = image.Height;
    }

    /// <summary>
    /// Gets the clipping paths this image contains.
    /// </summary>
    public IReadOnlyList<IClipPath> ClipPaths
    {
        get
        {
            Initialize();

            var clipPaths = new List<IClipPath>(_values.Count);
            foreach (var value in _values)
            {
                var clipPath = CreateClipPath(value);
                if (clipPath is not null)
                    clipPaths.Add(clipPath);
            }

            return clipPaths;
        }
    }

    /// <summary>
    /// Gets the values of this 8bim profile.
    /// </summary>
    public IReadOnlyList<IEightBimValue> Values
    {
        get
        {
            Initialize();

            return _values;
        }
    }

    /// <summary>
    /// Gets the exif profile inside the 8bim profile.
    /// </summary>
    /// <returns>The exif profile.</returns>
    public IExifProfile? GetExifProfile()
    {
        Initialize();

        var value = FindValue(ExifProfileId);
        if (value is null)
            return null;

        return new ExifProfile(value.ToByteArray());
    }

    /// <summary>
    /// Gets the iptc profile inside the 8bim profile.
    /// </summary>
    /// <returns>The iptc profile.</returns>
    public IIptcProfile? GetIptcProfile()
    {
        Initialize();

        var value = FindValue(IptcProfileId);
        if (value is null)
            return null;

        return new IptcProfile(value.ToByteArray());
    }

    /// <summary>
    /// Gets or sets the xmp profile inside the 8bim profile.
    /// </summary>
    /// <returns>The xmp profile.</returns>
    public IXmpProfile? GetXmpProfile()
    {
        Initialize();

        var value = FindValue(XmpProfileId);
        if (value is null)
            return null;

        return new XmpProfile(value.ToByteArray());
    }

    /// <summary>
    /// Sets the exif profile inside the 8bim profile.
    /// </summary>
    /// <param name="profile">The exif profile.</param>
    public void SetExifProfile(IExifProfile? profile)
    {
        Initialize();

        var currentValue = FindValue(ExifProfileId);

        if (currentValue is not null)
            _values.Remove(currentValue);

        SetData(null);

        if (profile is null)
            return;

        var data = profile.ToByteArray();
        if (data is not null)
            _values.Add(new EightBimValue(ExifProfileId, null, data));
    }

    /// <summary>
    /// Sets the iptc profile inside the 8bim profile.
    /// </summary>
    /// <param name="profile">The iptc profile.</param>
    public void SetIptcProfile(IIptcProfile? profile)
    {
        Initialize();

        var currentValue = FindValue(IptcProfileId);

        if (currentValue is not null)
            _values.Remove(currentValue);

        SetData(null);

        if (profile is null)
            return;

        var data = profile.ToByteArray();
        if (data is not null)
            _values.Add(new EightBimValue(IptcProfileId, null, data));
    }

    /// <summary>
    /// Sets the xmp profile inside the 8bim profile.
    /// </summary>
    /// <param name="profile">The xmp profile.</param>
    public void SetXmpProfile(IXmpProfile? profile)
    {
        Initialize();

        var currentValue = FindValue(XmpProfileId);

        if (currentValue is not null)
            _values.Remove(currentValue);

        SetData(null);

        if (profile is null)
            return;

        var data = profile.ToByteArray();
        if (data is not null)
            _values.Add(new EightBimValue(XmpProfileId, null, data));
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected override void UpdateData()
    {
        var data = GetDataProtected();
        if (data is not null)
            return;

        if (_values is null)
            return;

        data = EightBimWriter.Write(_values);
        SetData(data);
    }

    private ClipPath? CreateClipPath(IEightBimValue value)
    {
        if (value.Name is null)
            return null;

        var d = GetClipPath(value.ToByteArray());
        if (string.IsNullOrEmpty(d))
            return null;

        var doc = XmlHelper.CreateDocument();
        doc.CreateXmlDeclaration("1.0", "iso-8859-1", null);

        var svg = XmlHelper.CreateElement(doc, "svg");
        XmlHelper.SetAttribute(svg, "width", _width);
        XmlHelper.SetAttribute(svg, "height", _height);

        var g = XmlHelper.CreateElement(svg, "g");

        var path = XmlHelper.CreateElement(g, "path");
        XmlHelper.SetAttribute(path, "fill", "#00000000");
        XmlHelper.SetAttribute(path, "stroke", "#00000000");
        XmlHelper.SetAttribute(path, "stroke-width", "0");
        XmlHelper.SetAttribute(path, "stroke-antialiasing", "false");
        XmlHelper.SetAttribute(path, "d", d);

        return new ClipPath(value.Name, doc.CreateNavigator());
    }

    private IEightBimValue? FindValue(int id)
    {
        return _values
            .Where(value => value.Id == id)
            .FirstOrDefault();
    }

    private string? GetClipPath(byte[] data)
    {
        if (_width == 0 || _height == 0)
            return null;

        return ClipPathReader.Read(_width, _height, data);
    }

    [MemberNotNull(nameof(_values))]
    private void Initialize()
    {
        if (_values is not null)
            return;

        _values = EightBimReader.Read(GetDataProtected());
    }
}
