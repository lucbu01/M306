using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    class Entry
    {
        public int id { get; private set; }
        public Category category { get; set; }
        public String description { get; set; }
        public double value { get; set; }
        public DateTime dateTime { get; set; }

        public Entry(int id, Category category, String description, double value, DateTime dateTime) {
            this.id = id;
            this.category = category;
            this.description = description;
            this.value = value;
            this.dateTime = dateTime;
        }
    }
}
