using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    static class EntryList
    {
        public static  List<Entry> entries{ get; set; }

        public static void addEntry(Entry entry) {
            entries.Add(entry);
        }

        public static List<Entry> filter(DateTime startDate, DateTime endDate) {
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
        public static List<Entry> filter(int categoryId)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries) {
                if (entry.categoryId == categoryId) {
                    result.Add(entry);
                }
            }
            return result;
        }
        public static List<Entry> filter(int categoryId, DateTime startDate, DateTime endDate)
        {
            List<Entry> result = new List<Entry>();
            foreach (Entry entry in entries)
            {
                if (entry.dateTime >= startDate && entry.dateTime <= endDate && entry.categoryId == categoryId)
                {
                    result.Add(entry);
                }
            }
            return result;
        }
    }
}
