// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickProperty
    {
        [DataMember(Name = "type")]
        private string _Type
        {
            get;
            set;
        }

        [OnDeserialized]
        private void Deserializated(StreamingContext context)
        {
            Type = new MagickType(_Type);
        }

        [DataMember(Name = "readonly")]
        public bool IsReadOnly
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

        public MagickType Type
        {
            get;
            private set;
        }
    }
}
