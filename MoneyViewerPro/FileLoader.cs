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
        public static EntryList loadEntries(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(EntryList));
            string data = File.ReadAllText(path);
            using (TextReader tr = new StringReader(data))
            {
                EntryList deserialized = (EntryList)deserializer.Deserialize(tr);
                return deserialized;
            }
        }

        public static CategoryList loadCategory(string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CategoryList));
            string data = File.ReadAllText(path);
            using (TextReader tr = new StringReader(data))
            {
                CategoryList deserialized = (CategoryList)deserializer.Deserialize(tr);
                return deserialized;
            }
        }
    }
}
