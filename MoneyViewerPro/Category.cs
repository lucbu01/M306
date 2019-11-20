using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    public class Category
    {
        public int id { get; private set; }
        public String name { get; set; }
        public String description { get; set; }

        public Category(int id, String name, String description) {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }
}
