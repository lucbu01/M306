using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MoneyViewerPro
{
    class FileWriter
    {
        public static void write(string path, EntryList entries) {
            string serializedData = string.Empty;

            XmlSerializer serializer = new XmlSerializer(entries.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, entries);
                serializedData = sw.ToString();
            }

            File.WriteAllText(path, serializedData);
        }

        public static void write(string path, CategoryList entries)
        {
            string serializedData = string.Empty;

            XmlSerializer serializer = new XmlSerializer(entries.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, entries);
                serializedData = sw.ToString();
            }

            File.WriteAllText(path, serializedData);
        }
    }
}
