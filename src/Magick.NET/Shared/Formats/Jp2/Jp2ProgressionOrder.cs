// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick.Formats.Jp2
{
    /// <summary>
    /// Specifies jp2 progression orders.
    /// </summary>
    public enum Jp2ProgressionOrder
    {
        /// <summary>
        /// Layer-resolution-component-precinct order.
        /// </summary>
        LRCP = 0,

        /// <summary>
        /// Resolution-layer-component-precinct order.
        /// </summary>
        RLCP = 1,

        /// <summary>
        /// Resolution-precinct-component-layer order.
        /// </summary>
        RPCL = 2,

        /// <summary>
        /// Precinct-component-resolution-layer order.
        /// </summary>
        PCRL = 3,

        /// <summary>
        /// Component-precinct-resolution-layer order.
        /// </summary>
        CPRL = 4,
    }
}