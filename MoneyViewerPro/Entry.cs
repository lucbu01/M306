using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    [Serializable]
    public class Entry
    {

        [JsonProperty("category")]
        public Category category { get; set; } = new Category();
        [JsonProperty("description")]
        public string description { get; set; } = "";
        [JsonProperty("value")]
        public double value { get; set; } = 0.00;
        [JsonProperty("dateTime")]
        public DateTime dateTime { get; set; } = DateTime.Now;

        public Entry(Category category, string description, double value, DateTime dateTime) {
            this.category = category;
            this.description = description;
            this.value = value;
            this.dateTime = dateTime;
        }

        public Entry() { }
    }
}
