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

#if PLATFORM_AnyCPU && !NETSTANDARD
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using ImageMagick.Configuration;

namespace ImageMagick
{
    internal static class NativeLibraryLoader
    {
        private static volatile bool _loaded;

        private static Assembly Assembly
        {
            get
            {
                return typeof(NativeLibraryLoader).Assembly;
            }
        }

        public static void Load()
        {
            if (_loaded)
                return;

            _loaded = true;
            ExtractLibrary();
        }

        private static string CreateCacheDirectory()
        {
            AssemblyFileVersionAttribute version = (AssemblyFileVersionAttribute)Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];

#if NET20
            string path = Path.Combine(MagickAnyCPU.CacheDirectory, "Magick.NET.net20." + version.Version);
#else
            string path = Path.Combine(MagickAnyCPU.CacheDirectory, "Magick.NET.net40." + version.Version);
#endif
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                GrantEveryoneReadAndExecuteAccess(path);
            }

            return path;
        }

        private static void ExtractLibrary()
        {
            string name = NativeLibrary.Name + "-" + NativeLibrary.QuantumName + "-" + NativeLibrary.PlatformName;
            string cacheDirectory = CreateCacheDirectory();
            string tempFile = Path.Combine(cacheDirectory, name + ".dll");

            WriteAssembly(tempFile);

            MagickNET.SetNativeLibraryDirectory(cacheDirectory);

            MagickNET.Initialize(ConfigurationFiles.Default, cacheDirectory);
        }

        private static void GrantEveryoneReadAndExecuteAccess(string cacheDirectory)
        {
            if (!MagickAnyCPU.HasSharedCacheDirectory || !MagickAnyCPU.UsesDefaultCacheDirectory)
                return;

            DirectoryInfo directoryInfo = new DirectoryInfo(cacheDirectory);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
            SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            InheritanceFlags inheritanceFlags = InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit;
            directorySecurity.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadAndExecute, inheritanceFlags, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private static void WriteAssembly(string tempFile)
        {
            if (File.Exists(tempFile))
                return;

            string resourceName = "ImageMagick.Resources.Library.Magick.Native_" + NativeLibrary.PlatformName + ".gz";

            using (Stream stream = Assembly.GetManifestResourceStream(resourceName))
            {
                using (GZipStream compressedStream = new GZipStream(stream, CompressionMode.Decompress, false))
                {
                    using (FileStream fileStream = File.Open(tempFile, FileMode.CreateNew))
                    {
                        compressedStream.CopyTo(fileStream);
                    }
                }
            }
        }
    }
}
#endif