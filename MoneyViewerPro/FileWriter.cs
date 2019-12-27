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
            IFormatter formatter = new BinaryFormatter(); 
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter streamWriter = new StreamWriter(stream);
            string toWrite;
            if(password == null)
            {
                toWrite = JsonConvert.SerializeObject(data);
            } else
            {
                StringWriter stringWriter = new StringWriter();
                serializer.Serialize(stringWriter, data);
                string toEncrypt = stringWriter.ToString();
                toWrite = Encrypter.encrypt(toEncrypt, password);
            }
            streamWriter.Write(toWrite);
            streamWriter.Close();
            stream.Close();
        }
    }
}
