using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magick.NET.Tests.Shared.Optimizers.ImageOptimizerTests
{
    public partial class ImageOptimizerTests
    {
        private static FileStream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.ReadWrite);
        }
    }
}
