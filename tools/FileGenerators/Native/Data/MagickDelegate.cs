// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickDelegate
    {
        [DataMember(Name = "arguments")]
        private List<MagickArgument> _Arguments = new List<MagickArgument>();

        [OnDeserialized]
        private void Deserialized(StreamingContext context)
        {
            if (string.IsNullOrEmpty(Type))
                Type = "void";
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
            }
        }

        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        [DataMember(Name = "type")]
        public string Type { get; set; } = string.Empty;
    }
}
