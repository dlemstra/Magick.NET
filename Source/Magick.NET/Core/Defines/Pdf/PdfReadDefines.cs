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

using System.Collections.Generic;
using ImageMagick.Defines;

namespace ImageMagick
{
  /// <summary>
  /// Class for defines that are used when a pdf image is read.
  /// </summary>
  public sealed class PdfReadDefines : ReadDefinesCreator
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfReadDefines"/> class.
    /// </summary>
    public PdfReadDefines()
      : base(MagickFormat.Pdf)
    {
    }

    /// <summary>
    /// Gets or sets the size where the image should be scaled to (pdf:fit-page).
    /// </summary>
    public MagickGeometry FitPage
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether use of the cropbox should be forced (pdf:use-trimbox).
    /// </summary>
    public bool? UseCropBox
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether use of the trimbox should be forced (pdf:use-trimbox).
    /// </summary>
    public bool? UseTrimBox
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public override IEnumerable<IDefine> Defines
    {
      get
      {
        if (FitPage != null)
          yield return CreateDefine("fit-page", FitPage);

        if (UseCropBox.HasValue)
          yield return CreateDefine("use-cropbox", UseCropBox.Value);

        if (UseTrimBox.HasValue)
          yield return CreateDefine("use-trimbox", UseTrimBox.Value);
      }
    }
  }
}