using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    class Category
    {
        public int id { get; private set; }
        public String name { get; set; }

        public Category(int id, String name) {
            this.id = id;
            this.name = name;
        }
    }
}
