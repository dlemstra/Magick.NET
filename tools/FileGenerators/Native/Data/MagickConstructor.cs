// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickConstructor
    {
        [DataMember(Name = "arguments")]
        private List<MagickArgument> _arguments = new List<MagickArgument>();

        [DataMember(Name = "throws")]
        public bool Throws { get; set; }

        public IEnumerable<MagickArgument> Arguments
        {
            get
            {
                if (_arguments == null)
                    yield break;

                foreach (var argument in _arguments)
                {
                    yield return argument;
                }

                if (Throws)
                    yield return MagickArgument.CreateException();
            }
        }
    }
}
