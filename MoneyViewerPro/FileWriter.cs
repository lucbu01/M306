using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyViewerPro
{
    class FileWriter
    {
        public static void write(FileData data, string path)
        {
            write(data, path, null);
        }

        public static void write(FileData data, string path, string password)
        {
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter streamWriter = new StreamWriter(stream);
            string toWrite;
            if(password == null)
            {
                toWrite = JsonConvert.SerializeObject(data);
            } else
            {
                string toEncrypt = JsonConvert.SerializeObject(data);
                toWrite = Encrypter.encrypt(toEncrypt, password);
            }
            streamWriter.Write(toWrite);
            streamWriter.Close();
            stream.Close();
        }
    }
}
