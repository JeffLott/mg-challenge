using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mg.Challenge.Core.Services
{
    public class JsonFileWriter
    {
        public bool WriteFileAsJson<T>(string fileName, T item)
        {
            if (!File.Exists(fileName))
            {
                try
                {
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(item));
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
