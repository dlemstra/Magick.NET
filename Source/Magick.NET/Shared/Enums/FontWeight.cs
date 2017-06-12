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

using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Specifies font weight.
    /// </summary>
    public enum FontWeight
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Thin (100)
        /// </summary>
        Thin = 100,

        /// <summary>
        /// Extra light (200)
        /// </summary>
        ExtraLight = 200,

        /// <summary>
        /// Ultra light (200)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "UltraLight", Justification = "Name should be consistant with the rest.")]
        UltraLight = 200,

        /// <summary>
        /// Light (300)
        /// </summary>
        Light = 300,

        /// <summary>
        /// Normal (400)
        /// </summary>
        Normal = 400,

        /// <summary>
        /// Regular (400)
        /// </summary>
        Regular = 400,

        /// <summary>
        /// Medium (500)
        /// </summary>
        Medium = 500,

        /// <summary>
        /// Demi bold (600)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DemiBold", Justification = "Name should be consistant with the rest.")]
        DemiBold = 600,

        /// <summary>
        /// Semi bold (600)
        /// </summary>
        SemiBold = 600,

        /// <summary>
        /// Bold (700)
        /// </summary>
        Bold = 700,

        /// <summary>
        /// Extra bold (800)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ExtraBold", Justification = "Name should be consistant with the rest.")]
        ExtraBold = 800,

        /// <summary>
        /// Ultra bold (800)
        /// </summary>
        UltraBold = 800,

        /// <summary>
        /// Heavy (900)
        /// </summary>
        Heavy = 900,

        /// <summary>
        /// Black (900)
        /// </summary>
        Black = 900
    }
}