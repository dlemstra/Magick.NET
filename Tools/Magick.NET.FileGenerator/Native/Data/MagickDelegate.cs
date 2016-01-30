//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magick.NET.FileGenerator
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
    public string Name
    {
      get;
      set;
    }

    [DataMember(Name = "type")]
    public string Type
    {
      get;
      set;
    }
  }
}
