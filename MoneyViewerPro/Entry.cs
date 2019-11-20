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
        public int categoryId { get; set; }
        public String description { get; set; }
        public double value { get; set; }
        public DateTime dateTime { get; set; }

        public Entry(int id, int categoryId, String description, double value, DateTime dateTime) {
            this.id = id;
            this.categoryId = categoryId;
            this.description = description;
            this.value = value;
            this.dateTime = dateTime;
            EntryList.addEntry(this);
        }
    }
}
