using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    [Serializable]
    public class EntryList
    {
        [JsonProperty("entries")]
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
            entries.Sort((x, y) => y.dateTime.CompareTo(x.dateTime));
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
        public List<Entry> filter(Category category)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries) {
                if (entry.category == category) {
                    result.Add(entry);
                }
            }
            return result;
        }
        public List<Entry> filter(Category category, DateTime startDate, DateTime endDate)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries)
            {
                if (entry.dateTime >= startDate && entry.dateTime <= endDate && entry.category == category)
                {
                    result.Add(entry);
                }
            }
            return result;
        }

        public List<object> years()
        {
            List<object> years = new List<object>();
            foreach(Entry entry in entries)
            {
                if(years.IndexOf(entry.dateTime.Year) == -1)
                {
                    years.Add(entry.dateTime.Year);
                }
            }
            return years;
        }
    }
}
