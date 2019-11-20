using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyViewerPro
{
    public class CategoryList
    {
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

        public Category getCategoryById(int id) {
            foreach (Category category in categories) {
                if (category.id == id) {
                    return category;
                }
            }
            return null;
        }
    }
}
