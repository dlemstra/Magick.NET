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
        private MagickConstructor _Constructor = new MagickConstructor();

        [DataMember(Name = "delegates")]
        private List<MagickDelegate> _Delegates = new List<MagickDelegate>();

        [DataMember(Name = "methods")]
        private List<MagickMethod> _Methods = new List<MagickMethod>();

        [DataMember(Name = "properties")]
        private List<MagickProperty> _Properties = new List<MagickProperty>();

        [DataMember(Name = "dynamic")]
        private string _Dynamic
        {
            get;
            set;
        }

        [DataMember(Name = "nativeConstructor")]
        private bool _HasNativeConstructor
        {
            get;
            set;
        }

        [OnDeserializing]
        private void BeforeDeserialization(StreamingContext context)
        {
            Access = "public";
            HasInstance = true;
            Namespace = "ImageMagick";
        }

        [DataMember(Name = "access")]
        public string Access
        {
            get;
            set;
        }

        [DataMember(Name = "className")]
        public string ClassName
        {
            get;
            set;
        }

        public MagickConstructor Constructor
        {
            get
            {
                if (_Constructor == null)
                    _Constructor = new MagickConstructor();

                return _Constructor;
            }
        }

        public IEnumerable<MagickDelegate> Delegates
        {
            get
            {
                if (_Delegates != null)
                    return _Delegates;

                return Enumerable.Empty<MagickDelegate>();
            }
        }


        public DynamicMode DynamicMode
        {
            get
            {
                if (string.IsNullOrEmpty(_Dynamic))
                    return DynamicMode.None;

                return (DynamicMode)Enum.Parse(typeof(DynamicMode), _Dynamic);
            }
        }

        public string FileName
        {
            get;
            set;
        }

        [DataMember(Name = "instance")]
        public bool HasInstance
        {
            get;
            set;
        }

        [DataMember(Name = "interface")]
        public bool HasInterface
        {
            get;
            set;
        }

        [DataMember(Name = "noConstructor")]
        public bool HasNoConstructor
        {
            get;
            set;
        }

        public bool HasNativeConstructor
        {
            get
            {
                return _HasNativeConstructor || (!IsConst && (IsDynamic && DynamicMode.HasFlag(DynamicMode.NativeToManaged)));
            }
        }

        [DataMember(Name = "const")]
        public bool IsConst
        {
            get;
            set;
        }

        public bool IsDynamic
        {
            get
            {
                return DynamicMode != DynamicMode.None;
            }
        }

        [DataMember(Name = "quantumType")]
        public bool IsQuantumType
        {
            get;
            set;
        }

        [DataMember(Name = "static")]
        public bool IsStatic
        {
            get;
            set;
        }

        public IEnumerable<MagickMethod> Methods
        {
            get
            {
                if (_Methods != null)
                    return _Methods;

                return Enumerable.Empty<MagickMethod>();
            }
        }

        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "namespace")]
        public string Namespace
        {
            get;
            set;
        }

        public IEnumerable<MagickProperty> Properties
        {
            get
            {
                if (_Properties != null)
                    return _Properties;

                return Enumerable.Empty<MagickProperty>();
            }
        }
    }
}
