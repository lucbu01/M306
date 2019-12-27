using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    [Serializable]
    public class CategoryList
    {

        [JsonProperty("categories")]
        public List<Category> categories { get; set; }

        public CategoryList()
        {
            this.categories = new List<Category>();
        }

        public CategoryList(List<Category> categories)
        {
            this.categories = categories;
        }

        public void addCategory(Category category)
        {
            categories.Add(category);
        }

        public Category getCategoryByName(string name) {
            foreach (Category category in categories) {
                if (category.name == name) {
                    return category;
                }
            }
            return null;
        }
    }
}
