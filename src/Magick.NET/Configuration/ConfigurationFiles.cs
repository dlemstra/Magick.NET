// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageMagick.Configuration
{
    /// <summary>
    /// Encapsulates the configuration files of ImageMagick.
    /// </summary>
    public sealed class ConfigurationFiles
    {
        private ConfigurationFiles()
        {
            Colors = new ConfigurationFile("colors.xml");
            Configure = new ConfigurationFile("configure.xml");
            Delegates = new ConfigurationFile("delegates.xml");
            English = new ConfigurationFile("english.xml");
            Locale = new ConfigurationFile("locale.xml");
            Log = new ConfigurationFile("log.xml");
            Policy = new ConfigurationFile("policy.xml");
            Thresholds = new ConfigurationFile("thresholds.xml");
            Type = new ConfigurationFile("type.xml");
            TypeGhostscript = new ConfigurationFile("type-ghostscript.xml");
        }

        /// <summary>
        /// Gets the default configuration.
        /// </summary>
        public static ConfigurationFiles Default
            => new ConfigurationFiles();

        /// <summary>
        /// Gets the colors configuration.
        /// </summary>
        public IConfigurationFile Colors { get; }

        /// <summary>
        /// Gets the configure configuration.
        /// </summary>
        public IConfigurationFile Configure { get; }

        /// <summary>
        /// Gets the delegates configuration.
        /// </summary>
        public IConfigurationFile Delegates { get; }

        /// <summary>
        /// Gets the english configuration.
        /// </summary>
        public IConfigurationFile English { get; }

        /// <summary>
        /// Gets the locale configuration.
        /// </summary>
        public IConfigurationFile Locale { get; }

        /// <summary>
        /// Gets the log configuration.
        /// </summary>
        public IConfigurationFile Log { get; }

        /// <summary>
        /// Gets the policy configuration.
        /// </summary>
        public IConfigurationFile Policy { get; }

        /// <summary>
        /// Gets the thresholds configuration.
        /// </summary>
        public IConfigurationFile Thresholds { get; }

        /// <summary>
        /// Gets the type configuration.
        /// </summary>
        public IConfigurationFile Type { get; }

        /// <summary>
        /// Gets the type-ghostscript configuration.
        /// </summary>
        public IConfigurationFile TypeGhostscript { get; }

        internal IEnumerable<IConfigurationFile> Files
        {
            get
            {
                yield return Colors;
                yield return Configure;
                yield return Delegates;
                yield return English;
                yield return Locale;
                yield return Log;
                yield return Policy;
                yield return Thresholds;
                yield return Type;
                yield return TypeGhostscript;
            }
        }

        internal void WriteInDirectory(string path)
        {
            foreach (var configFile in Files)
            {
                string outputFile = Path.Combine(path, configFile.FileName);
                if (File.Exists(outputFile))
                    continue;

                using (var fileStream = File.Open(outputFile, FileMode.CreateNew))
                {
                    byte[] data = Encoding.UTF8.GetBytes(configFile.Data);
                    fileStream.Write(data, 0, data.Length);
                }
            }
        }
    }
}