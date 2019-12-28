using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyViewerPro
{
    class FileLoader
    {
        public static FileData load(string path)
        {
            return load(path, null);
        }

        public static FileData load(string path, string password)
        {
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader streamReader = new StreamReader(stream);
            string fileContent = streamReader.ReadToEnd();
            streamReader.Close();
            stream.Close();
            if (password != null)
            {
                try
                {
                    fileContent = Encrypter.decrypt(fileContent, password);
                } catch
                {
                    throw new Exception("decrypt error");
                }
            }
            try
            {
                return JsonConvert.DeserializeObject<FileData>(fileContent);
            } catch
            {
                throw new Exception("file format error");
            }
        }
    }
}
