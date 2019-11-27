using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    public class Category
    {
        public String name { get; set; }
        public String description { get; set; }

        public Category(String name, String description) {
            this.name = name;
            this.description = description;
        }
    }
}
