// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace FileGenerator.Native;

internal sealed class MagickType
{
    private string _type = string.Empty;
    private bool _isEnum;

    public MagickType(string type)
    {
        var typName = type;
        if (string.IsNullOrEmpty(type))
        {
            typName = "void";
        }
        else if (typName.EndsWith("?"))
        {
            typName = typName.Substring(0, typName.Length - 1);
            IsNullable = true;
        }

        _type = typName;
        _isEnum = File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET.Core\Enums\" + typName + ".cs"));
        _isEnum = _isEnum || File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET\Enums\" + typName + ".cs"));
    }

    public bool HasInstance
    {
        get
        {
            if (IsDelegate)
                return false;

            return _type switch
            {
                "byte" or
                "byte[]" or
                "bool" or
                "double" or
                "double[]" or
                "Instance" or
                "long" or
                "QuantumType" or
                "QuantumType[]" or
                "size_t" or
                "ssize_t" or
                "string" or
                "ulong" or
                "void*" => false,
                _ => !_isEnum,
            };
        }
    }

    public bool IsFixed
        => _type.EndsWith("[]") || IsSpan;

    public bool IsSpan
        => _type.StartsWith("ReadOnlySpan<");

    public bool IsBool
        => ManagedName == "bool";

    public bool IsChannels
        => ManagedName == "Channels";

    public bool IsDelegate
        => _type.EndsWith("Delegate");

    public bool IsInstance
        => _type == "Instance";

    public bool IsNativeString
        => _type == "nativeString";

    public bool IsQuantumType
        => _type == "QuantumType" || _type == "QuantumType[]";

    public bool IsString
        => _type == "string";

    public bool IsVoid
        => _type == "void" || _type == "voidInstance";

    public bool IsNullable { get; }

    public string ManagedName
        => GetManagedName(_type);

    public string FixedName
        => ManagedName
            .Replace("[]", string.Empty)
            .Replace("ReadOnlySpan<", string.Empty)
            .Replace(">", string.Empty)
            + "*";

    public string ManagedTypeCast
    {
        get
        {
            if (NeedsTypeCast)
                return "(" + ManagedName + ")";

            return string.Empty;
        }
    }

    public string NativeName
        => GetNativeName(_type);

    public string NativeTypeCast
    {
        get
        {
            if (!NeedsTypeCast)
                return string.Empty;

            if (IsChannels)
                return "(NativeChannelsType)";
            else
                return "(" + NativeName + ")";
        }
    }

    private bool NeedsTypeCast
    {
        get
        {
            if (_type == "Instance" || HasInstance || IsString)
                return false;

            return _type != NativeName || _type != ManagedName;
        }
    }

    public string GetNativeName(string type)
    {
        if (_type == "void")
            return "void";

        if (_type == "nativeString" || _type == "string")
            return "IntPtr";

        if (_isEnum || _type == "size_t")
            return "UIntPtr";

        if (_type == "ssize_t" || _type == "Instance" || _type == "voidInstance" || HasInstance)
            return "IntPtr";

        return type;
    }

    public string GetManagedName(string type)
    {
        if (type == "size_t" || type == "ssize_t")
            return "int";

        if (_type == "voidInstance")
            return "void";

        if (_type == "Instance")
            return "IntPtr";

        if (_type == "nativeString")
            return "string";

        return type;
    }
}
