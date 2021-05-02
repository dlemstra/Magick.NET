// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    internal sealed class MagickType
    {
        private string _type = string.Empty;
        private bool _isEnum;

        public MagickType(string type)
        {
            _type = !string.IsNullOrEmpty(type) ? type : "void";
            _isEnum = File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET.Core\Enums\" + type + ".cs"));
            _isEnum = _isEnum || File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET\Enums\" + type + ".cs"));
        }

        public bool HasInstance
        {
            get
            {
                if (IsDelegate)
                    return false;

                switch (_type)
                {
                    case "byte":
                    case "byte[]":
                    case "bool":
                    case "double":
                    case "double[]":
                    case "Instance":
                    case "long":
                    case "QuantumType":
                    case "QuantumType[]":
                    case "size_t":
                    case "ssize_t":
                    case "string":
                    case "ulong":
                        return false;
                    default:
                        return !_isEnum;
                }
            }
        }

        public bool IsBool
            => Managed == "bool";

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

        public string Managed
            => GetManagedType(_type);

        public string ManagedTypeCast
        {
            get
            {
                if (NeedsTypeCast)
                    return "(" + Managed + ")";

                return string.Empty;
            }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        public string Native
            => GetNativeType(_type);

        public string NativeTypeCast
        {
            get
            {
                if (NeedsTypeCast)
                    return "(" + Native + ")";

                return string.Empty;
            }
        }

        private bool NeedsTypeCast
        {
            get
            {
                if (_type == "Instance" || HasInstance || IsString)
                    return false;

                return _type != Native || _type != Managed;
            }
        }

        public string GetNativeType(string type)
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

        public string GetManagedType(string type)
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
}