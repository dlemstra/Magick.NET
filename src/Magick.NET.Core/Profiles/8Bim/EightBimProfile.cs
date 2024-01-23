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
    private static readonly short IptcProfileId = 1028;

    private readonly int _height;
    private readonly int _width;

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
    public IReadOnlyCollection<IClipPath> ClipPaths
    {
        get
        {
            Initialize();

            var clipPaths = new List<IClipPath>();
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
    /// Gets or sets the iptc profile inside the 8bim profile.
    /// </summary>
    public IIptcProfile? IptcProfile
    {
        get
        {
            Initialize();

            var value = FindValue(IptcProfileId);
            if (value is null)
                return null;

            return new IptcProfile(value.ToByteArray());
        }

        set
        {
            Initialize();

            var currentValue = FindValue(IptcProfileId);

            if (currentValue is not null)
                _values.Remove(currentValue);

            SetData(null);

            if (value is null)
                return;

            var data = value.ToByteArray();
            if (data is not null)
                _values.Add(new EightBimValue(IptcProfileId, null, data));
        }
    }

    /// <summary>
    /// Gets the values of this 8bim profile.
    /// </summary>
    public IReadOnlyCollection<IEightBimValue> Values
    {
        get
        {
            Initialize();

            return _values;
        }
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected override void UpdateData()
    {
        var data = GetData();
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

        _values = EightBimReader.Read(GetData());
    }
}
