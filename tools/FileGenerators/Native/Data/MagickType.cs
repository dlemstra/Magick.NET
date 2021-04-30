// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    internal sealed class MagickType
    {
        private string _Type = string.Empty;
        private bool _IsEnum;

        private bool _NeedsTypeCast
        {
            get
            {
                if (_Type == "Instance" || HasInstance || IsString)
                    return false;

                return _Type != Native || _Type != Managed;
            }
        }

        public MagickType(string type)
        {
            _Type = !string.IsNullOrEmpty(type) ? type : "void";
            _IsEnum = File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET.Core\Enums\" + type + ".cs"));
            _IsEnum = _IsEnum || File.Exists(PathHelper.GetFullPath(@"\src\Magick.NET\Enums\" + type + ".cs"));
        }

        public string GetNativeType(string type)
        {
            if (_Type == "void")
                return "void";

            if (_Type == "nativeString" || _Type == "string")
                return "IntPtr";

            if (_IsEnum || _Type == "size_t")
                return "UIntPtr";

            if (_Type == "ssize_t" || _Type == "Instance" || _Type == "voidInstance" || HasInstance)
                return "IntPtr";

            return type;
        }

        public string GetManagedType(string type)
        {
            if (type == "size_t" || type == "ssize_t")
                return "int";

            if (_Type == "voidInstance")
                return "void";

            if (_Type == "Instance")
                return "IntPtr";

            if (_Type == "nativeString")
                return "string";

            return type;
        }

        public bool HasInstance
        {
            get
            {
                if (IsDelegate)
                    return false;

                switch (_Type)
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
                        return !_IsEnum;
                }
            }
        }

        public bool IsBool
            => Managed == "bool";

        public bool IsDelegate
            => _Type.EndsWith("Delegate");

        public bool IsInstance
            => _Type == "Instance";

        public bool IsNativeString
            => _Type == "nativeString";

        public bool IsQuantumType
            => _Type == "QuantumType" || _Type == "QuantumType[]";

        public bool IsString
            => _Type == "string";

        public bool IsVoid
            => _Type == "void" || _Type == "voidInstance";

        public string Managed
            => GetManagedType(_Type);

        public string ManagedTypeCast
        {
            get
            {
                if (_NeedsTypeCast)
                    return "(" + Managed + ")";

                return "";
            }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        public string Native
            => GetNativeType(_Type);

        public string NativeTypeCast
        {
            get
            {
                if (_NeedsTypeCast)
                    return "(" + Native + ")";

                return "";
            }
        }
    }
}