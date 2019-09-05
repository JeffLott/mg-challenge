using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mg.Challenge.Core.Services
{
    public class FileAccessor
    {
        public string[] ReadFile(string fileName)
        {
            if (File.Exists(fileName))
                return File.ReadAllLines(fileName);

            return null;
        }
    }
}
