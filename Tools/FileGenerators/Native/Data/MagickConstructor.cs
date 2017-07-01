// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileGenerator.Native
{
    [DataContract]
    internal sealed class MagickConstructor
    {
        [DataMember(Name = "arguments")]
        private List<MagickArgument> _Arguments = new List<MagickArgument>();

        [DataMember(Name = "throws")]
        public bool Throws
        {
            get;
            set;
        }

        public IEnumerable<MagickArgument> Arguments
        {
            get
            {
                if (_Arguments == null)
                    yield break;

                foreach (var argument in _Arguments)
                {
                    yield return argument;
                }

                if (Throws)
                    yield return MagickArgument.CreateException();
            }
        }
    }
}
