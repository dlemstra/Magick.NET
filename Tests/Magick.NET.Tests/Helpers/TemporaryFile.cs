//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;
using System.IO;

namespace Magick.NET.Tests
{
  [ExcludeFromCodeCoverage]
  public class TemporaryFile : IDisposable
  {
    private FileInfo _TempFile;

    public TemporaryFile(byte[] data)
    {
      _TempFile = new FileInfo(Path.GetTempFileName());
      File.WriteAllBytes(_TempFile.FullName, data);
    }

    public void Dispose()
    {
      if (_TempFile.Exists)
        _TempFile.Delete();
    }

    public FileInfo FileInfo
    {
      get
      {
        return _TempFile;
      }
    }
  }
}
