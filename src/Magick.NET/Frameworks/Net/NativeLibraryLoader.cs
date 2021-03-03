// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if PLATFORM_AnyCPU && !NETSTANDARD
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using ImageMagick.Configuration;

namespace ImageMagick
{
    internal static class NativeLibraryLoader
    {
        private static volatile bool _loaded;

        private static Assembly Assembly
            => typeof(NativeLibraryLoader).Assembly;

        public static void Load()
        {
            if (_loaded)
                return;

            _loaded = true;
            ExtractLibrary();
        }

        private static string CreateCacheDirectory()
        {
            var version = (AssemblyFileVersionAttribute)Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];

#if NET20
            var path = Path.Combine(MagickAnyCPU.CacheDirectory, "Magick.NET.net20." + version.Version);
#else
            var path = Path.Combine(MagickAnyCPU.CacheDirectory, "Magick.NET.net40." + version.Version);
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
            var name = NativeLibrary.Name + "-" + NativeLibrary.QuantumName + "-" + NativeLibrary.PlatformName;
            var cacheDirectory = CreateCacheDirectory();
            var tempFile = Path.Combine(cacheDirectory, name + ".dll");

            WriteAssembly(tempFile);

            MagickNET.SetNativeLibraryDirectory(cacheDirectory);

            MagickNET.Initialize(ConfigurationFiles.Default, cacheDirectory);
        }

        private static void GrantEveryoneReadAndExecuteAccess(string cacheDirectory)
        {
            if (!MagickAnyCPU.HasSharedCacheDirectory || !MagickAnyCPU.UsesDefaultCacheDirectory)
                return;

            var directoryInfo = new DirectoryInfo(cacheDirectory);
            var directorySecurity = directoryInfo.GetAccessControl();
            var identity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            var inheritanceFlags = InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit;
            directorySecurity.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.ReadAndExecute, inheritanceFlags, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private static void WriteAssembly(string tempFile)
        {
            if (File.Exists(tempFile))
                return;

            var resourceName = "ImageMagick.Resources.Library.Magick.Native_" + NativeLibrary.PlatformName + ".gz";

            using (var stream = Assembly.GetManifestResourceStream(resourceName))
            {
                using (var compressedStream = new GZipStream(stream, CompressionMode.Decompress, false))
                {
                    using (var fileStream = File.Open(tempFile, FileMode.CreateNew))
                    {
                        compressedStream.CopyTo(fileStream);
                    }
                }
            }
        }
    }
}
#endif