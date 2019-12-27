using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    [Serializable]
    public class FileData
    {
        [JsonProperty("entryList")]
        public EntryList EntryList { get; set; }

        [JsonProperty("categoryList")]
        public CategoryList CategoryList { get; set; }

        public FileData()
        {

        }

        public FileData(EntryList entryList, CategoryList categoryList)
        {
            EntryList = entryList;
            CategoryList = categoryList;
        }
    }
}
