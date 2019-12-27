using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    [Serializable]
    public class Category
    {
        [JsonProperty("name")]
        public string name { get; set; } = "";
        [JsonProperty("description")]
        public string description { get; set; } = "";

        public Category(string name, string description) {
            this.name = name;
            this.description = description;
        }

        public Category()
        {

        }
    }
}
