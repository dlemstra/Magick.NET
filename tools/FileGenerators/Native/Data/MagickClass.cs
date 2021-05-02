// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickClass
    {
        [DataMember(Name = "constructor")]
        private MagickConstructor? _constructor = new MagickConstructor();

        [DataMember(Name = "delegates")]
        private List<MagickDelegate>? _delegates = new List<MagickDelegate>();

        [DataMember(Name = "methods")]
        private List<MagickMethod>? _methods = new List<MagickMethod>();

        [DataMember(Name = "properties")]
        private List<MagickProperty>? _properties = new List<MagickProperty>();

        [DataMember(Name = "dynamic")]
        private string _dynamic = string.Empty;

        [DataMember(Name = "nativeConstructor")]
        private bool _hasNativeConstructor = false;

        [DataMember(Name = "access")]
        public string Access { get; set; } = string.Empty;

        [DataMember(Name = "className")]
        public string ClassName { get; set; } = string.Empty;

        public MagickConstructor Constructor
        {
            get
            {
                if (_constructor == null)
                    _constructor = new MagickConstructor();

                return _constructor;
            }
        }

        public IEnumerable<MagickDelegate> Delegates
        {
            get
            {
                if (_delegates != null)
                    return _delegates;

                return Enumerable.Empty<MagickDelegate>();
            }
        }

        public DynamicMode DynamicMode
        {
            get
            {
                if (string.IsNullOrEmpty(_dynamic))
                    return DynamicMode.None;

                return (DynamicMode)Enum.Parse(typeof(DynamicMode), _dynamic);
            }
        }

        public string FileName { get; set; } = string.Empty;

        [DataMember(Name = "instance")]
        public bool HasInstance { get; set; }

        [DataMember(Name = "interface")]
        public bool HasInterface { get; set; }

        [DataMember(Name = "noConstructor")]
        public bool HasNoConstructor { get; set; }

        public bool HasNativeConstructor
            => _hasNativeConstructor || (!IsConst && (IsDynamic && DynamicMode.HasFlag(DynamicMode.NativeToManaged)));

        [DataMember(Name = "const")]
        public bool IsConst { get; set; }

        public bool IsDynamic
            => DynamicMode != DynamicMode.None;

        [DataMember(Name = "quantumType")]
        public bool IsQuantumType { get; set; }

        [DataMember(Name = "static")]
        public bool IsStatic { get; set; }

        public IEnumerable<MagickMethod> Methods
        {
            get
            {
                if (_methods != null)
                    return _methods;

                return Enumerable.Empty<MagickMethod>();
            }
        }

        public string Name { get; set; } = string.Empty;

        [DataMember(Name = "namespace")]
        public string Namespace { get; set; } = string.Empty;

        [DataMember(Name = "notNullable")]
        public bool NotNullable { get; set; }

        public IEnumerable<MagickProperty> Properties
        {
            get
            {
                if (_properties != null)
                    return _properties;

                return Enumerable.Empty<MagickProperty>();
            }
        }

        [OnDeserializing]
        private void BeforeDeserialization(StreamingContext context)
        {
            Access = "public";
            HasInstance = true;
            Namespace = "ImageMagick";
        }
    }
}
