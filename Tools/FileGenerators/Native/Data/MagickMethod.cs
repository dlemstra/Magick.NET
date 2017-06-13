//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
