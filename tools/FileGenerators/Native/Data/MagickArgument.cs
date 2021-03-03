// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickArgument
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

        [DataMember(Name = "const")]
        public bool IsConst
        {
            get;
            set;
        }

        public bool IsEnum
        {
            get;
            private set;
        }

        public bool IsHidden
        {
            get;
            set;
        }

        [DataMember(Name = "out")]
        public bool IsOut
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

        public MagickType Type
        {
            get;
            private set;
        }

        public static MagickArgument CreateException()
        {
            return new MagickArgument()
            {
                Name = "exception",
                IsHidden = true,
                IsOut = true,
                Type = new MagickType("Instance")
            };
        }
    }
}
