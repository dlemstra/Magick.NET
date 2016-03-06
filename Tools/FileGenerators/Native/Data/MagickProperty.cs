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

    [DataMember(Name = "readonly")]
    public bool IsReadOnly
    {
      get;
      set;
    }
  }
}
