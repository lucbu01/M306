using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    public class EntryList
    {
        public List<Entry> entries{ get; set; }

        public EntryList()
        {
            this.entries = new List<Entry>();
        }

        public EntryList(List<Entry> entries)
        {
            this.entries = entries;
        }

        public void addEntry(Entry entry) {
            entries.Add(entry);
        }

        public List<Entry> filter(DateTime startDate, DateTime endDate) {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries)
            {
                if (entry.dateTime >= startDate && entry.dateTime <= endDate)
                {
                    result.Add(entry);
                }
            }
            return result;
        }
        public List<Entry> filter(string categoryName)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries) {
                if (entry.categoryName == categoryName) {
                    result.Add(entry);
                }
            }
            return result;
        }
        public List<Entry> filter(string categoryName, DateTime startDate, DateTime endDate)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries)
            {
                if (entry.dateTime >= startDate && entry.dateTime <= endDate && entry.categoryName == categoryName)
                {
                    result.Add(entry);
                }
            }
            return result;
        }
    }
}
