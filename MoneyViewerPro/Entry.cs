using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    public class Entry
    {
        public int id { get; private set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public DateTime dateTime { get; set; }

        public Entry(int id, string categoryName, string description, double value, DateTime dateTime) {
            this.id = id;
            this.categoryName = categoryName;
            this.description = description;
            this.value = value;
            this.dateTime = dateTime;
        }
    }
}
