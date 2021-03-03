// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickMethod
    {
        [DataMember(Name = "type")]
        private string _Type
        {
            get;
            set;
        }

        [DataMember(Name = "arguments")]
        private List<MagickArgument> _Arguments = new List<MagickArgument>();

        [OnDeserialized]
        private void Deserialized(StreamingContext context)
        {
            if (string.IsNullOrEmpty(_Type))
                ReturnType = new MagickType(CreatesInstance ? "voidInstance" : "void");
            else
                ReturnType = new MagickType(CreatesInstance ? "Instance" : _Type);
        }

        public IEnumerable<MagickArgument> Arguments
        {
            get
            {
                if (_Arguments != null)
                {
                    foreach (var argument in _Arguments)
                    {
                        yield return argument;
                    }
                }

                if (Throws)
                    yield return MagickArgument.CreateException();
            }
        }

        [DataMember(Name = "cleanup")]
        public MagickCleanupMethod Cleanup
        {
            get;
            set;
        }

        [DataMember(Name = "instance")]
        public bool CreatesInstance
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

        [DataMember(Name = "name")]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Name = "throws")]
        public bool Throws
        {
            get;
            set;
        }

        public MagickType ReturnType
        {
            get;
            private set;
        }
    }
}
